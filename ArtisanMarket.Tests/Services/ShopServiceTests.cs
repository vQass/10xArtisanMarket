using ArtisanMarket.Application.Models;
using ArtisanMarket.Application.Services;
using ArtisanMarket.Domain.Entities;
using ArtisanMarket.Infrastructure.Data;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ArtisanMarket.Tests.Services;

/// <summary>
/// Testy jednostkowe dla serwisu ShopService
/// Pokrywa kluczowe funkcjonalności zarządzania sklepami zgodnie z zasadami biznesowymi
/// Wykorzystuje in-memory database dla rzeczywistej interakcji z bazą danych
/// </summary>
public class ShopServiceTests : IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly ShopService _shopService;

    public ShopServiceTests()
    {
        // Konfiguracja in-memory bazy danych dla testów
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new ApplicationDbContext(options);
        _shopService = new ShopService(_context);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    #region CreateShopAsync Tests

    /// <summary>
    /// TC-SHOP-001: Tworzenie nowego sklepu (Happy Path)
    /// Warunki: Użytkownik nie ma sklepu, poprawne dane wejściowe
    /// Oczekiwany wynik: Sklep utworzony pomyślnie
    /// </summary>
    [Fact]
    public async Task CreateShopAsync_WithValidDataAndNoExistingShop_ShouldCreateShopSuccessfully()
    {
        // Arrange
        var userId = "user-123";
        var createShopDto = new CreateShopDto
        {
            Name = "Sklep Testowy",
            Description = "Opis testowego sklepu",
            ContactEmail = "kontakt@sklep.pl",
            Phone = "123-456-789"
        };

        // Act
        var result = await _shopService.CreateShopAsync(userId, createShopDto);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(createShopDto.Name);
        result.Slug.Should().Be("sklep-testowy");
        result.Description.Should().Be(createShopDto.Description);

        // Weryfikacja, że sklep został zapisany w bazie danych
        var savedShop = await _context.Shops.FirstOrDefaultAsync(s => s.UserId == userId);
        savedShop.Should().NotBeNull();
        savedShop!.Name.Should().Be(createShopDto.Name);
        savedShop.Slug.Should().Be("sklep-testowy");
        savedShop.Description.Should().Be(createShopDto.Description);
        savedShop.ContactEmail.Should().Be(createShopDto.ContactEmail);
        savedShop.Phone.Should().Be(createShopDto.Phone);
        savedShop.DeletedAt.Should().BeNull();
    }

    /// <summary>
    /// TC-SHOP-002: Próba utworzenia drugiego sklepu
    /// Warunki: Użytkownik już ma sklep
    /// Oczekiwany wynik: Wyjątek InvalidOperationException
    /// </summary>
    [Fact]
    public async Task CreateShopAsync_WithExistingShop_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var userId = "user-123";
        var createShopDto = new CreateShopDto { Name = "Drugi Sklep" };

        var existingShop = new Shop
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Name = "Pierwszy Sklep",
            Slug = "pierwszy-sklep",
            DeletedAt = null
        };

        await _context.Shops.AddAsync(existingShop);
        await _context.SaveChangesAsync();

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _shopService.CreateShopAsync(userId, createShopDto));

        exception.Message.Should().Be("Użytkownik już posiada sklep w systemie.");
    }

    /// <summary>
    /// TC-SHOP-004: Generowanie unikalnego slug - dodanie sufiksu numerycznego
    /// Warunki: Slug już istnieje, należy dodać sufiks
    /// Oczekiwany wynik: Slug z sufiksem numerycznym
    /// </summary>
    [Fact]
    public async Task CreateShopAsync_WithExistingSlug_ShouldGenerateUniqueSlugWithSuffix()
    {
        // Arrange
        var userId = "user-123";
        var createShopDto = new CreateShopDto { Name = "Sklep Testowy" };

        var existingShop = new Shop
        {
            Id = Guid.NewGuid(),
            UserId = "other-user",
            Name = "Sklep Testowy",
            Slug = "sklep-testowy",
            DeletedAt = null
        };

        await _context.Shops.AddAsync(existingShop);
        await _context.SaveChangesAsync();

        // Act
        var result = await _shopService.CreateShopAsync(userId, createShopDto);

        // Assert
        result.Slug.Should().Be("sklep-testowy-1");

        // Weryfikacja w bazie danych
        var savedShop = await _context.Shops.FirstOrDefaultAsync(s => s.UserId == userId);
        savedShop.Should().NotBeNull();
        savedShop!.Slug.Should().Be("sklep-testowy-1");
    }

    /// <summary>
    /// Test generowania slug z polskimi znakami
    /// Warunki: Nazwa zawiera polskie znaki
    /// Oczekiwany wynik: Slug bez polskich znaków
    /// </summary>
    [Fact]
    public async Task CreateShopAsync_WithPolishCharacters_ShouldGenerateSlugWithoutPolishChars()
    {
        // Arrange
        var userId = "user-123";
        var createShopDto = new CreateShopDto { Name = "Sklep Łódź" };

        // Act
        var result = await _shopService.CreateShopAsync(userId, createShopDto);

        // Assert
        result.Slug.Should().Be("sklep-lodz");

        // Weryfikacja w bazie danych
        var savedShop = await _context.Shops.FirstOrDefaultAsync(s => s.UserId == userId);
        savedShop.Should().NotBeNull();
        savedShop!.Slug.Should().Be("sklep-lodz");
    }

    /// <summary>
    /// Test generowania slug ze znakami specjalnymi
    /// Warunki: Nazwa zawiera znaki specjalne i wielokrotne spacje
    /// Oczekiwany wynik: Slug zawierający tylko litery, cyfry i myślniki
    /// </summary>
    [Fact]
    public async Task CreateShopAsync_WithSpecialCharacters_ShouldCleanSlug()
    {
        // Arrange
        var userId = "user-123";
        var createShopDto = new CreateShopDto { Name = "Sklep & Test!!!  123" };

        // Act
        var result = await _shopService.CreateShopAsync(userId, createShopDto);

        // Assert
        result.Slug.Should().Be("sklep-test-123");

        // Weryfikacja w bazie danych
        var savedShop = await _context.Shops.FirstOrDefaultAsync(s => s.UserId == userId);
        savedShop.Should().NotBeNull();
        savedShop!.Slug.Should().Be("sklep-test-123");
    }

    /// <summary>
    /// Test ignorowania usuniętych sklepów przy sprawdzaniu duplikatów
    /// Warunki: Istnieje "usunięty" sklep tego samego użytkownika
    /// Oczekiwany wynik: Utworzenie nowego sklepu możliwe
    /// </summary>
    [Fact]
    public async Task CreateShopAsync_WithSoftDeletedShop_ShouldAllowCreatingNewShop()
    {
        // Arrange
        var userId = "user-123";
        var createShopDto = new CreateShopDto { Name = "Nowy Sklep" };

        var deletedShop = new Shop
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Name = "Stary Sklep",
            Slug = "stary-sklep",
            DeletedAt = DateTimeOffset.UtcNow // Usunięty sklep
        };

        await _context.Shops.AddAsync(deletedShop);
        await _context.SaveChangesAsync();

        // Act
        var result = await _shopService.CreateShopAsync(userId, createShopDto);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be("Nowy Sklep");

        // Weryfikacja w bazie danych - powinny być dwa sklepy (jeden usunięty, jeden nowy)
        var shops = await _context.Shops.ToListAsync();
        shops.Should().HaveCount(2);

        var activeShop = shops.First(s => s.DeletedAt == null);
        activeShop.Name.Should().Be("Nowy Sklep");
        activeShop.UserId.Should().Be(userId);
    }

    #endregion

    #region GetUserShopAsync Tests

    /// <summary>
    /// Test pobierania sklepu użytkownika - Happy Path
    /// Warunki: Użytkownik ma sklep
    /// Oczekiwany wynik: DTO sklepu
    /// </summary>
    [Fact]
    public async Task GetUserShopAsync_WithExistingShop_ShouldReturnShopDto()
    {
        // Arrange
        var userId = "user-123";
        var shop = new Shop
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Name = "Mój Sklep",
            Slug = "mój-sklep",
            Description = "Opis sklepu",
            DeletedAt = null
        };

        await _context.Shops.AddAsync(shop);
        await _context.SaveChangesAsync();

        // Act
        var result = await _shopService.GetUserShopAsync(userId);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(shop.Id);
        result.Name.Should().Be(shop.Name);
        result.Slug.Should().Be(shop.Slug);
        result.Description.Should().Be(shop.Description);
    }

    /// <summary>
    /// Test pobierania sklepu użytkownika - brak sklepu
    /// Warunki: Użytkownik nie ma sklepu
    /// Oczekiwany wynik: null
    /// </summary>
    [Fact]
    public async Task GetUserShopAsync_WithNoShop_ShouldReturnNull()
    {
        // Arrange
        var userId = "user-without-shop";

        // Act
        var result = await _shopService.GetUserShopAsync(userId);

        // Assert
        result.Should().BeNull();
    }

    /// <summary>
    /// Test ignorowania usuniętych sklepów przy pobieraniu
    /// Warunki: Użytkownik ma tylko usunięty sklep
    /// Oczekiwany wynik: null
    /// </summary>
    [Fact]
    public async Task GetUserShopAsync_WithOnlySoftDeletedShop_ShouldReturnNull()
    {
        // Arrange
        var userId = "user-123";
        var deletedShop = new Shop
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Name = "Usunięty Sklep",
            Slug = "usuniety-sklep",
            DeletedAt = DateTimeOffset.UtcNow
        };

        await _context.Shops.AddAsync(deletedShop);
        await _context.SaveChangesAsync();

        // Act
        var result = await _shopService.GetUserShopAsync(userId);

        // Assert
        result.Should().BeNull();
    }

    #endregion
}

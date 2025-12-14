using ArtisanMarket.Application.Models;
using ArtisanMarket.Domain.Entities;
using ArtisanMarket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ArtisanMarket.Application.Services;

/// <summary>
/// Implementacja serwisu zarządzania sklepami.
/// Zapewnia funkcjonalności tworzenia sklepów i sprawdzania ich istnienia.
/// </summary>
public class ShopService : IShopService
{
    private readonly ApplicationDbContext _context;

    public ShopService(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Tworzy nowy sklep dla zalogowanego użytkownika.
    /// Sprawdza czy użytkownik już nie ma sklepu (reguła biznesowa: jeden użytkownik = jeden sklep).
    /// </summary>
    /// <param name="userId">ID użytkownika tworzącego sklep</param>
    /// <param name="dto">Dane sklepu do utworzenia</param>
    /// <returns>DTO utworzonego sklepu</returns>
    /// <exception cref="InvalidOperationException">Gdy użytkownik już ma sklep</exception>
    public async Task<ShopDto> CreateShopAsync(string userId, CreateShopDto dto)
    {
        // Sprawdź czy użytkownik już ma sklep
        var existingShop = await _context.Shops
            .FirstOrDefaultAsync(s => s.UserId == userId && s.DeletedAt == null);

        if (existingShop != null)
        {
            throw new InvalidOperationException("Użytkownik już posiada sklep w systemie.");
        }

        // Wygeneruj slug na podstawie nazwy (uproszczona wersja)
        var slug = GenerateSlug(dto.Name);

        // Upewnij się, że slug jest unikalny
        var existingSlug = await _context.Shops
            .FirstOrDefaultAsync(s => s.Slug == slug && s.DeletedAt == null);

        if (existingSlug != null)
        {
            // Dodaj sufiks numeryczny jeśli slug już istnieje
            var counter = 1;
            var uniqueSlug = $"{slug}-{counter}";
            while (await _context.Shops.AnyAsync(s => s.Slug == uniqueSlug && s.DeletedAt == null))
            {
                counter++;
                uniqueSlug = $"{slug}-{counter}";
            }
            slug = uniqueSlug;
        }

        // Utwórz nowy sklep
        var shop = new Shop
        {
            Id = Guid.CreateVersion7(),
            UserId = userId,
            Name = dto.Name,
            Slug = slug,
            Description = dto.Description,
            ContactEmail = dto.ContactEmail,
            Phone = dto.Phone,
            DeletedAt = null
        };

        _context.Shops.Add(shop);
        await _context.SaveChangesAsync();

        // Zwróć DTO utworzonego sklepu
        return new ShopDto(
            shop.Id,
            shop.Name,
            shop.Slug,
            shop.Description
        );
    }

    /// <summary>
    /// Pobiera sklep powiązany z użytkownikiem.
    /// </summary>
    /// <param name="userId">ID użytkownika</param>
    /// <returns>DTO sklepu lub null jeśli użytkownik nie ma sklepu</returns>
    public async Task<ShopDto?> GetUserShopAsync(string userId)
    {
        var shop = await _context.Shops
            .FirstOrDefaultAsync(s => s.UserId == userId && s.DeletedAt == null);

        if (shop == null)
        {
            return null;
        }

        return new ShopDto(
            shop.Id,
            shop.Name,
            shop.Slug,
            shop.Description
        );
    }

    /// <summary>
    /// Generuje slug na podstawie nazwy sklepu.
    /// Zamienia polskie znaki, usuwa znaki specjalne, zamienia spacje na myślniki.
    /// </summary>
    /// <param name="name">Nazwa sklepu</param>
    /// <returns>Wygenerowany slug</returns>
    private static string GenerateSlug(string name)
    {
        // Zamień polskie znaki na łacińskie
        var slug = name
            .ToLower()
            .Replace("ą", "a")
            .Replace("ć", "c")
            .Replace("ę", "e")
            .Replace("ł", "l")
            .Replace("ń", "n")
            .Replace("ó", "o")
            .Replace("ś", "s")
            .Replace("ź", "z")
            .Replace("ż", "z");

        // Usuń znaki specjalne, zostaw tylko litery, cyfry, spacje i myślniki
        slug = System.Text.RegularExpressions.Regex.Replace(slug, @"[^a-z0-9\s-]", "");

        // Zamień spacje i wielokrotne myślniki na pojedyncze myślniki
        slug = System.Text.RegularExpressions.Regex.Replace(slug, @"\s+", "-");
        slug = System.Text.RegularExpressions.Regex.Replace(slug, @"-+", "-");

        // Usuń myślniki z początku i końca
        slug = slug.Trim('-');

        return slug;
    }
}
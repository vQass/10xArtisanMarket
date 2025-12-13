using ArtisanMarket.Application.Models;
using ArtisanMarket.Domain.Entities;
using ArtisanMarket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ArtisanMarket.Application.Services;

/// <summary>
/// Implementacja serwisu zarządzania produktami.
/// Zapewnia funkcjonalności tworzenia produktów i sprawdzania ich własności.
/// </summary>
public class ProductService : IProductService
{
    private readonly ApplicationDbContext _context;

    public ProductService(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Tworzy nowy produkt w sklepie zalogowanego użytkownika.
    /// Sprawdza czy użytkownik ma sklep (reguła biznesowa: produkt może być utworzony tylko przez właściciela sklepu).
    /// </summary>
    /// <param name="userId">ID użytkownika tworzącego produkt</param>
    /// <param name="dto">Dane produktu do utworzenia</param>
    /// <returns>DTO utworzonego produktu</returns>
    /// <exception cref="InvalidOperationException">Gdy użytkownik nie ma sklepu</exception>
    public async Task<ProductDto> CreateProductAsync(string userId, CreateProductDto dto)
    {
        // Sprawdź czy użytkownik ma sklep
        var shop = await _context.Shops
            .FirstOrDefaultAsync(s => s.UserId == userId && s.DeletedAt == null);

        if (shop == null)
        {
            throw new InvalidOperationException("Nie masz uprawnień do dodawania produktów. Najpierw utwórz sklep.");
        }

        // Utwórz nowy produkt
        var product = new Product
        {
            Id = Guid.CreateVersion7(),
            ShopId = shop.Id,
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            IsActive = true
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        // Zwróć DTO utworzonego produktu
        return new ProductDto(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            new ShopDto(shop.Id, shop.Name, shop.Slug, shop.Description),
            product.IsActive
        );
    }

    /// <summary>
    /// Pobiera listę produktów sklepu użytkownika.
    /// </summary>
    /// <param name="userId">ID użytkownika</param>
    /// <returns>Lista DTO produktów</returns>
    public async Task<List<ProductDto>> GetUserProductsAsync(string userId)
    {
        var products = await _context.Products
            .Include(p => p.Shop)
            .Where(p => p.Shop.UserId == userId && p.Shop.DeletedAt == null)
            .OrderBy(p => p.Name)
            .Select(p => new ProductDto(
                p.Id,
                p.Name,
                p.Description,
                p.Price,
                new ShopDto(p.Shop.Id, p.Shop.Name, p.Shop.Slug, p.Shop.Description),
                p.IsActive
            ))
            .ToListAsync();

        return products;
    }
}

using ArtisanMarket.Application.Models;
using ArtisanMarket.Domain.Entities;
using ArtisanMarket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ArtisanMarket.Application.Services;

/// <summary>
/// Implementacja serwisu katalogu sklepów.
/// Zapewnia dostęp do danych sklepów z bazy danych przez Entity Framework Core.
/// </summary>
public class CatalogService : ICatalogService
{
    private readonly ApplicationDbContext _context;

    public CatalogService(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Pobiera listę wszystkich aktywnych sklepów (bez usuniętych).
    /// Filtruje sklepy, które nie mają ustawionego DeletedAt.
    /// </summary>
    /// <returns>Lista DTO sklepów do wyświetlenia w katalogu</returns>
    public async Task<List<ShopDto>> GetAllShopsAsync()
    {
        var shops = await _context.Shops
            .Where(s => s.DeletedAt == null)
            .OrderBy(s => s.Name)
            .Select(s => new ShopDto(
                s.Id,
                s.Name,
                s.Slug,
                s.Description
            ))
            .ToListAsync();

        return shops;
    }

    /// <summary>
    /// Pobiera szczegóły sklepu na podstawie slug.
    /// Filtruje tylko aktywne sklepy (bez usuniętych).
    /// </summary>
    /// <param name="slug">Slug sklepu</param>
    /// <returns>DTO ze szczegółami sklepu lub null jeśli nie znaleziono</returns>
    public async Task<ShopDetailsDto?> GetShopBySlugAsync(string slug)
    {
        var shop = await _context.Shops
            .Where(s => s.DeletedAt == null && s.Slug == slug)
            .Select(s => new ShopDetailsDto(
                s.Id,
                s.Name,
                s.Slug,
                s.Description,
                s.ContactEmail,
                s.Phone
            ))
            .FirstOrDefaultAsync();

        return shop;
    }

    /// <summary>
    /// Pobiera listę aktywnych produktów dla sklepu na podstawie slug.
    /// Filtruje tylko aktywne sklepy i produkty.
    /// </summary>
    /// <param name="slug">Slug sklepu</param>
    /// <returns>Lista DTO produktów sklepu</returns>
    public async Task<List<ProductDto>> GetShopProductsAsync(string slug)
    {
        var products = await _context.Shops
            .Where(s => s.DeletedAt == null && s.Slug == slug)
            .SelectMany(s => s.Products.Where(p => p.IsActive))
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


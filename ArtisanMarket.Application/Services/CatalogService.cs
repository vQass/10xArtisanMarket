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
}


using ArtisanMarket.Application.Models;

namespace ArtisanMarket.Application.Services;

/// <summary>
/// Interfejs serwisu do zarządzania katalogiem sklepów.
/// Odpowiedzialny za pobieranie danych sklepów do wyświetlenia w katalogu publicznym.
/// </summary>
public interface ICatalogService
{
    /// <summary>
    /// Pobiera listę wszystkich aktywnych sklepów (bez usuniętych).
    /// </summary>
    /// <returns>Lista DTO sklepów do wyświetlenia w katalogu</returns>
    Task<List<ShopDto>> GetAllShopsAsync();

    /// <summary>
    /// Pobiera szczegóły sklepu na podstawie slug.
    /// </summary>
    /// <param name="slug">Slug sklepu</param>
    /// <returns>DTO ze szczegółami sklepu lub null jeśli nie znaleziono</returns>
    Task<ShopDetailsDto?> GetShopBySlugAsync(string slug);

    /// <summary>
    /// Pobiera listę aktywnych produktów dla sklepu na podstawie slug.
    /// </summary>
    /// <param name="slug">Slug sklepu</param>
    /// <returns>Lista DTO produktów sklepu</returns>
    Task<List<ProductDto>> GetShopProductsAsync(string slug);
}


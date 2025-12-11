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
}


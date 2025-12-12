using ArtisanMarket.Application.Models;

namespace ArtisanMarket.Application.Services;

/// <summary>
/// Interfejs serwisu do zarządzania sklepami.
/// Odpowiedzialny za tworzenie sklepów, sprawdzanie czy użytkownik już ma sklep oraz pobieranie danych sklepu.
/// </summary>
public interface IShopService
{
    /// <summary>
    /// Tworzy nowy sklep dla zalogowanego użytkownika.
    /// </summary>
    /// <param name="userId">ID użytkownika tworzącego sklep</param>
    /// <param name="dto">Dane sklepu do utworzenia</param>
    /// <returns>DTO utworzonego sklepu</returns>
    /// <exception cref="InvalidOperationException">Gdy użytkownik już ma sklep</exception>
    Task<ShopDto> CreateShopAsync(string userId, CreateShopDto dto);

    /// <summary>
    /// Pobiera sklep powiązany z użytkownikiem.
    /// </summary>
    /// <param name="userId">ID użytkownika</param>
    /// <returns>DTO sklepu lub null jeśli użytkownik nie ma sklepu</returns>
    Task<ShopDto?> GetUserShopAsync(string userId);
}

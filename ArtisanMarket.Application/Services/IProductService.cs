using ArtisanMarket.Application.Models;

namespace ArtisanMarket.Application.Services;

/// <summary>
/// Interfejs serwisu do zarządzania produktami.
/// Odpowiedzialny za tworzenie produktów, sprawdzanie własności sklepu oraz pobieranie danych produktów.
/// </summary>
public interface IProductService
{
    /// <summary>
    /// Tworzy nowy produkt w sklepie zalogowanego użytkownika.
    /// </summary>
    /// <param name="userId">ID użytkownika tworzącego produkt</param>
    /// <param name="dto">Dane produktu do utworzenia</param>
    /// <returns>DTO utworzonego produktu</returns>
    /// <exception cref="InvalidOperationException">Gdy użytkownik nie ma sklepu</exception>
    Task<ProductDto> CreateProductAsync(string userId, CreateProductDto dto);

    /// <summary>
    /// Pobiera listę produktów sklepu użytkownika.
    /// </summary>
    /// <param name="userId">ID użytkownika</param>
    /// <returns>Lista DTO produktów</returns>
    Task<List<ProductDto>> GetUserProductsAsync(string userId);
}

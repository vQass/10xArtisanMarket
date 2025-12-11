namespace ArtisanMarket.Application.Models;

/// <summary>
/// ViewModel dla strony katalogu sklepów.
/// Przechowuje stan UI i dane wyświetlane na stronie katalogu.
/// </summary>
public class CatalogViewModel
{
    /// <summary>
    /// Lista wszystkich aktywnych sklepów do wyświetlenia.
    /// </summary>
    public List<ShopDto> Shops { get; set; } = new();

    /// <summary>
    /// Flaga wskazująca, czy dane są obecnie ładowane.
    /// </summary>
    public bool IsLoading { get; set; } = false;

    /// <summary>
    /// Komunikat błędu wyświetlany użytkownikowi w przypadku problemów z ładowaniem danych.
    /// </summary>
    public string? ErrorMessage { get; set; }
}


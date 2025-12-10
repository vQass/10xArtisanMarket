# Blazor Component Implementation Plan: Katalog Sklepów (Shop Catalog)

## 1. Przegląd komponentu/strony

Strona katalogu sklepów to główna strona publiczna aplikacji ArtisanMarket, która wyświetla listę wszystkich dostępnych sklepów rzemieślniczych. Umożliwia użytkownikom przeglądanie sklepów z nazwami i opisami oraz nawigację do indywidualnych stron sklepów. Strona stanowi punkt wejścia dla klientów chcących poznać ofertę platformy i jest dostępna bez wymagania rejestracji/logowania.

## 2. Struktura i Routing

- **Typ**: Blazor Page (@page)
- **Route**: "/" (strona główna) oraz "/catalog" (alias)
- **Parametry wejściowe ([Parameter])**: Brak - strona statyczna bez parametrów

## 3. Modele i Dane

- **ViewModel**: `CatalogViewModel`
  - `List<ShopDto> Shops` - lista wszystkich aktywnych sklepów
  - `bool IsLoading` - stan ładowania danych
  - `string? ErrorMessage` - komunikat błędu (jeśli wystąpi)

- **Command/Input Model**: Nie dotyczy (brak formularzy)

- **Wymagane Serwisy**:
  - `ICatalogService` - serwis do pobierania danych sklepów

## 4. Interakcje i Logika Biznesowa

- **Ładowanie danych (`OnInitializedAsync`)**:
  - Wywołanie `ICatalogService.GetAllShopsAsync()`
  - Filtrowanie tylko aktywnych sklepów (bez deleted_at)
  - Ustawienie `IsLoading = true/false`

- **Akcje użytkownika**:
  - **Kliknięcie w nazwę/opis sklepu**: Nawigacja do `/shop/{shop.Slug}`

- **Nawigacja**: Przekierowanie do indywidualnej strony sklepu po kliknięciu

## 5. Względy bezpieczeństwa

- **Autoryzacja**: Brak wymagań (`[AllowAnonymous]` lub bez atrybutu)
- **Walidacja danych**: Nie dotyczy (tylko wyświetlanie danych)

## 6. Obsługa błędów i UI Feedback

- **Sukces**: Wyświetlenie listy sklepów w interfejsie
- **Błąd ładowania danych**: Wyświetlenie komunikatu "Nie udało się załadować listy sklepów. Spróbuj odświeżyć stronę."
- **Wyjątki**: Logowanie błędów, wyświetlenie ogólnego komunikatu błędu w toast notification

## 7. Rozważania dotyczące wydajności

- **Strategia ładowania**: Jednorazowe ładowanie wszystkich sklepów w `OnInitializedAsync`
- **Zarządzanie stanem**: Minimalne użycie `StateHasChanged` - tylko przy błędach
- **Blazor Server specific**: Unikanie blokowania wątku UI przez asynchroniczne ładowanie danych

## 8. Etapy wdrożenia

1. **Stworzenie ViewModelu**
   - Utworzenie `CatalogViewModel.cs` w `ArtisanMarket.Application/Models/`

2. **Implementacja interfejsu serwisu**
   - Utworzenie `ICatalogService.cs` w `ArtisanMarket.Application/Services/`

3. **Implementacja serwisu**
   - Utworzenie `CatalogService.cs` w `ArtisanMarket.Application/Services/`
   - Implementacja `GetAllShopsAsync()` z użyciem EF Core
   - Rejestracja serwisu w `Program.cs`

4. **Stworzenie strony Blazor**
   - Utworzenie `Pages/Catalog/Index.razor`
   - Dodanie dyrektywy `@page "/"` oraz `@page "/catalog"`

5. **Implementacja logiki C#**
   - Wstrzyknięcie `ICatalogService`
   - Implementacja `OnInitializedAsync()` z obsługą błędów
   - Dodanie właściwości `CatalogViewModel`

6. **Implementacja UI**
   - Lista sklepów z kartami/wierszami
   - Linki nawigacyjne do `/shop/{shop.Slug}`
   - Responsywny design z wykorzystaniem Bootstrap/Tailwind

7. **Obsługa stanów UI**
   - Spinner ładowania podczas `IsLoading = true`
   - Komunikat błędu gdy `ErrorMessage` nie jest pusty

8. **Testowanie**
   - Test jednostkowy dla `CatalogService.GetAllShopsAsync()`
   - Test integracyjny ładowania strony
   - Test nawigacji do sklepów
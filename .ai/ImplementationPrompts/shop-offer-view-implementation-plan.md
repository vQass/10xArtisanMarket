# Blazor Component Implementation Plan: Podgląd Oferty Sklepu

## 1. Przegląd komponentu

Strona podglądu oferty sklepu umożliwia użytkownikom przeglądanie szczegółów wybranego sklepu oraz jego produktów. Jest to strona publiczna dostępna dla wszystkich odwiedzających, umożliwiająca poznanie oferty rzemieślników. Głównymi funkcjonalnościami są:
- Wyświetlanie podstawowych informacji o sklepie (nazwa, opis, dane kontaktowe)
- Lista aktywnych produktów sklepu z cenami i opisami
- Przyciski "Zamów" dla każdego produktu przekierowujące do składania zamówienia
- Responsywny layout dostosowany do urządzeń mobilnych i desktopowych

## 2. Struktura i Routing

- **Typ**: Blazor Page (@page)
- **Route**: `/shop/{slug}` - gdzie `slug` to unikalny identyfikator sklepu w URL
- **Parametry wejściowe (`[Parameter]`)**:
  - `[Parameter] public string Slug { get; set; }` - slug sklepu przekazywany przez URL

## 3. Modele i Dane

- **ViewModel**: `ShopOfferViewModel`
  - `ShopDto Shop` - dane sklepu (id, name, slug, description)
  - `List<ProductDto> Products` - lista produktów sklepu
  - `bool IsLoading` - flaga ładowania danych
  - `string? ErrorMessage` - komunikat błędu
  - `bool ShopNotFound` - flaga gdy sklep nie istnieje

- **Rozszerzone DTO**:
  - `ShopDetailsDto` - rozszerzenie `ShopDto` o dane kontaktowe (contact_email, phone)

- **Wymagane Serwisy**:
  - `ICatalogService` - rozszerzony o metody `GetShopBySlugAsync(string slug)` i `GetShopProductsAsync(string slug)`

## 4. Interakcje i Logika Biznesowa

- **Ładowanie danych (`OnInitializedAsync`)**:
  - Pobranie slug z parametrów URL
  - Walidacja obecności slug (przekierowanie do katalogu jeśli brak)
  - Równoległe wywołanie `CatalogService.GetShopBySlugAsync(slug)` i `CatalogService.GetShopProductsAsync(slug)`
  - Filtrowanie tylko aktywnych produktów (IsActive = true)

- **Akcje użytkownika**:
  - **Kliknięcie "Zamów" przy produkcie**: Przekierowanie do `/order/{productId}` z parametrem ID produktu
  - **Brak akcji dla sprzedawcy**: Strona jest tylko do przeglądania, nie zawiera funkcji zarządzania

- **Nawigacja**:
  - Przycisk "Powrót do katalogu" przekierowuje do `/catalog`
  - Linki do składania zamówień otwierają strony zamówienia w tym samym oknie

## 5. Względy bezpieczeństwa

- **Autoryzacja**: Brak wymagań (`[AllowAnonymous]`) - strona publiczna dostępna dla wszystkich
- **Walidacja danych**:
  - Slug sklepu walidowany pod kątem formatu URL (tylko litery, cyfry, myślniki)
  - Dostęp tylko do nieusuniętych sklepów i aktywnych produktów
  - Brak wrażliwych danych użytkownika w odpowiedziach

## 6. Obsługa błędów i UI Feedback

- **Sukces**: Wyświetlenie danych sklepu i produktów w responsywnym layoucie
- **Błąd walidacji**: Slug nieprawidłowy → przekierowanie do strony 404 lub katalogu z komunikatem
- **Sklep nie istnieje**: Wyświetlenie komunikatu "Sklep nie został znaleziony" z linkiem do katalogu
- **Brak produktów**: Wyświetlenie informacji "Ten sklep nie ma jeszcze produktów"
- **Wyjątki**: Logowanie błędów, wyświetlenie ogólnego komunikatu "Nie udało się załadować danych sklepu"
- **Ładowanie**: Spinner z tekstem "Ładowanie oferty sklepu..." podczas pobierania danych

## 7. Rozważania dotyczące wydajności

- Równoległe ładowanie danych sklepu i produktów aby zmniejszyć czas ładowania strony
- Lazy loading produktów jeśli lista będzie bardzo duża (choć w MVP prawdopodobnie niepotrzebne)
- Cache'owanie danych sklepu jeśli to samo zostanie odwiedzone wielokrotnie
- Unikanie niepotrzebnych zapytań - sprawdzenie czy slug istnieje przed pobraniem produktów

## 8. Etapy wdrożenia

1. **Rozszerzenie ICatalogService i CatalogService**:
   - Dodanie metod `GetShopBySlugAsync(string slug)` i `GetShopProductsAsync(string slug)`
   - Implementacja w CatalogService z właściwymi filtrami EF Core

2. **Utworzenie ShopDetailsDto**:
   - Rozszerzenie istniejącego ShopDto o pola kontaktowe
   - Aktualizacja AppModels.cs

3. **Utworzenie ShopOfferViewModel**:
   - Dodanie do AppModels.cs z właściwościami dla sklepu, produktów i stanu UI

4. **Utworzenie strony ShopOffer.razor**:
   - Implementacja routingu `/shop/{slug}`
   - Podstawowy layout z miejscami na dane sklepu i listę produktów

5. **Implementacja logiki ładowania danych**:
   - OnInitializedAsync z obsługą błędów
   - Równoległe ładowanie sklepu i produktów
   - Obsługa stanów ładowania i błędów

6. **Implementacja UI dla sklepu**:
   - Nagłówek z nazwą i opisem sklepu
   - Sekcja danych kontaktowych (jeśli dostępne)
   - Przycisk powrotu do katalogu

7. **Implementacja listy produktów**:
   - Karty produktów w gridzie responsywnym
   - Wyświetlanie nazwy, opisu, ceny
   - Przyciski "Zamów" z linkami do strony zamówienia

8. **Testowanie i walidacja**:
   - Testy jednostkowe dla nowych metod serwisu
   - Testy integracyjne ładowania strony
   - Walidacja responsywności UI

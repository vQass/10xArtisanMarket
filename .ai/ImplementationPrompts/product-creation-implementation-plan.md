# Blazor Component Implementation Plan: Dodawanie Produktów

## 1. Przegląd komponentu/strony

Strona umożliwia sprzedawcom dodawanie nowych produktów do swojego sklepu. Jest to formularz z polami: nazwa produktu, opis, cena. Po pomyślnym dodaniu produktu użytkownik zostaje przekierowany do listy produktów sklepu z komunikatem o sukcesie.

## 2. Struktura i Routing

- **Typ**: Page (@page)
- **Route**: `/dashboard/products/add`
- **Parametry wejściowe**: Brak - strona jest samodzielna

## 3. Modele i Dane

- **ViewModel**: `CreateProductViewModel`
  - `CreateProductDto Model` - dane formularza z walidacją
  - `bool IsLoading` - flaga ładowania podczas przetwarzania
  - `string? ErrorMessage` - komunikat błędu ogólnego
  - `bool IsSuccess` - flaga sukcesu (opcjonalna dla przekierowania)
- **Command/Input Model**: `CreateProductDto` (już istnieje)
  - `string Name` - nazwa produktu (wymagana, 2-200 znaków)
  - `string? Description` - opis (opcjonalny, max 1000 znaków)
  - `decimal Price` - cena (wymagana, > 0.01)
- **Wymagane Serwisy**:
  - `IProductService` - do tworzenia produktów
  - `IShopService` - do weryfikacji czy użytkownik ma sklep

## 4. Interakcje i Logika Biznesowa

- **Ładowanie danych (`OnInitializedAsync`)**:
  - Sprawdź czy użytkownik jest zalogowany
  - Pobierz ID użytkownika
  - Zweryfikuj czy użytkownik ma sklep (jeśli nie - przekieruj do tworzenia sklepu)
  - Jeśli wszystko OK - wyświetl formularz

- **Akcje użytkownika**:
  - **Dodaj produkt**: Walidacja formularza → wywołanie `ProductService.CreateProductAsync(userId, model)` → przekierowanie do `/dashboard/products` z komunikatem sukcesu
  - **Anuluj**: Przekierowanie do `/dashboard/products` bez zapisywania

- **Nawigacja**:
  - Sukces: Przekierowanie do listy produktów (`/dashboard/products`)
  - Brak sklepu: Przekierowanie do tworzenia sklepu (`/dashboard/shop`)
  - Niezalogowany: Przekierowanie do logowania (`/auth/login`)

## 5. Względy bezpieczeństwa

- **Autoryzacja**: `[Authorize]` - tylko zalogowani użytkownicy
- **Walidacja danych**:
  - Klient: DataAnnotations na `CreateProductDto`
  - Serwer: Dodatkowa walidacja w `ProductService` (sprawdzenie własności sklepu)
- **Reguły biznesowe**:
  - Użytkownik musi mieć sklep aby dodać produkt
  - Produkt automatycznie przypisywany do sklepu użytkownika
  - Cena musi być większa od zera

## 6. Obsługa błędów i UI Feedback

- **Sukces**: Toast notification "Produkt został dodany pomyślnie" + przekierowanie do listy produktów
- **Błąd walidacji**: Wyświetlenie komunikatów pod polami formularza (DataAnnotationsValidator)
- **Wyjątki**:
  - `InvalidOperationException`: "Nie masz uprawnień do dodawania produktów" (brak sklepu)
  - Ogólne wyjątki: "Wystąpił błąd podczas dodawania produktu. Spróbuj ponownie."
- **Stan ładowania**: Spinner na przycisku podczas przetwarzania, wyłączenie formularza

## 7. Rozważania dotyczące wydajności

- Minimalne zapytania do bazy: tylko sprawdzenie istnienia sklepu przy inicjalizacji
- Brak lazy loading - bezpośrednie wywołania serwisów
- StateHasChanged tylko gdy potrzebne (po zmianach stanu ładowania/błędów)
- Formularz z SupplyParameterFromForm dla optymalizacji renderowania

## 8. Etapy wdrożenia

1. **Stworzenie IProductService i ProductService**
   - Interfejs z metodą `Task<ProductDto> CreateProductAsync(string userId, CreateProductDto dto)`
   - Implementacja sprawdzająca własność sklepu i tworząca produkt
   - Rejestracja w DI kontenerze

2. **Stworzenie CreateProductViewModel**
   - Klasa podobna do CreateShopViewModel
   - Właściwości: Model, IsLoading, ErrorMessage, IsSuccess

3. **Implementacja strony AddProduct.razor**
   - Route `/dashboard/products/add`
   - Formularz z polami: Name (InputText), Description (InputTextArea), Price (InputNumber)
   - Przyciski: "Dodaj produkt" (submit), "Anuluj" (onclick)
   - Obsługa OnInitializedAsync i CreateProductAsync

4. **Rejestracja serwisów**
   - Dodanie ProductService do ServiceCollectionExtensions

5. **Testowanie**
   - Sprawdzenie walidacji formularza
   - Test przypadków błędów (brak sklepu, nieprawidłowe dane)
   - Weryfikacja przekierowań i komunikatów

W trakcie implementacji powinieneś wzorować się na stronie @ShopManagement.razor
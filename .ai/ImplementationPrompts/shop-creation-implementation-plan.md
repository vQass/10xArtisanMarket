# Blazor Component Implementation Plan: Okno tworzenia sklepu

## 1. Przegląd komponentu/strony
Strona umożliwiająca zalogowanym użytkownikom utworzenie swojego sklepu w systemie ArtisanMarket. Jest to kluczowa funkcjonalność dla sprzedawców - pozwala na założenie profilu sklepowego z podstawowymi informacjami kontaktowymi. Strona sprawdza czy użytkownik już posiada sklep i w razie potrzeby przekierowuje do zarządzania istniejącym sklepem.

## 2. Struktura i Routing
- **Typ**: Page (@page)
- **Route**: `/dashboard/shop`
- **Parametry wejściowe ([Parameter])**:
  - Brak parametrów - strona działa w kontekście aktualnie zalogowanego użytkownika

## 3. Modele i Dane
- **ViewModel**: `CreateShopViewModel` z właściwościami:
  - `CreateShopDto Model` - dane formularza
  - `bool IsLoading` - stan ładowania
  - `string? ErrorMessage` - globalny komunikat błędu
  - `bool IsSuccess` - stan sukcesu
- **Command/Input Model**: `CreateShopDto` (już zdefiniowany w `AppModels.cs`)
- **Wymagane Serwisy**:
  - `IShopService` - do tworzenia sklepu i sprawdzania czy użytkownik już ma sklep
  - `NavigationManager` - do przekierowań
  - `AuthenticationStateProvider` - do pobierania aktualnego użytkownika

## 4. Interakcje i Logika Biznesowa
- **Ładowanie danych (OnInitializedAsync)**:
  - Sprawdź czy użytkownik jest zalogowany (przez AuthenticationStateProvider)
  - Wywołaj `IShopService.GetUserShopAsync()` aby sprawdzić czy użytkownik już ma sklep
  - Jeśli ma sklep - przekieruj do `/dashboard/shop/manage` lub pokaż komunikat
  - Jeśli nie ma sklepu - wyświetl formularz tworzenia

- **Akcje użytkownika**:
  - **Przycisk "Utwórz sklep"**: Walidacja formularza → wywołanie `IShopService.CreateShopAsync(Model)` → przekierowanie do `/dashboard` z komunikatem sukcesu
  - **Przycisk "Anuluj"**: Przekierowanie do `/dashboard` bez zapisywania

- **Nawigacja**:
  - Sukces: Przekierowanie do `/dashboard` z toastem "Sklep został pomyślnie utworzony"
  - Anulowanie: Przekierowanie do `/dashboard`

## 5. Względy bezpieczeństwa
- **Autoryzacja**: `[Authorize]` - tylko zalogowani użytkownicy mają dostęp
- **Walidacja danych**: DataAnnotations w CreateShopDto + serwerowa walidacja w serwisie
- **Weryfikacja biznesowa**: Serwis sprawdza czy użytkownik już nie ma sklepu (reguła biznesowa: jeden użytkownik = jeden sklep)

## 6. Obsługa błędów i UI Feedback
- **Sukces**: Wyświetlenie toast notification "Sklep został pomyślnie utworzony" i automatyczne przekierowanie do dashboardu
- **Błąd walidacji**: Wyświetlenie komunikatów błędów pod odpowiednimi polami formularza (EditContext)
- **Błędy biznesowe**: Wyświetlenie komunikatu błędu na górze formularza (np. "Już posiadasz sklep w systemie")
- **Wyjątki**: Logowanie błędów, wyświetlenie ogólnego komunikatu "Wystąpił błąd podczas tworzenia sklepu. Spróbuj ponownie."

## 7. Rozważania dotyczące wydajności
- **Strategia ładowania**: Jednorazowe sprawdzenie stanu sklepu przy ładowaniu strony
- **Zarządzanie stanem**: Minimalny stan UI (tylko IsLoading, ErrorMessage, IsSuccess)
- **Unikanie blokowania UI**: Async operacje bez blokowania wątku UI Blazor Server

## 8. Etapy wdrożenia
1. **Utworzenie interfejsu IShopService** w `ArtisanMarket.Application/Services`
2. **Implementacja ShopService** z metodami `CreateShopAsync` i `GetUserShopAsync`
3. **Rejestracja serwisu** w `ServiceCollectionExtensions.cs`
4. **Utworzenie strony ShopManagement.razor** w `ArtisanMarket.BlazorApp/Pages/Seller/`
5. **Implementacja logiki C#** - wstrzyknięcie serwisów, obsługa OnInitializedAsync, metoda CreateShop
6. **Implementacja UI formularza** - EditForm z polami: nazwa, opis, email, telefon
7. **Podpięcie walidacji** - EditContext, ValidationSummary, przyciski akcji
8. **Obsługa stanów UI** - loading spinner, komunikaty błędów/sukcesu
9. **Testy jednostkowe** dla ShopService używając xUnit
10. **Test integracyjny** - sprawdzenie całego przepływu tworzenia sklepu

# Plan Testów dla Projektu ArtisanMarket

## 1. Wprowadzenie i Cele Testowania

### 1.1 Cel Projektu
ArtisanMarket to platforma e-commerce umożliwiająca rzemieślnikom sprzedaż produktów bezpośrednio klientom. Projekt realizuje zasadę "jeden użytkownik = jeden sklep" z płatnościami przy odbiorze.

### 1.2 Cele Testowania
- Zapewnienie wysokiej jakości i niezawodności kluczowych funkcjonalności MVP
- Weryfikacja poprawności logiki biznesowej i reguł walidacji
- Minimalizacja ryzyka błędów w krytycznych ścieżkach użytkownika
- Przygotowanie solidnej bazy dla przyszłego rozwoju

### 1.3 Zakres Testów
**W zakresie MVP:**
- Rejestracja i logowanie użytkowników
- Tworzenie i zarządzanie sklepami
- Dodawanie i zarządzanie produktami
- Przeglądanie katalogu sklepów i produktów
- Składanie zamówień z dostawą do domu

**Poza zakresem MVP (nie testowane):**
- Integracje płatnicze
- Systemy prowizji
- Zdjęcia produktów
- Warianty produktów
- Systemy ocen i recenzji
- Zaawansowane filtry wyszukiwania
- Statusy zamówień
- Anulowanie zamówień przez aplikację

## 2. Typy Testów do Przeprowadzenia

### 2.1 Testy Jednostkowe (Unit Tests)
**Framework:** xUnit
**Pokrycie:** Min. 80% dla logiki biznesowej

**Obszary testowania:**
- Serwisy aplikacji (ShopService, ProductService, CatalogService)
- Walidacja modeli wejściowych (CreateShopDto, CreateProductDto, CreateOrderDto)
- Helper methods (generowanie slug, obliczenia cen)
- Logika biznesowa (reguły "jeden użytkownik = jeden sklep")

**Narzędzia:**
- xUnit
- Moq (dla mockowania zależności)
- FluentAssertions (dla czytelnych asercji)
- Coverlet (dla pokrycia kodu)

### 2.2 Testy Integracyjne
**Framework:** xUnit z TestContainers lub lokalną bazą testową

**Obszary testowania:**
- Interakcje z bazą danych przez EF Core
- Migracje bazy danych
- Transakcje biznesowe (tworzenie zamówienia)
- Integracja z ASP.NET Core Identity

**Scenariusze kluczowe:**
- Pełny cykl tworzenia sklepu i produktów
- Proces składania zamówienia
- Soft delete i jego wpływ na relacje

### 2.3 Testy Interfejsu Użytkownika (UI Tests)
**Framework:** Playwright lub Selenium z xUnit

**Obszary testowania:**
- Krytyczne ścieżki użytkownika (happy path)
- Walidacja formularzy
- Obsługa błędów po stronie klienta
- Responsywność interfejsu

**Scenariusze kluczowe:**
- Rejestracja nowego użytkownika
- Tworzenie sklepu przez sprzedawcę
- Dodawanie produktów
- Przeglądanie katalogu jako kupujący
- Składanie zamówienia

### 2.4 Testy Wydajnościowe
**Narzędzia:** NBomber, k6 lub JMeter

**Obszary testowania:**
- Czas ładowania stron Blazor
- Wydajność zapytań do bazy danych
- Zużycie pamięci przy dużej liczbie użytkowników
- Czas odpowiedzi API

**Metryki docelowe:**
- Czas ładowania stron: < 2 sekundy
- Czas odpowiedzi API: < 500ms
- Zużycie pamięci: stabilne przy 100+ użytkownikach

### 2.5 Testy Bezpieczeństwa
**Narzędzia:** OWASP ZAP, ręczne testy penetracyjne

**Obszary testowania:**
- Autoryzacja i uwierzytelnianie
- Weryfikacja uprawnień do zasobów
- Zabezpieczenia przed SQL injection
- Zabezpieczenia przed XSS
- Walidacja danych wejściowych

### 2.6 Testy Konteneryzacji i Wdrożenia
**Framework:** Docker + GitHub Actions

**Obszary testowania:**
- Budowanie obrazów Docker
- Uruchamianie aplikacji w kontenerze
- Migracje bazy danych w środowisku kontenerowym
- CI/CD pipeline

## 3. Scenariusze Testowe dla Kluczowych Funkcjonalności

### 3.1 Zarządzanie Sklepami

#### TC-SHOP-001: Tworzenie nowego sklepu (Happy Path)
**Warunki wstępne:** Użytkownik zalogowany, nie ma sklepu
**Kroki:**
1. Przejdź do strony zarządzania sklepem
2. Wypełnij formularz: nazwa, opis, email kontaktowy, telefon
3. Kliknij "Utwórz sklep"
**Oczekiwany wynik:** Sklep utworzony, przekierowanie do zarządzania produktami

#### TC-SHOP-002: Próba utworzenia drugiego sklepu
**Warunki wstępne:** Użytkownik ma już sklep
**Kroki:**
1. Przejdź do strony tworzenia sklepu
2. Wypełnij formularz danymi
3. Kliknij "Utwórz sklep"
**Oczekiwany wynik:** Błąd "Użytkownik już posiada sklep w systemie"

#### TC-SHOP-003: Walidacja formularza tworzenia sklepu
**Scenariusze:**
- Pusta nazwa → błąd walidacji
- Nazwa > 100 znaków → błąd walidacji
- Nieprawidłowy format email → błąd walidacji
- Telefon > 20 znaków → błąd walidacji

#### TC-SHOP-004: Generowanie unikalnego slug
**Warunki wstępne:** Istnieje sklep o nazwie "Sklep Testowy"
**Kroki:**
1. Utwórz sklep o nazwie "Sklep Testowy"
**Oczekiwany wynik:** Slug "sklep-testowy-2" (lub podobny z sufiksem)

### 3.2 Zarządzanie Produktami

#### TC-PROD-001: Dodawanie produktu (Happy Path)
**Warunki wstępne:** Użytkownik ma sklep
**Kroki:**
1. Przejdź do strony produktów
2. Kliknij "Dodaj produkt"
3. Wypełnij formularz: nazwa, opis, cena
4. Kliknij "Zapisz"
**Oczekiwany wynik:** Produkt dodany, widoczny na liście

#### TC-PROD-002: Próba dodania produktu bez sklepu
**Warunki wstępne:** Użytkownik nie ma sklepu
**Kroki:**
1. Przejdź do strony dodawania produktu
2. Wypełnij formularz
3. Kliknij "Zapisz"
**Oczekiwany wynik:** Błąd "Nie masz uprawnień do dodawania produktów. Najpierw utwórz sklep."

#### TC-PROD-003: Walidacja formularza produktu
**Scenariusze:**
- Pusta nazwa → błąd walidacji
- Cena ≤ 0 → błąd walidacji
- Cena > maksymalna wartość decimal → błąd walidacji
- Opis > 1000 znaków → błąd walidacji

### 3.3 Przeglądanie Katalogu

#### TC-CAT-001: Wyświetlanie listy sklepów
**Warunki wstępne:** Istnieje co najmniej jeden sklep
**Kroki:**
1. Przejdź do strony katalogu
**Oczekiwany wynik:** Lista aktywnych sklepów z nazwami i opisami

#### TC-CAT-002: Przeglądanie oferty sklepu
**Warunki wstępne:** Istnieje sklep z produktami
**Kroki:**
1. Przejdź do katalogu
2. Kliknij w nazwę sklepu
**Oczekiwany wynik:** Strona z detalami sklepu i listą produktów

#### TC-CAT-003: Soft delete - sklep niewidoczny w katalogu
**Warunki wstępne:** Sklep został "usunięty" (DeletedAt != null)
**Kroki:**
1. Przejdź do katalogu
**Oczekiwany wynik:** Usunięty sklep nie jest widoczny

### 3.4 Składanie Zamówień

#### TC-ORDER-001: Składanie zamówienia (Happy Path)
**Warunki wstępne:** Kupujący zalogowany, istnieje produkt
**Kroki:**
1. Przejdź do oferty sklepu
2. Wybierz produkt
3. Wypełnij formularz zamówienia: ilość, dane dostawy
4. Kliknij "Zamów"
**Oczekiwany wynik:** Zamówienie utworzone, komunikat sukcesu

#### TC-ORDER-002: Walidacja formularza zamówienia
**Scenariusze:**
- Brak wybranego produktu → błąd walidacji
- Ilość ≤ 0 → błąd walidacji
- Puste dane adresowe → błąd walidacji
- Nieprawidłowy kod pocztowy → błąd walidacji

#### TC-ORDER-003: Snapshot ceny produktu
**Warunki wstępne:** Produkt kosztuje 100 zł
**Kroki:**
1. Składź zamówienie
2. Zmień cenę produktu na 150 zł
**Oczekiwany wynik:** W zamówieniu nadal widoczna cena 100 zł

## 4. Środowisko Testowe

### 4.1 Środowisko Deweloperskie
- **Baza danych:** PostgreSQL lokalny lub Docker
- **IDE:** Visual Studio 2022 / VS Code
- **Narzędzia:** dotnet CLI, Docker Desktop

### 4.2 Środowisko CI/CD
- **GitHub Actions:** Automatyczne uruchamianie testów
- **Konteneryzacja:** Docker dla spójności środowisk
- **Baza testowa:** PostgreSQL w kontenerze

### 4.3 Konfiguracja Testów
```json
{
  "ConnectionStrings": {
    "TestDatabase": "Host=localhost;Database=artisanmarket_test;Username=test;Password=test"
  },
  "TestSettings": {
    "UseInMemoryDatabase": false,
    "ResetDatabaseBeforeEachTest": true
  }
}
```

## 5. Narzędzia do Testowania

### 5.1 Testy Jednostkowe i Integracyjne
- **xUnit:** Framework testów
- **Moq:** Mockowanie zależności
- **FluentAssertions:** Czytelne asercje
- **TestContainers:** Kontenery dla testów integracyjnych
- **Respawn:** Resetowanie bazy danych między testami

### 5.2 Testy UI
- **Playwright:** End-to-end testing
- **Selenium WebDriver:** Alternatywa dla Playwright
- **BrowserStack:** Testy na różnych przeglądarkach

### 5.3 Testy Wydajnościowe
- **NBomber:** Load testing dla .NET
- **k6:** Performance testing
- **Application Insights:** Monitorowanie wydajności

### 5.4 Testy Bezpieczeństwa
- **OWASP ZAP:** Automated security testing
- **SQLMap:** Testowanie SQL injection
- **Burp Suite:** Manual security testing

### 5.5 CI/CD i Konteneryzacja
- **GitHub Actions:** CI/CD pipeline
- **Docker:** Konteneryzacja aplikacji
- **TestContainers:** Kontenery dla testów

## 6. Harmonogram Testów

### 6.1 Faza 1: Przygotowanie (Tydzień 1-2)
- Konfiguracja frameworków testowych
- Utworzenie struktury projektu testów
- Implementacja podstawowych testów jednostkowych

### 6.2 Faza 2: Testy Jednostkowe (Tydzień 3-4)
- Testy serwisów aplikacji
- Testy walidacji modeli
- Testy logiki biznesowej
- Osiągnięcie min. 80% pokrycia kodu

### 6.3 Faza 3: Testy Integracyjne (Tydzień 5-6)
- Testy z bazą danych
- Testy transakcji biznesowych
- Integracja z Identity

### 6.4 Faza 4: Testy UI (Tydzień 7-8)
- Testy krytycznych ścieżek
- Testy walidacji formularzy
- Testy responsywności

### 6.5 Faza 5: Testy Regresyjne i Wydajnościowe (Tydzień 9-10)
- Pełne testy regresyjne
- Testy wydajnościowe
- Testy bezpieczeństwa podstawowe

### 6.6 Faza 6: Przygotowanie do Wdrożenia (Tydzień 11-12)
- Testy konteneryzacji
- Testy CI/CD
- Dokumentacja i szkolenia

## 7. Kryteria Akceptacji Testów

### 7.1 Kryteria Akceptacji dla Wydania
- **Pokrycie kodu:** Min. 80% dla logiki biznesowej
- **Testy jednostkowe:** Wszystkie przechodzą
- **Testy integracyjne:** Wszystkie przechodzą
- **Testy UI:** Wszystkie krytyczne ścieżki przechodzą
- **Błędy:** Zero błędów krytycznych (Severity 1)
- **Wydajność:** Czas odpowiedzi API < 500ms

### 7.2 Definicje Poziomów Błędów
- **Severity 1 (Krytyczny):** Błędy uniemożliwiające podstawowe funkcje (np. niemożność utworzenia sklepu)
- **Severity 2 (Wysoki):** Błędy wpływające na główne funkcje (np. błędna walidacja formularza)
- **Severity 3 (Średni):** Błędy wpływające na UX (np. błędne komunikaty)
- **Severity 4 (Niski):** Drobne błędy kosmetyczne

## 8. Role i Odpowiedzialności w Procesie Testowania

### 8.1 Inżynier QA/Test Manager
- **Odpowiedzialności:**
  - Projektowanie strategii i planów testów
  - Nadzór nad jakością kodu i testów
  - Raportowanie postępów i ryzyka
  - Współpraca z zespołem deweloperskim

### 8.2 Developerzy
- **Odpowiedzialności:**
  - Pisanie testów jednostkowych dla własnego kodu
  - Utrzymanie wysokiej jakości kodu
  - Wsparcie w debugowaniu błędów
  - Code review testów innych developerów

### 8.3 Product Owner
- **Odpowiedzialności:**
  - Definiowanie akceptacyjnych kryteriów jakości
  - Priorytetyzacja błędów
  - Decyzje o zakresie testów

### 8.4 DevOps/Infrastructure Team
- **Odpowiedzialności:**
  - Konfiguracja środowisk testowych
  - Utrzymanie CI/CD pipeline
  - Wsparcie w testach konteneryzacji

## 9. Procedury Raportowania Błędów

### 9.1 Narzędzia Raportowania
- **GitHub Issues:** Dla błędów wymagających naprawy
- **TestRail/Jira:** Dla zorganizowanego śledzenia testów (jeśli dostępne)
- **Azure DevOps:** Alternatywne narzędzie do zarządzania testami

### 9.2 Format Raportu Błędu
**Tytuł:** [SEVERITY] Krótki opis błędu

**Opis:**
- **Warunki wstępne:** Jak odtworzyć błąd
- **Kroki do odtworzenia:** Numerowane kroki
- **Oczekiwany wynik:** Co powinno się stać
- **Aktualny wynik:** Co się dzieje
- **Środowisko:** Browser, OS, wersja aplikacji
- **Załączniki:** Zrzuty ekranu, logi, pliki

**Priorytet:** Krytyczny/Wysoki/Średni/Niski
**Severity:** 1-4
**Przypisany do:** Odpowiedzialny developer

### 9.3 Proces Rozwiązywania Błędów
1. **Raportowanie:** QA tworzy issue z pełnym opisem
2. **Triage:** Product Owner określa priorytet
3. **Naprawa:** Developer implementuje fix
4. **Testowanie:** QA weryfikuje naprawę
5. **Zamknięcie:** Issue zamknięte po potwierdzeniu

### 9.4 Raporty Podsumowujące
- **Daily Reports:** Stan testów, nowe błędy, blokery
- **Weekly Reports:** Postęp, metryki jakości, ryzyka
- **Release Reports:** Podsumowanie wyników testów przed wydaniem

---

**Data utworzenia:** Grudzień 2025
**Wersja:** 1.0
**Autor:** Inżynier QA
**Zatwierdzony przez:** Tech Lead / Product Owner

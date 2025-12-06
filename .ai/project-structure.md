# Struktura Projektu ArtisanMarket

## Analiza Obecnego Stanu

### ✅ Co jest już zaimplementowane:
1. **Podstawowa struktura Blazor App**
   - Projekt `ArtisanMarket.BlazorApp` z .NET 9
   - Konfiguracja Blazor Server z interaktywnymi komponentami
   - Struktura folderów: `Components/`, `Data/`, `wwwroot/`

2. **ASP.NET Core Identity**
   - `ApplicationUser` (rozszerza `IdentityUser`)
   - `ApplicationDbContext` (rozszerza `IdentityDbContext<ApplicationUser>`)
   - Migracje Identity (CreateIdentitySchema)
   - Komponenty Identity: Login, Register, Manage, itp.
   - Serwisy Identity: `IdentityUserAccessor`, `IdentityRedirectManager`, `IdentityRevalidatingAuthenticationStateProvider`

3. **Podstawowe strony**
   - Home, Error, Counter, Weather (szablonowe)
   - Layout: MainLayout, NavMenu

### ❌ Co wymaga implementacji:

1. **Baza danych**
   - ❌ Zmiana z SQL Server na PostgreSQL
   - ❌ Dodanie pakietu `Npgsql.EntityFrameworkCore.PostgreSQL`

2. **Modele domenowe**
   - ❌ `Shop` - model sklepu
   - ❌ `Product` - model produktu
   - ❌ `Order` - model zamówienia
   - ❌ Relacje między modelami

3. **Strony Blazor (Pages)**
   - ❌ Katalog sklepów (publiczny)
   - ❌ Strona sklepu z produktami
   - ❌ Formularz zamówienia
   - ❌ Panel sprzedawcy (zarządzanie sklepem, produktami, zamówieniami)
   - ❌ Panel kupującego (historia zamówień)
   - ❌ Strona potwierdzenia zamówienia

4. **Logika biznesowa / Serwisy**
   - ❌ Serwisy do zarządzania sklepami
   - ❌ Serwisy do zarządzania produktami
   - ❌ Serwisy do zarządzania zamówieniami
   - ❌ Walidacja biznesowa

5. **Testy jednostkowe**
   - ❌ Projekt testowy (xUnit)
   - ❌ Testy dla logiki biznesowej

6. **Konfiguracja**
   - ❌ Connection string dla PostgreSQL
   - ❌ Dockerfile dla konteneryzacji
   - ❌ GitHub Actions workflow dla CI/CD

---

## Proponowana Struktura Projektu (Clean Architecture)

```
ArtisanMarket/
├── .ai/                          # Dokumentacja projektu
│   ├── prd.md
│   ├── tech-stack.md
│   └── project-structure.md      # Ten dokument
│
├── .cursor/                      # Reguły Cursor
│   └── rules/
│       └── shared.mdc
│
├── .github/                      # CI/CD
│   └── workflows/
│       └── ci-cd.yml             # ❌ DO UTWORZENIA
│
├── ArtisanMarket.Domain/         # ❌ DO UTWORZENIA - Warstwa domenowa
│   ├── Entities/                  # Encje domenowe
│   │   ├── Shop.cs                # Model sklepu
│   │   ├── Product.cs             # Model produktu
│   │   └── Order.cs               # Model zamówienia
│   └── ArtisanMarket.Domain.csproj
│
├── ArtisanMarket.Application/     # ❌ DO UTWORZENIA - Warstwa aplikacyjna
│   ├── Services/                  # Serwisy biznesowe
│   │   ├── IShopService.cs        # Interfejs serwisu sklepów
│   │   ├── ShopService.cs         # Implementacja serwisu sklepów
│   │   ├── IProductService.cs     # Interfejs serwisu produktów
│   │   ├── ProductService.cs     # Implementacja serwisu produktów
│   │   ├── IOrderService.cs       # Interfejs serwisu zamówień
│   │   └── OrderService.cs        # Implementacja serwisu zamówień
│   ├── DTOs/                      # Data Transfer Objects (opcjonalnie)
│   │   ├── ShopDto.cs
│   │   ├── ProductDto.cs
│   │   └── OrderDto.cs
│   └── ArtisanMarket.Application.csproj
│
├── ArtisanMarket.Infrastructure/  # ❌ DO UTWORZENIA - Warstwa infrastruktury
│   ├── Data/                      # Dostęp do danych
│   │   ├── ApplicationDbContext.cs  # Kontekst EF Core
│   │   ├── ApplicationUser.cs      # Użytkownik Identity
│   │   └── Configurations/          # Konfiguracje EF Core
│   │       ├── ShopConfiguration.cs
│   │       ├── ProductConfiguration.cs
│   │       └── OrderConfiguration.cs
│   ├── Migrations/                 # Migracje bazy danych
│   │   └── [migracje będą generowane automatycznie]
│   └── ArtisanMarket.Infrastructure.csproj
│
├── ArtisanMarket.BlazorApp/       # ✅ ISTNIEJE - Warstwa prezentacji
│   ├── Components/                # ✅ ISTNIEJE
│   │   ├── Account/               # ✅ Identity components
│   │   │   ├── Pages/             # ✅ Login, Register, Manage
│   │   │   └── Shared/            # ✅ Komponenty pomocnicze
│   │   │
│   │   ├── Layout/                # ✅ ISTNIEJE
│   │   │   ├── MainLayout.razor
│   │   │   └── NavMenu.razor
│   │   │
│   │   ├── Pages/                 # ✅ ISTNIEJE (częściowo)
│   │   │   ├── Home.razor         # ✅ ISTNIEJE (do modyfikacji)
│   │   │   ├── Error.razor        # ✅ ISTNIEJE
│   │   │   │
│   │   │   ├── Catalog/           # ❌ DO UTWORZENIA
│   │   │   │   ├── Index.razor    # Lista wszystkich sklepów
│   │   │   │   └── _Imports.razor
│   │   │   │
│   │   │   ├── Shop/              # ❌ DO UTWORZENIA
│   │   │   │   ├── Details.razor  # Strona sklepu z produktami
│   │   │   │   └── _Imports.razor
│   │   │   │
│   │   │   ├── Order/             # ❌ DO UTWORZENIA
│   │   │   │   ├── Create.razor   # Formularz zamówienia
│   │   │   │   ├── Confirmation.razor # Potwierdzenie zamówienia
│   │   │   │   └── _Imports.razor
│   │   │   │
│   │   │   ├── Seller/            # ❌ DO UTWORZENIA - Panel sprzedawcy
│   │   │   │   ├── Dashboard.razor     # Panel główny
│   │   │   │   ├── Shop/               # Zarządzanie sklepem
│   │   │   │   │   ├── Create.razor    # Tworzenie sklepu
│   │   │   │   │   └── Edit.razor      # Edycja sklepu
│   │   │   │   ├── Products/           # Zarządzanie produktami
│   │   │   │   │   ├── Index.razor     # Lista produktów
│   │   │   │   │   ├── Create.razor    # Dodawanie produktu
│   │   │   │   │   ├── Edit.razor      # Edycja produktu
│   │   │   │   │   └── Delete.razor    # Usuwanie produktu
│   │   │   │   └── Orders/             # Zamówienia
│   │   │   │       └── Index.razor     # Lista zamówień
│   │   │   │
│   │   │   └── Buyer/             # ❌ DO UTWORZENIA - Panel kupującego
│   │   │       └── Orders/              # Historia zamówień
│   │   │           └── Index.razor    # Lista zamówień kupującego
│   │   │
│   │   ├── Shared/                # ❌ DO UTWORZENIA - Komponenty współdzielone
│   │   │   ├── ProductCard.razor  # Karta produktu (do użycia w katalogu)
│   │   │   └── ShopCard.razor     # Karta sklepu (do użycia w katalogu)
│   │   │
│   │   ├── App.razor              # ✅ ISTNIEJE
│   │   ├── Routes.razor           # ✅ ISTNIEJE
│   │   └── _Imports.razor         # ✅ ISTNIEJE
│   │
│   ├── ViewModels/                 # ❌ DO UTWORZENIA (opcjonalnie)
│   │   ├── ShopViewModel.cs        # ViewModel dla sklepu
│   │   ├── ProductViewModel.cs     # ViewModel dla produktu
│   │   └── OrderViewModel.cs       # ViewModel dla zamówienia
│   │
│   ├── Program.cs                  # ✅ ISTNIEJE (wymaga modyfikacji)
│   ├── ArtisanMarket.BlazorApp.csproj  # ✅ ISTNIEJE (wymaga modyfikacji)
│   ├── appsettings.json            # ✅ ISTNIEJE (wymaga modyfikacji)
│   ├── appsettings.Development.json # ✅ ISTNIEJE
│   │
│   └── wwwroot/                    # ✅ ISTNIEJE
│       ├── css/
│       └── lib/
│
├── ArtisanMarket.Tests/            # ✅ ISTNIEJE - Projekt testowy
│   ├── Application/                # Testy warstwy aplikacyjnej
│   │   └── Services/
│   │       ├── ShopServiceTests.cs
│   │       ├── ProductServiceTests.cs
│   │       └── OrderServiceTests.cs
│   ├── Infrastructure/             # Testy warstwy infrastruktury
│   │   └── Data/
│   │       └── ApplicationDbContextTests.cs
│   └── ArtisanMarket.Tests.csproj  # ✅ ISTNIEJE
│
├── docker/                         # ❌ DO UTWORZENIA - Konfiguracja Docker
│   ├── Dockerfile                  # Dockerfile dla aplikacji
│   └── docker-compose.yml          # Docker Compose (opcjonalnie)
│
├── ArtisanMarket.sln               # ✅ ISTNIEJE (wymaga rozszerzenia)
├── .gitignore                      # ✅ ISTNIEJE
├── LICENSE.txt                     # ✅ ISTNIEJE
└── README.md                       # ❌ DO UTWORZENIA (opcjonalnie)

```

---

## Szczegółowy Opis Komponentów

### 1. ArtisanMarket.Domain - Warstwa Domenowa

**Odpowiedzialność:** Zawiera encje domenowe i logikę biznesową niezależną od infrastruktury.

#### Entities/

#### Shop.cs
```csharp
// Właściwości:
// - Id (int, PK)
// - Name (string, wymagane, unikalne)
// - OwnerId (string, FK do ApplicationUser)
// - Owner (ApplicationUser, navigation property)
// - Products (ICollection<Product>, navigation property)
// - CreatedAt (DateTime)
```

#### Product.cs
```csharp
// Właściwości:
// - Id (int, PK)
// - Name (string, wymagane)
// - Description (string, wymagane)
// - Price (decimal, wymagane, > 0)
// - ShopId (int, FK do Shop)
// - Shop (Shop, navigation property)
// - Orders (ICollection<Order>, navigation property)
// - CreatedAt (DateTime)
```

#### Order.cs
```csharp
// Właściwości:
// - Id (int, PK)
// - OrderNumber (string, unikalne, generowane)
// - ProductId (int, FK do Product)
// - Product (Product, navigation property)
// - BuyerId (string, FK do ApplicationUser)
// - Buyer (ApplicationUser, navigation property)
// - ShippingFirstName (string, wymagane)
// - ShippingLastName (string, wymagane)
// - ShippingStreet (string, wymagane)
// - ShippingHouseNumber (string, wymagane)
// - ShippingPostalCode (string, wymagane)
// - ShippingCity (string, wymagane)
// - CreatedAt (DateTime)
```

### 2. ArtisanMarket.Application - Warstwa Aplikacyjna

**Odpowiedzialność:** Zawiera logikę biznesową, serwisy aplikacyjne i interfejsy.

**Zależności:** ArtisanMarket.Domain

#### Services/
- **IShopService, ShopService** - logika biznesowa dla sklepów
- **IProductService, ProductService** - logika biznesowa dla produktów
- **IOrderService, OrderService** - logika biznesowa dla zamówień

#### DTOs/ (opcjonalnie)
- Obiekty transferu danych między warstwami

### 3. ArtisanMarket.Infrastructure - Warstwa Infrastruktury

**Odpowiedzialność:** Implementacja dostępu do danych, konfiguracja EF Core, migracje.

**Zależności:** ArtisanMarket.Domain, ArtisanMarket.Application

#### Data/
- **ApplicationDbContext** - kontekst EF Core z `DbSet<Shop>`, `DbSet<Product>`, `DbSet<Order>`
- **ApplicationUser** - użytkownik Identity
- **Configurations/** - konfiguracje EF Core dla encji (Fluent API)

**Wymagane zmiany:**
- Przeniesienie ApplicationDbContext z BlazorApp
- Przeniesienie ApplicationUser z BlazorApp
- Konfiguracja relacji w Configurations/
- Zmiana providera z SQL Server na PostgreSQL
- Walidacja reguł biznesowych (np. jeden sklep na użytkownika)

### 4. ArtisanMarket.BlazorApp - Warstwa Prezentacji

**Odpowiedzialność:** Interfejs użytkownika, komponenty Blazor, routing.

**Zależności:** ArtisanMarket.Application, ArtisanMarket.Infrastructure

#### Strony Blazor - Routing

```
/                           → Home (katalog sklepów)
/catalog                    → Catalog/Index (lista sklepów)
/shop/{id}                  → Shop/Details (produkty sklepu)
/order/create/{productId}   → Order/Create (formularz zamówienia)
/order/confirmation/{id}    → Order/Confirmation (potwierdzenie)

/seller                     → Seller/Dashboard (panel sprzedawcy)
/seller/shop/create         → Seller/Shop/Create
/seller/shop/edit           → Seller/Shop/Edit
/seller/products            → Seller/Products/Index
/seller/products/create     → Seller/Products/Create
/seller/products/edit/{id}  → Seller/Products/Edit
/seller/products/delete/{id}→ Seller/Products/Delete
/seller/orders              → Seller/Orders/Index

/buyer/orders               → Buyer/Orders/Index (historia zamówień)
```

### 5. ArtisanMarket.Tests - Projekt Testowy

**ShopService:**
- `CreateShopAsync(userId, shopName)` - tworzenie sklepu
- `GetShopByUserIdAsync(userId)` - pobranie sklepu użytkownika
- `GetShopByIdAsync(shopId)` - pobranie sklepu po ID
- `GetAllShopsAsync()` - lista wszystkich sklepów (publiczna)
- `UpdateShopAsync(shopId, shopName)` - aktualizacja sklepu
- Walidacja: jeden sklep na użytkownika, unikalna nazwa

**ProductService:**
- `CreateProductAsync(shopId, product)` - dodanie produktu
- `GetProductsByShopIdAsync(shopId)` - produkty sklepu
- `GetProductByIdAsync(productId)` - produkt po ID
- `UpdateProductAsync(productId, product)` - aktualizacja
- `DeleteProductAsync(productId)` - usunięcie (z walidacją zamówień)
- Walidacja: cena > 0, wszystkie pola wymagane

**OrderService:**
- `CreateOrderAsync(productId, buyerId, shippingData)` - utworzenie zamówienia
- `GetOrdersByBuyerIdAsync(buyerId)` - zamówienia kupującego
- `GetOrdersByShopIdAsync(shopId)` - zamówienia sklepu
- `GetOrderByIdAsync(orderId)` - zamówienie po ID
- Generowanie unikalnego numeru zamówienia

**Zależności:** Wszystkie projekty aplikacji

#### Struktura testów:

**Priorytetowe testy:**
- `ShopServiceTests`: tworzenie sklepu, walidacja "jeden sklep na użytkownika"
- `ProductServiceTests`: dodawanie produktów, walidacja ceny
- `OrderServiceTests`: składanie zamówień, generowanie numeru zamówienia
- Testy kontekstu: relacje między modelami

### 6. Zależności między Projektami

```
ArtisanMarket.BlazorApp
    ├── ArtisanMarket.Application
    │   └── ArtisanMarket.Domain
    └── ArtisanMarket.Infrastructure
        └── ArtisanMarket.Domain

ArtisanMarket.Tests
    ├── ArtisanMarket.BlazorApp
    ├── ArtisanMarket.Application
    └── ArtisanMarket.Infrastructure
```

### 7. Konfiguracja Projektów

**ArtisanMarket.Domain.csproj:**
- Brak zależności zewnętrznych (tylko .NET Standard/Core)
- Czyste encje domenowe

**ArtisanMarket.Application.csproj:**
- Referencja do: ArtisanMarket.Domain
- Brak zależności od EF Core

**ArtisanMarket.Infrastructure.csproj:**
- Referencje do: ArtisanMarket.Domain, ArtisanMarket.Application
- Pakiet: `Npgsql.EntityFrameworkCore.PostgreSQL`
- Pakiet: `Microsoft.AspNetCore.Identity.EntityFrameworkCore`
- Pakiet: `Microsoft.EntityFrameworkCore.Tools`

**ArtisanMarket.BlazorApp.csproj:**
- Referencje do: ArtisanMarket.Application, ArtisanMarket.Infrastructure
- Pakiet: `Microsoft.AspNetCore.Identity.EntityFrameworkCore` (dla komponentów Identity)

**Program.cs - wymagane zmiany:**
- Rejestracja serwisów z ArtisanMarket.Application
- Rejestracja ApplicationDbContext z ArtisanMarket.Infrastructure
- Konfiguracja PostgreSQL (connection string)
- Konfiguracja walidacji

**appsettings.json:**
- Connection string dla PostgreSQL

---

## Kolejność Implementacji (Sugerowana)

### Faza 1: Fundamenty - Struktura Projektów
1. ✅ Utworzenie projektu ArtisanMarket.Domain
2. ✅ Utworzenie projektu ArtisanMarket.Application
3. ✅ Utworzenie projektu ArtisanMarket.Infrastructure
4. ✅ Konfiguracja zależności między projektami

### Faza 2: Warstwa Domenowa
5. ✅ Utworzenie encji domenowych (Shop, Product, Order) w Domain
6. ✅ Definicja relacji i reguł biznesowych

### Faza 3: Warstwa Infrastruktury
7. ✅ Przeniesienie ApplicationDbContext do Infrastructure
8. ✅ Przeniesienie ApplicationUser do Infrastructure
9. ✅ Konfiguracja EF Core (Configurations/)
10. ✅ Zmiana providera z SQL Server na PostgreSQL
11. ✅ Utworzenie migracji

### Faza 4: Warstwa Aplikacyjna
12. ✅ Implementacja serwisów (ShopService, ProductService, OrderService)
13. ✅ Rejestracja serwisów w Program.cs

### Faza 5: Interfejs Użytkownika - Publiczny
14. ✅ Strona katalogu sklepów (Catalog/Index)
15. ✅ Strona sklepu z produktami (Shop/Details)
16. ✅ Formularz zamówienia (Order/Create)
17. ✅ Strona potwierdzenia (Order/Confirmation)

### Faza 6: Interfejs Użytkownika - Panel Sprzedawcy
18. ✅ Panel główny sprzedawcy (Seller/Dashboard)
19. ✅ Tworzenie/edycja sklepu (Seller/Shop)
20. ✅ Zarządzanie produktami (Seller/Products)
21. ✅ Lista zamówień (Seller/Orders)

### Faza 7: Interfejs Użytkownika - Panel Kupującego
22. ✅ Historia zamówień (Buyer/Orders)

### Faza 8: Testy i Optymalizacja
23. ✅ Testy jednostkowe dla serwisów (Application)
24. ✅ Testy infrastruktury (Infrastructure)
25. ✅ Walidacja i obsługa błędów

### Faza 9: Wdrożenie
26. ✅ Dockerfile
27. ✅ GitHub Actions workflow
28. ✅ Konfiguracja produkcyjna

---

## Uwagi Architektoniczne

1. **Clean Architecture - Separacja Warstw:**
   - **Domain** - niezależna od innych warstw, zawiera tylko encje i logikę domenową
   - **Application** - zależy tylko od Domain, zawiera logikę biznesową i interfejsy serwisów
   - **Infrastructure** - implementuje interfejsy z Application, zawiera dostęp do danych
   - **BlazorApp** - warstwa prezentacji, zależy od Application i Infrastructure

2. **Blazor Pages vs Components:**
   - Używamy Blazor Pages dla wszystkich widoków (zgodnie z regułami)
   - Komponenty Razor tylko dla współdzielonych elementów UI (ProductCard, ShopCard)

3. **Walidacja:**
   - Walidacja domenowa w encjach Domain
   - Walidacja biznesowa w serwisach Application
   - Walidacja po stronie klienta w formularzach Blazor (DataAnnotations)

4. **Bezpieczeństwo:**
   - Weryfikacja uprawnień w serwisach Application (np. tylko właściciel sklepu może edytować produkty)
   - Używanie `[Authorize]` dla stron wymagających logowania

5. **Relacje:**
   - ApplicationUser → Shop (1:1) - jeden użytkownik, jeden sklep
   - Shop → Product (1:N) - jeden sklep, wiele produktów
   - Product → Order (1:N) - jeden produkt, wiele zamówień
   - ApplicationUser → Order (1:N) - jeden użytkownik, wiele zamówień (jako kupujący)

6. **Numer zamówienia:**
   - Generowany automatycznie przy tworzeniu zamówienia w OrderService
   - Format: np. "ORD-{timestamp}-{random}" lub sekwencyjny

7. **Dependency Injection:**
   - Wszystkie serwisy rejestrowane w Program.cs
   - ApplicationDbContext rejestrowany w Infrastructure, używany przez serwisy Application

---

## Status Implementacji

- ✅ = Zaimplementowane
- ❌ = Wymaga implementacji
- 🔄 = Wymaga modyfikacji istniejącego kodu

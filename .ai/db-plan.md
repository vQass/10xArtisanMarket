# Schemat Bazy Danych - ArtisanMarket

## 1. Lista tabel z kolumnami, typami danych i ograniczeniami

### users
Tabela użytkowników.
- `id` UUID PRIMARY KEY - Identyfikator użytkownika (link do auth.users.id)
- `email` VARCHAR(255) NOT NULL
- `encrypted_password` NOT NULL
- `created_at` TIMESTAMPTZ NOT NULL DEFAULT NOW() - Data utworzenia konta

**Ograniczenia:**
- UNIQUE (email) - Email musi być unikalny

### shops
Tabela sklepów z relacją 1:0..1 do użytkowników.
- `id` UUID PRIMARY KEY DEFAULT gen_random_uuid() - Unikalny identyfikator sklepu
- `user_id` UUID NOT NULL - Referencja do użytkownika (sprzedawcy)
- `name` VARCHAR(100) NOT NULL - Nazwa sklepu
- `slug` VARCHAR(110) NOT NULL - Unikalny slug sklepu (dla URL-i)
- `description` TEXT NULL - Opcjonalny opis sklepu
- `contact_email` VARCHAR(256) NULL - Opcjonalny email kontaktowy
- `phone` VARCHAR(50) NULL - Opcjonalny numer telefonu
- `deleted_at` TIMESTAMPTZ NULL - Data miękkiego usunięcia

**Ograniczenia:**
- UNIQUE (user_id) - Jeden użytkownik = jeden sklep
- UNIQUE (slug) - Slug musi być unikalny
- FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE

### products
Tabela produktów należących do sklepów.
- `id` UUID PRIMARY KEY DEFAULT gen_random_uuid() - Unikalny identyfikator produktu
- `shop_id` UUID NOT NULL - Referencja do sklepu
- `name` VARCHAR(200) NOT NULL - Nazwa produktu
- `description` TEXT NULL - Opis produktu
- `price` MONEY NOT NULL - Cena produktu
- `is_active` BOOLEAN NOT NULL DEFAULT TRUE - Czy produkt jest aktywny/widoczny
- `deleted_at` TIMESTAMPTZ NULL - Data miękkiego usunięcia

**Ograniczenia:**
- FOREIGN KEY (shop_id) REFERENCES shops(id) ON DELETE CASCADE
- CHECK (price > 0::money) - Cena musi być większa od zera

### order_statuses
Tabela statusów zamówień.
- `id` INTEGER PRIMARY KEY - Identyfikator statusu
- `name` VARCHAR(50) NOT NULL - Nazwa statusu
- `description` TEXT NULL - Opis statusu

**Ograniczenia:**
- UNIQUE (name)

**Dane początkowe:**
- (1, 'złożone', 'Zamówienie zostało złożone przez kupującego')
- (2, 'potwierdzone', 'Zamówienie zostało potwierdzone przez sprzedawcę')
- (3, 'wysłane', 'Zamówienie zostało wysłane do kupującego')

### orders
Tabela główna zamówień z danymi adresowymi kupującego.
- `id` UUID PRIMARY KEY DEFAULT gen_random_uuid() - Unikalny identyfikator zamówienia
- `user_id` UUID NOT NULL - Referencja do kupującego
- `status_id` INTEGER NOT NULL DEFAULT 1 - Status zamówienia
- `shipping_first_name` VARCHAR(100) NOT NULL - Imię do wysyłki
- `shipping_last_name` VARCHAR(100) NOT NULL - Nazwisko do wysyłki
- `shipping_street` VARCHAR(200) NOT NULL - Ulica do wysyłki
- `shipping_house_number` VARCHAR(20) NOT NULL - Numer domu/mieszkania do wysyłki
- `shipping_postal_code` VARCHAR(10) NOT NULL - Kod pocztowy do wysyłki
- `shipping_city` VARCHAR(100) NOT NULL - Miasto do wysyłki
- `deleted_at` TIMESTAMPTZ NULL - Data miękkiego usunięcia

**Ograniczenia:**
- FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE
- FOREIGN KEY (status_id) REFERENCES order_statuses(id) ON DELETE RESTRICT

### order_items
Tabela pozycji zamówienia z snapshotem danych produktu.
- `id` UUID PRIMARY KEY DEFAULT gen_random_uuid() - Unikalny identyfikator pozycji
- `order_id` UUID NOT NULL - Referencja do zamówienia
- `product_id` UUID NOT NULL - Referencja do produktu
- `product_name` VARCHAR(200) NOT NULL - Snapshot nazwy produktu
- `product_price` MONEY NOT NULL - Snapshot ceny produktu
- `quantity` INTEGER NOT NULL DEFAULT 1 - Ilość zamówionego produktu

**Ograniczenia:**
- FOREIGN KEY (order_id) REFERENCES orders(id) ON DELETE CASCADE
- FOREIGN KEY (product_id) REFERENCES products(id) ON DELETE CASCADE
- CHECK (quantity > 0) - Ilość musi być większa od zera

## 2. Relacje między tabelami

- **users → shops**: Jeden-do-zero-lub-jednego (1:0..1)
  - Jeden użytkownik może mieć maksymalnie jeden sklep
  - Implementowane przez UNIQUE constraint na shops.user_id

- **shops → products**: Jeden-do-wielu (1:N)
  - Jeden sklep może mieć wiele produktów
  - Kaskadowe usuwanie produktów przy usunięciu sklepu

- **users → orders**: Jeden-do-wielu (1:N) - jako kupujący
  - Jeden użytkownik może złożyć wiele zamówień
  - Kaskadowe usuwanie zamówień przy usunięciu użytkownika

- **orders → order_items**: Jeden-do-wielu (1:N)
  - Jedno zamówienie może zawierać wiele pozycji
  - Kaskadowe usuwanie pozycji przy usunięciu zamówienia

- **products → order_items**: Jeden-do-wielu (1:N)
  - Jeden produkt może być zamówiony wiele razy w różnych zamówieniach
  - Kaskadowe usuwanie pozycji przy usunięciu produktu

- **order_statuses → orders**: Jeden-do-wielu (1:N)
  - Jeden status może być przypisany do wielu zamówień
  - RESTRICT na usunięcie statusu jeśli są zamówienia z tym statusem

## 3. Indeksy

### Indeksy wydajnościowe
- `idx_products_shop_id` ON products(shop_id) - Optymalizacja listowania produktów sklepu
- `idx_products_shop_id_is_active` ON products(shop_id, is_active) - Złożony indeks dla filtrowania aktywnych produktów sklepu
- `idx_orders_user_id` ON orders(user_id) - Optymalizacja wyświetlania zamówień kupującego
- `idx_order_items_order_id` ON order_items(order_id) - Optymalizacja wyświetlania pozycji zamówienia
- `idx_order_items_product_id` ON order_items(product_id) - Optymalizacja zapytań po produkcie
- `idx_shops_slug` ON shops(slug) - Optymalizacja wyszukiwania sklepu po slug
- `idx_shops_user_id` ON shops(user_id) - Optymalizacja sprawdzania czy użytkownik ma sklep

### Indeksy unikalności (częściowo pokryte przez constraints)
- `idx_users_id` ON users(id) - Link do auth.users
- `idx_users_email` ON users(email) - Unikalność email
- `idx_shops_user_id` ON shops(user_id) - Unikalność relacji użytkownik-sklep
- `idx_shops_slug` ON shops(slug) - Unikalność slug sklepów
- `idx_order_statuses_name` ON order_statuses(name) - Unikalność nazw statusów

## 4. Zasady PostgreSQL (RLS - Row Level Security)

Row Level Security będzie implementowane po stronie aplikacji zgodnie z decyzjami z sesji planowania. Baza danych nie zawiera polityk RLS, ponieważ izolacja danych między sprzedawcami i kupującymi jest obsługiwana przez logikę aplikacji.

## 5. Dodatkowe uwagi i wyjaśnienia

### Decyzje projektowe

- **Soft delete**: Implementowane przez pole `deleted_at` typu TIMESTAMPTZ we wszystkich encjach głównych (shops, products, orders). Pozwala na zachowanie integralności referencyjnej i audytu historycznego.

- **Snapshot danych produktu**: W tabeli order_items przechowywane są `product_name` i `product_price` jako snapshot w momencie składania zamówienia. Zapewnia historyczną spójność danych nawet po zmianie ceny lub nazwy produktu.

- **Typ MONEY**: Użyty dla cen produktów i snapshotów cen w zamówieniach. Zapewnia dokładne obliczenia finansowe bez problemów z precyzją zmiennoprzecinkową.

- **Unikalny slug sklepów**: Pole `slug` w tabeli shops jest unikalne i służy do generowania czytelnych URL-i. Aplikacja powinna automatycznie generować slug z nazwy sklepu z sufiksami numerycznymi w przypadku konfliktów.

- **Dane adresowe w zamówieniach**: Wszystkie dane adresowe kupującego są przechowywane bezpośrednio w tabeli orders dla kompletności danych zamówienia i ułatwienia audytu. Dane adresowe są wspólne dla wszystkich pozycji w zamówieniu.

- **Wielopozycyjne zamówienia**: Zamówienie może zawierać wiele produktów poprzez tabelę order_items. Każda pozycja przechowuje snapshot produktu i ilość. Pozwala to na złożenie zamówienia z wieloma produktami w jednej transakcji.

- **Statusy zamówień**: Tabela order_statuses zawiera podstawowe statusy (złożone, potwierdzone, wysłane) przygotowujące na przyszłe rozszerzenia, choć MVP nie wymaga zaawansowanego zarządzania statusami.

- **Pole is_active w produktach**: Umożliwia tymczasowe ukrywanie produktów bez ich usuwania, co jest przydatne dla zarządzania dostępnością.

- **Pole quantity w order_items**: Pozwala na zamówienie większej ilości tego samego produktu w jednej pozycji zamówienia.

### Uwagi dotyczące bezpieczeństwa
- Wszystkie operacje na danych powinny być wykonywane z odpowiednią weryfikacją uprawnień po stronie aplikacji
- Izolacja danych między sprzedawcami i kupującymi jest obsługiwana przez logikę biznesową aplikacji

### Uwagi dotyczące skalowalności
- Schemat jest znormalizowany do 3NF z uzasadnioną denormalizacją dla snapshotów zamówień
- Indeksy są strategicznie rozmieszczone dla optymalizacji najczęściej wykonywanych zapytań
- UUID jako klucze główne zapewniają dobrą dystrybucję i unikają problemów z sekwencjami w środowiskach rozproszonych
- Struktura wielopozycyjnych zamówień jest skalowalna i pozwala na łatwe rozszerzenie o dodatkowe funkcjonalności

### Przygotowanie na przyszłe rozszerzenia
- Tabela order_statuses umożliwia łatwe dodawanie nowych statusów bez zmian schematu
- Opcjonalne pola w tabeli shops (description, contact_email, phone) przygotowują na przyszłe funkcjonalności
- Struktura order_items pozwala na łatwe dodanie dodatkowych pól jak rabaty, opcje produktu itp.
- Brak created_at/updated_at zgodnie z decyzjami z sesji planowania - aplikacja może implementować własne mechanizmy śledzenia zmian jeśli będzie to potrzebne
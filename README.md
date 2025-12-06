# ArtisanMarket 🛍️

> Platforma marketplace dla twórców rękodzieł - proste rozwiązanie do sprzedaży produktów rzemieślniczych online

[![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?style=flat&logo=.net)](https://dotnet.microsoft.com/)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-15+-336791?style=flat&logo=postgresql)](https://www.postgresql.org/)
[![Docker](https://img.shields.io/badge/Docker-Ready-2496ED?style=flat&logo=docker)](https://www.docker.com/)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE.txt)

## 📋 O Projekcie

ArtisanMarket to dedykowana platforma e-commerce zaprojektowana z myślą o twórcach rękodzieł. W wersji MVP skupia się na dostarczeniu prostego, szybkiego i darmowego narzędzia do założenia sklepu online, wystawienia produktów i przyjmowania zamówień z płatnością przy odbiorze.

### 🎯 Główna Wartość

- **Dla Sprzedawców**: Łatwe założenie sklepu bez kosztów i wiedzy technicznej
- **Dla Kupujących**: Dostęp do unikalnego katalogu produktów rzemieślniczych
- **Dla Platformy**: Prosty model biznesowy bez integracji płatniczych

## 🏗️ Architektura

Projekt implementuje **Clean Architecture** z podziałem na warstwy:

```
ArtisanMarket/
├── 🏛️ Domain/           # Encje domenowe i logika biznesowa
├── 🔧 Application/      # Serwisy biznesowe i interfejsy
├── 💾 Infrastructure/   # Dostęp do danych i EF Core
├── 🎨 BlazorApp/        # Interfejs użytkownika (Blazor Pages)
└── 🧪 Tests/           # Testy jednostkowe (xUnit)
```

## 🛠️ Technologie

### Backend & Framework
- **.NET 9** - Nowoczesna platforma .NET
- **Blazor Pages** - Server-side rendering dla interaktywnego UI
- **ASP.NET Core Identity** - Uwierzytelnianie i zarządzanie użytkownikami

### Baza Danych
- **PostgreSQL** - Relacyjna baza danych
- **Entity Framework Core** - ORM z Code-First approach
- **Npgsql** - Provider PostgreSQL dla EF Core

### Testowanie & Jakość
- **xUnit** - Framework do testów jednostkowych
- **FluentAssertions** - Czytelne asercje w testach

### Wdrożenie
- **Docker** - Konteneryzacja aplikacji
- **GitHub Actions** - CI/CD pipeline
- **DigitalOcean** - Hosting w chmurze

## ✨ Funkcjonalności MVP

### 👤 Dla Wszystkich Użytkowników
- ✅ Rejestracja i logowanie konta
- ✅ Przeglądanie katalogu sklepów
- ✅ Przeglądanie produktów w sklepach

### 🛒 Dla Sprzedawców
- ✅ Tworzenie jednego sklepu na konto
- ✅ Dodawanie, edycja i usuwanie produktów
- ✅ Zarządzanie zamówieniami (lista z danymi wysyłkowymi)
- ✅ Panel sprzedawcy z pełnym overview

### 🛍️ Dla Kupujących
- ✅ Składanie zamówień z formularzem adresowym
- ✅ Potwierdzenie zamówienia z numerem referencyjnym
- ✅ Historia własnych zamówień

### 🚫 Nie W Zakresie MVP
- Integracje płatnicze (płatność przy odbiorze)
- Zdjęcia produktów
- Warianty produktów (rozmiary, kolory)
- Recenzje i oceny
- Zaawansowane wyszukiwanie i filtry
- Statusy zamówień
- Anulowanie zamówień przez aplikację

## 🚀 Uruchomienie Projektu

### Wymagania wstępne
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [PostgreSQL 15+](https://www.postgresql.org/download/)
- [Docker](https://www.docker.com/get-started) (opcjonalnie)

### 1. Klonowanie repozytorium
```bash
git clone https://github.com/your-username/ArtisanMarket.git
cd ArtisanMarket
```

### 2. Konfiguracja bazy danych
```bash
# Utwórz bazę danych PostgreSQL
createdb artisanmarket_db

# Zaktualizuj connection string w appsettings.json
```

### 3. Uruchomienie aplikacji
```bash
# Przywróć zależności
dotnet restore

# Uruchom migracje
dotnet ef database update

# Uruchom aplikację
dotnet run --project ArtisanMarket.BlazorApp
```

Aplikacja będzie dostępna pod adresem: `https://localhost:5001`

### 4. Uruchomienie z Docker (opcjonalnie)
```bash
# Zbuduj i uruchom kontenery
docker-compose up --build
```

## 🧪 Testowanie

```bash
# Uruchom wszystkie testy
dotnet test

# Uruchom testy z pokryciem
dotnet test --collect:"XPlat Code Coverage"
```

## 📁 Struktura Projektu

```
ArtisanMarket/
├── .ai/                          # Dokumentacja projektu
├── .github/workflows/           # CI/CD pipeline
├── docker/                      # Konfiguracja Docker
├── ArtisanMarket.Domain/        # Warstwa domenowa
├── ArtisanMarket.Application/   # Warstwa aplikacyjna
├── ArtisanMarket.Infrastructure/ # Warstwa infrastruktury
├── ArtisanMarket.BlazorApp/     # Warstwa prezentacji
├── ArtisanMarket.Tests/         # Testy jednostkowe
├── ArtisanMarket.sln           # Rozwiązanie Visual Studio
├── .gitignore                  # Ignorowane pliki
├── LICENSE.txt                 # Licencja MIT
└── README.md                   # Ten plik
```

### Standardy Kodowania
- Polskie nazwy w interfejsie użytkownika
- Angielskie nazwy w kodzie
- Testy dla każdej nowej funkcjonalności
- Dokumentacja kluczowych decyzji architektonicznych
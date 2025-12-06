# Stos Technologiczny Projektu ArtisanMarket

Poniższy dokument opisuje kompletny stos technologiczny wybrany do budowy i wdrożenia aplikacji ArtisanMarket.

## Technologie Aplikacji

- **Backend:** **.NET 9**
  - Wybrany ze względu na wysoką wydajność, bezpieczeństwo, skalowalność oraz spójność ekosystemu.

- **Frontend:** **Blazor Pages**
  - Umożliwia tworzenie interaktywnego interfejsu użytkownika przy użyciu języka C#, co przyspiesza rozwój i upraszcza architekturę aplikacji.

- **Baza Danych:** **PostgreSQL**
  - Zaawansowana, darmowa i otwartoźródłowa relacyjna baza danych, znana ze swojej niezawodności, skalowalności i bogatego zestawu funkcji.

- **ORM:** **Entity Framework Core**
  - Ułatwia mapowanie obiektowo-relacyjne, co przyspiesza rozwój aplikacji i upraszcza zarządzanie danymi.

- **Testowanie:** **xUnit**
  - Framework do testów jednostkowych w .NET, co zapewnia łatwą integrację z procesem CI/CD.

## Infrastruktura i Wdrożenie (CI/CD)

- **System CI/CD:** **GitHub Actions**
  - Zapewni automatyzację procesów budowania, testowania i wdrażania aplikacji bezpośrednio z repozytorium kodu.

- **Hosting:** **DigitalOcean**
  - Platforma chmurowa wybrana do hostowania aplikacji.

- **Konteneryzacja:** **Docker**
  - Aplikacja będzie uruchamiana w kontenerach Docker, co zapewni spójność środowisk i ułatwi zarządzanie wdrożeniami na platformie DigitalOcean.

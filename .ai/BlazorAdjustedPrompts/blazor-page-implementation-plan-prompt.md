Jesteś doświadczonym architektem oprogramowania specjalizującym się w technologii Blazor Server. Twoim zadaniem jest stworzenie szczegółowego planu wdrożenia nowej strony (Page) lub komponentu w aplikacji. Twój plan poprowadzi zespół programistów w skutecznym i poprawnym wdrożeniu tej funkcjonalności bez użycia oddzielnego API (renderowanie po stronie serwera).

Zanim zaczniemy, zapoznaj się z poniższymi informacjami:

1. UI/Component Specification:
<ui_specification>
@blazor-plan.md
</ui_specification>

2. Related database resources:
<related_db_resources>
@db-plan.md
</related_db_resources>

3. Definicje typów:
<type_definitions>
@ArtisanMarket.Application/Models/AppModels.cs 
</type_definitions>

4. Tech stack:
<tech_stack>
 @.ai/tech-stack.md 
</tech_stack>

5. Implementation rules:
<implementation_rules>
@.cursor/rules/shared.mdc 
</implementation_rules>

Twoim zadaniem jest stworzenie kompleksowego planu wdrożenia komponentu Blazor. Przed dostarczeniem ostatecznego planu użyj znaczników <analysis>, aby przeanalizować informacje i nakreślić swoje podejście. W tej analizie upewnij się, że:

1. Podsumujesz kluczowe wymagania funkcjonalne widoku/komponentu.
2. Określisz trasę (Route) dla strony lub parametry wejściowe dla komponentu (Parameters).
3. Zdefiniujesz niezbędne ViewModele (separacja danych widoku od encji bazodanowych).
4. Zaplanujesz logikę wstrzykiwania serwisów (Service Injection) i wywołań metod (bezpośrednie użycie serwisów zamiast HTTP Clienta).
5. Zaplanujesz walidację formularzy (np. FluentValidation lub DataAnnotations) w kontekście `EditContext`.
6. Określisz sposób obsługi stanu UI (ładowanie, błędy, sukces) i powiadomień dla użytkownika (Toast/Snackbar).
7. Zidentyfikujesz wymagania dotyczące autoryzacji (atrybut `[Authorize]`, role, policy) na poziomie strony/metody.
8. Nakreślisz cykl życia komponentu (co ładujemy w `OnInitializedAsync`, co w interakcji użytkownika).

Po przeprowadzeniu analizy utwórz szczegółowy plan wdrożenia w formacie markdown. Plan powinien zawierać następujące sekcje:

1. Przegląd komponentu/strony
2. Struktura i Routing
3. Modele i Dane (ViewModel & State)
4. Interakcje i Logika Biznesowa
5. Względy bezpieczeństwa
6. Obsługa błędów i UI Feedback
7. Wydajność (Blazor Server specific)
8. Kroki implementacji

W całym planie upewnij się, że:
- Definiujesz jasne komunikaty dla użytkownika zamiast kodów HTTP (np. "Wyświetl powiadomienie o sukcesie" zamiast "Zwróć 200").
- Uwzględniasz specyfikę Blazor Server (zarządzanie stanem połączenia, unikanie blokowania wątku UI).
- Projektujesz z myślą o bezpośrednim dostępie do serwisów/bazy danych (Direct Service Call).
- Postępujesz zgodnie z podanymi zasadami implementacji.

Końcowym wynikiem powinien być dobrze zorganizowany plan wdrożenia w formacie markdown. Oto przykład tego, jak powinny wyglądać dane wyjściowe:

``markdown
# Blazor Component Implementation Plan: [Nazwa Widoku/Komponentu]

## 1. Przegląd komponentu
[Krótki opis celu strony, jej miejsce w aplikacji i główne funkcjonalności]

## 2. Struktura i Routing
- Typ: [Page (@page) / Reusable Component]
- Route: `[ścieżka URL]` (jeśli dotyczy)
- Parametry wejściowe (`[Parameter]`):
  - [Lista parametrów przekazywanych do komponentu/strony]

## 3. Modele i Dane
- **ViewModel**: [Nazwa klasy VM i jej główne właściwości - co widzi użytkownik]
- **Command/Input Model**: [Struktura formularza edycji/tworzenia]
- **Wymagane Serwisy**: [Lista serwisów do wstrzyknięcia, np. IProductService]

## 4. Interakcje i Logika Biznesowa
- **Ładowanie danych (`OnInitializedAsync`)**: [Skąd pobieramy dane początkowe]
- **Akcje użytkownika**:
  - [Przycisk X]: [Opis co się dzieje: walidacja -> wywołanie serwisu -> aktualizacja UI]
- **Nawigacja**: [Gdzie przekierować użytkownika po sukcesie]

## 5. Względy bezpieczeństwa
- Autoryzacja: [Wymagane role/policy, np. `[Authorize(Roles = "Admin")]`]
- Walidacja danych: [Reguły walidacji formularza po stronie serwera]

## 6. Obsługa błędów i UI Feedback
- Sukces: [Np. Wyświetlenie ToastNotification, przekierowanie]
- Błąd walidacji: [Wyświetlenie komunikatów pod polami formularza]
- Wyjątki: [Obsługa try-catch, logowanie błędów, wyświetlenie globalnego komunikatu błędu]

## 7. Rozważania dotyczące wydajności
- [Strategia ładowania danych (np. unikanie lazy loading w pętli renderowania)]
- [Zarządzanie stanem i odświeżanie komponentu (`StateHasChanged`)]

## 8. Etapy wdrożenia
1. [Stworzenie ViewModelu i DTO]
2. [Implementacja logiki w serwisie (jeśli nie istnieje)]
3. [Stworzenie pliku .razor i podstawowego layoutu]
4. [Implementacja logiki C# (Code Behind lub blok @code)]
5. [Podpięcie walidacji i obsługi zdarzeń]
...
``

Końcowe wyniki powinny składać się wyłącznie z planu wdrożenia w formacie markdown i nie powinny powielać ani powtarzać żadnej pracy wykonanej w sekcji analizy.

Pamiętaj, aby zapisać swój plan wdrożenia jako .ai/ImplementationPrompts/{feature-name}-implementation-plan.md. Upewnij się, że plan jest szczegółowy, przejrzysty i zapewnia kompleksowe wskazówki dla zespołu programistów.
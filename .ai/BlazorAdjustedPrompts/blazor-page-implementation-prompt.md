Twoim zadaniem jest wdrożenie funkcjonalności w Blazor (Razor Component/Page) w oparciu o dostarczony plan implementacji. Celem jest stworzenie spójnej, dobrze zorganizowanej i produkcyjnej implementacji obejmującej UI, logikę, walidację, obsługę błędów oraz pełen przepływ użytkownika, zgodnie z krokami określonymi w planie.

Najpierw przeanalizuj załączony plan implementacji:

<implementation_plan>
{{implementation-plan}} ← dodaj referencję do planu implementacji (np. @feature-implementation-plan.md)
</implementation_plan>

<types> {{types}} ← dodaj referencje do definicji typów (np. @types) </types>

<implementation_rules>
{{frontend-backend-rules}} ← dodaj referencje do reguł projektowych (np. @shared.mdc, @frontend.mdc, @blazor.mdc)
</implementation_rules>

<implementation_approach>
Realizuj maksymalnie 3 kroki planu implementacji, krótko podsumuj co zrobiłeś i opisz plan na kolejne 3 działania. W tym miejscu zatrzymaj pracę i czekaj na mój feedback.
</implementation_approach>

1. Analiza planu wdrożenia

Przeanalizuj szczegółowo dostarczone założenia:
określ, czy implementacja dotyczy: Razor Page, Razor Component, partial class code-behind, usługi, modelu lub ich kombinacji
zdecyduj o strukturze UI (formularze, elementy interaktywne, listy, layout)
określ wszystkie potrzebne parametry wejściowe, powiązania z danymi (@bind), eventy (OnClick, OnValidSubmit, itp.)
zrozum wymagania dotyczące logiki biznesowej, przetwarzania danych, interakcji z backendem / DI
rozpoznaj wymagania dotyczące walidacji (DataAnnotations/FluentValidation) oraz obsługi błędów (komunikaty w UI)
określ wymagane modele ViewModel, DTO lub klasy pomocnicze

2. Rozpocznij implementację
utwórz projektową strukturę plików (component, code-behind, modele)
zdefiniuj UI w pliku .razor oraz logikę w .razor.cs lub inline – zgodnie z regułami
skonfiguruj parametry i powiązania ([Parameter], EventCallback, @bind-Value)
zaimplementuj logikę formularzy, walidacji i obsługi interakcji użytkownika
odwzoruj dokładnie kolejne kroki logiki biznesowej z planu
dodaj odpowiednią obsługę błędów (np. walidacja, komunikaty, stany UI)
przygotuj dane wyjściowe / aktualizacje UI zgodnie z oczekiwanym rezultatem

3. Walidacja i obsługa błędów
zaimplementuj precyzyjną walidację wszystkich danych wejściowych
zastosuj EditForm + ValidationSummary lub niestandardowe mechanizmy walidacji
przewiduj możliwe wyjątki i błędy (np. brak danych, null, błędy usług)
zapewnij czytelne komunikaty w UI
zadbaj o stabilne i przewidywalne stany komponentu (loading, error, empty, success)

4. Testowanie i edge case'y
określ scenariusze skrajne, które wymagają pokrycia testami lub ręcznym sprawdzeniem
uwzględnij błędne dane użytkownika, błędy usług, brak danych, nieoczekiwane interakcje UI
upewnij się, że wszystkie elementy planu są obsłużone przez implementację

5. Dokumentacja
dodaj komentarze tam, gdzie logika jest złożona i wymaga uzasadnienia
udokumentuj główny komponent, jego parametry i funkcje pomocnicze
przejrzyście opisuj nietypowe decyzje projektowe
Po zakończeniu implementacji upewnij się, że kod zawiera wszystkie wymagane importy, definicje komponentów, powiązania danych, dependency injection oraz wszelkie klasy pomocnicze niezbędne do realizacji funkcjonalności.

Jeśli natrafisz na niejasności lub musisz przyjąć założenia – przedstaw je przed rozpoczęciem implementacji.
# Dokument wymagań produktu (PRD) - ArtisanMarket
## 1. Przegląd produktu
ArtisanMarket to platforma e-commerce typu marketplace, zaprojektowana z myślą o twórcach rękodzieł. Celem projektu w wersji MVP (Minimum Viable Product) jest dostarczenie prostego, szybkiego i darmowego narzędzia, które umożliwia sprzedawcom łatwe założenie własnego sklepu online, wystawienie produktów i przyjmowanie zamówień. Kupujący zyskują dostęp do unikalnego katalogu produktów rzemieślniczych. Model biznesowy MVP opiera się na uproszczonym procesie zamówień z płatnością przy odbiorze, eliminując złożoność integracji płatniczych i koncentrując się na podstawowej funkcjonalności handlowej.

## 2. Problem użytkownika
Twórcy rękodzieła napotykają znaczące bariery przy próbie sprzedaży swoich produktów online. Istniejące platformy marketplace są często zbyt ogólne, co sprawia, że ich unikalne produkty giną w tłumie masowej produkcji. Alternatywnie, są one kosztowne, pobierając wysokie prowizje lub opłaty abonamentowe, które są nieopłacalne dla małych działalności. Z kolei założenie własnego, niezależnego sklepu internetowego wiąże się z dużymi kosztami początkowymi, wymaga wiedzy technicznej i jest procesem czasochłonnym. Brakuje prostego, dedykowanego rozwiązania, które pozwoliłoby rzemieślnikom na szybkie uruchomienie sprzedaży bez ponoszenia kosztów i bez skomplikowanych procedur.

## 3. Wymagania funkcjonalne
### Moduł Uwierzytelniania i Konta
- Użytkownicy (zarówno sprzedawcy, jak i kupujący) mogą zarejestrować konto w systemie, podając jedynie adres e-mail i hasło.
- Zarejestrowani użytkownicy mogą logować się do swojego konta.
- Każde konto użytkownika jest uniwersalne, ale jedno konto może być powiązane tylko z jednym sklepem.

### Moduł Sprzedawcy
- Po zalogowaniu, użytkownik może utworzyć jeden sklep powiązany ze swoim kontem.
- Sprzedawca może dodawać produkty do swojego sklepu, podając ich nazwę, opis i cenę.
- Sprzedawca ma dostęp do panelu, w którym widoczna jest lista wszystkich otrzymanych zamówień.
- W panelu sprzedawcy dostępne są szczegóły każdego zamówienia, w tym dane produktu oraz dane adresowe kupującego do wysyłki.
- Sprzedawca może edytować i usuwać produkty w swoim sklepie.

### Moduł Kupującego
- Użytkownik (zarówno zalogowany, jak i niezalogowany) może przeglądać publiczny katalog sklepów i produktów.
- Użytkownik może złożyć zamówienie na wybrany produkt. Proces ten wymaga rejestracji/logowania.
- Podczas składania zamówienia kupujący podaje swoje dane adresowe niezbędne do wysyłki.
- Po złożeniu zamówienia kupujący otrzymuje ekran z potwierdzeniem oraz informacją, że płatność zostanie zrealizowana przy odbiorze przesyłki.
- Zalogowany kupujący ma dostęp do panelu z historią swoich zamówień.

### Moduł Katalogu i Zamówień
- Aplikacja posiada publiczną stronę z listą wszystkich sklepów.
- Każdy sklep ma swoją dedykowaną stronę, na której wyświetlana jest lista jego produktów.
- Proces zamówienia nie obejmuje integracji z systemami płatności online.
- Historia zamówień jest przechowywana i dostępna zarówno dla sprzedawcy, jak i kupującego.

## 4. Granice produktu
Poniższe funkcjonalności celowo NIE wchodzą w zakres wersji MVP, aby zapewnić szybkie dostarczenie kluczowej wartości produktu:
- Integracje z systemami płatności online (np. Przelewy24, Stripe, PayPal).
- Systemy prowizji i rozliczeń finansowych na platformie.
- Możliwość dodawania zdjęć do produktów.
- Zaawansowane opcje produktów (np. warianty, rozmiary, kolory).
- Systemy ocen i recenzji produktów lub sklepów.
- Mechanizmy moderacji treści, zgłaszania nadużyć.
- Zaawansowane narzędzia marketingowe (np. kody rabatowe, programy lojalnościowe).
- Funkcje społecznościowe (np. komentarze, wiadomości prywatne między użytkownikami).
- Wyszukiwarka oraz systemy filtrowania i sortowania w katalogu.
- Statusy zamówień (np. "w trakcie realizacji", "wysłane").
- Proces anulowania zamówienia z poziomu aplikacji.
- Dedykowane aplikacje mobilne (iOS/Android) lub desktopowe.

## 5. Historyjki użytkowników
### Uwierzytelnianie i Zarządzanie Kontem
- ID: US-001
- Tytuł: Rejestracja nowego użytkownika
- Opis: Jako nowy użytkownik, chcę móc założyć konto w serwisie przy użyciu mojego adresu e-mail i hasła, aby móc korzystać z funkcji platformy, takich jak założenie sklepu lub złożenie zamówienia.
- Kryteria akceptacji:
  1. Formularz rejestracji zawiera pola na adres e-mail i hasło.
  2. System waliduje, czy podany adres e-mail jest w poprawnym formacie.
  3. System sprawdza, czy adres e-mail nie jest już zarejestrowany.
  4. Hasło musi mieć co najmniej 8 znaków.
  5. Po pomyślnej rejestracji użytkownik jest automatycznie zalogowany i przekierowany na stronę główną.

- ID: US-002
- Tytuł: Logowanie użytkownika
- Opis: Jako zarejestrowany użytkownik, chcę móc zalogować się na swoje konto przy użyciu mojego adresu e-mail i hasła, aby uzyskać dostęp do mojego panelu sprzedawcy lub historii zakupów.
- Kryteria akceptacji:
  1. Formularz logowania zawiera pola na adres e-mail i hasło.
  2. Po podaniu poprawnych danych użytkownik zostaje zalogowany.
  3. Po podaniu niepoprawnych danych użytkownik widzi komunikat o błędzie.
  4. Po zalogowaniu użytkownik jest przekierowywany do swojego panelu.

### Funkcjonalności Sprzedawcy
- ID: US-003
- Tytuł: Tworzenie sklepu przez sprzedawcę
- Opis: Jako zalogowany użytkownik, chcę móc utworzyć swój własny sklep, aby móc dodawać do niego produkty i rozpocząć sprzedaż.
- Kryteria akceptacji:
  1. Użytkownik, który nie ma jeszcze sklepu, widzi opcję "Utwórz sklep".
  2. Proces tworzenia sklepu wymaga podania jego unikalnej nazwy.
  3. System sprawdza, czy nazwa sklepu nie jest już zajęta.
  4. Jedno konto użytkownika może być powiązane tylko z jednym sklepem. Po utworzeniu sklepu opcja "Utwórz sklep" znika.
  5. Nowo utworzony sklep jest od razu widoczny w publicznym katalogu.

- ID: US-004
- Tytuł: Dodawanie produktu do sklepu
- Opis: Jako sprzedawca, chcę móc dodać nowy produkt do mojego sklepu, podając jego nazwę, opis i cenę, aby kupujący mogli go znaleźć i zamówić.
- Kryteria akceptacji:
  1. W panelu sprzedawcy dostępny jest formularz dodawania produktu.
  2. Formularz zawiera pola: "Nazwa produktu", "Opis", "Cena".
  3. Wszystkie pola są wymagane.
  4. Pole "Cena" akceptuje tylko wartości numeryczne, większe od zera.
  5. Po poprawnym dodaniu, produkt jest od razu widoczny na stronie sklepu.

- ID: US-005
- Tytuł: Edycja produktu w sklepie
- Opis: Jako sprzedawca, chcę móc edytować istniejący produkt w moim sklepie, aby zaktualizować jego nazwę, opis lub cenę.
- Kryteria akceptacji:
  1. Przy każdym produkcie na liście w panelu sprzedawcy znajduje się przycisk "Edytuj".
  2. Po kliknięciu przycisku sprzedawca jest przenoszony do formularza edycji, który jest wypełniony aktualnymi danymi produktu.
  3. Walidacja danych jest taka sama jak przy dodawaniu produktu.
  4. Zaktualizowane dane produktu są widoczne na stronie sklepu.

- ID: US-006
- Tytuł: Usuwanie produktu ze sklepu
- Opis: Jako sprzedawca, chcę móc usunąć produkt z mojego sklepu, jeśli nie jest już dostępny w mojej ofercie.
- Kryteria akceptacji:
  1. Przy każdym produkcie na liście w panelu sprzedawcy znajduje się przycisk "Usuń".
  2. System prosi o potwierdzenie przed usunięciem produktu.
  3. Po potwierdzeniu produkt jest usuwany ze sklepu i nie jest już widoczny publicznie.
  4. Nie można usunąć produktu, który jest częścią jakiegokolwiek złożonego zamówienia.

- ID: US-007
- Tytuł: Przeglądanie otrzymanych zamówień
- Opis: Jako sprzedawca, chcę mieć dostęp do panelu z listą wszystkich zamówień złożonych w moim sklepie, aby móc je zrealizować.
- Kryteria akceptacji:
  1. W panelu sprzedawcy znajduje się sekcja "Zamówienia".
  2. Lista zamówień zawiera podstawowe informacje: numer zamówienia, datę złożenia, nazwę zamówionego produktu.
  3. Kliknięcie na zamówienie na liście rozwija jego szczegóły: pełne dane produktu oraz dane do wysyłki podane przez kupującego (imię, nazwisko, adres).

### Funkcjonalności Kupującego i Publiczne
- ID: US-008
- Tytuł: Przeglądanie publicznego katalogu sklepów
- Opis: Jako gość lub zalogowany użytkownik, chcę móc przeglądać listę wszystkich dostępnych sklepów na platformie, aby odkrywać nowych twórców.
- Kryteria akceptacji:
  1. Strona główna lub dedykowana podstrona "Katalog" wyświetla listę wszystkich aktywnych sklepów.
  2. Każdy element na liście zawiera nazwę sklepu i jest linkiem do strony tego sklepu.
  3. Katalog jest widoczny dla wszystkich użytkowników, bez konieczności logowania.

- ID: US-009
- Tytuł: Przeglądanie produktów w sklepie
- Opis: Jako gość lub zalogowany użytkownik, chcę móc wejść na stronę wybranego sklepu i zobaczyć wszystkie produkty, które oferuje.
- Kryteria akceptacji:
  1. Strona sklepu wyświetla jego nazwę.
  2. Poniżej nazwy znajduje się lista produktów, a każdy z nich ma widoczną nazwę, opis i cenę.
  3. Przy każdym produkcie znajduje się przycisk "Zamów".

- ID: US-010
- Tytuł: Składanie zamówienia
- Opis: Jako zalogowany kupujący, chcę móc złożyć zamówienie na wybrany produkt, podając dane do wysyłki, aby sfinalizować zakup.
- Kryteria akceptacji:
  1. Kliknięcie przycisku "Zamów" przy produkcie przenosi zalogowanego użytkownika do formularza zamówienia.
  2. Jeśli użytkownik nie jest zalogowany, jest proszony o zalogowanie się lub rejestrację.
  3. Formularz zamówienia wymaga podania danych do wysyłki (imię, nazwisko, ulica, numer domu/mieszkania, kod pocztowy, miasto).
  4. Po złożeniu zamówienia użytkownik widzi ekran potwierdzenia z numerem zamówienia i jasną informacją: "Dziękujemy za zamówienie. Sprzedawca wkrótce je wyśle. Zapłacisz za nie gotówką przy odbiorze przesyłki."
  5. Zamówienie jest zapisywane i staje się widoczne w panelu sprzedawcy oraz w historii zamówień kupującego.

- ID: US-011
- Tytuł: Przeglądanie historii swoich zamówień
- Opis: Jako kupujący, chcę mieć dostęp do panelu z historią wszystkich moich zamówień, aby móc śledzić swoje zakupy.
- Kryteria akceptacji:
  1. W panelu użytkownika dostępna jest sekcja "Moje zamówienia".
  2. Lista zawiera wszystkie złożone przez użytkownika zamówienia.
  3. Każdy element na liście zawiera numer zamówienia, datę, nazwę produktu i jego cenę.

## 6. Metryki sukcesu
Sukces projektu ArtisanMarket MVP będzie oceniany na podstawie osiągnięcia następujących kluczowych celów, które odzwierciedlają rozwiązanie podstawowych problemów użytkowników:

1.  Sprzedawcy są w stanie samodzielnie i bezproblemowo założyć sklep oraz dodać do niego swoje pierwsze produkty w ramach jednej, krótkiej sesji. Proces ten jest intuicyjny i nie wymaga wiedzy technicznej.

2.  Aktywne sklepy na platformie są uzupełnione produktami. Celem jest, aby każdy sklep posiadał przynajmniej kilka gotowych do zamówienia produktów, co świadczy o zaangażowaniu sprzedawców i wartości katalogu dla kupujących.

3.  Kupujący mogą z łatwością odnaleźć produkty i złożyć zamówienie. Proces zakupowy jest prosty, a po jego zakończeniu kupujący otrzymuje jasne i zrozumiałe potwierdzenie z informacją o dalszych krokach i płatności przy odbiorze.

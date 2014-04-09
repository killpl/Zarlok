
	Projekt ŻARŁOK ma za zadanie stworzenie systemu ułatwiającego odchudzanie 
	i kontrolę nad ilością spożywanych kalorii za pomocą łatwej w obsłudze aplikacji na telefon 
	z systemem Android, strony internetowej pozwalającej na podgląd historii swoich wpisów 
	i porównywanie ze znajomymi, a także motywującego użytkowników systemu osiągnięć.

1. Strona internetowa
 - Konta użytkowników - logowanie, rejestracja, edycja konta użytkownika.
 - Lista znajomych: dodawanie i usuwanie użytkowników do znajomych, wyszukiwanie.
 - Profile znajomych - podgląd ich osiągnięć.
 - Profil zalogowanego użytkownika - podgląd własnych osiągnięć, wszystkich osiągnięć, historia jedzenia, wykresy kalorii.
 - Ranking ludzi z największą liczbą osiągnięć.
 - Kalendarz z listą rzeczy zjedzonych w ostatnim miesiącu.
 - Administracja - dodawanie kategorii posiłków i samych rodzajów posiłków, usuwanie, edycja.

2. Aplikacja na telefon - na Androida
 - Logowanie/wylogowanie w systemie, edycja konta użytkownika.
 - Połączenie z serwerem - pobranie kategorii i nazw posiłków.
 - Wybór tego co się aktualnie zjada.
 - Historia kilku ostatnich posiłków z aktualnego dnia.
 - Informacje o otrzymanych tego dnia osiągnięciach.

3. Serwer i baza danych
 - Baza danych w PostgreSQL.
 - Procedury ułatwiające obsługę w PL/pgSQL - złożone zapytania, agregacja, tworzenie danych statystycznych (do wykresów).
 - Triggery dla dodawania osiągnięć.
 - Serwer komunikacyjny udostępniający obsługę bazy danych dla aplikacji na telefonie - API.


Informacje techniczne:
 - Strona internetowa - Microsoft MVC 4, Entity Framework, HTML5, CSS3.
 - Telefon - Android 4.1-4.2 Jelly Bean.
 - Serwer - PostgreSQL, serwer proxy do komunikacji z aplikacją na telefonie.

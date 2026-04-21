# APBD – Ćwiczenie 5

## Opis
Projekt przedstawia aplikację ASP.NET Core Web API do zarządzania salami oraz rezerwacjami w centrum szkoleniowym.

Dane są przechowywane wyłącznie w pamięci aplikacji (statyczne listy), bez użycia bazy danych ani Entity Framework.

## Funkcjonalności
- przeglądanie listy sal
- filtrowanie sal po parametrach
- dodawanie nowych sal
- aktualizacja danych sal
- usuwanie sal
- zarządzanie rezerwacjami
- walidacja danych i reguły biznesowe

## Kontrolery
- RoomsController
- ReservationsController

## Endpointy

### Rooms

- GET /api/rooms  
  Zwraca wszystkie sale

- GET /api/rooms/{id}  
  Zwraca salę po ID

- GET /api/rooms/building/{buildingCode}  
  Zwraca sale z wybranego budynku

- GET /api/rooms?minCapacity=20&hasProjector=true&activeOnly=true  
  Filtrowanie sal

- POST /api/rooms  
  Dodaje nową salę

- PUT /api/rooms/{id}  
  Aktualizuje dane sali

- DELETE /api/rooms/{id}  
  Usuwa salę

---

### Reservations

- GET /api/reservations  
  Zwraca wszystkie rezerwacje

- GET /api/reservations/{id}  
  Zwraca rezerwację po ID

- GET /api/reservations?date=2026-05-10&status=confirmed&roomId=2  
  Filtrowanie rezerwacji

- POST /api/reservations  
  Tworzy nową rezerwację

- PUT /api/reservations/{id}  
  Aktualizuje rezerwację

- DELETE /api/reservations/{id}  
  Usuwa rezerwację

---

## Modele danych

### Room
- Id
- Name
- BuildingCode
- Floor
- Capacity
- HasProjector
- IsActive

### Reservation
- Id
- RoomId
- OrganizerName
- Topic
- Date
- StartTime
- EndTime
- Status

---

## Walidacja i reguły biznesowe

- pola tekstowe nie mogą być puste
- Capacity musi być większe od 0
- EndTime musi być późniejsze niż StartTime
- nie można dodać rezerwacji dla nieistniejącej sali
- nie można dodać rezerwacji dla nieaktywnej sali
- rezerwacje tej samej sali nie mogą nachodzić na siebie czasowo tego samego dnia

---

## Statusy HTTP

- 200 OK – poprawne zapytanie
- 201 Created – utworzenie zasobu
- 204 No Content – usunięcie
- 400 Bad Request – błędne dane
- 404 Not Found – brak zasobu
- 409 Conflict – konflikt (np. nachodzące rezerwacje)

---

## Uruchomienie

```bash
dotnet run
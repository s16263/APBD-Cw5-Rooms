using System;
using System.Collections.Generic;
using APBD_Cw5.Models;

namespace APBD_Cw5.Data
{
    public static class AppData
    {
        public static List<Room> Rooms { get; } = new List<Room>
        {
            new Room { Id = 1, Name = "Lab 101", BuildingCode = "A", Floor = 1, Capacity = 20, HasProjector = true, IsActive = true },
            new Room { Id = 2, Name = "Room 202", BuildingCode = "A", Floor = 2, Capacity = 30, HasProjector = false, IsActive = true },
            new Room { Id = 3, Name = "Conference 3", BuildingCode = "B", Floor = 1, Capacity = 15, HasProjector = true, IsActive = true },
            new Room { Id = 4, Name = "Lab 204", BuildingCode = "B", Floor = 2, Capacity = 24, HasProjector = true, IsActive = false },
            new Room { Id = 5, Name = "Workshop Hall", BuildingCode = "C", Floor = 0, Capacity = 50, HasProjector = true, IsActive = true }
        };

        public static List<Reservation> Reservations { get; } = new List<Reservation>
        {
            new Reservation
            {
                Id = 1,
                RoomId = 1,
                OrganizerName = "Anna Kowalska",
                Topic = "Warsztaty z HTTP",
                Date = new DateTime(2026, 5, 10),
                StartTime = new TimeSpan(10, 0, 0),
                EndTime = new TimeSpan(12, 0, 0),
                Status = "confirmed"
            },
            new Reservation
            {
                Id = 2,
                RoomId = 2,
                OrganizerName = "Jan Nowak",
                Topic = "Konsultacje APBD",
                Date = new DateTime(2026, 5, 10),
                StartTime = new TimeSpan(9, 0, 0),
                EndTime = new TimeSpan(10, 30, 0),
                Status = "planned"
            },
            new Reservation
            {
                Id = 3,
                RoomId = 3,
                OrganizerName = "Maria Zielińska",
                Topic = "REST API szkolenie",
                Date = new DateTime(2026, 5, 11),
                StartTime = new TimeSpan(13, 0, 0),
                EndTime = new TimeSpan(15, 0, 0),
                Status = "confirmed"
            },
            new Reservation
            {
                Id = 4,
                RoomId = 1,
                OrganizerName = "Tomasz Wiśniewski",
                Topic = "Analiza wymagań",
                Date = new DateTime(2026, 5, 12),
                StartTime = new TimeSpan(8, 30, 0),
                EndTime = new TimeSpan(10, 0, 0),
                Status = "cancelled"
            }
        };
    }
}
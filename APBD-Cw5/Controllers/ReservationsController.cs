using APBD_Cw5.Data;
using APBD_Cw5.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Cw5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        // GET /api/reservations
        // GET /api/reservations?date=2026-05-10&status=confirmed&roomId=2
        [HttpGet]
        public IActionResult GetReservations(
            [FromQuery] DateTime? date,
            [FromQuery] string? status,
            [FromQuery] int? roomId)
        {
            var query = AppData.Reservations.AsQueryable();

            if (date.HasValue)
                query = query.Where(r => r.Date.Date == date.Value.Date);

            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(r => r.Status.Equals(status, StringComparison.OrdinalIgnoreCase));

            if (roomId.HasValue)
                query = query.Where(r => r.RoomId == roomId.Value);

            return Ok(query.ToList());
        }

        // GET /api/reservations/{id}
        [HttpGet("{id}")]
        public IActionResult GetReservation(int id)
        {
            var reservation = AppData.Reservations.FirstOrDefault(r => r.Id == id);

            if (reservation == null)
                return NotFound();

            return Ok(reservation);
        }

        // POST /api/reservations
        [HttpPost]
        public IActionResult AddReservation([FromBody] Reservation reservation)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var room = AppData.Rooms.FirstOrDefault(r => r.Id == reservation.RoomId);

            if (room == null)
                return BadRequest("Room does not exist.");

            if (!room.IsActive)
                return BadRequest("Room is inactive.");

            var conflictExists = AppData.Reservations.Any(r =>
                r.RoomId == reservation.RoomId &&
                r.Date.Date == reservation.Date.Date &&
                r.Status.ToLower() != "cancelled" &&
                reservation.StartTime < r.EndTime &&
                reservation.EndTime > r.StartTime
            );

            if (conflictExists)
                return Conflict("Reservation time conflicts with an existing reservation.");

            reservation.Id = AppData.Reservations.Any() ? AppData.Reservations.Max(r => r.Id) + 1 : 1;
            AppData.Reservations.Add(reservation);

            return CreatedAtAction(nameof(GetReservation), new { id = reservation.Id }, reservation);
        }

        // PUT /api/reservations/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateReservation(int id, [FromBody] Reservation updatedReservation)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingReservation = AppData.Reservations.FirstOrDefault(r => r.Id == id);

            if (existingReservation == null)
                return NotFound();

            var room = AppData.Rooms.FirstOrDefault(r => r.Id == updatedReservation.RoomId);

            if (room == null)
                return BadRequest("Room does not exist.");

            if (!room.IsActive)
                return BadRequest("Room is inactive.");

            var conflictExists = AppData.Reservations.Any(r =>
                r.Id != id &&
                r.RoomId == updatedReservation.RoomId &&
                r.Date.Date == updatedReservation.Date.Date &&
                r.Status.ToLower() != "cancelled" &&
                updatedReservation.StartTime < r.EndTime &&
                updatedReservation.EndTime > r.StartTime
            );

            if (conflictExists)
                return Conflict("Reservation time conflicts with an existing reservation.");

            existingReservation.RoomId = updatedReservation.RoomId;
            existingReservation.OrganizerName = updatedReservation.OrganizerName;
            existingReservation.Topic = updatedReservation.Topic;
            existingReservation.Date = updatedReservation.Date;
            existingReservation.StartTime = updatedReservation.StartTime;
            existingReservation.EndTime = updatedReservation.EndTime;
            existingReservation.Status = updatedReservation.Status;

            return Ok(existingReservation);
        }

        // DELETE /api/reservations/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteReservation(int id)
        {
            var reservation = AppData.Reservations.FirstOrDefault(r => r.Id == id);

            if (reservation == null)
                return NotFound();

            AppData.Reservations.Remove(reservation);

            return NoContent();
        }
    }
}
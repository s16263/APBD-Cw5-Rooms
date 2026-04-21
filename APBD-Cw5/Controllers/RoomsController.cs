using APBD_Cw5.Data;
using APBD_Cw5.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Cw5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        // GET /api/rooms
        [HttpGet]
        public IActionResult GetRooms()
        {
            return Ok(AppData.Rooms);
        }

        // GET /api/rooms/{id}
        [HttpGet("{id}")]
        public IActionResult GetRoom(int id)
        {
            var room = AppData.Rooms.FirstOrDefault(r => r.Id == id);

            if (room == null)
                return NotFound();

            return Ok(room);
        }

        // GET /api/rooms/building/{buildingCode}
        [HttpGet("building/{buildingCode}")]
        public IActionResult GetRoomsByBuilding(string buildingCode)
        {
            var rooms = AppData.Rooms
                .Where(r => r.BuildingCode.ToLower() == buildingCode.ToLower())
                .ToList();

            return Ok(rooms);
        }

        // GET /api/rooms?minCapacity=20&hasProjector=true&activeOnly=true
        [HttpGet]
        public IActionResult GetFilteredRooms(
            [FromQuery] int? minCapacity,
            [FromQuery] bool? hasProjector,
            [FromQuery] bool? activeOnly)
        {
            var query = AppData.Rooms.AsQueryable();

            if (minCapacity.HasValue)
                query = query.Where(r => r.Capacity >= minCapacity.Value);

            if (hasProjector.HasValue)
                query = query.Where(r => r.HasProjector == hasProjector.Value);

            if (activeOnly == true)
                query = query.Where(r => r.IsActive);

            return Ok(query.ToList());
        }

        // POST /api/rooms
        [HttpPost]
        public IActionResult AddRoom([FromBody] Room room)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            room.Id = AppData.Rooms.Max(r => r.Id) + 1;
            AppData.Rooms.Add(room);

            return CreatedAtAction(nameof(GetRoom), new { id = room.Id }, room);
        }

        // PUT /api/rooms/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateRoom(int id, [FromBody] Room updatedRoom)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var room = AppData.Rooms.FirstOrDefault(r => r.Id == id);

            if (room == null)
                return NotFound();

            room.Name = updatedRoom.Name;
            room.BuildingCode = updatedRoom.BuildingCode;
            room.Floor = updatedRoom.Floor;
            room.Capacity = updatedRoom.Capacity;
            room.HasProjector = updatedRoom.HasProjector;
            room.IsActive = updatedRoom.IsActive;

            return Ok(room);
        }

        // DELETE /api/rooms/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteRoom(int id)
        {
            var room = AppData.Rooms.FirstOrDefault(r => r.Id == id);

            if (room == null)
                return NotFound();

            var hasReservations = AppData.Reservations.Any(r => r.RoomId == id);

            if (hasReservations)
                return Conflict("Room has reservations and cannot be deleted");

            AppData.Rooms.Remove(room);

            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Booking_Platform.Models;
using Booking_Platform.Data;
using Microsoft.EntityFrameworkCore;

namespace Booking_Platform.Controllers
{
	public class RoomsController : Controller
    {
        private readonly ILogger<RoomsController> _logger;
        private readonly ApplicationDbContext _context;


        public RoomsController(ILogger<RoomsController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        /*public IActionResult Rooms()
        {
            var rooms = GetAvailableRooms();
            return View(rooms.ToList());
        }*/

        /*public IEnumerable<RoomDto> GetAvailableRooms()
        {
            var room1 = new RoomDto("Room-1", "/images/room-1.jpg", 202.22m, "description for room-1", "room-1 address", 2);
            var room2 = new RoomDto(2, "Room-2", "/images/room-2.jpg", 442.44m, "description for room-2", "room-2 address", 4);

            IEnumerable<RoomDto> availableRooms = new RoomDto[] {
                room1,
                room2
            };

            foreach(var room in availableRooms)
            {
                Console.WriteLine(room.Title, room.Price, room.Description);
            }

            return availableRooms.ToArray();

            // Connect to the database
            //var rooms = db.master_staff
            //.Where(r => r.room_number == roomNumber)
            //.Select(x => new RoomDto(r.title, r.image, r.price, r.description, r.address, r.capacity))
            //.ToArray();
            //return rooms;
        }*/

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomDto>>> GetRooms()
        {
            return await _context.Rooms.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDto>> GetRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);

            if (room == null)
            {
                return NotFound();
            }

            return room;
        }

        [HttpPost]
        public async Task<ActionResult<RoomDto>> PostRoom(RoomDto room)
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRoom), new { id = room.Id }, room);
        }
    }
}


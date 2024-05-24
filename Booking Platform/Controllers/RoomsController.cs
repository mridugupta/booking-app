
using Microsoft.AspNetCore.Mvc;
using Booking_Platform.Models;
using Booking_Platform.Data;
using Microsoft.EntityFrameworkCore;

namespace Booking_Platform.Controllers
{
	public class RoomsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<RoomDto>> GetRooms()
        {
            return await _context.Rooms.ToListAsync();
        }

    }
}


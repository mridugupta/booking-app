using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Booking_Platform.Data;
using Booking_Platform.Models;

namespace Booking_Platform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<BookingDto>> PostBooking(BookingDto booking)
        {
            var room = await _context.Rooms.FindAsync(booking.Room.Id);
            if (room == null || room.Capacity < booking.NumberOfPeople)
            {
                return BadRequest("Invalid booking request.");
            }

            var overlappingBookings = await _context.Bookings
                .Where(b => b.Room.Id == booking.Room.Id &&
                            b.EndDate >= booking.StartDate &&
                            b.StartDate <= booking.EndDate)
                .ToListAsync();

            if (overlappingBookings.Any())
            {
                return BadRequest("The room is already booked for the selected dates.");
            }

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBookingByRoomId), new { id = booking.Id }, booking);
        }

        [HttpGet("{roomId}")]
        public async Task<ActionResult<IList<BookingDto>>> GetBookingByRoomId(int roomId)
        {
            return await _context.Bookings.Where(r => r.Room.Id == roomId).ToListAsync();
        }

        [HttpGet]
        public async Task<List<BookingDto>> GetBookings()
        {
            return await _context.Bookings.ToListAsync();
        }
    }
}
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


        [HttpPost("AddBooking")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddBooking([FromForm] int roomId, [FromForm] string email,
            [FromForm] DateTime startDate, [FromForm] DateTime endDate, [FromForm] int numberOfPeople)
        {
            try
            {
                var room = await _context.Rooms.FindAsync(roomId);
                if (room == null)
                {
                    return RedirectToAction("Index", "Home", new { success = false, message = "Invalid booking request." });
                }

                if (room.Capacity < numberOfPeople)
                {
                    return RedirectToAction("Index", "Home", new { success = false, message = "The room can host a maximum of " + room.Capacity + " individuals." });
                }

                var overlappingBookings = await _context.Bookings
                    .Where(b => b.Room.Id == roomId &&
                                b.EndDate >= startDate &&
                                b.StartDate <= endDate)
                    .ToListAsync();

                if (overlappingBookings.Any())
                {
                    return RedirectToAction("Index", "Home", new { success = false, message = "The room is already booked for the selected dates." });
                }

                var booking = new BookingDto
                {
                    Email = email,
                    StartDate = startDate,
                    EndDate = endDate,
                    NumberOfPeople = numberOfPeople,
                    Room = room
                };

                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home", new { success = true, message = "The room has been successfully booked." });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home", new { success = false, message = "Something went wrong. Please try again later." });
            }
        }

        [HttpGet]
        public async Task<List<BookingDto>> GetBookings()
        {
            return await _context.Bookings.ToListAsync();
        }
    }
}
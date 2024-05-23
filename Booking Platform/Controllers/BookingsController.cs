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
        public async Task<ActionResult> AddBooking([FromForm] int roomId, [FromForm] string email,
            [FromForm] DateTime startDate, [FromForm] DateTime endDate, [FromForm] int numberOfPeople)
        {
            try
            {
                var room = await _context.Rooms.FindAsync(roomId);
                if (room == null)
                {
                    return BadRequest("Invalid booking request.");
                }

                if (room.Capacity < numberOfPeople)
                {
                    return BadRequest("The room can host a maximum of $bookingRequest.NumberOfPeople individuals.");
                }

                var overlappingBookings = await _context.Bookings
                    .Where(b => b.Room.Id == roomId &&
                                b.EndDate >= startDate &&
                                b.StartDate <= endDate)
                    .ToListAsync();

                if (overlappingBookings.Any())
                {
                    return BadRequest("The room is already booked for the selected dates.");
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
            }
            catch (Exception ex)
            {
                // Log error
                //_logger.LogError($"An error occurred while processing the booking: {ex}");

                // Handle the exception and return an appropriate response
                return StatusCode(500, "An error occurred while processing the booking. Please try again later.");
            }

            return RedirectToAction("Index", "Home");
        }
/*
        [HttpGet("{roomId}")]
        public async Task<ActionResult<IList<BookingDto>>> GetBookingByRoomId(int roomId)
        {
            return await _context.Bookings.Where(r => r.Room.Id == roomId).ToListAsync();
        }*/

        [HttpGet]
        public async Task<List<BookingDto>> GetBookings()
        {
            return await _context.Bookings.ToListAsync();
        }
    }
}
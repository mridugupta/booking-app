using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Booking_Platform.Data;
using Booking_Platform.Models;

namespace Booking_Platform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BookingsController> _logger;

        public BookingsController(ILogger<BookingsController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost("AddBooking")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddBooking([FromForm] int roomId, [FromForm] string email,
            [FromForm] DateTime startDate, [FromForm] DateTime endDate, [FromForm] int numberOfPeople)
        {
            try
            {
                TempData["showMessage"] = "true";
                var room = await _context.Rooms.FindAsync(roomId);
                if (room == null)
                {
                    TempData["status"] = PredefinedMessages.Failure;
                    TempData["message"] = PredefinedMessages.InvalidBookingRequest;
                    return RedirectToAction("Index", "Home");
                }

                if (room.Capacity < numberOfPeople)
                {
                    TempData["status"] = PredefinedMessages.Failure;
                    TempData["message"] = string.Format(PredefinedMessages.RoomCapacityExceeded, room.Capacity);
                    return RedirectToAction("Index", "Home");
                }

                var overlappingBookings = await _context.Bookings
                    .Where(b => b.Room.Id == roomId &&
                                b.EndDate >= startDate &&
                                b.StartDate <= endDate)
                    .ToListAsync();

                if (overlappingBookings.Any())
                {
                    TempData["status"] = PredefinedMessages.Failure;
                    TempData["message"] = PredefinedMessages.RoomAlreadyBooked;
                    return RedirectToAction("Index", "Home");
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

                TempData["status"] = PredefinedMessages.Success;
                TempData["message"] = PredefinedMessages.BookingSuccessful;
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                TempData["status"] = PredefinedMessages.Failure;
                TempData["message"] = PredefinedMessages.GeneralError;
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<List<BookingDto>> GetBookings()
        {
            return await _context.Bookings.ToListAsync();
        }
    }
}
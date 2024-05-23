using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Booking_Platform.Models;

namespace Booking_Platform.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly Data.ApplicationDbContext _context;

    public HomeController(Data.ApplicationDbContext context, ILogger<HomeController> logger)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {

        var roomsController = new RoomsController(_context);
        var bookingsController = new BookingsController(_context);

        var rooms = await roomsController.GetRooms();
        var bookings = await bookingsController.GetBookings();

        var model = new Tuple<List<RoomDto>, List<BookingDto>>(rooms, bookings);
        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}


using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Booking_Platform.Models;
using Azure.Core;

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

    public async Task<IActionResult> Index(bool? success, string message)
    {
        var roomsController = new RoomsController(_context);
        var rooms = await roomsController.GetRooms();
        SetViewData(success, message);

        var model = new List<RoomDto>(rooms);
        return View(model);
    }

    private void SetViewData(bool? success, string message)
    {
        ViewData["ShowMessage"] = (success != null).ToString().ToLower();
        ViewData["Status"] = success == true ? "Success" : "Error";
        ViewData["Message"] = message;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}


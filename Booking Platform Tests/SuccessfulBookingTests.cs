using Microsoft.EntityFrameworkCore;
using Moq;
using Booking_Platform.Data;
using Booking_Platform.Models;
using Booking_Platform.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Http;
using NUnit.Framework.Internal;

namespace Booking_Platform_Tests;

public class SuccessfulBookingTests
{
    private ApplicationDbContext context;
    private BookingsController bookingsController;
    private ILogger<BookingsController> logger;
    private readonly string email = "test@example.com";
    private readonly DateTime startDate = DateTime.Now;
    private readonly DateTime endDate = DateTime.Now.AddDays(2);
    private readonly int capacity = 2;
    private int roomId = 0;
    private RoomDto room = new();

    public void AddRooms()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        context = new ApplicationDbContext(options);

        var room1 = new RoomDto("Room-1", "/images/room-1.jpeg", 202.22m, "Comfortable Stay", "123 Main St", 2);
        var room2 = new RoomDto("Room-2", "/images/room-2.jpeg", 355.45m, "Cozy Retreat", "789 Oak St", 5);

        context.Rooms.AddRange(room1, room2);

        context.SaveChangesAsync();

        logger = new Mock<ILogger<BookingsController>>().Object;
        var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());


        bookingsController = new BookingsController(logger, context)
        {
            TempData = tempData
        };

        if (context.Rooms != null && context.Rooms.Count() > 0)
        {
            room = context.Rooms.FirstOrDefault();
            roomId = room.Id;
        }
    }


    [Test]
    public async Task AddBooking_ValidInput_VerifyRedirectToActionResult()
    {
        //Act
        AddRooms();
        var result = await bookingsController.AddBooking(roomId, email, startDate, endDate, capacity);

        //Assert
        var redirectToActionResult = result as RedirectToActionResult;
        Assert.IsNotNull(redirectToActionResult);
        Assert.That(redirectToActionResult.ActionName, Is.EqualTo("Index"));
        Assert.That(redirectToActionResult.ControllerName, Is.EqualTo("Home"));

        context.Rooms.RemoveRange(context.Rooms);
        context.Bookings.RemoveRange(context.Bookings);
        await context.SaveChangesAsync();

    }

    [Test]
    public async Task AddBooking_ValidInput_VerifyBookingRecordDetails()
    {
        // Act
        AddRooms();
        var result = await bookingsController.AddBooking(roomId, email, startDate, endDate, capacity);

        // Assert
        var bookings = context.Bookings;
        Assert.IsNotNull(bookings);
        // Verify only one record is added
        Assert.That(bookings.Count(), Is.EqualTo(1));

        // Verify booking record matches with the input provided
        var booking = bookings.FirstOrDefault();
        Assert.That(booking?.Email, Is.EqualTo(email));
        Assert.That(booking?.StartDate.Date, Is.EqualTo(startDate.Date));
        Assert.That(booking?.EndDate.Date, Is.EqualTo(endDate.Date));
        Assert.That(booking?.NumberOfPeople, Is.EqualTo(2));
        Assert.That(booking?.Room, Is.EqualTo(room));

        Assert.That(bookingsController.TempData["status"], Is.EqualTo(PredefinedMessages.Success));
        Assert.That(bookingsController.TempData["message"], Is.EqualTo(PredefinedMessages.BookingSuccessful));

        context.Rooms.RemoveRange(context.Rooms);
        context.Bookings.RemoveRange(context.Bookings);
        await context.SaveChangesAsync();
    }
}


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

public class BookingFailureTest
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

    [SetUp]
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
    public async Task AddBooking_InvalidBooking_VerifyInvalidBookingRequestErrorMessageAsync()
    {
        //Act
        var invalidRoomId = 1;
        var result = await bookingsController.AddBooking(invalidRoomId, email, startDate, endDate, capacity);

        //Assert
        var redirectToActionResult = result as RedirectToActionResult;
        Assert.IsNotNull(redirectToActionResult);
        Assert.That(redirectToActionResult.ActionName, Is.EqualTo("Index"));
        Assert.That(redirectToActionResult.ControllerName, Is.EqualTo("Home"));

        Assert.That(bookingsController.TempData["status"], Is.EqualTo(PredefinedMessages.Failure));
        Assert.That(bookingsController.TempData["message"], Is.EqualTo(PredefinedMessages.InvalidBookingRequest));
        Dispose();

    }

    [Test]
    public async Task AddBooking_InvalidBooking_VerifyRoomCapacityExceededMessage()
    {
        // Act
        int overCapacity = 6;
        var result = await bookingsController.AddBooking(roomId, "xyz@gmail.com", startDate, endDate, overCapacity);

        // Assert

        // Verify that booking was not added
        var bookings = context.Bookings;
        // Verify no bookings were added
        Assert.That(bookings.Count(), Is.EqualTo(0));

        Assert.That(bookingsController.TempData["status"], Is.EqualTo(PredefinedMessages.Failure));
        Assert.That(bookingsController.TempData["message"], Is.EqualTo(string.Format(PredefinedMessages.RoomCapacityExceeded, room.Capacity)));

        Dispose();
    }

    [Test]
    public async Task AddBooking_InvalidBooking_VerifyRoomAlreadyBookedErrorMessage()
    {
        // Act
        var email2 = "test2@example.com";
        var result1 = await bookingsController.AddBooking(roomId, email, startDate, endDate, capacity);
        var result2 = await bookingsController.AddBooking(roomId, email2, startDate.AddDays(1), endDate, capacity);

        // Assert
        var bookings = context.Bookings;
        // Verify only one booking record exists
        Assert.That(bookings.Count(), Is.EqualTo(1));

        // Verify that new booking was not created
        var booking = bookings.Any(b => b.Email.Equals(email2));
        Assert.IsFalse(booking);

        Assert.That(bookingsController.TempData["status"], Is.EqualTo(PredefinedMessages.Failure));
        Assert.That(bookingsController.TempData["message"], Is.EqualTo(PredefinedMessages.RoomAlreadyBooked));
        Dispose();

    }

    private void Dispose()
    {
        context.Rooms.RemoveRange(context.Rooms);
        context.Bookings.RemoveRange(context.Bookings);
        context.SaveChangesAsync();
    }
}


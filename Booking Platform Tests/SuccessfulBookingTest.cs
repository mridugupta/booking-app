using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Booking_Platform;
using Booking_Platform.Data;
using Booking_Platform.Models;
using Booking_Platform.Controllers;
using System.Collections.Generic;

namespace Booking_Platform_Tests;

public class SuccessfulBookingTest
{
    [Fact]
    public async Task AddBooking_ValidInput_SuccessfullyCreatesBookingRecord()
    {
        // Arrange
        var mockContext = new Mock<ApplicationDbContext>();
        var mockBooking = new Mock<DbSet<BookingDto>>();
        var mockRooms = new Mock<DbSet<RoomDto>>();


        var room1 = new RoomDto("Room-1", "/images/room-1.jpeg", 202.22m, "Comfortable Stay", "123 Main St", 2);
        var room2 = new RoomDto("Room-2", "/images/room-2.jpeg", 355.45m, "Cozy Retreat", "789 Oak St", 5);

        var rooms = new List<RoomDto>
        {
            room1,
            room2
        };

        mockRooms.Setup(x => x.Set<RoomDto>())
        .Returns(rooms);


        //mockContext.Setup(m => m.Bookings).Returns(mockBooking.Object);

        var roomsController = new RoomsController(mockContext.Object);
        var bookingsController = new BookingsController(mockContext.Object);

        // Act
        var result = await controller.AddBooking(1, "test@example.com", DateTime.Now, DateTime.Now.AddDays(2), 3);

        // Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectToActionResult.ActionName);
        Assert.Equal("Home", redirectToActionResult.ControllerName);
    }

}

using Booking_Platform.Data;
using Booking_Platform.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


void SeedData(IServiceProvider serviceProvider)
{
    using var scope = serviceProvider.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();


    if (!context.Rooms.Any())
    {
        var room1 = new RoomDto("Room-1", "/images/room-1.jpg", 202.22m, "description for room-1", "123 Main St", 2);
        var room2 = new RoomDto("Room-2", "/images/room-2.jpg", 355.45m, "description for room-2", "789 Oak St", 5);
        room1.Id = 1;
        room2.Id = 2;

        context.Rooms.AddRange(
            room1, room2);
        context.SaveChanges();
    }


    //context.Bookings.AddRange(
    //new BookingDto(room1.Id,"abc@gmail.com", DateTime.Today, DateTime.Today.AddDays(2), 2, room1));

}

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => {
    options.UseInMemoryDatabase("BookingDb");
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();


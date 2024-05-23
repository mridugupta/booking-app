using Booking_Platform.Models;
using Booking_Platform.Data;
using Microsoft.EntityFrameworkCore;

void SeedData(IServiceProvider serviceProvider)
{
    using var scope = serviceProvider.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();


    if (!context.Rooms.Any())
    {

        var room1 = new RoomDto("Room-1", "/images/room-1.jpeg", 202.22m, "Comfortable Stay", "123 Main St", 2);
        var room2 = new RoomDto("Room-2", "/images/room-2.jpeg", 355.45m, "Cozy Retreat", "789 Oak St", 5);
        var room3 = new RoomDto("Room-3", "/images/room-3.jpeg", 402.22m, "Luxury Suite", "101 Maple Ave", 7);
        var room4 = new RoomDto("Room-4", "/images/room-4.jpeg", 300.57m, "Elegant Haven", "202 Pine Rd", 4);

        room1.Id = 1;
        room2.Id = 2;
        room3.Id = 3;
        room4.Id = 4;

        context.Rooms.AddRange(
        room1, room2, room3, room4);

        context.Bookings.AddRange(
        new BookingDto(room1.Id,"abc@gmail.com", DateTime.Today, DateTime.Today.AddDays(2), 2, room1));
        
        context.SaveChanges();
    }

}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => {
    options.UseInMemoryDatabase("BookingPlatformDb");
    });

var app = builder.Build();

SeedData(app.Services);
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


using Microsoft.EntityFrameworkCore.InMemory;
using Booking_Platform.Models;
using Booking_Platform.Data;
using Microsoft.EntityFrameworkCore;

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


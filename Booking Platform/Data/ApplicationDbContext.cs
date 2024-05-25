using Microsoft.EntityFrameworkCore;
using Booking_Platform.Models;

namespace Booking_Platform.Data
{
	public class ApplicationDbContext : DbContext, IDisposable
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<RoomDto> Rooms { get; set; }
        public DbSet<BookingDto> Bookings { get; set; }
    }
}
using Microsoft.EntityFrameworkCore;
using Booking_Platform.Models;

namespace Booking_Platform.Data
{
	public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<InMemoryDbContextOptionsExtensions> options) : base(options) { }

        public DbSet<RoomDto> Rooms { get; set; }
        public DbSet<BookingDto> Bookings { get; set; }
    }
}
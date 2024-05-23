
namespace Booking_Platform.Models
{
    public class BookingDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfPeople { get; set; }
        public RoomDto Room { get; set; }

        public BookingDto()
        {

        }
        public BookingDto (string email, DateTime startDate, DateTime endDate, int numberOfPeople, RoomDto room)
        {
            Email = email;
            StartDate = startDate;
            EndDate = endDate;
            NumberOfPeople = NumberOfPeople;
            Room = room;
        }
    }
}


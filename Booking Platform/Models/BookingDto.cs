
namespace Booking_Platform.Models
{
    public class BookingDto
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string Email { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfPeople { get; set; }
        public RoomDto Room { get; set; }

        public BookingDto()
        {

        }
        public BookingDto(int roomId, string email, DateTime startDate, DateTime endDate, int numberOfPeople, RoomDto room)
        {
            RoomId = roomId;
            Email = email;
            StartDate = startDate;
            EndDate = endDate;
            NumberOfPeople = NumberOfPeople;
            Room = room;
        }
    }
}


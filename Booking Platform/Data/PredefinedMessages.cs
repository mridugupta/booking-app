using Microsoft.EntityFrameworkCore;
using Booking_Platform.Models;

namespace Booking_Platform.Data
{
	public static class PredefinedMessages
    {
        public const string Success = "Success";
        public const string Failure = "Failure";
        public const string BookingSuccessful = "The room has been successfully booked.";
        public const string InvalidBookingRequest = "Invalid booking request.";
        public const string RoomCapacityExceeded = "The room can host a maximum of {0} individuals";
        public const string RoomAlreadyBooked = "The room is already booked for the selected dates.";
        public const string GeneralError = "Something went wrong. Please try again later.";
    }
}

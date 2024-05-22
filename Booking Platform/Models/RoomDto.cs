﻿

namespace Booking_Platform.Models
{
	public class RoomDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int Capacity { get; set; }


        public RoomDto(int id, string title, string imageUrl, decimal price, string description, string address, int capacity)
        {
            Id = id;
            Title = title;
            ImageUrl = imageUrl;
            Price = price;
            Description = description;
            Address = address;
            Capacity = capacity;
        }
    }
}


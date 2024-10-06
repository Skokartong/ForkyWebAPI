using System.ComponentModel.DataAnnotations;

namespace ForkyWebAPI.Models.BookingDTOs
{
    public class NewBookingDTO
    {
        public int NumberOfGuests { get; set; }
        public DateTime BookingStart { get; set; }
        public DateTime BookingEnd { get; set; }
        public string? Message { get; set; }
        public int FK_RestaurantId { get; set; }
        public int FK_AccountId { get; set; }
    }
}

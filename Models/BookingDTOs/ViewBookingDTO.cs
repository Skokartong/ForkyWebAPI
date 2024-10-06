namespace ForkyWebAPI.Models.BookingDTOs
{
    public class ViewBookingDTO
    {
        public string CustomerName { get; set; }
        public string RestaurantName { get; set; }
        public int NumberOfGuests { get; set; }
        public DateTime BookingStart { get; set; }
        public DateTime BookingEnd { get; set; }
        public string? Message { get; set; }
    }
}

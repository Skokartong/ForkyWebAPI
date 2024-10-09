namespace ForkyWebAPI.Models.BookingDTOs
{
    public class ViewBookingDTO
    {
        public int Id { get; set; }
        public int FK_AccountId { get; set; }
        public string? CustomerName { get; set; }
        public int FK_RestaurantId { get; set; }
        public string? RestaurantName { get; set; }
        public int TableId { get; set; }
        public int NumberOfGuests { get; set; }
        public DateTime BookingStart { get; set; }
        public DateTime BookingEnd { get; set; }
        public string? Message { get; set; }
        public required string OperationResult { get; set; } 
    }
}

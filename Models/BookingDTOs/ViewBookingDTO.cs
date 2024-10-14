using System.ComponentModel.DataAnnotations.Schema;

namespace ForkyWebAPI.Models.BookingDTOs
{
    public class ViewBookingDTO
    {
        public int Id { get; set; }
        [ForeignKey("Account")]
        public int FK_AccountId { get; set; }
        public string? CustomerName { get; set; }
        [ForeignKey("Restaurant")]
        public int FK_RestaurantId { get; set; }
        public string? RestaurantName { get; set; }
        [ForeignKey("Table")]
        public int FK_TableId { get; set; }
        public int NumberOfGuests { get; set; }
        public DateTime BookingStart { get; set; }
        public DateTime BookingEnd { get; set; }
        public string? Message { get; set; }
        public required string OperationResult { get; set; } 
    }
}

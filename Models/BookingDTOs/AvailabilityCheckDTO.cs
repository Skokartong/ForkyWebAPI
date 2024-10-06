using System.ComponentModel.DataAnnotations.Schema;

namespace ForkyWebAPI.Models.BookingDTOs
{
    public class AvailabilityCheckDTO
    {
        //[ForeignKey("Table")]
        //public int FK_TableId { get; }

        [ForeignKey("Restaurant")]
        public int FK_RestaurantId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int NumberOfGuests { get; set; }
    }
}

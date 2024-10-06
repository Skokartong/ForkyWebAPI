using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ForkyWebAPI.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int NumberOfGuests { get; set; }
        [Required]
        public DateTime BookingStart { get; set; }
        [Required]
        public DateTime BookingEnd { get; set; }
        public string? Message { get; set; }

        [ForeignKey("Account")]
        public int FK_AccountId { get; set; }
        public virtual Account Account { get; set; }

        [ForeignKey("Table")]
        public int FK_TableId { get; set; }
        public virtual Table Table { get; set; }

        [ForeignKey("Restaurant")]
        public int FK_RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}

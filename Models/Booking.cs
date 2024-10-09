using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ForkyWebAPI.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required int NumberOfGuests { get; set; }
        [Required]
        public required DateTime BookingStart { get; set; }
        [Required]
        public required DateTime BookingEnd { get; set; }
        public string? Message { get; set; }

        [ForeignKey("Account")]
        public required int FK_AccountId { get; set; }
        public virtual Account? Account { get; set; }

        [ForeignKey("Table")]
        public required int FK_TableId { get; set; }
        public virtual Table? Table { get; set; }

        [ForeignKey("Restaurant")]
        public required int FK_RestaurantId { get; set; }
        public virtual Restaurant? Restaurant { get; set; }
    }
}

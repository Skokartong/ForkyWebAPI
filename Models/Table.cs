using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ForkyWebAPI.Models
{
    public class Table
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int TableNumber { get; set; }
        [Required]
        public int AmountOfSeats { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }

        [ForeignKey("Restaurant")]
        public int FK_RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}

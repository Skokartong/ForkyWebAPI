using System.ComponentModel.DataAnnotations.Schema;

namespace ForkyWebAPI.Models.RestaurantDTOs
{
    public class NewTableDTO
    {
        public int TableNumber { get; set; }
        public int AmountOfSeats { get; set; }

        [ForeignKey("Restaurant")]
        public int FK_RestaurantId { get; set; }
    }
}

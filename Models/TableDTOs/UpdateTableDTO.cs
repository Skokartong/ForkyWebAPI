using System.ComponentModel.DataAnnotations.Schema;

namespace ForkyWebAPI.Models.TableDTOs
{
    public class UpdateTableDTO
    {
        public int TableNumber { get; set; }
        public int AmountOfSeats { get; set; }

        [ForeignKey("Restaurant")]
        public int FK_RestaurantId { get; set; }
    }
}

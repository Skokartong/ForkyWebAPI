using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForkyWebAPI.Models.MenuDTOs
{
    public class UpdateMenuDTO
    {
        [Required]
        [MaxLength(30)]
        public string NameOfDish { get; set; }

        [MaxLength(30)]
        public string Drink { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        public string? Ingredients { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        [ForeignKey("Restaurant")]
        public int FK_RestaurantId { get; set; }
    }
}

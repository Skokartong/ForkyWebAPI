using System.ComponentModel.DataAnnotations;

namespace ForkyWebAPI.Models.RestaurantDTOs
{
    public class MenuDTO
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
        public int FK_RestaurantId { get; set; }
    }
}

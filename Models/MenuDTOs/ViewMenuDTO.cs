using System.ComponentModel.DataAnnotations;

namespace ForkyWebAPI.Models.MenuDTOs
{
    public class ViewMenuDTO
    {
        public string NameOfDish { get; set; }

        public string Drink { get; set; }

        public bool IsAvailable { get; set; }

        public string? Ingredients { get; set; }

        public double Price { get; set; }
    }
}

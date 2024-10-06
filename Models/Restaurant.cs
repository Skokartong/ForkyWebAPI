using System.ComponentModel.DataAnnotations;

namespace ForkyWebAPI.Models
{
    public class Restaurant
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string RestaurantName { get; set; }
        [Required]
        [MaxLength(50)]
        public string TypeOfRestaurant { get; set; }
        public string Location { get; set; }
        public string? AdditionalInformation { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Menu> Menus { get; set; }
        public virtual ICollection<Table> Tables { get; set; }
    }
}

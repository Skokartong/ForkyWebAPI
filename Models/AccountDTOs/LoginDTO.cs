using System.ComponentModel.DataAnnotations;

namespace ForkyWebAPI.Models.AccountDTOs
{
    public class LoginDTO
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

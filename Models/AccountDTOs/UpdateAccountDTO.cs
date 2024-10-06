using System.ComponentModel.DataAnnotations;

namespace ForkyWebAPI.Models.AccountDTOs
{
    public class UpdateAccountDTO
    {
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}

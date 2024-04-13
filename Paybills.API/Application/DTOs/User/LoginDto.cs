using System.ComponentModel.DataAnnotations;

namespace Paybills.API.DTOs
{
    public class LoginDto
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
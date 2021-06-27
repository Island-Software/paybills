using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class BillTypeRegisterDto
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public bool Active { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class BillTypeDto
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public bool Active { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace Paybills.API.DTOs
{
    public class BillRegisterDto
    {
        [Required]
        public int TypeId { get; set; }        
        public float Value { get; set; }
        [Required]
        public int Month { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
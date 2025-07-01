using System.ComponentModel.DataAnnotations;

namespace Paybills.API.Application.DTOs.ReceivingType
{
    public class ReceivingTypeRegisterDto
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public bool Active { get; set; }
    }
}
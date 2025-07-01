using System;
using System.ComponentModel.DataAnnotations;

namespace Paybills.API.Application.DTOs.Receiving
{
    public class ReceivingRegisterDto
    {
        [Required]
        public int TypeId { get; set; }
        public float Value { get; set; }
        [Required]
        public int Month { get; set; }
        [Required]
        public int Year { get; set; }
        public DateTime? ReceivingDate { get; set; }
        public bool Received { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
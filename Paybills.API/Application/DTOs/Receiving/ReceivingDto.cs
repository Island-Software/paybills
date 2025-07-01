using System;
using Paybills.API.Application.DTOs.ReceivingType;

namespace Paybills.API.Application.DTOs.Receiving
{
    public class ReceivingDto
    {
        public int Id { get; set; }
        public ReceivingTypeDto ReceivingType { get; set; }
        public float Value { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public DateTime? ReceivingDate { get; set; }
        public bool Received { get; set; }
        public string[] UserName { get; set; }
    }
}
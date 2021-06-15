using System;

namespace API.Entities
{
    public class Bill
    {
        public int Id { get; set; }
        public BillType BillType { get; set; }
        public float Value { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
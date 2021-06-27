using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Bills")]
    public class Bill
    {
        public int Id { get; set; }
        public BillType BillType { get; set; }
        public float Value { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public ICollection<AppUser> Users { get; set; }
    }
}
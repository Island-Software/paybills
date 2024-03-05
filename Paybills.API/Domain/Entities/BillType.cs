using System.ComponentModel.DataAnnotations.Schema;
using Paybills.API.Interfaces;

namespace Paybills.API.Entities
{
    [Table("BillTypes")]
    public class BillType
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
    }
}
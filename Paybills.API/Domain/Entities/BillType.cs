using System.ComponentModel.DataAnnotations.Schema;

namespace Paybills.API.Domain.Entities
{
    [Table("BillTypes")]
    public class BillType
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class BillType
    {
        public int Id { get; set; }
        
        public string Description { get; set; }
        
        public bool Active { get; set; }
    }
}
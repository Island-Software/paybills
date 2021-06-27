using System;
using System.Collections.Generic;

namespace API.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public ICollection<BillDto> Bills { get; set; }
    }
}
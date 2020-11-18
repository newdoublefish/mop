using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Department : BaseEntity
    {
        public string Name { get; set; }
        public int ParentId { get; set; }
        public string Description { get; set; }
    }
}

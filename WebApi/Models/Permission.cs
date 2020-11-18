using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Permission : BaseEntity
    {
        public string Name { get; set; }
        public int Type { get; set; }
        public string Path { get; set; }
    }
}

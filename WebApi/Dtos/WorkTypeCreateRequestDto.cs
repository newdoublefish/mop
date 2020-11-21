using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class WorkTypeCreateRequestDto
    {
        public String Name { get; set; }
        public String Description { get; set; }
        public int ParentId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class DepartmentResponseDto:BaseDto
    {
        public string Name { get; set; }
        public int ParentId { get; set; }
        public string Description { get; set; }
    }
}

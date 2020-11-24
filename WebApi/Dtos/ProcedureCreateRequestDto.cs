using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class ProcedureCreateRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int WorkTypeId { get; set; }
    }
}

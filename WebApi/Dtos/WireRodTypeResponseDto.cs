using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class WireRodTypeResponseDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int WorkTypeId { get; set; }
    }
}

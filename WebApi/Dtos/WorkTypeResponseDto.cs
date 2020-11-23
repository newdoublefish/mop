using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Dtos
{
    public class WorkTypeResponseDto : BaseDto
    {
        public String Description { get; set; }
        public int ParentId { get; set; }

        public WorkTypeResponseDto Parent { get; set; }

        public List<WorkTypeResponseDto> Childs { get; set; }
    }
}

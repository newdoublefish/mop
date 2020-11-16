using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class UserCreateRequestDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Status { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class UserResponseDto : BaseDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmployeeNumber { get; set; }
        public string Mobile { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public DateTime BirthDay { get; set; }
        public int Gender { get; set; }
        public int Status { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class UserCreateRequestDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmployeeNumber { get; set; }
        [Required(ErrorMessage = "手机号码不能为空"), RegularExpression(@"^\+?\d{0,4}?[1][3-8]\d{9}$", ErrorMessage = "手机号码格式错误")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "部门不能为空")]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "角色不能为空")]
        public int[] RoleIds { get; set; }
        public DateTime BirthDay { get; set; }
        public int Gender { get; set; }
        public int Status { get; set; }
    }
}

using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class User : BaseEntity
    {

        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmployeeNumber { get; set; }
        public string Mobile { get; set; }
        public int DepartmentId { get; set; }
        public DateTime BirthDay { get; set; }
        public int Gender { get; set; }
        public int Status { get; set; }

        [Navigate(nameof(DepartmentId))]
        public Department Department { get; set; }

        [Navigate(ManyToMany = typeof(UserRole))]
        public ICollection<Role> Roles { get; set; }
    }
}

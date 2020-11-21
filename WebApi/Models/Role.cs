using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        [Navigate(ManyToMany = typeof(UserRole))]
        public ICollection<User> Users { get; set; }
    }
}

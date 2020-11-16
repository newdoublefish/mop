using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class User
    {
        [Column(IsIdentity = true)]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Status { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}

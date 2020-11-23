using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class WorkType : BaseEntity
    {
        [Column(DbType = "varchar(128) NOT NULL")]
        public String Name { get; set; }
        [Column(DbType = "varchar(128) NOT NULL")]
        public String Description { get; set; }
        public int ParentId { get; set; }

        [Navigate(nameof(ParentId))]
        public WorkType Parent { get; set; }


        [Navigate(nameof(ParentId))]
        public ICollection<WorkType> Childs { get; set; }
    }
}

using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class ManuOrderFeedback
    {
        [Column(IsIdentity = true)]
        public int Id { get; set; }
        public string ManuOrder { get; set; }
        public int Count { get; set; }
        public int UserId { get; set; }
        public int Type { get; set; }
        public string Spec1 { get; set; }
        public string Spec2 { get; set; }
        public string Spec3 { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }


    }
}

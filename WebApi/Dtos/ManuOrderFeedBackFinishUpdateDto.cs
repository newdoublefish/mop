using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class ManuOrderFeedbackFinishUpdateDto
    {
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndAt { get; set; }
        /// <summary>
        /// 异常类型(单独表)
        /// </summary>
        public string ExceptionTypeId { get; set; }
        /// <summary>
        /// 异常时间
        /// </summary>
        public DateTime ExceptionTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
    }
}

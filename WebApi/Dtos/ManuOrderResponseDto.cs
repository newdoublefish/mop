using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class ManuOrderResponseDto: BaseDto
    {
        /// <summary>
        /// 制令单
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 图号
        /// </summary>
        public string DrawingNumber { get; set; }
        /// <summary>
        /// 制定单数量
        /// </summary>
        public int Total { get; set; }
        public int Status { get; set; }
    }
}

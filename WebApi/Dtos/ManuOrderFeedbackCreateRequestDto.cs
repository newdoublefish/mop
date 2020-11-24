using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dto

{
    public class ManuOrderFeedbackCreateRequestDto 
    {
        /// <summary>
        /// 制令单
        /// </summary>
        public string ManuOrder { get; set; }
        /// <summary>
        /// 制定单数量
        /// </summary>
        public int ManuOrderCount { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 类型(单独表)：1设备性（自动机/半自动机/注塑） 2 劳动密集型 3 全检以及OQC
        /// </summary>
        public int WorkTypeId { get; set; }
        /// <summary>
        /// 组别特殊项1
        /// </summary>
        public string Spec1 { get; set; }
        /// <summary>
        /// 组别特殊项2
        /// </summary>
        public string Spec2 { get; set; }
        /// <summary>
        /// 组别特殊项3
        /// </summary>
        public string Spec3 { get; set; }
        /// <summary>
        /// 产前确认
        /// </summary>
        public string Confirm { get; set; }
        /// <summary>
        /// 长度 自动机
        /// </summary>
        public int Length { get; set; }
        /// <summary>
        /// 颜色 自动机
        /// </summary>
        public string Color { get; set; }
        /// <summary>
        /// 工序(单独表) 与类型workType相关
        /// </summary>
        public int ProcedureId { get; set; }
        /// <summary>
        /// 线材类型(单独表) 与类型workType相关
        /// </summary>
        public int WireRodTypeId { get; set; }
        /// <summary>
        /// 单品数量
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 点位
        /// </summary>
        public int Position { get; set; }
        /// <summary>
        /// 总数
        /// </summary>
    }
}

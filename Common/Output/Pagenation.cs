using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Output
{
    /// <summary>
    /// 分页信息输出
    /// </summary>
    public class Pagenation<T>
    {
        /// <summary>
        /// 第几页
        /// </summary>
        public long Page { get; set; } = 0;
        /// <summary>
        /// 每页数量
        /// </summary>
        public long Size { get; set; } = 0;
        /// <summary>
        /// 数据总数
        /// </summary>
        public long Total { get; set; } = 0;

        /// <summary>
        /// 数据
        /// </summary>
        public IList<T> List { get; set; }
    }
}

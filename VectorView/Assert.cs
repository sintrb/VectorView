using System;
using System.Collections.Generic;
using System.Text;

namespace Sin.VectorView
{
    /// <summary>
    /// 断言类
    /// </summary>
    public class Assert
    {
        /// <summary>
        /// 非空断言
        /// </summary>
        /// <param name="name">值名称</param>
        /// <param name="obj">需要判断的变量</param>
        /// <returns>非空返回tru，否则抛出异常</returns>
        static public bool NotNull(String name, Object obj)
        {
            if (obj == null)
            {
                throw new Exception(String.Format("{0}值为空", name));
            }
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Sin.VectorView
{
    /// <summary>
    /// ������
    /// </summary>
    public class Assert
    {
        /// <summary>
        /// �ǿն���
        /// </summary>
        /// <param name="name">ֵ����</param>
        /// <param name="obj">��Ҫ�жϵı���</param>
        /// <returns>�ǿշ���tru�������׳��쳣</returns>
        static public bool NotNull(String name, Object obj)
        {
            if (obj == null)
            {
                throw new Exception(String.Format("{0}ֵΪ��", name));
            }
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace classdiagram
{
    public interface IBookable
    {
        /// <summary>
        /// 可被预约的
        /// </summary>
        bool IsAvailable();
        /// <summary>
        /// 被预约
        /// </summary>
        void GotBooked();
    }
}
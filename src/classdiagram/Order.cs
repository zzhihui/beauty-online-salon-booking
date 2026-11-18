using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace classdiagram
{
    public abstract class Order
    {
        private int id;
        private DateTime createTime;
        private int createrId;

        public OrderState State
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public DateTime CloseTime
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public string Comment
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        /// <summary>
        /// 重新打开订单
        /// </summary>
        /// <returns>true if success</returns>
        public bool Reopen()
        {
            throw new System.NotImplementedException();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace classdiagram
{
    public class BookingOrder : Order
    {
        public ConfirmState ConfirmState
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public classdiagram.IBookable[] Content
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public string Feedback
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace classdiagram
{
    public abstract class User
    {
        private static int id;
        public string Name
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public string Email
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public string Password
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public string PhoneNumber
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public void Login()
        {
            throw new System.NotImplementedException();
        }

        public void Logout()
        {
            throw new System.NotImplementedException();
        }
    }
}
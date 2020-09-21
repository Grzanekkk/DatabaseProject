using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseProject
{
    public static class ValidateDate
    {
        public static bool EmailPasswordUsername(string email, string password, string username)
        {
            if (!Email(email))
            {
                return false;
            }
            else if(!Password(password))
            {
                return false;
            }
            else if (!Username(username))
            {
                return false;
            }

            return true;
        }

        public static bool Email(string email)
        {
            if (!email.Contains("@") || !email.Contains(".") || email.Equals("") || email.Length < 5)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool Password(string password)
        {
            if (password.Length < 8)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool Username(string username)
        {
            if (username.Equals(""))
            {
                return false;
            }
            {
                return true;
            }
        }

    }
}

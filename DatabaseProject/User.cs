using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DatabaseProject
{

    public class User
    {
        // public User currentUser;     // TO DO

        public string id, username, email, password, country;

        private User(string _id, string _username, string _email, string _password, string _country)
        {
            id = _id;
            username = _username;
            email = _email;
            password = _password;
            country = _country;
        }

        public static User GetUserWithEmailAndPassword(string email, string password)
        {
            DBAccess dbAccess = new DBAccess();
            DataTable dtUsers = new DataTable();

            string query = $"Select * from Users Where Email = '{email}' AND Password = '{password}'";

            dbAccess.ReadDataThroughAdapter(query, dtUsers);

            if (dtUsers.Rows.Count == 1)
            {
                User currentUser = new User
                (
                    dtUsers.Rows[0]["ID"].ToString(),
                    dtUsers.Rows[0]["Username"].ToString(),
                    dtUsers.Rows[0]["Email"].ToString(),
                    dtUsers.Rows[0]["Password"].ToString(),
                    dtUsers.Rows[0]["Country"].ToString()
                );

                dbAccess.CloseConnection();
                return currentUser;
            }
            else
            {
                return null;    // DO ZMIANY !!!!!!!!!!!!!!!!!!
            }
        }
    }
}

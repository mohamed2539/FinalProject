using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace IT508_Project.Utilities
{
    public class UserDataHelper
    {
        public static int GetUserID(string username)
        {
            int userId = 0;
            string connectionString = @"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;";

            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                string query = "SELECT UserID FROM IMS_Users WHERE UserName = @UserName";
                SqlCommand command = new SqlCommand(query, sqlcon);
                command.Parameters.AddWithValue("@UserName", username);

                sqlcon.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    userId = Convert.ToInt32(reader["UserID"]);
                }

                reader.Close();
            }

            return userId;
        }
    }
}
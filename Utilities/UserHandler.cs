using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace IT508_Project.Utilities
{
    public class UserHandler
    {
        private SqlConnection sqlcon;

        public UserHandler(SqlConnection connection)
        {
            sqlcon = connection;
        }

        public void AddUser(int userID, string userName, string password, string userRole)
        {
            try
            {
                string query = @"INSERT INTO IMS_Users (UserID, username, password, Role) VALUES (@UserID, @username, @password, @Role)";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.Parameters.AddWithValue("@username", userName);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@Role", userRole);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Handle exception
            }
        }

        public void UpdateUser(int userID, string userName, string password, string userRole)
        {
            try
            {
                string query = @"UPDATE IMS_Users SET username = @username, password = @password, Role = @Role WHERE UserID = @UserID";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.Parameters.AddWithValue("@username", userName);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@Role", userRole);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Handle exception
            }
        }

        public void DeleteUser(int userID)
        {
            try
            {
                string query = "DELETE FROM IMS_Users WHERE UserID = @UserID";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Handle exception
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace IT508_Project.Utilities
{
    public class ValidationHelper
    {
        private string connectionString;

        public ValidationHelper(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool TransIDExists(string transID)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                string query = "SELECT COUNT(*) FROM Tran_Master WHERE Trans_ID = @Trans_ID";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.Parameters.AddWithValue("@Trans_ID", transID);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        public bool IsUserAdmin(string username)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                string query = "SELECT COUNT(*) FROM IMS_Users WHERE username = @username AND Role = 'Admin'";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.Parameters.AddWithValue("@username", username);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        public bool CheckItemExists(int itemId, int whId)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                string checkQuery = @"select count(*) from Stock where Item_ID=@Item_ID and WH_ID=@WH_ID";
                SqlCommand checkCmd = new SqlCommand(checkQuery, sqlcon);
                checkCmd.Parameters.AddWithValue("@Item_ID", itemId);
                checkCmd.Parameters.AddWithValue("@WH_ID", whId);
                int count = (int)checkCmd.ExecuteScalar();
                return count > 0;
            }
        }

    }
}
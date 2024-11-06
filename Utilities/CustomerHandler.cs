using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace IT508_Project.Utilities
{
    public class CustomerHandler
    {
        private SqlConnection sqlcon;
        private int userId;

        public CustomerHandler(SqlConnection connection, int user)
        {
            sqlcon = connection;
            userId = user;
        }

        public void AddCustomer(int cusId, string cusName, string cusContactNum, string cusEmail)
        {
            try
            {
                string query = @"INSERT INTO IMS_Customer (Cus_ID, Cus_Name, Cus_Contact_Num, Cus_Email, Created_By) VALUES (@Cus_ID, @Cus_Name, @Cus_Contact_Num, @Cus_Email, @Created_By)";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.Parameters.AddWithValue("@Cus_ID", cusId);
                cmd.Parameters.AddWithValue("@Cus_Name", cusName);
                cmd.Parameters.AddWithValue("@Cus_Contact_Num", cusContactNum);
                cmd.Parameters.AddWithValue("@Cus_Email", cusEmail);
                cmd.Parameters.AddWithValue("@Created_By", userId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Handle exception
            }
        }

        public void UpdateCustomer(int cusId, string cusName, string cusContactNum, string cusEmail)
        {
            try
            {
                string query = @"UPDATE IMS_Customer SET Cus_Name = @Cus_Name, Cus_Contact_Num = @Cus_Contact_Num, Cus_Email = @Cus_Email, Created_By = @Created_By WHERE Cus_ID = @Cus_ID";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.Parameters.AddWithValue("@Cus_ID", cusId);
                cmd.Parameters.AddWithValue("@Cus_Name", cusName);
                cmd.Parameters.AddWithValue("@Cus_Contact_Num", cusContactNum);
                cmd.Parameters.AddWithValue("@Cus_Email", cusEmail);
                cmd.Parameters.AddWithValue("@Created_By", userId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Handle exception
            }
        }

        public void DeleteCustomer(int cusId)
        {
            try
            {
                string query = "DELETE FROM IMS_Customer WHERE Cus_ID = @Cus_ID";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.Parameters.AddWithValue("@Cus_ID", cusId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Handle exception
            }
        }
    }
}
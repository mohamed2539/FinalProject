using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace IT508_Project.Utilities
{
    public class SupplierHandler
    {
        private SqlConnection sqlcon;
        private int userId;

        public SupplierHandler(SqlConnection connection, int user)
        {
            sqlcon = connection;
            userId = user;
        }

        public void AddSupplier(int supId, string supName, string supContactNum, string supEmail)
        {
            try
            {
                string query = @"INSERT INTO IMS_Supplier (Supplier_ID, Supplier_Name, Supplier_Contact_Num, Supplier_Email, Created_By) 
                             VALUES (@Supplier_ID, @Supplier_Name, @Supplier_Contact_Num, @Supplier_Email, @Created_By)";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.Parameters.AddWithValue("@Supplier_ID", supId);
                cmd.Parameters.AddWithValue("@Supplier_Name", supName);
                cmd.Parameters.AddWithValue("@Supplier_Contact_Num", supContactNum);
                cmd.Parameters.AddWithValue("@Supplier_Email", supEmail);
                cmd.Parameters.AddWithValue("@Created_By", userId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log it)
            }
        }

        public void UpdateSupplier(int supId, string supName, string supContactNum, string supEmail)
        {
            try
            {
                string query = @"UPDATE IMS_Supplier 
                             SET Supplier_Name = @Supplier_Name, Supplier_Contact_Num = @Supplier_Contact_Num, Supplier_Email = @Supplier_Email, Created_By = @Created_By 
                             WHERE Supplier_ID = @Supplier_ID";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.Parameters.AddWithValue("@Supplier_ID", supId);
                cmd.Parameters.AddWithValue("@Supplier_Name", supName);
                cmd.Parameters.AddWithValue("@Supplier_Contact_Num", supContactNum);
                cmd.Parameters.AddWithValue("@Supplier_Email", supEmail);
                cmd.Parameters.AddWithValue("@Created_By", userId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log it)
            }
        }

        public void DeleteSupplier(int supId)
        {
            try
            {
                string query = "DELETE FROM IMS_Supplier WHERE Supplier_ID = @Supplier_ID";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.Parameters.AddWithValue("@Supplier_ID", supId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Handle exception
            }
        }
    }
}
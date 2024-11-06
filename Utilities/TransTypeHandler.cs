using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace IT508_Project.Utilities
{
    public class TransTypeHandler
    {
        private SqlConnection sqlcon;

        public TransTypeHandler(SqlConnection connection)
        {
            sqlcon = connection;
        }

        public void AddTransType(int transTypeId, string transTypeName)
        {
            try
            {
                string query = @"INSERT INTO Trans_Type (Trans_Type_ID, Trans_Type_Name) VALUES (@Trans_Type_ID, @Trans_Type_Name)";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.Parameters.AddWithValue("@Trans_Type_ID", transTypeId);
                cmd.Parameters.AddWithValue("@Trans_Type_Name", transTypeName);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Handle exception
            }
        }

        public void UpdateTransType(int transTypeId, string transTypeName)
        {
            try
            {
                string query = @"UPDATE Trans_Type SET Trans_Type_Name = @Trans_Type_Name WHERE Trans_Type_ID = @Trans_Type_ID";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.Parameters.AddWithValue("@Trans_Type_ID", transTypeId);
                cmd.Parameters.AddWithValue("@Trans_Type_Name", transTypeName);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Handle exception
            }
        }

        public void DeleteTransType(int transTypeId)
        {
            try
            {
                string query = "DELETE FROM Trans_Type WHERE Trans_Type_ID = @Trans_Type_ID";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.Parameters.AddWithValue("@Trans_Type_ID", transTypeId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Handle exception
            }
        }
    }
}
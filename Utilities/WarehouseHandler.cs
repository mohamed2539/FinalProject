using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace IT508_Project.Utilities
{
    public class WarehouseHandler
    {
        private SqlConnection sqlcon;
        private int userId;

        public WarehouseHandler(SqlConnection connection, int user)
        {
            sqlcon = connection;
            userId = user;
        }

        public void AddWarehouse(int whId, string whName)
        {
            try
            {
                string query = @"INSERT INTO IMS_WH (WH_ID, WH_Name, Created_By) VALUES (@WH_ID, @WH_Name, @Created_By)";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.Parameters.AddWithValue("@WH_ID", whId);
                cmd.Parameters.AddWithValue("@WH_Name", whName);
                cmd.Parameters.AddWithValue("@Created_By", userId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Handle exception
            }
        }

        public void UpdateWarehouse(int whId, string whName)
        {
            try
            {
                string query = @"UPDATE IMS_WH SET WH_Name = @WH_Name, Created_By = @Created_By WHERE WH_ID = @WH_ID";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.Parameters.AddWithValue("@WH_ID", whId);
                cmd.Parameters.AddWithValue("@WH_Name", whName);
                cmd.Parameters.AddWithValue("@Created_By", userId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Handle exception
            }
        }

        public void DeleteWarehouse(int whId)
        {
            try
            {
                string query = "DELETE FROM IMS_WH WHERE WH_ID = @WH_ID";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.Parameters.AddWithValue("@WH_ID", whId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Handle exception
            }
        }
    }
}
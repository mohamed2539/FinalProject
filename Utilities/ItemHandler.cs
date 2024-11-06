using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace IT508_Project.Utilities
{
    public class ItemHandler
    {
        private SqlConnection sqlcon;
        private int userId;

        public ItemHandler(SqlConnection connection, int user)
        {
            sqlcon = connection;
            userId = user;
        }

        public void AddItem(int itemId, string itemName, string category, DateTime prodDate, DateTime expDate, int reorderThreshold, string measureUnit)
        {
            try
            {
                string query = @"INSERT INTO IMS_Items (Item_ID, Item_Name, Category, Prod_Date, Exp_Date, Reorder_Threshold, Measure_Unit, Created_By) VALUES (@Item_ID, @Item_Name, @Category, @Prod_Date, @Exp_Date, @Reorder_Threshold, @Measure_Unit, @Created_By)";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.Parameters.AddWithValue("@Item_ID", itemId);
                cmd.Parameters.AddWithValue("@Item_Name", itemName);
                cmd.Parameters.AddWithValue("@Category", category);
                cmd.Parameters.AddWithValue("@Prod_Date", prodDate);
                cmd.Parameters.AddWithValue("@Exp_Date", expDate);
                cmd.Parameters.AddWithValue("@Reorder_Threshold", reorderThreshold);
                cmd.Parameters.AddWithValue("@Measure_Unit", measureUnit);
                cmd.Parameters.AddWithValue("@Created_By", userId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Handle exception
            }
        }

        public void UpdateItem(int itemId, string itemName, string category, DateTime prodDate, DateTime expDate, int reorderThreshold, string measureUnit)
        {
            try
            {
                string query = @"UPDATE IMS_Items SET Item_Name = @Item_Name, Category = @Category, Prod_Date = @Prod_Date, Exp_Date = @Exp_Date, Reorder_Threshold = @Reorder_Threshold, Measure_Unit = @Measure_Unit, Created_By = @Created_By WHERE Item_ID = @Item_ID";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.Parameters.AddWithValue("@Item_ID", itemId);
                cmd.Parameters.AddWithValue("@Item_Name", itemName);
                cmd.Parameters.AddWithValue("@Category", category);
                cmd.Parameters.AddWithValue("@Prod_Date", prodDate);
                cmd.Parameters.AddWithValue("@Exp_Date", expDate);
                cmd.Parameters.AddWithValue("@Reorder_Threshold", reorderThreshold);
                cmd.Parameters.AddWithValue("@Measure_Unit", measureUnit);
                cmd.Parameters.AddWithValue("@Created_By", userId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Handle exception
            }
        }

        public void DeleteItem(int itemId)
        {
            try
            {
                string query = "DELETE FROM IMS_Items WHERE Item_ID = @Item_ID";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.Parameters.AddWithValue("@Item_ID", itemId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Handle exception
            }
        }
    }
}
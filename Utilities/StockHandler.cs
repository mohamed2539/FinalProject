using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Azure;

namespace IT508_Project.Utilities
{
    public class StockHandler
    {
        private readonly string _connectionString;

        public StockHandler(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void InsertLinkItemWh(string itemId, string whId, int quantity, int openBalance, string position)
        {
            string query = @"insert into Stock (Item_ID, WH_ID, Quantity, Open_Balance, Position) values (@Item_ID, @WH_ID, @Quantity, @Open_Balance, @Position)";
            using (SqlConnection sqlcon = new SqlConnection(_connectionString))
            {
                using (SqlCommand sqlcmd = new SqlCommand(query, sqlcon))
                {
                    sqlcmd.Parameters.AddWithValue("@Item_ID", itemId);
                    sqlcmd.Parameters.AddWithValue("@WH_ID", whId);
                    sqlcmd.Parameters.AddWithValue("@Quantity", quantity);
                    sqlcmd.Parameters.AddWithValue("@Open_Balance", openBalance);
                    sqlcmd.Parameters.AddWithValue("@Position", position);
                    sqlcon.Open();
                    sqlcmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateLinkItemWh(string itemId, string whId, int openBalance, string position)
        {
            string query = @"UPDATE Stock SET Open_Balance = @Open_Balance, Position = @Position where Item_ID=@Item_ID and WH_ID=@WH_ID;";
            using (SqlConnection sqlcon = new SqlConnection(_connectionString))
            {
                using (SqlCommand sqlcmd = new SqlCommand(query, sqlcon))
                {
                    sqlcmd.Parameters.AddWithValue("@Item_ID", itemId);
                    sqlcmd.Parameters.AddWithValue("@WH_ID", whId);
                    sqlcmd.Parameters.AddWithValue("@Open_Balance", openBalance);
                    sqlcmd.Parameters.AddWithValue("@Position", position);
                    sqlcon.Open();
                    sqlcmd.ExecuteNonQuery();
                }
            }
        }

        public void DelLinkItemWh(string itemId, string whId)
        {
            string query = "DELETE FROM Stock where Item_ID=@Item_ID and WH_ID=@WH_ID;";
            using (SqlConnection sqlcon = new SqlConnection(_connectionString))
            {
                using (SqlCommand sqlcmd = new SqlCommand(query, sqlcon))
                {
                    sqlcmd.Parameters.AddWithValue("@Item_ID", itemId);
                    sqlcmd.Parameters.AddWithValue("@WH_ID", whId);
                    sqlcon.Open();
                    sqlcmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateInStockQuantity(int itemId, int whId, int addedQuantity, string position)
        {
            using (SqlConnection sqlcon = new SqlConnection(_connectionString))
            {
                sqlcon.Open();

                // Get current quantity
                string getQntyQuery = "SELECT Quantity FROM Stock WHERE Item_ID = @Item_ID and WH_ID = @WH_ID";
                using (SqlCommand getQntyCmd = new SqlCommand(getQntyQuery, sqlcon))
                {
                    getQntyCmd.Parameters.AddWithValue("@Item_ID", itemId);
                    getQntyCmd.Parameters.AddWithValue("@WH_ID", whId);
                    int currentQnty = (int)getQntyCmd.ExecuteScalar();

                    // Calculate updated quantity
                    int updatedQuantity = currentQnty + addedQuantity;

                    // Update stock quantity and position
                    string qntyUpdateQuery = @"UPDATE Stock SET Quantity = @updatedQuantity, Position = @Position WHERE Item_ID = @Item_ID and WH_ID = @WH_ID";
                    using (SqlCommand qntyUpdateCmd = new SqlCommand(qntyUpdateQuery, sqlcon))
                    {
                        qntyUpdateCmd.Parameters.AddWithValue("@Item_ID", itemId);
                        qntyUpdateCmd.Parameters.AddWithValue("@WH_ID", whId);
                        qntyUpdateCmd.Parameters.AddWithValue("@updatedQuantity", updatedQuantity);
                        qntyUpdateCmd.Parameters.AddWithValue("@Position", position);
                        qntyUpdateCmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public void UpdateOutStockQuantity(int itemId, int whId, int disbursedQuantity, string position)
        {
            using (SqlConnection sqlcon = new SqlConnection(_connectionString))
            {
                sqlcon.Open();

                // Get current quantity
                string getQntyQury = "SELECT Quantity FROM Stock WHERE Item_ID = @Item_ID and WH_ID = @WH_ID";
                using (SqlCommand getQntyCmd = new SqlCommand(getQntyQury, sqlcon))
                {
                    getQntyCmd.Parameters.AddWithValue("@Item_ID", itemId);
                    getQntyCmd.Parameters.AddWithValue("@WH_ID", whId);
                    int currentQnty = (int)getQntyCmd.ExecuteScalar();

                    // Check if current quantity is greater than or equal to disbursed quantity
                    if (currentQnty >= disbursedQuantity)
                    {
                        // Calculate updated quantity
                        int updatedQuantity = currentQnty - disbursedQuantity;

                        // Update stock quantity and position
                        string qntyUpdateQury = @"UPDATE Stock SET Quantity = @updatedQuantity, Position = @Position WHERE Item_ID = @Item_ID and WH_ID = @WH_ID";
                        using (SqlCommand qntyUpdateCmd = new SqlCommand(qntyUpdateQury, sqlcon))
                        {
                            qntyUpdateCmd.Parameters.AddWithValue("@Item_ID", itemId);
                            qntyUpdateCmd.Parameters.AddWithValue("@WH_ID", whId);
                            qntyUpdateCmd.Parameters.AddWithValue("@updatedQuantity", updatedQuantity);
                            qntyUpdateCmd.Parameters.AddWithValue("@Position", position);
                            qntyUpdateCmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        // Handle the case where current quantity is less than disbursed quantity
                        throw new InvalidOperationException("Disbursed quantity exceeds current stock quantity.");
                    }
                }
            }
        }

        public void auditItemQuantity(int itemID, int whId, int physicalQty)
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(_connectionString))
                {
                    string updateQuery = "UPDATE Stock SET Quantity = @PhysicalQty WHERE Item_ID = @Item_ID and WH_ID = @WH_ID";
                    sqlcon.Open();
                    SqlCommand cmd = new SqlCommand(updateQuery, sqlcon);
                    cmd.Parameters.AddWithValue("@PhysicalQty", physicalQty);
                    cmd.Parameters.AddWithValue("@Item_ID", itemID);
                    cmd.Parameters.AddWithValue("@WH_ID", whId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
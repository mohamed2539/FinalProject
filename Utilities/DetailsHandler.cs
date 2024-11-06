using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace IT508_Project.Utilities
{
    public class DetailsHandler
    {
        private string connectionString;

        public DetailsHandler(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void InsertTransaction(int serialNum, int transId, int warehouseId, int transTypeId, int itemId, int quantity, string position)
        {
            // Validate Quantity
            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than 0.", nameof(quantity));
            }

            // Proceed with database insertion if validation passes
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                string gvInsert = @"INSERT INTO Transactions (Serial, Trans_ID, WH_ID, Trans_Type_ID, Item_ID, Quantity, Position) 
                            VALUES (@SerialNum, @TransID, @WarehouseID, @TransTypeID, @ItemID, @Quantity, @Position)";
                SqlCommand gvInsertCmd = new SqlCommand(gvInsert, sqlcon);
                gvInsertCmd.Parameters.AddWithValue("@SerialNum", serialNum);
                gvInsertCmd.Parameters.AddWithValue("@TransID", transId);
                gvInsertCmd.Parameters.AddWithValue("@WarehouseID", warehouseId);
                gvInsertCmd.Parameters.AddWithValue("@TransTypeID", transTypeId);
                gvInsertCmd.Parameters.AddWithValue("@ItemID", itemId);
                gvInsertCmd.Parameters.AddWithValue("@Quantity", quantity);
                gvInsertCmd.Parameters.AddWithValue("@Position", position);
                gvInsertCmd.ExecuteNonQuery();
            }
        }

        public void UpdateStockQuantity(int itemId, int whId, int addedQuantity, string position)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                string getQntyQuery = "SELECT Quantity FROM Stock WHERE Item_ID = @Item_ID and WH_ID = @WH_ID";
                SqlCommand getQntyCmd = new SqlCommand(getQntyQuery, sqlcon);
                getQntyCmd.Parameters.AddWithValue("@Item_ID", itemId);
                getQntyCmd.Parameters.AddWithValue("@WH_ID", whId);
                int currentQnty = (int)getQntyCmd.ExecuteScalar();
                int updatedQuantity = currentQnty + addedQuantity;

                string qntyUpdateQuery = @"UPDATE Stock SET Quantity = @updatedQuantity, Position = @Position WHERE Item_ID = @Item_ID and WH_ID = @WH_ID";
                SqlCommand qntyUpdateCmd = new SqlCommand(qntyUpdateQuery, sqlcon);
                qntyUpdateCmd.Parameters.AddWithValue("@Item_ID", itemId);
                qntyUpdateCmd.Parameters.AddWithValue("@WH_ID", whId);
                qntyUpdateCmd.Parameters.AddWithValue("@updatedQuantity", updatedQuantity);
                qntyUpdateCmd.Parameters.AddWithValue("@Position", position);
                qntyUpdateCmd.ExecuteNonQuery();
            }
        }
    }
}
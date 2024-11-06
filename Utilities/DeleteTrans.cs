using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace IT508_Project.Utilities
{
    public class DeleteTrans
    {
        private string connectionString;

        public DeleteTrans(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void DeleteTransaction(string transID, int transTypeID, int warehouseID, GridView gvTransDetails, Label lblSuccessMessage, Label lblErrorMessage)
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    sqlcon.Open();

                    foreach (GridViewRow row in gvTransDetails.Rows)
                    {
                        int serial = Convert.ToInt32(gvTransDetails.DataKeys[row.RowIndex].Values["Serial"]);
                        int itemID = Convert.ToInt32(gvTransDetails.DataKeys[row.RowIndex].Values["Item_ID"]);
                        int rowQuantity = Convert.ToInt32(gvTransDetails.DataKeys[row.RowIndex].Values["Quantity"]);

                        string getQntyQury = "SELECT Quantity FROM Stock WHERE Item_ID = @Item_ID and WH_ID = @WH_ID";
                        SqlCommand getQntyCmd = new SqlCommand(getQntyQury, sqlcon);
                        getQntyCmd.Parameters.AddWithValue("@Item_ID", itemID);
                        getQntyCmd.Parameters.AddWithValue("@WH_ID", warehouseID);
                        int currentQnty = (int)getQntyCmd.ExecuteScalar();

                        int updatedQuantity = (transTypeID <= 40) ? currentQnty - rowQuantity : currentQnty + rowQuantity;

                        string qntyUpdateQury = @"UPDATE Stock SET Quantity = @updatedQuantity WHERE Item_ID = @Item_ID and WH_ID = @WH_ID";
                        SqlCommand qntyUpdateCmd = new SqlCommand(qntyUpdateQury, sqlcon);
                        qntyUpdateCmd.Parameters.AddWithValue("@Item_ID", itemID);
                        qntyUpdateCmd.Parameters.AddWithValue("@WH_ID", warehouseID);
                        qntyUpdateCmd.Parameters.AddWithValue("@updatedQuantity", updatedQuantity);
                        qntyUpdateCmd.ExecuteNonQuery();

                        string deleteQuery = "DELETE FROM Transactions WHERE Serial=@Serial and Trans_ID=@Trans_ID and WH_ID=@WH_ID and Trans_Type_ID=@Trans_Type_ID";
                        SqlCommand cmd = new SqlCommand(deleteQuery, sqlcon);
                        cmd.Parameters.AddWithValue("@Serial", serial);
                        cmd.Parameters.AddWithValue("@Trans_ID", transID);
                        cmd.Parameters.AddWithValue("@WH_ID", warehouseID);
                        cmd.Parameters.AddWithValue("@Trans_Type_ID", transTypeID);
                        cmd.ExecuteNonQuery();
                    }

                    string deleteMasterQury = @"DELETE FROM Tran_Master WHERE Trans_ID = @Trans_ID";
                    SqlCommand deleteMasterCmd = new SqlCommand(deleteMasterQury, sqlcon);
                    deleteMasterCmd.Parameters.AddWithValue("@Trans_ID", transID);
                    deleteMasterCmd.ExecuteNonQuery();

                    lblSuccessMessage.Text = "Transaction Deleted Successfully!";
                    lblErrorMessage.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = "Error: " + ex.Message;
            }
        }
    }
}
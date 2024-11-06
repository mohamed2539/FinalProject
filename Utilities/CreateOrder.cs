using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IT508_Project
{
    public partial class CreateOrder
    {


        //private void InsertOrderDetails()
        //{
        //    //ORDER BY Srial DESC

        //    try
        //    {

        //        SqlConnection mySqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");
        //        string GetLastSerialQuery = @"SELECT TOP 1 Srial FROM OrderDetails WHERE Order_id = @OrderID ORDER BY Srial DESC";
        //        mySqlConnection.Open();
        //        //this to handel with serail increment 

        //        SqlCommand getLastSerialCmd = new SqlCommand(GetLastSerialQuery, mySqlConnection);
        //        getLastSerialCmd.Parameters.AddWithValue("@OrderID", OrderID.Text.Trim());



        //        object lastSerialObj = getLastSerialCmd.ExecuteScalar();
        //        int newSrial = 1; // Default starting value

        //        if (lastSerialObj != null)
        //        {
        //            newSrial = Convert.ToInt32(lastSerialObj) + 1; // Increment the last serial
        //        }

        //        //this [If] check If Quantity is Empty
        //        if (string.IsNullOrWhiteSpace(ItemQuantity.Text))
        //        {
        //            OrderErrorMessage.Text = "All Failds Is Require To Sent Your Requset";
        //            return;
        //        }
        //        else
        //        {

        //            //this if to check [If] Quantity less than Zero
        //            int quantity;
        //            if (!int.TryParse(ItemQuantity.Text.Trim(), out quantity) || quantity <= 0)
        //            {

        //                OrderErrorMessage.Text = "You need to Enter Vaild Quantity";
        //                return;
        //            }
        //            //This else Belong To [If Statement] Which Check Quantity  if less than Zero
        //            else
        //            {

        //                //This If to check quantity before order Created 
        //                //Convert.ToInt32(ItemQuantity.Text)
        //                if (quantity > GetPosStoreQuantity())
        //                {
        //                    OrderErrorMessage.Text = "The Quantity is Out Of Bound";

        //                }
        //                //This else Belong To [If Statement] Which check user quantity if less than Current
        //                //Quantity before order Created
        //                else
        //                {
        //                    //SqlConnection mySqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");
        //                    string OrderDetailsQuery = @"INSERT INTO OrderDetails
        //                                       (Srial,
        //                                        Order_id,
        //                                        Wh_id,
        //                                        trans_type,
        //                                        item_id,
        //                                        Quantity,
        //                                        Price) 
        //                                    VALUES
        //                                       (@OrderSrial, 
        //                                        @OrderID,
        //                                        @Wh_ID,
        //                                        @Trans_Type_Name,
        //                                        @Item_ID,
        //                                        @ItemQuantity,
        //                                        @Price)";
        //                    SqlCommand OrderDetailsQueryCmd = new SqlCommand(OrderDetailsQuery, mySqlConnection);
        //                    OrderDetailsQueryCmd.Parameters.AddWithValue("@OrderSrial"                                     ,newSrial);
        //                    OrderDetailsQueryCmd.Parameters.AddWithValue("@OrderID"                  ,OrderID.Text.ToString().Trim());
        //                    OrderDetailsQueryCmd.Parameters.AddWithValue("@Wh_ID"                      ,Wh_ID.Text.ToString().Trim());
        //                    OrderDetailsQueryCmd.Parameters.AddWithValue("@Trans_Type_Name"  ,Trans_Type_Name.Text.ToString().Trim());
        //                    OrderDetailsQueryCmd.Parameters.AddWithValue("@Item_ID"                  ,Item_ID.Text.ToString().Trim());
        //                    OrderDetailsQueryCmd.Parameters.AddWithValue("@ItemQuantity"        ,ItemQuantity.Text.ToString().Trim());
        //                    OrderDetailsQueryCmd.Parameters.AddWithValue("@Price"                      ,Price.Text.ToString().Trim());
        //                    OrderDetailsQueryCmd.ExecuteNonQuery();

        //                    mySqlConnection.Close();

        //                }

        //            }

        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        // Display the exception message in the label
        //        OrderErrorMessage.Text = $"An error occurred: {ex.Message}";
                
        //    }
            
        //}
        //private int GetPosStoreQuantity()
        //{
        //    SqlConnection mySqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");
        //    string GetCurrentQuantity = @"Select quantity from PosStoree WHERE item_id = @Item_id AND wh_id = @Wh_id";

        //    mySqlConnection.Open();

        //    //This Command to get Quantity and add new Quantity with old to table 
        //    SqlCommand GetQuantityCommand = new SqlCommand(GetCurrentQuantity, mySqlConnection);
        //    GetQuantityCommand.Parameters.AddWithValue("@Item_id", Item_ID.SelectedValue);
        //    GetQuantityCommand.Parameters.AddWithValue("@Wh_id", Wh_ID.SelectedValue);
        //    int CurrentQuantity = Convert.ToInt32(GetQuantityCommand.ExecuteScalar());
        //    GetQuantityCommand.ExecuteNonQuery();

           
        //    return CurrentQuantity;
        //}

    }
}
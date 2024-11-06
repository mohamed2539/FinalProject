using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace IT508_Project
{
    public class CreateOrderDetailsModel
    {
        MyFramework framework = new MyFramework();
        //int OrderId;
        //int Serial;
        //int WhereHouseId;
        //int ItemId;
        //int Transaction;
        //int Quantity;
        //decimal ItemPrice;

        public void InsertOrderInfo(TextBox Serial,TextBox OrderId,DropDownList WhereHouseId,DropDownList trans
                                ,DropDownList ItemList,TextBox ItemQuantity, TextBox ItemPrice ,Label ErrorMessage)
        {


            int PosWhId = framework.PosStoreWHId();

            try
            {
                SqlConnection mySqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");

                //this [If] check If Quantity is Empty
                if (string.IsNullOrWhiteSpace(ItemQuantity.Text))
                {
                    ErrorMessage.Text = "All Failds Is Require To Sent Your Requset";
                    return;
                }
                else
                {
                    //this if to check [If] Quantity less than Zero
                    int CheckQuantity;
                    if (!int.TryParse(ItemQuantity.Text.Trim(), out CheckQuantity) || CheckQuantity <= 0)
                    {
                        ErrorMessage.Text = "You need to Enter Vaild Quantity";
                        return;
                    }
                    //This else Belong To [If Statement] Which Check Quantity  if less than Zero
                    else
                    {
                        //This If to check quantity before order Created 
                        if ( CheckQuantity > framework.GetPosStoreQuantity(ItemList , PosWhId))
                        {
                            ErrorMessage.Text = "The Quantity is Out Of Bound";
                        }
                        //This else Belong To [If Statement] Which check user quantity if less than Current
                        //Quantity before order Created
                        else
                        {
                            //SqlConnection mySqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");
                            string OrderDetailsQuery = @"INSERT INTO OrderDetails
                                   (Srial, Order_id, Wh_id, trans_type, item_id, Quantity, Price) 
                                   VALUES (@OrderSrial, @OrderID, @Wh_ID, @Trans_Type_Name, @Item_ID, @ItemQuantity, @Price)";
                            SqlCommand OrderDetailsQueryCmd = new SqlCommand(OrderDetailsQuery, mySqlConnection);
                            mySqlConnection.Open();
                            OrderDetailsQueryCmd.Parameters.AddWithValue("@OrderSrial",         Serial.Text.ToString().Trim());
                            OrderDetailsQueryCmd.Parameters.AddWithValue("@OrderID",            OrderId.Text.ToString().Trim());
                            OrderDetailsQueryCmd.Parameters.AddWithValue("@Wh_ID",              WhereHouseId.Text.ToString().Trim());
                            OrderDetailsQueryCmd.Parameters.AddWithValue("@Trans_Type_Name",    trans.Text.ToString().Trim());
                            OrderDetailsQueryCmd.Parameters.AddWithValue("@Item_ID",            ItemList.Text.ToString().Trim());
                            OrderDetailsQueryCmd.Parameters.AddWithValue("@ItemQuantity",       ItemQuantity.Text.ToString().Trim());
                            OrderDetailsQueryCmd.Parameters.AddWithValue("@Price",              ItemPrice.Text.ToString().Trim());
                            OrderDetailsQueryCmd.ExecuteNonQuery();

                            mySqlConnection.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Display the exception message in the label
                ErrorMessage.Text = $"An error occurred: {ex.Message}";
            }
        }

        public void UpadateOrderInfo(TextBox Serial, TextBox OrderId, DropDownList WhereHouseId, DropDownList trans
                                , DropDownList ItemList, TextBox ItemQuantity, TextBox ItemPrice, Label ErrorMessage)
        {



            int PosWhId = framework.PosStoreWHId();


            try
            {
                SqlConnection mySqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");

                //this [If] check If Quantity is Empty
                if (string.IsNullOrWhiteSpace(ItemQuantity.Text))
                {
                    ErrorMessage.Text = "All Failds Is Require To Sent Your Requset";
                    return;
                }
                else
                {
                    //this if to check [If] Quantity less than Zero
                    int CheckQuantity;
                    if (!int.TryParse(ItemQuantity.Text.Trim(), out CheckQuantity) || CheckQuantity <= 0)
                    {
                        ErrorMessage.Text = "You need to Enter Vaild Quantity";
                        return;
                    }
                    //This else Belong To [If Statement] Which Check Quantity  if less than Zero
                    else
                    {
                        //This If to check quantity before order Created 
                        if (CheckQuantity > framework.GetPosStoreQuantity(ItemList, PosWhId))
                        {
                            ErrorMessage.Text = "The Quantity is Out Of Bound";
                        }
                        //This else Belong To [If Statement] Which check user quantity if less than Current
                        //Quantity before order Created
                        else
                        {
                            //SqlConnection mySqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");
                    string UpdateDetailsQuery = "UPDATE OrderDetails SET Srial=@OrderSrial, Order_id = @OrderID, " +
                                                                        "Wh_id = @Wh_ID," +
                                                                        "trans_type =@Trans_Type_Name, " +
                                                                        "item_id = @Item_ID ," +
                                                                        "Quantity = @ItemQuantity ," +
                                                                        "Price = @Price" +
                                                                        " WHERE order_id = @OrderId";
                            SqlCommand UpdateDetailsCommand = new SqlCommand(UpdateDetailsQuery, mySqlConnection);
                            mySqlConnection.Open();
                            UpdateDetailsCommand.Parameters.AddWithValue("@OrderSrial",         Serial.Text.ToString().Trim());
                            UpdateDetailsCommand.Parameters.AddWithValue("@OrderID",            OrderId.Text.ToString().Trim());
                            UpdateDetailsCommand.Parameters.AddWithValue("@Wh_ID",              WhereHouseId.SelectedValue.ToString());
                            UpdateDetailsCommand.Parameters.AddWithValue("@Trans_Type_Name",    trans.SelectedValue.ToString());
                            UpdateDetailsCommand.Parameters.AddWithValue("@Item_ID",            ItemList.SelectedValue.ToString());
                            UpdateDetailsCommand.Parameters.AddWithValue("@ItemQuantity",       ItemQuantity.Text.ToString().Trim());
                            UpdateDetailsCommand.Parameters.AddWithValue("@Price",              ItemPrice.Text.ToString().Trim());

                            UpdateDetailsCommand.ExecuteNonQuery();

                            mySqlConnection.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Display the exception message in the label
                ErrorMessage.Text = $"An error occurred: {ex.Message}";
            }
        }

        public void DeleteOrders(int RSerialNum ,int RorderId, int RwhId, int Rtrans)
        {
            // Delete the OrderDetails record
            SqlConnection mySqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");
            string deleteQuery = "DELETE FROM OrderDetails WHERE Srial = @Srial AND Order_id = @Order_id AND Wh_id = @Wh_id AND trans_type=@TransactionType";
            SqlCommand cmd = new SqlCommand(deleteQuery, mySqlConnection);
            mySqlConnection.Open();
            cmd.Parameters.AddWithValue("@Srial", RSerialNum);
            cmd.Parameters.AddWithValue("@Order_id", RorderId);
            cmd.Parameters.AddWithValue("@Wh_id", RwhId);
            cmd.Parameters.AddWithValue("@TransactionType", Rtrans);
            cmd.ExecuteNonQuery();
        }
    }
}
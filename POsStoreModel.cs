using NPOI.SS.Formula.Functions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace IT508_Project
{
    public class POsStoreModel
    {


        // Properties
        public int      PosItemId       { get; set; }
        public int      PoswhId         { get; set; }
        public int      PosOpenBalance  { get; set; }
        public int      PosQuantity     { get; set; }
        public string   PosItemPosition { get; set; }
        public decimal  PosPrice        { get; set; }
        //private SqlConnection DBConnection(string dataSource, string initialCatalog, bool integratedSecurity, bool trustServerCertificate, bool encrypt)
        //{
        //    string connectionString = $@"Data Source                ={dataSource}; 
        //                                    Initial Catalog         ={initialCatalog};
        //                                    Integrated Security     ={integratedSecurity};
        //                                    TrustServerCertificate  ={trustServerCertificate};
        //                                    Encrypt={encrypt};";
        //    return new SqlConnection(connectionString);



        //}

           
        }

        public class POsStoreManagerModel {

        MyFramework framework = new MyFramework();

        private string DBStringConnection;
        public POsStoreManagerModel() { /*this.DBStringConnection = Connection; */}
        public void InsertPosInfo(DropDownList PoItemId, DropDownList PowhId, TextBox PoOpenBalance,TextBox PosQty
                        , TextBox PosPostion,TextBox Price,Label ErrorMessage)

        {

            framework.PosItemExist(PoItemId, PoOpenBalance, PosQty, PosPostion, ErrorMessage);

            try
            {

                SqlConnection mySqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDBI; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");


                //this [If] check If Quantity is Empty
                if (string.IsNullOrWhiteSpace(PosQty.Text))
                {
                    ErrorMessage.Text = "All Failds Is Require To Sent Your Requset";
                    return;
                }
                else
                {

                    //this if to check [If] Quantity less than Zero
                    int CheckQuantity;
                    if (!int.TryParse(PosQty.Text.Trim(), out CheckQuantity) || CheckQuantity <= 0)
                    {

                        ErrorMessage.Text = "You need to Enter Vaild Quantity";
                        return;
                    }
                    //This else Belong To [If Statement] Which Check Quantity  if less than Zero
                    else
                    {

                        //This else Belong To [If Statement] Which check user quantity if less than Current
                        //Quantity before order Created
     
                            //SqlConnection mySqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDBI; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");
                            string POsInsertQuery = @"INSERT INTO PosStoree (item_id,
                                                         wh_id,
                                                        Open_Balance,
                                                        quantity,
                                                        Item_Position,
                                                        price) 
                                                  VALUES 
                                                        (@Item_id,
                                                         @Wh_id,
                                                         @OpenBalance,
                                                         @Quantity,
                                                         @ItemPosition,
                                                         @ItemPrice)";
                            SqlCommand InsertCommand = new SqlCommand(POsInsertQuery, mySqlConnection);
                            mySqlConnection.Open();
                            InsertCommand.Parameters.AddWithValue("@Item_id",           PoItemId.SelectedValue);
                            InsertCommand.Parameters.AddWithValue("@Wh_id",             PowhId.SelectedValue);
                            InsertCommand.Parameters.AddWithValue("@OpenBalance",       PoOpenBalance.Text.Trim());
                            InsertCommand.Parameters.AddWithValue("@Quantity",          PosQty.Text.Trim());
                            InsertCommand.Parameters.AddWithValue("@ItemPosition",      PosPostion.Text.Trim());
                            InsertCommand.Parameters.AddWithValue("@ItemPrice",         Price.Text.Trim());
                            InsertCommand.ExecuteNonQuery();
                            mySqlConnection.Close();

                        }

                    }
            }
            catch (Exception ex)
            {
                // Display the exception message in the label
                ErrorMessage.Text = $"An error occurred: {ex.Message}";

            }

            
        }


        public void UpadateOrderInfo(DropDownList PoItemId, DropDownList PowhId, TextBox PoOpenBalance, TextBox PosQty
                        , TextBox PosPostion, TextBox Price, Label ErrorMessage)


        {
            try
            {

                SqlConnection mySqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDBI; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");


                //this [If] check If Quantity is Empty
                if (string.IsNullOrWhiteSpace(PosQty.Text))
                {
                    ErrorMessage.Text = "All Failds Is Require To Sent Your Requset";
                    return;
                }
                else
                {

                    //this if to check [If] Quantity less than Zero
                    int CheckQuantity;
                    if (!int.TryParse(PosQty.Text.Trim(), out CheckQuantity) || CheckQuantity <= 0)
                    {

                        ErrorMessage.Text = "You need to Enter Vaild Quantity";
                        return;
                    }
                    //This else Belong To [If Statement] Which Check Quantity  if less than Zero
                    else
                    {

                        SqlConnection UpdateSqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDBI; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");
                        string UpdatePOsStore = @"UPDATE POSstoree SET  item_id = @itemId,
                                                                wh_id           = @whId,
                                                                Open_Balance    = @OpenBalance,
                                                                quantity        = @quantity,
                                                                Item_Position   = @ItemPosition,
                                                                Price           = @ItemPrice
                                                              WHERE 
                                                                item_id         = @itemId 
                                                              AND 
                                                                wh_id           = @whId";
                        SqlCommand UpdatePOsStoreCommand = new SqlCommand(UpdatePOsStore, UpdateSqlConnection);
                        UpdateSqlConnection.Open();
                        UpdatePOsStoreCommand.Parameters.AddWithValue("@Item_id",       PoItemId.SelectedValue);
                        UpdatePOsStoreCommand.Parameters.AddWithValue("@Wh_id",         PowhId.SelectedValue);
                        UpdatePOsStoreCommand.Parameters.AddWithValue("@OpenBalance",   PoOpenBalance.Text.Trim());
                        UpdatePOsStoreCommand.Parameters.AddWithValue("@Quantity",      PosQty.Text.Trim());
                        UpdatePOsStoreCommand.Parameters.AddWithValue("@ItemPosition",  PosPostion.Text.Trim());
                        UpdatePOsStoreCommand.Parameters.AddWithValue("@ItemPrice",     Price.Text.Trim());
                        UpdatePOsStoreCommand.ExecuteNonQuery();

                            mySqlConnection.Close();

                        }   

                }


            }
            catch (Exception ex)
            {
                // Display the exception message in the label
                ErrorMessage.Text = $"An error occurred: {ex.Message}";

            }

        }



        public void DeleteOrders(DropDownList PoItemId, DropDownList PowhId)
        {

            // Delete the OrderDetails record
            SqlConnection mySqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDBI; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");
            string deleteQuery = "DELETE FROM POSstoree WHERE item_id = @itemId AND wh_id = @whId";
            SqlCommand DeletePosCommand = new SqlCommand(deleteQuery, mySqlConnection);
            mySqlConnection.Open();
            DeletePosCommand.Parameters.AddWithValue("@Item_id",    PoItemId.SelectedValue);
            DeletePosCommand.Parameters.AddWithValue("@Wh_id",      PowhId.SelectedValue);
            DeletePosCommand.ExecuteNonQuery();

        }
    }
}
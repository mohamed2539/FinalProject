using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IT508_Project
{
    public partial class SaleByOneStore : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void OnePiceButton_Click(object sender, EventArgs e)
        {
            bool itemExists = false;
            string ReturendItemId = "";
            string ReturendwHId = "";
            SqlConnection mySqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDBI; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");
            string SelectItemIdQuery = "Select item_id , wh_id from PosStoree";

            mySqlConnection.Open();
            //This Command to insert data in database 
            SqlCommand SelectItemIdCommand = new SqlCommand(SelectItemIdQuery, mySqlConnection);
            SqlDataReader ItemIdReader = SelectItemIdCommand.ExecuteReader();
            //this while to Read Item id 


            while (ItemIdReader.Read())
            {
                ReturendItemId  = ItemIdReader["item_id"].ToString();
                ReturendwHId    = ItemIdReader["wh_id"].ToString();

                if (Item_id.SelectedValue.Equals(ReturendItemId)&& Wh_id.SelectedValue.Equals(ReturendwHId))
                {

                    ErrorMessage.Text = "This item Is already exist you can only add Quantity to it ";
                    itemExists = true;
                    break; // Exit if found
                }
            }
            ItemIdReader.Close();


            if (!itemExists)
            {
            //This [if] for if item id is exist
           
                //Start code to insert data in store
                if (OpenBalance.Text.Equals("") ||
                    Quantity.Text.Equals("") ||
                    ItemPosition.Text.Equals(""))
                {
                    ErrorMessage.Text = "You Should Fill All Fileds ";

                }
                else
                {

                    //this if to check [If] Quantity less than Zero
                    int CheckOpenBalance, CheckQuantity, CheckPrice;
                    if (!int.TryParse(OpenBalance.Text.Trim(), out CheckOpenBalance) || CheckOpenBalance <= 0){



                        ErrorMessage.Text = "You need to Enter Vaild Number ";
                        OpenBalance.Text = "";
                        Quantity.Text = "";
                        ItemPrice.Text = "";
                        return;

                    }
                    if (!int.TryParse(Quantity.Text.Trim(), out CheckQuantity) || CheckQuantity <= 0)
                    {
                        ErrorMessage.Text = "You need to Enter Vaild Quantity";
                        OpenBalance.Text = "";
                        Quantity.Text = "";
                        ItemPrice.Text = "";
                        return;
                    }
                    if (!int.TryParse(ItemPrice.Text.Trim(), out CheckPrice) || CheckPrice <= 0)
                    {

                        ErrorMessage.Text = "You need to Enter Vaild Price";
                        OpenBalance.Text = "";
                        Quantity.Text = "";
                        ItemPrice.Text = "";
                        return;
                    }

                    

                        string query = @"INSERT INTO PosStoree (item_id,
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
                    //mySqlConnection.Open();

                    //This Command to insert data in database 
                    SqlCommand InsertCommand = new SqlCommand(query, mySqlConnection);

                    InsertCommand.Parameters.AddWithValue("@Item_id"        ,Item_id.Text.Trim());
                    InsertCommand.Parameters.AddWithValue("@Wh_id"          ,Wh_id.Text.Trim());
                    InsertCommand.Parameters.AddWithValue("@OpenBalance"    ,OpenBalance.Text.Trim());
                    InsertCommand.Parameters.AddWithValue("@Quantity"       ,Quantity.Text.Trim());
                    InsertCommand.Parameters.AddWithValue("@ItemPosition"   ,ItemPosition.Text.Trim());
                    InsertCommand.Parameters.AddWithValue("@ItemPrice"      ,ItemPrice.Text.Trim());
                    InsertCommand.ExecuteNonQuery();




                    ErrorMessage.Text = "";
                    SuccessMessage.Text = "New Item Inserted ... ";
                    OpenBalance.Text = "";
                    Quantity.Text = "";
                    ItemPosition.Text = "";
                    ItemPrice.Text = "";
                    string script = "setTimeout(function() { window.location.href = 'SaleByOneStore.aspx'; }, 1500);"; // 3000 مللي ثانية
                    ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);

                }

            
            mySqlConnection.Close();
        }
        }

        protected void ShowAllStoreData_Click(object sender, EventArgs e)
        {
            string Username = Session["username"].ToString();
            MyFramework framework = new MyFramework();
            framework.CheckUserRole(Username , "ShowPOSStoreData", ErrorMessage);

            //Response.Redirect("ShowPOSStoreData.aspx");
        }

        protected void IcrementButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("POSIncoming.aspx");
        }




       


    }
}


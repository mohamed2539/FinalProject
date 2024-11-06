using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IT508_Project
{
    public partial class EditPOSStoreData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) // Change to !IsPostBack
            {
                if (Request.QueryString["ItemID"] != null && Request.QueryString["WhID"] != null)
                {
                    int PosItemId = Convert.ToInt32(Request.QueryString["ItemID"]);
                    int PosWhId = Convert.ToInt32(Request.QueryString["WhID"]);
                    LoadDropDownLists(PosItemId, PosWhId);
                    loadPOsStoreInfo(PosItemId, PosWhId);
                }
            }
        }

        protected void loadPOsStoreInfo(int PositemId, int PosWhId)
        {
            SqlConnection PosStoreSqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");
            string PosStorInfo = @"SELECT * FROM PosStoree WHERE item_id=@ItemId and wh_id=@whId";
            PosStoreSqlConnection.Open();
            SqlCommand PosStoreCommand = new SqlCommand(PosStorInfo, PosStoreSqlConnection);
            PosStoreCommand.Parameters.AddWithValue("@ItemId", PositemId);
            PosStoreCommand.Parameters.AddWithValue("@whId", PosWhId);
            SqlDataReader PosStoreRead = PosStoreCommand.ExecuteReader();

            if (PosStoreRead.Read())
            {
                ItemId.SelectedValue = PosStoreRead["item_id"].ToString();
                whId.SelectedValue = PosStoreRead["wh_id"].ToString();
                OpenBalance.Text = PosStoreRead["Open_Balance"].ToString();
                quantity.Text = PosStoreRead["quantity"].ToString();
                ItemPosition.Text = PosStoreRead["Item_Position"].ToString();
                Price.Text = PosStoreRead["Price"].ToString();

                //Response.Write(ItemId.SelectedValue, whId.SelectedValue, OpenBalance.Text);
            }
            PosStoreRead.Close();
            PosStoreSqlConnection.Close();
        }

        private void LoadDropDownLists(int listItemId, int listWhId)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=localhost\SQLEXPRESS; Initial Catalog=IMSDB; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");
            // WHERE item_id = @ItemId and wh_id=@whId
            // this DropDownList to load (ItemId)
            string itemQuery = "SELECT item_id FROM PosStoree WHERE item_id = @ItemId and wh_id=@whId";
            SqlCommand itemCommand = new SqlCommand(itemQuery, connection);
            itemCommand.Parameters.AddWithValue("@ItemId", listItemId);
            itemCommand.Parameters.AddWithValue("@whId", listWhId);

            connection.Open();
            SqlDataReader itemReader = itemCommand.ExecuteReader();
            ItemId.DataSource = itemReader;
            ItemId.DataTextField = "item_id";
            ItemId.DataValueField = "item_id";
            ItemId.DataBind();
            itemReader.Close();

            // this DropDownList to load (whId)
            string warehouseQuery = "SELECT wh_id FROM PosStoree WHERE item_id = @ItemId and wh_id=@whId";
            SqlCommand warehouseCommand = new SqlCommand(warehouseQuery, connection);
            warehouseCommand.Parameters.AddWithValue("@ItemId", listItemId);
            warehouseCommand.Parameters.AddWithValue("@whId", listWhId);
            SqlDataReader warehouseReader = warehouseCommand.ExecuteReader();
            whId.DataSource = warehouseReader;
            whId.DataTextField = "wh_id";
            whId.DataValueField = "wh_id";
            whId.DataBind();
            warehouseReader.Close();
            connection.Close();
        }

        protected void ClearFields()
        {
            OpenBalance.Text = "";
            quantity.Text = "";
            ItemPosition.Text = "";
            Price.Text = "";
        }
        protected void UpdatePOsStoreData()
        {
            MyFramework framework = new MyFramework();
            // Call the validation method
            if (!framework.ValidateInputs(OpenBalance, quantity, ItemPosition, Price, ErrorValidNumber))
            {
                string script = "setTimeout(function() { window.location.href = 'ShowPOSStoreData.aspx'; }, 1000);";
                ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);

                return; // Stop further processing
            }
            else
            {
                //Check Vaild Values

                /*****************************/

                SqlConnection UpdateSqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");
                string UpdatePOsStore = @"UPDATE POSstoree SET  item_id          = @itemId,
                                                                wh_id           = @whId,
                                                                Open_Balance    = @OpenBalance,
                                                                quantity        = @quantity,
                                                                Item_Position   = @ItemPosition,
                                                                Price           = @ItemPrice
                                                              WHERE 
                                                                item_id         = @itemId 
                                                              AND 
                                                                wh_id           = @whId";

                // Create a new command for the update
                SqlCommand UpdatePOsStoreCommand = new SqlCommand(UpdatePOsStore, UpdateSqlConnection);
                UpdateSqlConnection.Open();
                UpdatePOsStoreCommand.Parameters.AddWithValue("@itemId", ItemId.SelectedValue);
                UpdatePOsStoreCommand.Parameters.AddWithValue("@whId", whId.SelectedValue);
                UpdatePOsStoreCommand.Parameters.AddWithValue("@OpenBalance", OpenBalance.Text.ToString().Trim());
                UpdatePOsStoreCommand.Parameters.AddWithValue("@quantity", quantity.Text.ToString().Trim());
                UpdatePOsStoreCommand.Parameters.AddWithValue("@ItemPosition", ItemPosition.Text.ToString().Trim());
                UpdatePOsStoreCommand.Parameters.AddWithValue("@ItemPrice", Price.Text.ToString().Trim());

                try
                {
                    // Execute the update command
                    int rowsAffected = UpdatePOsStoreCommand.ExecuteNonQuery();
                    //Response.Write(rowsAffected);
                    if (rowsAffected > 0)
                    {
                        UpdatePOsStoreLabel.Text = "Customer Info Is Updeted Successfully ";

                        string script = "setTimeout(function() { window.location.href = 'ShowPOSStoreData.aspx'; }, 1500);";
                        ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);

                        //Response.Redirect("ShowPOSStoreData.aspx");
                    }
                    else
                    {
                        UpdatePOsStoreLabel.Text = "Some Error Happens You need to try Again";
                        ClearFields();
                    }
                }
                catch (SqlException ex)
                {
                    UpdatePOsStoreLabel.Text = $"Database error: {ex.Message}";
                    ClearFields();
                }
                catch (Exception ex)
                {
                    UpdatePOsStoreLabel.Text = $"An error occurred: {ex.Message}";
                    ClearFields();
                }
                finally
                {
                    UpdateSqlConnection.Close();
                }
            }
        }

        protected void POsStoreButton_Click(object sender, EventArgs e)
        {
            UpdatePOsStoreData();
            ClearFields();
        }
    }
}
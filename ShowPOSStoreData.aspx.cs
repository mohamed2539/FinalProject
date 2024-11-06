using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Web.HttpContext.Current.Response.Redirect;

namespace IT508_Project
{
    public partial class ShowPOSStoreData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowPosShowData();
        }


        protected void ShowPosShowData()
        {

            SqlConnection POSStoreeSqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDBI; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");
            string POSStoreeInfo = @"SELECT * FROM POSStoree";
            POSStoreeSqlConnection.Open();
            SqlCommand POSStoreeCommand = new SqlCommand(POSStoreeInfo, POSStoreeSqlConnection);
            SqlDataReader POSStoreeRead = POSStoreeCommand.ExecuteReader();

            //This Tow line to link Posstore Table data with Graid View
            ShowPosStoresData.DataSource = POSStoreeRead;
            ShowPosStoresData.DataBind();
           POSStoreeRead.Close();

        }


        protected void ClickedRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Response.Redirect("ShowPOSStoreData.aspx");
        }

        protected void EditOrDelete_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Edit")
            {
                // Get the row index from the CommandArgument
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                // Ensure the index is within the valid range
                if (rowIndex >= 0 && rowIndex < ShowPosStoresData.Rows.Count)
                {
                    // Retrieve the Order ID from the DataKeys
                    int PosItemId = Convert.ToInt32(ShowPosStoresData.DataKeys[rowIndex].Values["item_id"]);
                    int PosIWhID = Convert.ToInt32(ShowPosStoresData.DataKeys[rowIndex].Values["wh_id"]);

                    Response.Redirect($"EditPOSStoreData.aspx?ItemID={PosItemId}&WhID={PosIWhID}");

                }
            }
            else if (e.CommandName == "Delete")
            {
                // Get the row index from the CommandArgument
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                // Ensure the index is within the valid range
                if (rowIndex >= 0 && rowIndex < ShowPosStoresData.Rows.Count)
                {
                    // Retrieve the Order ID from the DataKeys
                    int PosItemId = Convert.ToInt32(ShowPosStoresData.DataKeys[rowIndex].Values["item_id"]);
                    int PosIWhID = Convert.ToInt32(ShowPosStoresData.DataKeys[rowIndex].Values["wh_id"]);


                    // Add your delete logic here
                    SqlConnection mySqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDBI; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");
                    string deleteQuery = "DELETE FROM POSStoree WHERE item_id = @item_id and wh_id=@wh_id";
                    SqlCommand cmd = new SqlCommand(deleteQuery, mySqlConnection);
                    cmd.Parameters.AddWithValue("@item_id", PosItemId);
                    cmd.Parameters.AddWithValue("@wh_id", PosIWhID);
                    mySqlConnection.Open();
                    cmd.ExecuteNonQuery();

                }
            }
        }

        protected void BackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("SaleByOneStore.aspx");
        }
    }
}
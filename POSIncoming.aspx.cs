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
    public partial class POSIncoming : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void AddQuantity_Click(object sender, EventArgs e)
        {
            int incrementalQuantity;

            SqlConnection mySqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDBI; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");
            string GetOpenBalancequery = @"Select quantity from PosStoree WHERE item_id = @Item_id AND wh_id = @Wh_id";

            mySqlConnection.Open();

            //This Command to get Quantity and add new Quantity with old to table 
            SqlCommand OpenBalancCommand = new SqlCommand(GetOpenBalancequery, mySqlConnection);
            OpenBalancCommand.Parameters.AddWithValue("@Item_id", IncomingItemID.Text.Trim());
            OpenBalancCommand.Parameters.AddWithValue("@Wh_id", IncomingWhID.Text.Trim());
            //object result = OpenBalancCommand.ExecuteScalar();
            int currentOpenBalance = (int)OpenBalancCommand.ExecuteScalar();

            // Coluct new Value to Quantity Column
            int newOpenBalance = currentOpenBalance + int.Parse(IncreemntalQuantity.Text.Trim());

            if (!int.TryParse(IncreemntalQuantity.Text.Trim(), out incrementalQuantity) || incrementalQuantity <= 0)
            {
                IncomingPosMessage.Text = "You Must Enter Value greater than Zerooo ";
                return;
            }else
            {

                string updateQuery = @"UPDATE PosStoree SET quantity = @NewOpenBalance WHERE item_id = @Item_id AND wh_id = @Wh_id";
                SqlCommand updateCommand = new SqlCommand(updateQuery, mySqlConnection);
                updateCommand.Parameters.AddWithValue("@NewOpenBalance", newOpenBalance);
                updateCommand.Parameters.AddWithValue("@Item_id", IncomingItemID.Text.Trim());
                updateCommand.Parameters.AddWithValue("@Wh_id", IncomingWhID.Text.Trim());
                updateCommand.ExecuteNonQuery();
                IncomingPosMessage.Text = "New Quantity has bean added to old Quantity ";
                string script = "setTimeout(function() { window.location.href = 'SaleByOneStore.aspx'; }, 1500);"; // 1.5 مللي ثانية
                ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                return;
            }

            //string script = $@"
            //                    console.log('Current Open Balance: {currentOpenBalance}');
            //                    console.log('Incremental Quantity: {IncreemntalQuantity.Text.Trim()}');
            //                    console.log('New Open Balance: {newOpenBalance}');
            //                ";
            //ClientScript.RegisterStartupScript(this.GetType(), "DebugInfo", script, true);


        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace IT508_Project
{
    public partial class ShowAllOrders
    {
        public class OrderDetail
        {
            public int SerialInfo { get; set; }
            public int OrderIDinfo { get; set; }
            public int Wh_idinfo { get; set; }
            public int trans_typeInfo { get; set; }
            public int item_idInfo { get; set; }
            public int QuantityInfo { get; set; }
            public decimal PriceInfo { get; set; }
        }

        public ShowAllOrders() { }

        protected void BindDataWithGraid()
        {

            SqlConnection mySqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");
            string SelectOrders = @"Select *  from OrderHeader";
            SqlCommand SelectCommand = new SqlCommand(SelectOrders, mySqlConnection);
            //SelectCommand.Parameters.AddWithValue("@Order_id", Order_id);
            mySqlConnection.Open();

            SqlDataReader reader = SelectCommand.ExecuteReader();
            GridViewOrders.DataSource = reader;
            GridViewOrders.DataBind();



        }
        protected void BindOrderDetailsWithGraid()
        {

            SqlConnection mySqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");
            string SelectOrders = @"Select *  from OrderDetails";
            SqlCommand SelectCommand = new SqlCommand(SelectOrders, mySqlConnection);
            //SelectCommand.Parameters.AddWithValue("@Order_id", Order_id);
            mySqlConnection.Open();

            SqlDataReader reader = SelectCommand.ExecuteReader();
            OrderDetailsGrid.DataSource = reader;
            OrderDetailsGrid.DataBind();

        }

         //this method for Edit and delete Button
        protected void GraidRowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Edit")
            {
                // Get the row index from the CommandArgument
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                // Ensure the index is within the valid range
                if (rowIndex >= 0 && rowIndex < GridViewOrders.Rows.Count)
                {
                    // Retrieve the Order ID from the DataKeys
                    int orderId = Convert.ToInt32(GridViewOrders.DataKeys[rowIndex].Value);
                    int whId = Convert.ToInt32(GridViewOrders.DataKeys[rowIndex]["Wh_id"]);
                    int transactionType = Convert.ToInt32(GridViewOrders.DataKeys[rowIndex]["transaction_type"]);
                    Response.Redirect($"EditOrderDetail.aspx?id={orderId}&whId={whId}&transactionType={transactionType}");
                }
            }
            else if (e.CommandName == "Delete")
            {
                // Get the row index from the CommandArgument
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                // Ensure the index is within the valid range
                if (rowIndex >= 0 && rowIndex < GridViewOrders.Rows.Count)
                {
                    // Retrieve the Order ID from the DataKeys
                    int orderId = Convert.ToInt32(GridViewOrders.DataKeys[rowIndex].Value);

                    // Add your delete logic here
                    SqlConnection mySqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");
                    string deleteQuery = "DELETE FROM OrderDetails WHERE Order_id = @Order_id";
                    SqlCommand cmd = new SqlCommand(deleteQuery, mySqlConnection);
                    cmd.Parameters.AddWithValue("@Order_id", orderId);
                    mySqlConnection.Open();
                    cmd.ExecuteNonQuery();


                    // Rebind the grid to reflect changes
                    BindGrid();
                }
            }

        }





    }
}
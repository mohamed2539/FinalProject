using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IT508_Project
{
    public partial class ShowAllCustomer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                if (!IsPostBack)
                {
                    BindCustomerDataWithGraid();
                }
        }

        // this Method to Baind GraidView With DataBase Data
        protected void BindCustomerDataWithGraid()
        {
            SqlConnection mySqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");
            string SelectOrders = @"Select *  from POSCustomer";
            SqlCommand SelectCommand = new SqlCommand(SelectOrders, mySqlConnection);
            mySqlConnection.Open();

            SqlDataReader reader = SelectCommand.ExecuteReader();
            ShowCustomerGraid.DataSource = reader;
            ShowCustomerGraid.DataBind();
        }

        private void DeleteCustomer(int customerId)
        {
            try
            {
                SqlConnection mySqlConnection = new SqlConnection(@"Data Source=localhost\SQLEXPRESS; Initial Catalog=IMSDB; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");
                string DeleteQuery = "DELETE FROM POSCustomer WHERE Customer_id = @Customer_id";

                    SqlCommand DeleteCommand = new SqlCommand(DeleteQuery, mySqlConnection);
                        DeleteCommand.Parameters.AddWithValue("@Customer_id", customerId);
                        mySqlConnection.Open();
                        DeleteCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                DeleteCustomerErrorMessage.Text = "There is Error it Happened  You Can\'t Delete This Customer ... ";
            }
        }

        protected void ShowCustomer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                // Get the row index from the CommandArgument
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                // Ensure the index is within the valid range
                if (rowIndex >= 0 && rowIndex < ShowCustomerGraid.Rows.Count)
                {  // Retrieve the Order ID from the DataKeys
                    int CustomerID = Convert.ToInt32(ShowCustomerGraid.DataKeys[rowIndex].Value);
                    Response.Redirect($"EditCustomer.aspx?id={CustomerID}");
                }
            }
            else if (e.CommandName == "Delete")
            {
                 int rowIndex = Convert.ToInt32(e.CommandArgument);

                // Ensure the index is within the valid range
                if (rowIndex >= 0 && rowIndex < ShowCustomerGraid.Rows.Count)
                {
                    // Retrieve the Order ID from the DataKeys
                    int CustomerId = Convert.ToInt32(ShowCustomerGraid.DataKeys[rowIndex].Value);
                    DeleteCustomer(CustomerId);
                    // Rebind the grid to reflect changes
                    BindCustomerDataWithGraid();
                }
            }
        }

        protected void ClickedRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
           
        }
    }
}
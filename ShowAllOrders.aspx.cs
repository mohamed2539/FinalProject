using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IT508_Project
{
    public partial class ShowAllOrders : System.Web.UI.Page
    {
        CreateOrderDetailsModel OrderModel = new CreateOrderDetailsModel();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
                BindGridDetails();
            }

        }


        private void BindGrid()
        {
            BindDataWithGraid();
           
        }  
        private void BindGridDetails()
        {
            BindOrderDetailsWithGraid();

        }

        protected void GridViewOrders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GraidRowCommand( sender, e);
        }


        //This Just To Avoid Redirect to Error Page To Delete Record Without Any Problem
        protected void ClickedRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //This is Empty Mthod Just To Solve Deleteing  Error 
        }

        protected void ClickedOrderHeaderRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //This is Empty Mthod Just To Solve Deleteing  Error 
        }
        protected void OrderDetailsGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {               

            if (e.CommandName == "Edit")
            {

                // Get the row index from the CommandArgument
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                // Ensure the index is within the valid range
                if (rowIndex >= 0 && rowIndex < OrderDetailsGrid.Rows.Count)
                {
                    // Retrieve the Order ID from the DataKeys
                    int SerialNum = Convert.ToInt32(OrderDetailsGrid.DataKeys[rowIndex]["Srial"]);
                    int orderId = Convert.ToInt32(OrderDetailsGrid.DataKeys[rowIndex]["Order_id"]);
                    int whId = Convert.ToInt32(OrderDetailsGrid.DataKeys[rowIndex]["Wh_id"]);
                    int transactionType = Convert.ToInt32(OrderDetailsGrid.DataKeys[rowIndex]["trans_type"]);

                    Response.Redirect($"EditOrderDetail.aspx?Serial={SerialNum}&id={orderId}&whId={whId}&transactionType={transactionType}");               

                }
            }
            else if (e.CommandName == "Delete")
            {
                // Get the row index from the CommandArgument
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                if (rowIndex >= 0 && rowIndex < OrderDetailsGrid.Rows.Count)
                {
                    int SerialNum = Convert.ToInt32(OrderDetailsGrid.DataKeys[rowIndex]["Srial"]);
                    int orderId = Convert.ToInt32(OrderDetailsGrid.DataKeys[rowIndex]["Order_id"]);
                    int whId = Convert.ToInt32(OrderDetailsGrid.DataKeys[rowIndex]["Wh_id"]);
                    int transactionType = Convert.ToInt32(OrderDetailsGrid.DataKeys[rowIndex]["trans_type"]);


                    OrderModel.DeleteOrders(SerialNum, orderId, whId, transactionType);


                    // Rebind the grid to reflect changes
                    BindGridDetails();
                }


            }
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static IT508_Project.CreateOrder;
using static NPOI.HSSF.Util.HSSFColor;

namespace IT508_Project
{
    public partial class CreateOrder : System.Web.UI.Page
    {
        MyFramework framework = new MyFramework();
        CreateOrderDetailsModel OrderMoldel = new CreateOrderDetailsModel();
        OrderHeaderModel        HeaderMoldel= new OrderHeaderModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoginUsername.Text = (string)Session["username"];
                FillDropList();
                GetHeaderTableOrdeID();
                //AutoIcreementSerail();
                //framework.GetOrderDetailsSerial(OrderSrial);
            }

        }

        //protected void CreateOrder_Click(object sender, EventArgs e){}

        // This fun to get Last order_id and get Time to show it in TextBox in order header  
        private void GetHeaderTableOrdeID()
        {
            HeaderMoldel.SetIdTimeInTextBox(OrderID , OrderTime);
        }
        // This fun to Serail in set it in TextBox
       
        protected void CreateOrderButton_Click(object sender, EventArgs e)
        {
            if (!IsOrderIDExists(OrderID.Text.Trim()))
            {
                OrderErrorMessage.Text = "Order ID does not exist. Please create the order header first.";
                return;
            }
            else
            {
                OrderMoldel.InsertOrderInfo(OrderSrial, OrderID, Wh_ID, Trans_Type_Name, Item_ID, ItemQuantity, Price, OrderErrorMessage);
                //InsertedMessage.Text = "Order Details Data Is Inserted . . . ";
                UpdataPosStoreQuantity();
                GenerateInvoice();
                //int InoviceId = Convert.ToInt32(OrderID.Text);
                //framework.FillInvoiceGrid(InvoiceLabelGrid, InoviceId);
                ItemQuantity.Text = "";
                Price.Text = "";
            }
        }

        private bool IsOrderIDExists(string orderId)
        {
            SqlConnection mySqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");
            string query = "SELECT COUNT(*) FROM OrderHeader WHERE order_id = @OrderID";
            SqlCommand cmd = new SqlCommand(query, mySqlConnection);
            cmd.Parameters.AddWithValue("@OrderID", orderId);
            mySqlConnection.Open();
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            return count > 0;
        }

        protected void SaveOrderHeader_Click(object sender, EventArgs e)
        {
            HeaderMoldel.InsertOrderHeader(OrderID, Wh_ID, Trans_Type_Name, Customer_id, LoginUsername);
            //InsertedMessage.Text = "Header Data Is Inserted . . . ";
            
            //InsertedMessage.Text = "";
        }

        // This Method to check Current Quantity and update it after order
        private void UpdataPosStoreQuantity()
        {
            framework.CalculateQtyAfterOrder(Item_ID,Wh_ID, ItemQuantity, OrderErrorMessage);
        }

        private decimal GetPrice(int itemid)
        {
            return framework.GetItemsPrice(itemid);
        }

        protected void Item_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            framework.GetPriceToSelectedItem(Item_ID, Price);
        }

        /*public void AutoIcreementSerail()
        {
            framework.IncrementSerial(OrderSrial);
        }*/
                               
        protected void FillDropList()
        {
            framework.FileItemsList(Item_ID);
        }

        private void GenerateInvoice()
        {
            SqlConnection mySqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");

            string orderId = OrderID.Text.Trim();
            string query = @"SELECT * FROM OrderHeader WHERE Order_id = @OrderID";
            SqlCommand InvoiceCommand = new SqlCommand(query, mySqlConnection);
            InvoiceCommand.Parameters.AddWithValue("@OrderID", orderId);
            mySqlConnection.Open();

            SqlDataReader reader = InvoiceCommand.ExecuteReader();
            if (reader.Read())
            {
                // Collect Order Data
                string whId = reader["wh_id"].ToString();
                string transType = reader["transaction_type"].ToString();
                string customerId = reader["Customer_id"].ToString();
                DateTime orderDate = Convert.ToDateTime(reader["order_date"]);
                string createdBy = reader["created_by"].ToString();

                // Show Data in invoice
                InvoiceLabel.Text += $"Order ID: {orderId}<br/>";
                InvoiceLabel.Text += $"Warehouse ID: {whId}<br/>";
                InvoiceLabel.Text += $"Transaction Type: {transType}<br/>";
                InvoiceLabel.Text += $"Customer ID: {customerId}<br/>";
                InvoiceLabel.Text += $"Order Date: {orderDate}<br/>";
                InvoiceLabel.Text += $"Created By: {createdBy}<br/>";
            }

            // Show Order Details Data
            DisplayOrderDetails(orderId);
        }

        private void DisplayOrderDetails(string orderId)
        {
            SqlConnection mySqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");

            string query = @"SELECT * FROM OrderDetails WHERE Order_id = @OrderID";

            SqlCommand DisplayInvoiceCommand = new SqlCommand(query, mySqlConnection);
            DisplayInvoiceCommand.Parameters.AddWithValue("@OrderID", orderId);
            mySqlConnection.Open();

            SqlDataReader reader = DisplayInvoiceCommand.ExecuteReader();
            InvoiceLabel.Text += "<br/>Order Details:<br/>";
            InvoiceLabel.Text += "<table><tr><th>Serial</th><th>Item ID</th><th>Quantity</th><th>Price</th></tr>";

            while (reader.Read())
            {
                InvoiceLabel.Text += "<tr>";
                InvoiceLabel.Text += $"<td>{reader["Srial"]}</td>";
                InvoiceLabel.Text += $"<td>{reader["item_id"]}</td>";
                InvoiceLabel.Text += $"<td>{reader["Quantity"]}</td>";
                InvoiceLabel.Text += $"<td>{reader["Price"]}</td>";
                InvoiceLabel.Text += "</tr>";
            }
            InvoiceLabel.Text += "</table>";
        }

        protected void PrintButton_Click(object sender, EventArgs e)
        {
            // this to print invoice
            string script = "window.print();";
            ClientScript.RegisterStartupScript(this.GetType(), "Print", script, true);
        }
        /******************************************************************/
    }
}
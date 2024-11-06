using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static IT508_Project.ShowAllOrders;

namespace IT508_Project
{
    public partial class EditOrderDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {



            if (!IsPostBack)
            {
                int Serial          = Convert.ToInt32(Request.QueryString["Serial"]);
                int orderId         = Convert.ToInt32(Request.QueryString["id"]);
                int GetWhId         = Convert.ToInt32(Request.QueryString["whId"]);
                int GetTransType    = Convert.ToInt32(Request.QueryString["transactionType"]);
                LoadOrderData       (orderId, GetWhId, GetTransType);
                LoadDropdowns       (orderId, GetWhId, GetTransType);
                LoadOrderdDtailsData(Serial, orderId , GetWhId, GetTransType);
            }

        }

        protected void UpadateHeader_Click(object sender, EventArgs e)
        {

            int GetUpdateOrderId = Convert.ToInt32(UpdateOrderId.Text);
            int GetUpdateWhID = Convert.ToInt32(UpdateWhID.SelectedValue);
            int GetUpdateTransID = Convert.ToInt32(UpdateTransID.SelectedValue);
            int GetUpdateCustomerID = Convert.ToInt32(UpdateCustomerID.SelectedValue);
            DateTime GetUpdateTime = DateTime.Parse(UpdateTime.Text);
            string GetUpdateUserName = UpdateUserName.Text;

            UpdateOrderInHeaderDatabase(GetUpdateOrderId, GetUpdateWhID, GetUpdateTransID, GetUpdateCustomerID, GetUpdateTime, GetUpdateUserName);
        }
        private void LoadOrderData(int orderId , int whId, int TransType)
        {

            SqlConnection mySqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");
            string SelectOrders = @"Select *  from OrderHeader WHERE Order_id =@Order_id AND Wh_id = @Wh_id 
                                                                       AND 
                                                                         transaction_type = @TransactionType";
            SqlCommand SelectCommand    = new SqlCommand(SelectOrders, mySqlConnection);
            SelectCommand.Parameters.AddWithValue("@Order_id", orderId);
            SelectCommand.Parameters.AddWithValue("@Wh_id", whId);
            SelectCommand.Parameters.AddWithValue("@TransactionType", TransType);
            mySqlConnection.Open();
  
                SqlDataReader reader = SelectCommand.ExecuteReader();
                if (reader.Read())
                {
                    
                    UpdateOrderId.Text              = reader["Order_id"].ToString();
                    UpdateWhID.SelectedValue        = reader["Wh_id"].ToString();
                    UpdateTransID.SelectedValue     = reader["transaction_type"].ToString();
                    UpdateCustomerID.SelectedValue  = reader["customer_id"].ToString();
                    UpdateTime.Text                 = reader["order_date"].ToString();
                    UpdateUserName.Text             = reader["created_by"].ToString();

            }
            
        }


        

        private decimal GetPrice(int itemid)
        {
            decimal price = 0;
            SqlConnection mySqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");
            string GePrice = @"Select Price from PosStoree WHERE Item_ID = @Item_ID";

            SqlCommand cmd = new SqlCommand(GePrice, mySqlConnection);
            cmd.Parameters.AddWithValue("@item_id", itemid);
            mySqlConnection.Open();
            //SqlDataReader reader = cmd.ExecuteReader();
            object result = cmd.ExecuteScalar();
            if (result != null)
            {
                price = Convert.ToDecimal(result); // Convert result to decimal
            }
            return price;
        }

        //Function to laod Order details Data From OrderdDtails DB Table
        public void LoadOrderdDtailsData(int Serial ,int orderId, int whId, int TransType)
        {

     
            try
            {
                SqlConnection mySqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");
                string SelectOrdersDetails = @"Select * from OrderDetails  WHERE Srial=@UpdataSerial AND Order_id=@Order_id AND Wh_id=@Wh_id AND trans_type=@TransactionType";
                SqlCommand SelectOrdersCommand = new SqlCommand(SelectOrdersDetails, mySqlConnection);
                SelectOrdersCommand.Parameters.AddWithValue("@UpdataSerial",       Serial);
                SelectOrdersCommand.Parameters.AddWithValue("@Order_id",          orderId);
                SelectOrdersCommand.Parameters.AddWithValue("@Wh_id",                whId);
                SelectOrdersCommand.Parameters.AddWithValue("@TransactionType", TransType);
                mySqlConnection.Open();

                SqlDataReader ReaderAllOrder = SelectOrdersCommand.ExecuteReader();

                if (ReaderAllOrder.Read())
                {

                    UpdataSerial.Text           = ReaderAllOrder["Srial"].ToString();
                    UpdateDeOrderId.Text        = ReaderAllOrder["Order_id"].ToString();
                    UpdataItemID.SelectedValue  = ReaderAllOrder["item_id"].ToString();
                    UpdataQuantity.Text         = ReaderAllOrder["Quantity"].ToString();
                    UpdataPrice.Text            = ReaderAllOrder["Price"].ToString();

                }
                else
                {
                    // If no data Return
                    ErrorMessageOrder.Text = " No Data Found";
                }

                ReaderAllOrder.Close();


            }
            catch (Exception ex)
            {
                ErrorMessageOrder.Text = "Error: " + ex.Message;
            }
            
        }

        private void LoadDropdowns(int orderId, int whId, int TransType)
        {
            // تحميل بيانات WHID
            SqlConnection mySqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");
            string GetWHID = @"Select wh_id  from OrderHeader WHERE Order_id =@Order_id AND Wh_id = @Wh_id AND transaction_type = @TransactionType";
            SqlCommand GetWHIDCommand = new SqlCommand(GetWHID, mySqlConnection);
            GetWHIDCommand.Parameters.AddWithValue("@Order_id", orderId);
            GetWHIDCommand.Parameters.AddWithValue("@Wh_id", whId);
            GetWHIDCommand.Parameters.AddWithValue("@TransactionType", TransType);
            mySqlConnection.Open();
            SqlDataReader GetWHIDreader = GetWHIDCommand.ExecuteReader();
            UpdateWhID.DataSource = GetWHIDreader;
            UpdateWhID.DataTextField = "wh_id";
            UpdateWhID.DataValueField = "wh_id";
            UpdateWhID.DataBind();
            GetWHIDreader.Close();

            // تحميل بيانات WHID

            string GetTransID = @"Select transaction_type  from OrderHeader WHERE Order_id =@Order_id AND Wh_id = @Wh_id AND transaction_type = @TransactionType";
            SqlCommand GetTransCommand = new SqlCommand(GetTransID, mySqlConnection);
            GetTransCommand.Parameters.AddWithValue("@Order_id", orderId);
            GetTransCommand.Parameters.AddWithValue("@Wh_id", whId);
            GetTransCommand.Parameters.AddWithValue("@TransactionType", TransType);
            SqlDataReader GetTransReader = GetTransCommand.ExecuteReader();
            UpdateTransID.DataSource = GetTransReader;
            UpdateTransID.DataTextField = "transaction_type";
            UpdateTransID.DataValueField = "transaction_type";
            UpdateTransID.DataBind();
            GetTransReader.Close();

            // تحميل بيانات CustomerID

            string CustomerIDQuary = @"Select customer_id  from OrderHeader WHERE Order_id =@Order_id AND Wh_id = @Wh_id AND transaction_type = @TransactionType";
            SqlCommand CustomerIDCommand = new SqlCommand(CustomerIDQuary, mySqlConnection);
            CustomerIDCommand.Parameters.AddWithValue("@Order_id", orderId);
            CustomerIDCommand.Parameters.AddWithValue("@Wh_id", whId);
            CustomerIDCommand.Parameters.AddWithValue("@TransactionType", TransType);
            SqlDataReader CustomerIDReader = CustomerIDCommand.ExecuteReader();
            UpdateCustomerID.DataSource = CustomerIDReader;
            UpdateCustomerID.DataTextField = "customer_id";
            UpdateCustomerID.DataValueField = "customer_id";
            UpdateCustomerID.DataBind();
            CustomerIDReader.Close();

            // Load  item id from OrderDetails

            string ItemIDQuary = @"Select item_id  from OrderDetails WHERE Order_id =@Order_id AND Wh_id = @Wh_id AND trans_type = @TransactionType";
            SqlCommand ItemIDCommand = new SqlCommand(ItemIDQuary, mySqlConnection);
            ItemIDCommand.Parameters.AddWithValue("@Order_id", orderId);
            ItemIDCommand.Parameters.AddWithValue("@Wh_id", whId);
            ItemIDCommand.Parameters.AddWithValue("@TransactionType", TransType);
            SqlDataReader ItemIDReader = ItemIDCommand.ExecuteReader();
            UpdataItemID.DataSource = ItemIDReader;
            UpdataItemID.DataTextField = "item_id";
            UpdataItemID.DataValueField = "item_id";
            UpdataItemID.DataBind();

            ItemIDReader.Close();
            mySqlConnection.Close();

        }

        private void UpdateOrderInHeaderDatabase(int orderId, int whId, int transId, int customerId, DateTime orderTime, string username)
        {
            string connectionString = @"Data Source=localhost\SQLEXPRESS; Initial Catalog=IMSDB; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;";

            string query = "UPDATE OrderHeader SET wh_id = @WhId, transaction_type = @TransId, customer_id = @CustomerId," +
                " order_date =@OrderTime, created_by = @Username WHERE order_id = @OrderId";
            SqlConnection connection = new SqlConnection(connectionString);

           
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@WhId", whId);
                command.Parameters.AddWithValue("@TransId", transId);
                command.Parameters.AddWithValue("@CustomerId", customerId);
                command.Parameters.AddWithValue("@OrderTime", orderTime);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@OrderId", orderId);
                command.ExecuteNonQuery();




            
        }

        protected void UpadateDeatails_Click(object sender, EventArgs e)
        {
            UpdateOrderHeader();
        }

        private void UpdateOrderHeader()
        {
            string connectionString = @"Data Source=localhost\SQLEXPRESS; Initial Catalog=IMSDB; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;";

            SqlConnection mySqlConnection = new SqlConnection(connectionString);

            string updateQuery = "UPDATE OrderHeader SET wh_id = @WhId, transaction_type = @TransId, customer_id = @CustomerId," +
                 " order_date =@OrderTime, created_by = @Username WHERE order_id = @OrderId";
            SqlCommand command = new SqlCommand(updateQuery, mySqlConnection);
                command.Parameters.AddWithValue("@WhID", UpdateWhID.SelectedValue);
                command.Parameters.AddWithValue("@TransID", UpdateTransID.SelectedValue);
                command.Parameters.AddWithValue("@CustomerID", UpdateCustomerID.SelectedValue);
                command.Parameters.AddWithValue("@OrderDate", UpdateTime.Text);
                command.Parameters.AddWithValue("@CreatedBy", UpdateUserName.Text);
                command.Parameters.AddWithValue("@OrderId", UpdateOrderId.Text);

                mySqlConnection.Open();
                command.ExecuteNonQuery();

            // Optionally redirect or show a success message
        }
    }
}
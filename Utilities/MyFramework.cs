using System;
using System.Collections.Generic;
using System.Collections;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace IT508_Project
{
    public class MyFramework
    {





        public bool PosItemExist(DropDownList Items,TextBox OBTextBox, TextBox QuantityTextBox, TextBox PositionTextBox ,Label ErrorMessage)
        {


            bool itemExists = false;
            string ReturendItemId = "";
            SqlConnection mySqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");
            string SelectItemIdQuery = "Select item_id from PosStoree";

            mySqlConnection.Open();
            //This Command to insert data in database 
            SqlCommand SelectItemIdCommand = new SqlCommand(SelectItemIdQuery, mySqlConnection);
            SqlDataReader ItemIdReader = SelectItemIdCommand.ExecuteReader();
            //this while to Read Item id 


            while (ItemIdReader.Read())
            {
                ReturendItemId = ItemIdReader["item_id"].ToString();

                if (Items.SelectedValue.Equals(ReturendItemId))
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
                if (!string.IsNullOrWhiteSpace(OBTextBox.Text) ||
                    !string.IsNullOrWhiteSpace(QuantityTextBox.Text) ||
                    !string.IsNullOrWhiteSpace(PositionTextBox.Text))
                {
                    ErrorMessage.Text = "You Should Fill All Fileds ";

                }
            }


            return true;
        }

        public int PosStoreWHId()
        {
            int WareHouseID;

            SqlConnection mySqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");

            // This query gets the highest (latest) order_id from the orderheader table
            string GetLastHeaderId = @"SELECT wh_id FROM PosStoree";
            mySqlConnection.Open();

            SqlCommand getLastIdcommand = new SqlCommand(GetLastHeaderId, mySqlConnection);

            // ExecuteScalar is sufficient here since you're expecting one value (the MAX order_id)
            WareHouseID = Convert.ToInt32(getLastIdcommand.ExecuteScalar());

            mySqlConnection.Close();

            return WareHouseID;

        }
        protected void CheckAdminUserRole(string username ,Label ErrorMessageRple ,Page currentPage) //this method to Check User Role if he User admin or normal user
        {
            SqlConnection mySqlConnection = new SqlConnection(@"Data Source=localhost\SQLEXPRESS; Initial Catalog=IMSDB; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");

            // Modify the query to filter by the specific user
            string CheckLoginRole = @"SELECT Role FROM IMS_Users WHERE username = @username"; // Assuming UserId is the identifier
            SqlCommand CheckLoginRoleCommand = new SqlCommand(CheckLoginRole, mySqlConnection);
            mySqlConnection.Open();
            CheckLoginRoleCommand.Parameters.AddWithValue("@username", username); // Pass the logged-in user's ID


            string userRole = (string)CheckLoginRoleCommand.ExecuteScalar(); // Use ExecuteScalar to get a single value

            if (userRole != null) // Check if a role was returned
            {
                if (userRole.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    currentPage.Response.Redirect("ShowAllOrders.aspx");
                }
                else
                {
                    ErrorMessageRple.Text = "This Page is Just for Admin";
                }
            }
            else
            {
                ErrorMessageRple.Text = "User role not found.";
            }
        }
        public decimal GetItemsPrice(int ItemId)
        {

            decimal price = 0;
            SqlConnection mySqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");
            string GePrice = @"Select Price from PosStoree WHERE Item_ID = @Item_ID";

            SqlCommand cmd = new SqlCommand(GePrice, mySqlConnection);
            cmd.Parameters.AddWithValue("@item_id", ItemId);
            mySqlConnection.Open();
            //SqlDataReader reader = cmd.ExecuteReader();
            object result = cmd.ExecuteScalar();
            if (result != null)
            {
                price = Convert.ToDecimal(result); // Convert result to decimal
            }
            return price;
        }
        public void GetPriceToSelectedItem(DropDownList ItemList, TextBox SelectedItemPrice)
        {


            string selectedValue = ItemList.SelectedValue;


            if (selectedValue != "0") // Check if a valid item is selected
            {
                int itemId;
                if (int.TryParse(selectedValue, out itemId))
                {
                    SelectedItemPrice.Text = GetItemsPrice(itemId).ToString("0.00"); // Fetch and display the price
                }
            }
            else
            {
                SelectedItemPrice.Text = string.Empty; // Clear the price if the default option is selected
            }


        }

        public void FileItemsList(DropDownList ListToFill)
        {
            SqlConnection mySqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");
            string FillItemList = @"Select item_id from PosStoree";

            mySqlConnection.Open();
            SqlCommand cmd = new SqlCommand(FillItemList, mySqlConnection);
            SqlDataReader FillItemReader = cmd.ExecuteReader();
            while (FillItemReader.Read())
            {
                //string optionValue = FillItemReader["item_id"].ToString();
                string optionText = FillItemReader["item_id"].ToString();

                ListToFill.Items.Add(new ListItem(optionText));
            }
            ListToFill.Items.Insert(0, new ListItem("Select Item", "0"));
            ListToFill.SelectedIndex = 0;
        }

        public void IncrementSerial(TextBox SerialNum, int CurrentId)
        {

            SqlConnection mySqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");
            string SrialQury = @"SELECT MAX(Srial),MAX(order_id) FROM OrderDetails";
            int NextSerial = 0;
            int ReturnedId = 0;
            mySqlConnection.Open();
            SqlCommand cmd = new SqlCommand(SrialQury, mySqlConnection);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                NextSerial = reader.IsDBNull(reader.GetOrdinal("MaxSerial")) ? 0 : Convert.ToInt32(reader["Srial"]);
                ReturnedId = reader.IsDBNull(reader.GetOrdinal("MaxOrderId")) ? 0 : Convert.ToInt32(reader["order_id"]);

                if (ReturnedId == CurrentId)
                {
                    NextSerial++;
                }
                else
                {
                    NextSerial = 1;
                }

            }
            SerialNum.Text = NextSerial.ToString();
        }
        public void GetOrderDetailsSerial(TextBox SerialNum)
        {


            string Serial = "";
            SqlConnection mySqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");
            string GetSerial = "Select Srial from OrderDetails";// this select to get Srial  
            mySqlConnection.Open();

            SqlCommand Serialcommand = new SqlCommand(GetSerial, mySqlConnection);
            SqlDataReader ReadSerial = Serialcommand.ExecuteReader();
            while (ReadSerial.Read())
            {
                Serial = ReadSerial[("Srial")].ToString();

            }
            ReadSerial.Close();

            mySqlConnection.Close();

            SerialNum.Text = Serial;
        }

        //This Function Take Pos Store whareHouseId
        public void CalculateQtyAfterOrder(DropDownList itemId, DropDownList whareHouseId,
                                       TextBox itemQuantity, Label ErrorMessage)
        {
            SqlConnection mySqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");
            string GetOpenBalancequery = @"Select quantity from PosStoree WHERE item_id = @Item_id AND wh_id = @Wh_id";
                                                  
            mySqlConnection.Open();

            //This Command to get Quantity and add new Quantity with old to table 
            SqlCommand OpenBalancCommand = new SqlCommand(GetOpenBalancequery, mySqlConnection);
            OpenBalancCommand.Parameters.AddWithValue("@Item_id", itemId.SelectedValue);
            OpenBalancCommand.Parameters.AddWithValue("@Wh_id", whareHouseId.SelectedValue);


            object result = OpenBalancCommand.ExecuteScalar();

            //int currentOpenBalance = 0;

            if (result == null)
            {
                //Item not found in the inventory.
                ErrorMessage.Text = "Item not found in the inventory.";
                return;
            }

            if (!int.TryParse(result.ToString(), out int currentOpenBalance))
            {
                

                ErrorMessage.Text = "Invalid quantity data in the inventory.";
                return;
            }



            // Validate item quantity input
            if (!int.TryParse(itemQuantity.Text.Trim(), out int quantity) || quantity <= 0)
            {
                ErrorMessage.Text = "Please enter a valid quantity for the item.";
                return;
            }

            // Calculate new balance
            int newOpenBalance = currentOpenBalance - quantity;
            // Update the quantity in the database
            string updateQuery = @"UPDATE PosStoree SET quantity = @NewOpenBalance WHERE item_id = @Item_id AND wh_id = @Wh_id";
            SqlCommand updateCommand = new SqlCommand(updateQuery, mySqlConnection);
            updateCommand.Parameters.AddWithValue("@NewOpenBalance", newOpenBalance);
            updateCommand.Parameters.AddWithValue("@Item_id", itemId.SelectedValue);
            updateCommand.Parameters.AddWithValue("@Wh_id", whareHouseId.SelectedValue);
            updateCommand.ExecuteNonQuery();
            mySqlConnection.Close();



        }
        public int GetPosStoreQuantity(DropDownList SelectedItem, int SelectedWH)
        {
            SqlConnection mySqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");
            string GetCurrentQuantity = @"Select quantity from PosStoree WHERE item_id = @Item_id AND wh_id = @Wh_id";

            mySqlConnection.Open();

            //This Command to get Quantity and add new Quantity with old to table 
            SqlCommand GetQuantityCommand = new SqlCommand(GetCurrentQuantity, mySqlConnection);
            GetQuantityCommand.Parameters.AddWithValue("@Item_id", SelectedItem.SelectedValue);
            GetQuantityCommand.Parameters.AddWithValue("@Wh_id", SelectedWH);
            int CurrentQuantity = Convert.ToInt32(GetQuantityCommand.ExecuteScalar());
            GetQuantityCommand.ExecuteNonQuery();


            return CurrentQuantity;
        }

        public void CheckUserRole(string username, string PageName, Label ErrorMassage) //this method to Check User Role if he User admin or normal user
        {
            SqlConnection mySqlConnection = new SqlConnection(@"Data Source=localhost\SQLEXPRESS; Initial Catalog=IMSDB; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");
            // Modify the query to filter by the specific user
            string CheckLoginRole = @"SELECT Role FROM IMS_Users WHERE username = @username"; // Assuming UserId is the identifier

            SqlCommand CheckLoginRoleCommand = new SqlCommand(CheckLoginRole, mySqlConnection);

            CheckLoginRoleCommand.Parameters.AddWithValue("@username", username); // Pass the logged-in user's ID
            mySqlConnection.Open();

            string userRole = (string)CheckLoginRoleCommand.ExecuteScalar(); // Use ExecuteScalar to get a single value

            if (userRole != null) // Check if a role was returned
            {
                if (userRole.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    HttpContext.Current.Response.Redirect($"{PageName}.aspx");
                }
                else
                {
                    ErrorMassage.Text = "This Page is Just for Admin";
                }
            }
            else
            {
                ErrorMassage.Text = "User role not found.";
            }
        }
        public bool ValidateIncrementalQuantity(TextBox CheckText, out int incrementalQuantity, Label messageLabel)
        {
            if (!int.TryParse(CheckText.Text.Trim(), out incrementalQuantity) || incrementalQuantity <= 0)
            {
                messageLabel.Text = "You Must Enter Value greater than Zerooo";
                return false;
            }
            return true;
        }
        public  bool ValidateInputs(TextBox openBalance, TextBox quantity, TextBox itemPosition, TextBox price, Label errorMessage)
        {
            // Check for empty fields
            if (string.IsNullOrWhiteSpace(openBalance.Text.Trim()) ||
                string.IsNullOrWhiteSpace(quantity.Text.Trim()) ||
                string.IsNullOrWhiteSpace(itemPosition.Text.Trim()) ||
                string.IsNullOrWhiteSpace(price.Text.Trim()))
            {
                errorMessage.Text = "You Should Fill All Fields";
                return false; // Validation failed
            }

            // Validate OpenBalance
            if (!int.TryParse(openBalance.Text.Trim(), out int checkOpenBalance) || checkOpenBalance <= 0)
            {
                errorMessage.Text = "You need to Enter a Valid Open Balance.";
                openBalance.Text = ""; // Optional: clear the field
              

                return false; // Validation failed
            }

            // Validate Quantity
            if (!int.TryParse(quantity.Text.Trim(), out int checkQuantity) || checkQuantity <= 0)
            {
                errorMessage.Text = "You need to Enter a Valid Quantity.";
                quantity.Text = ""; // Optional: clear the field
                return false; // Validation failed
            }

            // Validate Price
            if (!decimal.TryParse(price.Text.Trim(), out decimal checkPrice) || checkPrice <= 0)
            {
                errorMessage.Text = "You need to Enter a Valid Price.";
                price.Text = ""; // Optional: clear the field
                return false; // Validation failed
            }

            return true; // All validations passed
        }
        public void CheckPostiveValuee(TextBox textBox, Label Message)
        {

            if (textBox.Text.Equals("") ||
                    textBox.Text.Equals("") ||
                    textBox.Text.Equals(""))
            {
                Message.Text = "You Should Fill All Fileds ";

            }
            else
            {

                //this if to check [If] Quantity less than Zero
                int CheckOpenBalance, CheckQuantity, CheckPrice;
                if (!int.TryParse(textBox.Text.Trim(), out CheckOpenBalance) || CheckOpenBalance <= 0)
                {



                    Message.Text = "You need to Enter Vaild Number ";
                    textBox.Text = "";
                    textBox.Text = "";
                    textBox.Text = "";
                    return;

                }
                if (!int.TryParse(textBox.Text.Trim(), out CheckQuantity) || CheckQuantity <= 0)
                {
                    Message.Text = "You need to Enter Vaild Quantity";
                    textBox.Text = "";
                    textBox.Text = "";
                    textBox.Text = "";
                    return;
                }
                if (!int.TryParse(textBox.Text.Trim(), out CheckPrice) || CheckPrice <= 0)
                {

                    Message.Text = "You need to Enter Vaild Price";
                    textBox.Text = "";
                    textBox.Text = "";
                    textBox.Text = "";
                    return;
                }


            }



        }

        public void FillInvoiceGrid(GridView InvoicGrid ,int OrderId)
        {
            SqlConnection mySqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");
            string SelectOrders = @"Select *  from OrderDetails Where order_id =@orderId";
            SqlCommand SelectCommand = new SqlCommand(SelectOrders, mySqlConnection);
            mySqlConnection.Open();
            SelectCommand.Parameters.AddWithValue("@orderId", OrderId);

            SqlDataReader reader = SelectCommand.ExecuteReader();
            InvoicGrid.DataSource = reader;
            InvoicGrid.DataBind();

        }


        /*---------------------------------------------------------------*/
        public void GenerateInvoice(int Id , Label InviceLab)
        {
            SqlConnection mySqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");

            int orderId = Id;
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
                InviceLab.Text += $"Order ID: {orderId}<br/>";
                InviceLab.Text += $"Warehouse ID: {whId}<br/>";
                InviceLab.Text += $"Transaction Type: {transType}<br/>";
                InviceLab.Text += $"Customer ID: {customerId}<br/>";
                InviceLab.Text += $"Order Date: {orderDate}<br/>";
                InviceLab.Text += $"Created By: {createdBy}<br/>";
            }


            // Show Order Details Data
            DisplayOrderDetails(orderId , InviceLab);
        }

        private void DisplayOrderDetails(int orderId , Label DInvoiceLab)
        {
            SqlConnection mySqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");



            string query = @"SELECT * FROM OrderDetails WHERE Order_id = @OrderID";

            SqlCommand DisplayInvoiceCommand = new SqlCommand(query, mySqlConnection);
            DisplayInvoiceCommand.Parameters.AddWithValue("@OrderID", orderId);
            mySqlConnection.Open();

            SqlDataReader reader = DisplayInvoiceCommand.ExecuteReader();
            DInvoiceLab.Text += "<br/>Order Details:<br/>";
            DInvoiceLab.Text += "<table><tr><th>Serial</th><th>Item ID</th><th>Quantity</th><th>Price</th></tr>";

            while (reader.Read())
            {
                DInvoiceLab.Text += "<tr>";
                DInvoiceLab.Text += $"<td>{reader["Srial"]}</td>";
                DInvoiceLab.Text += $"<td>{reader["item_id"]}</td>";
                DInvoiceLab.Text += $"<td>{reader["Quantity"]}</td>";
                DInvoiceLab.Text += $"<td>{reader["Price"]}</td>";
                DInvoiceLab.Text += "</tr>";
            }

            DInvoiceLab.Text += "</table>";

        }

        /*---------------------------------------------------------------*/
        /**/


    }
}
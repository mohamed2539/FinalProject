using NPOI.OpenXmlFormats.Dml.Diagram;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IT508_Project
{
    public partial class AddNewCustomer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        protected void AddCustomer_Click(object sender, EventArgs e)
        {

            


            string CheckCustomerPhone = "";
            string CheckCustomerPhoneText = CustomerPhone.Text;

            SqlConnection ChackSqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");
            string ChackQuary = @"SELECT Customer_Phone FROM POSCustomer";
            ChackSqlConnection.Open();
            SqlCommand ChackCommand = new SqlCommand(ChackQuary, ChackSqlConnection);
            SqlDataReader ReadToCkeck = ChackCommand.ExecuteReader();
            while (ReadToCkeck.Read())
            {

                 CheckCustomerPhone = ReadToCkeck["Customer_Phone"].ToString();
            }



            //this If to Check  if Customer Name faild is empty
            if (CustomerName.Text.Trim().Equals("")|| CustomerPhone.Text.Trim().Equals("")
                || CustomerAddress.Text.Trim().Equals("")) 
            {
                CheckFillTextBox.Text = "This Failed Is Reqiured ";

            }
            else
            {


                //this If to Check Customer Phone Number
                if (CheckCustomerPhoneText.Equals(CheckCustomerPhone))
                {


                    CheckFillTextBox.Text = "This Customer Is AlReady Exist ";
                    CustomerName.Text = "";
                    CustomerPhone.Text = "";
                    CustomerAddress.Text = "";

                }
                else    //belong to IF which Check Phone
                {


                    AddNewPosCustomerModel addPosCustomer = new AddNewPosCustomerModel("Customer_name", "Customer_Phone", "Customer_Address", "POSCustomer");
                    addPosCustomer.InsertCustomer(CustomerName, CustomerPhone, CustomerAddress);

   
                    //Response.Redirect("AddNewCustomer.aspx");
                }//End Of else that belong to IF which Check Phone

            }
        }



        protected void CheckUserRole(string username) //this method to Check User Role if he User admin or normal user
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
                            Response.Redirect("ShowAllCustomer.aspx");
                        }
                        else
                        {
                            CheckFillTextBox.Text = "This Page is Just for Admin";
                        }
                    }
                    else
                    {
                        CheckFillTextBox.Text = "User role not found.";
                    }
                }
   

        protected void ShowCustomer_Click(object sender, EventArgs e)
        {
            string SeUsername = (string)Session["username"]; // Session

            if (!string.IsNullOrEmpty(SeUsername))
            {
                CheckUserRole(SeUsername); //  checking role method
            }
            else
            {
                // Messsage When the user is not logged in
                CheckFillTextBox.Text = "User is not logged in.";
            }

        }
    }
}
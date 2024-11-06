using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IT508_Project
{
    public partial class EditCustomer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


          if (!IsPostBack)
            {

                int CustID = Convert.ToInt32(Request.QueryString["id"]);
                loadCustomerInfo(CustID);
            }
        }


        protected void loadCustomerInfo(int CustomerId)
        {
            SqlConnection ChackSqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");
            string GetCustomerInfo = @"SELECT * FROM POSCustomer WHERE Customer_id =@EditCustomerID ";
            ChackSqlConnection.Open();
            SqlCommand GetCustomerCommand = new SqlCommand(GetCustomerInfo, ChackSqlConnection);
            GetCustomerCommand.Parameters.AddWithValue("@EditCustomerID",   CustomerId);
            GetCustomerCommand.Parameters.AddWithValue("@CustomerName",     EditCustomerName.Text.Trim());
            GetCustomerCommand.Parameters.AddWithValue("@CustomerPhone",    EditCustomerPhone.Text.Trim());
            GetCustomerCommand.Parameters.AddWithValue("@CustomerAddress",  EditCustomerAddress.Text.Trim());
            SqlDataReader GetCustomerRead = GetCustomerCommand.ExecuteReader();
            if (GetCustomerRead.Read())
            {

                EditCustomerID.Text         = GetCustomerRead["Customer_id"     ].ToString();
                EditCustomerName.Text       = GetCustomerRead["Customer_name"   ].ToString();
                EditCustomerPhone.Text      = GetCustomerRead["Customer_Phone"  ].ToString();
                EditCustomerAddress.Text    = GetCustomerRead["Customer_Address"].ToString();
            }

        }

        //protected void UpdateCustomer_Click(object sender, EventArgs e)
        //{

        //    if (string.IsNullOrWhiteSpace(EditCustomerID.Text) ||
        //      string.IsNullOrWhiteSpace(EditCustomerName.Text) ||
        //      string.IsNullOrWhiteSpace(EditCustomerPhone.Text)||
        //      string.IsNullOrWhiteSpace(EditCustomerAddress.Text))
        //    {
        //        UpdateCustomerLabel.Text = "Please fill in all fields.";
        //        return;
        //    }

        //    UpdatePOSCustomer();
        //}

        protected void ClearFields()
        {

            EditCustomerID.Text = "";
            EditCustomerName.Text = "";
            EditCustomerPhone.Text = "";
            EditCustomerAddress.Text = "";
        }

        protected void CheckPhoneExist(string PhoneNumber)
        {
            string CheckCustomerPhone = PhoneNumber;
            string CheckCustomerPhoneText = EditCustomerPhone.Text;

            SqlConnection ChackSqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");
            string ChackQuary = @"SELECT Customer_Phone FROM POSCustomer";
            ChackSqlConnection.Open();
            SqlCommand ChackCommand = new SqlCommand(ChackQuary, ChackSqlConnection);
            //ChackCommand.Parameters.AddWithValue("@CustomerPhone", CheckCustomerPhone);
            SqlDataReader ReadToCkeck = ChackCommand.ExecuteReader();
            while (ReadToCkeck.Read())
            {

                CheckCustomerPhone = ReadToCkeck["Customer_Phone"].ToString();
                if (CheckCustomerPhoneText.Equals(CheckCustomerPhone))
                {

                    CheckFaildsError.Text = "This Customer  Is Already Exist";
                    string script = "setTimeout(function() { window.location.href = 'ShowAllCustomer.aspx'; }, 1500);"; // 1.5 مللي ثانية
                    ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                    return;

                }
            }
           


        }
        protected void UpdatePOSCustomer()
        {


            string GetPhoneNumber = EditCustomerPhone.Text.Trim();

            CheckPhoneExist(GetPhoneNumber);

            SqlConnection UpdateSqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");
            string UpdateCustomerInfo = @"UPDATE POSCustomer SET Customer_name   = @CustomerName,
                                                                Customer_Phone   = @CustomerPhone,
                                                                Customer_Address = @CustomerAddress
                                                                WHERE 
                                                                     Customer_id = @EditCustomerID";

            // Create a new command for the update
            SqlCommand UpdateCustomerCommand = new SqlCommand(UpdateCustomerInfo, UpdateSqlConnection);
            UpdateSqlConnection.Open();
            UpdateCustomerCommand.Parameters.AddWithValue("@EditCustomerID",    EditCustomerID.Text.Trim());
            UpdateCustomerCommand.Parameters.AddWithValue("@CustomerName",      EditCustomerName.Text.Trim());
            UpdateCustomerCommand.Parameters.AddWithValue("@CustomerPhone",     EditCustomerPhone.Text.Trim());
            UpdateCustomerCommand.Parameters.AddWithValue("@CustomerAddress",   EditCustomerAddress.Text.Trim());
            


            try
            {
                // Execute the update command
                int rowsAffected = UpdateCustomerCommand.ExecuteNonQuery();

                //Response.Write(rowsAffected);
                if (rowsAffected > 0)
                {
                    UpdateCustomerLabel.Text = "Customer Info Is Updeted Successfully ";

                    string script = "setTimeout(function() { window.location.href = 'ShowAllCustomer.aspx'; }, 3000);"; // 3000 مللي ثانية
                    ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                }
                else
                {
                    UpdateCustomerLabel.Text = "Some Error Happens You need to try Again";
                    ClearFields();

                }
            }
            catch (SqlException ex)
            {
                UpdateCustomerLabel.Text = $"Database error: {ex.Message}";
                ClearFields();
            }
            catch (Exception ex)
            {
                UpdateCustomerLabel.Text = $"An error occurred: {ex.Message}";
                ClearFields();
            }
            finally
            {
                UpdateSqlConnection.Close();
            }
        }

        protected void UpdateCustomerInfo_Click(object sender, EventArgs e)
        {
            UpdatePOSCustomer();
            ClearFields();
        }
    }
}
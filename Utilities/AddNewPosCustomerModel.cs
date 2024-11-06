//using Microsoft.Office.Interop.Excel;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace IT508_Project
{
    public class AddNewPosCustomerModel
    {


        //int    CustomerId;
        string CustomerName;
        string CustomerPhone;
        string CustomerAdderss;
        string TabelName;
        public  AddNewPosCustomerModel(string name, string Phone, string Address, string tabelName)
        {
            this.CustomerName = name;
            this.CustomerPhone = Phone;
            this.CustomerAdderss = Address;
            this.TabelName = tabelName;
        }

        private SqlConnection DBConnection(string dataSource, string initialCatalog, bool integratedSecurity, bool trustServerCertificate, bool encrypt)
        {
            string connectionString = $@"Data Source={dataSource}; 
                                            Initial Catalog={initialCatalog};
                                            Integrated Security={integratedSecurity};
                                            TrustServerCertificate={trustServerCertificate};
                                            Encrypt={encrypt};";
            return new SqlConnection(connectionString);



        }
        public void InsertCustomer(TextBox Customer_name, TextBox Customer_Phone, TextBox Customer_Adderss)
        {
            //this.CustomerId= id;
   

            SqlConnection mySqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");
            string query = $"INSERT INTO {TabelName} ({CustomerName}, {CustomerPhone}, {CustomerAdderss}) VALUES (@CustomerName, @CustomerPhone, @CustomerAddress)";


            mySqlConnection.Open();
            SqlCommand cmd = new SqlCommand(query, mySqlConnection);
            cmd.Parameters.AddWithValue("@CustomerName", Customer_name.Text.ToString().Trim());
            cmd.Parameters.AddWithValue("@CustomerPhone", Customer_Phone.Text.ToString().Trim());
            cmd.Parameters.AddWithValue("@CustomerAddress", Customer_Adderss.Text.ToString().Trim());
            cmd.ExecuteNonQuery();
            mySqlConnection.Close();


            CustomerName = "";
            CustomerPhone = "";
            CustomerAdderss = "";

        }//End Of else that belong to IF which Check Phone
  
        public void UpdatePosCustomer(TextBox CustomerId,TextBox Customer_name, TextBox Customer_Phone, TextBox Customer_Adderss,Label Message)
    {
            string GetPhoneNumber = Message.Text.Trim();

            //CheckPhoneExist(GetPhoneNumber);

            SqlConnection UpdateSqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");
            string UpdateCustomerInfo = @"UPDATE POSCustomer SET Customer_name    = @CustomerName,
                                                                 Customer_Phone   = @CustomerPhone,
                                                                 Customer_Address = @CustomerAddress
                                                               WHERE 
                                                                 Customer_id      = @EditCustomerID";

            // Create a new command for the update
            SqlCommand UpdateCustomerCommand = new SqlCommand(UpdateCustomerInfo, UpdateSqlConnection);
            UpdateSqlConnection.Open();
            UpdateCustomerCommand.Parameters.AddWithValue("@EditCustomerID",    CustomerId.Text.Trim());
            UpdateCustomerCommand.Parameters.AddWithValue("@CustomerName",      Customer_name.Text.Trim());
            UpdateCustomerCommand.Parameters.AddWithValue("@CustomerPhone",     Customer_Phone.Text.Trim());
            UpdateCustomerCommand.Parameters.AddWithValue("@CustomerAddress",   Customer_Adderss.Text.Trim());



            try
            {
                // Execute the update command
                int rowsAffected = UpdateCustomerCommand.ExecuteNonQuery();

                //Response.Write(rowsAffected);
                if (rowsAffected > 0)
                {
                    Message.Text = "Customer Info Is Updeted Successfully ";

                   
                }
                else
                {
                    Message.Text = "Some Error Happens You need to try Again";
                    //ClearFields();

                }
            }
            catch (SqlException ex)
            {
                Message.Text = $"Database error: {ex.Message}";
                //ClearFields();
            }
            catch (Exception ex)
            {
                Message.Text = $"An error occurred: {ex.Message}";
                //ClearFields();
            }
            finally
            {
                UpdateSqlConnection.Close();
            }

        }


    }//End Of Class

}//End namespace

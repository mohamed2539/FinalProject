using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using IT508_Project.Utilities;

namespace IT508_Project
{
    public partial class addCustomer : System.Web.UI.Page
    {
        string connectionString = @"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;";
        private int userId;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Retrieve the username from the session
            string username = (string)Session["username"];

            // Use the utility class to get the UserID
            userId = UserDataHelper.GetUserID(username);

            lblErrorMessage.Visible = false;
            if (!IsPostBack)
            {
                string cusIDQury = "SELECT MAX(Cus_ID) FROM IMS_Customer";
                IDGenerator idGenerator = new IDGenerator(connectionString);
                int startCusID = 1001;
                int nextCusID = idGenerator.GetNextID(cusIDQury, startCusID);
                txtCusID.Text = nextCusID.ToString();

                clear();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtCusID.Text.Trim() == "" || Convert.ToInt32(txtCusID.Text.Trim()) < 1001)
            {
                lblErrorMessage.Text = "Please inter a valid Customer ID";
            }
            else
            {
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    sqlcon.Open();

                    // Check if Customer_ID exists
                    string checkCusIDQuery = @"SELECT COUNT(*) FROM IMS_Customer WHERE Cus_ID = @Cus_ID";
                    SqlCommand checkCusIDCmd = new SqlCommand(checkCusIDQuery, sqlcon);
                    checkCusIDCmd.Parameters.AddWithValue("@Cus_ID", Convert.ToInt32(txtCusID.Text.Trim()));
                    int count = (int)checkCusIDCmd.ExecuteScalar();
                    if (count > 0)
                    {
                        int cusID = Convert.ToInt32(txtCusID.Text.Trim());
                        string getCusDataQury = @"SELECT * FROM IMS_Customer WHERE Cus_ID = @Cus_ID";
                        SqlDataAdapter sqlDa = new SqlDataAdapter(getCusDataQury, sqlcon);
                        sqlDa.SelectCommand.Parameters.AddWithValue("@Cus_ID", cusID);
                        DataTable dtbl = new DataTable();
                        sqlDa.Fill(dtbl);

                        txtCusID.Text = cusID.ToString();
                        txtCusName.Text = dtbl.Rows[0][1].ToString();
                        txtContactNum.Text = dtbl.Rows[0][2].ToString();
                        txtCusEmail.Text = dtbl.Rows[0][3].ToString();

                        lblSuccessMessage.Text = "";
                        lblErrorMessage.Text = "";
                    }
                    else
                    {
                        // Check if Customer_Name exists
                        string checkCusNameQuery = @"SELECT COUNT(*) FROM IMS_Customer WHERE Cus_Name = @Cus_Name";
                        SqlCommand checkCusNameCmd = new SqlCommand(checkCusNameQuery, sqlcon);
                        checkCusNameCmd.Parameters.AddWithValue("@Cus_Name", txtCusName.Text.Trim());
                        int nameCount = (int)checkCusNameCmd.ExecuteScalar();
                        if (nameCount > 0)
                        {
                            lblErrorMessage.Visible = true;
                            lblErrorMessage.Text = "Customer Name Already Exists!!!";
                        }
                        else
                        {
                            CustomerHandler customerHandler = new CustomerHandler(sqlcon, userId);

                            customerHandler.AddCustomer(
                            Convert.ToInt32(txtCusID.Text.Trim()),
                            txtCusName.Text.Trim(),
                            txtContactNum.Text.Trim(),
                            txtCusEmail.Text.Trim()
                            );

                            clear();
                            lblSuccessMessage.Text = "Customer Added Successfully!";
                            lblErrorMessage.Text = "";
                        }
                    }
                }
            }
        }
        void clear()
        {
            txtCusName.Text = txtContactNum.Text = txtCusEmail.Text = "";
            lblSuccessMessage.Text = lblErrorMessage.Text = "";
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("createnewobject.aspx");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtCusName.Text.Trim() == "" || txtCusID.Text.Trim() == "")
            {
                lblErrorMessage.Text = "Please fill in all mandatory fields.";
                lblErrorMessage.Visible = true;
            }
            else if (Convert.ToInt32(txtCusID.Text.Trim()) < 1001)
            {
                lblErrorMessage.Text = "Unavailable Customer ID!!";
                lblErrorMessage.Visible = true;
            }
            else
            {
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    sqlcon.Open();
                    // Check if Customer_Name exists
                    string checkCusNameQuery = @"SELECT COUNT(*) FROM IMS_Customer WHERE Cus_Name = @Cus_Name and Cus_ID != @Cus_ID";
                    SqlCommand checkCusNameCmd = new SqlCommand(checkCusNameQuery, sqlcon);
                    checkCusNameCmd.Parameters.AddWithValue("@Cus_Name", txtCusName.Text.Trim());
                    checkCusNameCmd.Parameters.AddWithValue("@Cus_ID", Convert.ToInt32(txtCusID.Text.Trim()));
                    int supCount = (int)checkCusNameCmd.ExecuteScalar();
                    if (supCount > 0)
                    {
                        lblErrorMessage.Text = "Customer Name Already Exists!!!";
                        lblErrorMessage.Visible= true;
                    }
                    else
                    {
                        CustomerHandler customerHandler = new CustomerHandler(sqlcon, userId);

                        customerHandler.UpdateCustomer(
                        Convert.ToInt32(txtCusID.Text.Trim()),
                        txtCusName.Text.Trim(),
                        txtContactNum.Text.Trim(),
                        txtCusEmail.Text.Trim()
                        );

                        clear();
                        lblSuccessMessage.Text = "Customer Edited Successfully!";
                    }
                }
            }
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();

                CustomerHandler customerHandler = new CustomerHandler(sqlcon, userId);

                customerHandler.DeleteCustomer(Convert.ToInt32(txtCusID.Text.Trim()));

                clear();
                lblSuccessMessage.Text = "Customer Deleted Successfully!";
                lblErrorMessage.Text = "";
            }
        }
    }
}
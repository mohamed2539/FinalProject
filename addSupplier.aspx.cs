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
    public partial class addSupplier : System.Web.UI.Page
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
                string supIDQury = "SELECT MAX(Supplier_ID) FROM IMS_Supplier";
                IDGenerator idGenerator = new IDGenerator(connectionString);
                int startSupID = 10000;
                int nextSupID = idGenerator.GetNextID(supIDQury, startSupID);
                txtSupID.Text = nextSupID.ToString();

                clear();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtSupID.Text.Trim() == "" || Convert.ToInt32(txtSupID.Text.Trim()) < 10001)
            {
                lblErrorMessage.Text = "Please inter a valid Supplier ID";
            }
            else
            {
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    sqlcon.Open();

                    // Check if Supplier_ID exists
                    string checkSupIDQuery = @"SELECT COUNT(*) FROM IMS_Supplier WHERE Supplier_ID = @Supplier_ID";
                    SqlCommand checkSupIDCmd = new SqlCommand(checkSupIDQuery, sqlcon);
                    checkSupIDCmd.Parameters.AddWithValue("@Supplier_ID", Convert.ToInt32(txtSupID.Text.Trim()));
                    int count = (int)checkSupIDCmd.ExecuteScalar();
                    if (count > 0)
                    {
                        int supID = Convert.ToInt32(txtSupID.Text.Trim());
                        string getSupDataQury = @"SELECT * FROM IMS_Supplier WHERE Supplier_ID = @Supplier_ID";
                        SqlDataAdapter sqlDa = new SqlDataAdapter(getSupDataQury, sqlcon);
                        sqlDa.SelectCommand.Parameters.AddWithValue("@Supplier_ID", supID);
                        DataTable dtbl = new DataTable();
                        sqlDa.Fill(dtbl);

                        txtSupID.Text = supID.ToString();
                        txtSupName.Text = dtbl.Rows[0][1].ToString();
                        txtContactNum.Text = dtbl.Rows[0][2].ToString();
                        txtSupEmail.Text = dtbl.Rows[0][3].ToString();

                        lblSuccessMessage.Text = "";
                        lblErrorMessage.Text = "";
                    }
                    else
                    {
                        // Check if Supplier_Name exists
                        string checkSupNameQuery = @"SELECT COUNT(*) FROM IMS_Supplier WHERE Supplier_Name = @Supplier_Name";
                        SqlCommand checkSupNameCmd = new SqlCommand(checkSupNameQuery, sqlcon);
                        checkSupNameCmd.Parameters.AddWithValue("@Supplier_Name", txtSupName.Text.Trim());
                        int nameCount = (int)checkSupNameCmd.ExecuteScalar();
                        if (nameCount > 0)
                        {
                            lblErrorMessage.Visible = true;
                            lblErrorMessage.Text = "Supplier Name Already Exists!!!";
                        }
                        else
                        {
                            SupplierHandler supplierHandler = new SupplierHandler(sqlcon, userId);

                            supplierHandler.AddSupplier(
                            Convert.ToInt32(txtSupID.Text.Trim()),
                            txtSupName.Text.Trim(),
                            txtContactNum.Text.Trim(),
                            txtSupEmail.Text.Trim()
                            );

                            clear();
                            lblSuccessMessage.Text = "Supplier Added Successfully!";
                            lblErrorMessage.Text = "";
                        }
                    }
                }
            }
        }
        void clear()
        {
            txtSupName.Text = txtContactNum.Text = txtSupEmail.Text = "";
            lblSuccessMessage.Text = lblErrorMessage.Text = "";
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("createnewobject.aspx");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtSupName.Text.Trim() == "" || txtSupID.Text.Trim() == "")
            {
                lblErrorMessage.Text = "Please Fill Mandetory Fields!!";
                lblErrorMessage.Visible = true;
            }
            else if (Convert.ToInt32(txtSupID.Text.Trim()) < 10001)
            {
                lblErrorMessage.Text = "Unavailable Supplier ID!!";
                lblErrorMessage.Visible = true;
            }
            else
            {
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    sqlcon.Open();
                    // Check if Supplier_Name exists
                    string checkSupNameQuery = @"SELECT COUNT(*) FROM IMS_Supplier WHERE Supplier_Name = @Supplier_Name and Supplier_ID != @Supplier_ID";
                    SqlCommand checkSupNameCmd = new SqlCommand(checkSupNameQuery, sqlcon);
                    checkSupNameCmd.Parameters.AddWithValue("@Supplier_Name", txtSupName.Text.Trim());
                    checkSupNameCmd.Parameters.AddWithValue("@Supplier_ID", Convert.ToInt32(txtSupID.Text.Trim()));
                    int supCount = (int)checkSupNameCmd.ExecuteScalar();
                    if (supCount > 0)
                    {
                        lblErrorMessage.Text = "Supplier Name Already Exists!!!";
                        lblErrorMessage.Visible=true;
                    }
                    else
                    {
                        SupplierHandler supplierHandler = new SupplierHandler(sqlcon, userId);

                        supplierHandler.UpdateSupplier(
                        Convert.ToInt32(txtSupID.Text.Trim()),
                        txtSupName.Text.Trim(),
                        txtContactNum.Text.Trim(),
                        txtSupEmail.Text.Trim()
                        );

                        clear();
                        lblSuccessMessage.Text = "Supplier Edited Successfully!";
                        lblErrorMessage.Text = "";
                    }
                }
            }
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();

                SupplierHandler supplierHandler = new SupplierHandler(sqlcon, userId);

                supplierHandler.DeleteSupplier(Convert.ToInt32(txtSupID.Text.Trim()));

                clear();
                lblSuccessMessage.Text = "Supplier Deleted Successfully!";
                lblErrorMessage.Text = "";
            }
        }
    }
}
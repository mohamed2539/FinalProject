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
    public partial class addWH : System.Web.UI.Page
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
                string WHIDQury = "SELECT MAX(WH_ID) FROM IMS_WH";
                IDGenerator idGenerator = new IDGenerator(connectionString);
                int startWHID = 1000;
                int nextWHID = idGenerator.GetNextID(WHIDQury, startWHID);
                txtWHID.Text = nextWHID.ToString();

                clear();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtWHID.Text.Trim() == "" || Convert.ToInt32(txtWHID.Text.Trim()) < 101)
            {
                lblErrorMessage.Text = "Please inter a valid WH ID";
            }
            else
            {
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    sqlcon.Open();

                    // Check if WH_ID exists
                    string checkWHIDQuery = "SELECT COUNT(*) FROM IMS_WH WHERE WH_ID = @WH_ID";
                    SqlCommand checkWHIDCmd = new SqlCommand(checkWHIDQuery, sqlcon);
                    checkWHIDCmd.Parameters.AddWithValue("@WH_ID", Convert.ToInt32(txtWHID.Text.Trim()));
                    int count = (int)checkWHIDCmd.ExecuteScalar();
                    if (count > 0)
                    {
                        int WHID = Convert.ToInt32(txtWHID.Text.Trim());
                        string getWHDataQury = @"SELECT * FROM IMS_WH WHERE WH_ID = @WH_ID";
                        SqlDataAdapter sqlDa = new SqlDataAdapter(getWHDataQury, sqlcon);
                        sqlDa.SelectCommand.Parameters.AddWithValue("@WH_ID", WHID);
                        DataTable dtbl = new DataTable();
                        sqlDa.Fill(dtbl);

                        txtWHID.Text = WHID.ToString();
                        txtWHName.Text = dtbl.Rows[0][1].ToString();

                        lblSuccessMessage.Text = "";
                        lblErrorMessage.Text = "";
                    }
                    else
                    {
                        // Check if WH_Name exists
                        string checkWHNameQuery = "SELECT COUNT(*) FROM IMS_WH WHERE WH_Name = @WH_Name";
                        SqlCommand checkWHNameCmd = new SqlCommand(checkWHNameQuery, sqlcon);
                        checkWHNameCmd.Parameters.AddWithValue("@WH_Name", txtWHName.Text.Trim());
                        int nameCount = (int)checkWHNameCmd.ExecuteScalar();
                        if (nameCount > 0)
                        {
                            lblErrorMessage.Text = "WH Name Already Exists!!!";
                        }
                        else
                        {
                            WarehouseHandler warehouseHandler = new WarehouseHandler(sqlcon, userId);

                            warehouseHandler.AddWarehouse(
                            Convert.ToInt32(txtWHID.Text.Trim()),
                            txtWHName.Text.Trim()
                            );

                            clear();
                            lblSuccessMessage.Text = "WH Added Successfully!";
                            lblErrorMessage.Text = "";
                        }
                    }
                }
            }
        }
        void clear()
        {
            txtWHName.Text = "";
            lblSuccessMessage.Text = lblErrorMessage.Text = "";
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("createnewobject.aspx");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtWHName.Text.Trim() == "" || txtWHID.Text.Trim() == "")
            {
                lblErrorMessage.Text = "Please fill in all mandatory fields!!";
                lblErrorMessage.Visible = true;
            }
            else if (Convert.ToInt32(txtWHID.Text.Trim()) < 101)
            {
                lblErrorMessage.Text = "Unavailable User ID!!";
                lblErrorMessage.Visible = true;
            }
            else
            {
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    sqlcon.Open();
                    // Check if WH_Name exists
                    string checkWHNameQuery = @"SELECT COUNT(*) FROM IMS_WH WHERE WH_Name = @WH_Name and WH_ID != @WH_ID";
                    SqlCommand checkWHNameCmd = new SqlCommand(checkWHNameQuery, sqlcon);
                    checkWHNameCmd.Parameters.AddWithValue("@WH_Name", txtWHName.Text.Trim());
                    checkWHNameCmd.Parameters.AddWithValue("@WH_ID", Convert.ToInt32(txtWHID.Text.Trim()));
                    int whCount = (int)checkWHNameCmd.ExecuteScalar();
                    if (whCount > 0)
                    {
                        lblErrorMessage.Text = "WH Name Already Exists!!!";
                        lblErrorMessage.Visible = true;
                    }
                    else
                    {
                        WarehouseHandler warehouseHandler = new WarehouseHandler(sqlcon, userId);

                        warehouseHandler.UpdateWarehouse(
                        Convert.ToInt32(txtWHID.Text.Trim()),
                        txtWHName.Text.Trim()
                        );

                        clear();
                        lblSuccessMessage.Text = "WH Edited Successfully!";
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

                WarehouseHandler warehouseHandler = new WarehouseHandler(sqlcon, userId);

                warehouseHandler.DeleteWarehouse(Convert.ToInt32(txtWHID.Text.Trim()));

                clear();
                lblSuccessMessage.Text = "WH Deleted Successfully!";
                lblErrorMessage.Text = "";
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IT508_Project.Utilities;

namespace IT508_Project
{
    public partial class addNewUser : System.Web.UI.Page
    {
        string connectionString = @"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string userIDQury = "SELECT MAX(UserID) FROM IMS_Users";
                IDGenerator idGenerator = new IDGenerator(connectionString);
                int startUserID = 1;
                int nextUserID = idGenerator.GetNextID(userIDQury, startUserID);
                txtUserID.Text = nextUserID.ToString();

                clear();
            }
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            if (txtUserID.Text.Trim() == "" || Convert.ToInt32(txtUserID.Text.Trim()) < 1)
            {
                lblErrorMessage.Text = "Please inter a valid User ID";
            }
            else
            {
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    sqlcon.Open();

                    // Check if User_ID exists
                    string checkUserIDQuery = "SELECT COUNT(*) FROM IMS_Users WHERE UserID = @UserID";
                    SqlCommand checkUserIDCmd = new SqlCommand(checkUserIDQuery, sqlcon);
                    checkUserIDCmd.Parameters.AddWithValue("@UserID", Convert.ToInt32(txtUserID.Text.Trim()));
                    int count = (int)checkUserIDCmd.ExecuteScalar();
                    if (count > 0)
                    {
                        int userID = Convert.ToInt32(txtUserID.Text.Trim());
                        string getUserDataQury = @"SELECT * FROM IMS_Users WHERE UserID = @UserID";
                        SqlDataAdapter sqlDa = new SqlDataAdapter(getUserDataQury, sqlcon);
                        sqlDa.SelectCommand.Parameters.AddWithValue("@UserID", userID);
                        DataTable dtbl = new DataTable();
                        sqlDa.Fill(dtbl);

                        txtUserID.Text = userID.ToString();
                        txtusername.Text = dtbl.Rows[0][1].ToString();
                        txtpassword.Text = dtbl.Rows[0][2].ToString();
                        txtpassword.Attributes.Add("value", dtbl.Rows[0][2].ToString());
                        txtcpassword.Text = dtbl.Rows[0][2].ToString();
                        txtcpassword.Attributes.Add("value", dtbl.Rows[0][2].ToString());
                        txtddl.Text = dtbl.Rows[0][3].ToString();

                        lblSuccessMessage.Text = "";
                        lblErrorMessage.Text = "";
                    }
                    else
                    {
                        // Check if User_Name exists
                        string checkUserNameQuery = "SELECT COUNT(*) FROM IMS_Users WHERE username = @username";
                        SqlCommand checkUserNameCmd = new SqlCommand(checkUserNameQuery, sqlcon);
                        checkUserNameCmd.Parameters.AddWithValue("@username", txtusername.Text.Trim());
                        int nameCount = (int)checkUserNameCmd.ExecuteScalar();
                        if (nameCount > 0)
                        {
                            lblErrorMessage.Text = "Username Already Exists!!!";
                        }
                        else
                        {
                            if (txtpassword.Text != txtcpassword.Text)
                            {
                                lblErrorMessage.Text = "The passwords do not match!!!";
                                lblErrorMessage.Visible = true;
                            }
                            else if (txtusername.Text.Trim() == "")
                            {
                                lblErrorMessage.Text = "User Name cannot be empty!!!";
                                lblErrorMessage.Visible = true;
                            }
                            else
                            {
                                UserHandler userHandler = new UserHandler(sqlcon);

                                userHandler.AddUser(
                                Convert.ToInt32(txtUserID.Text.Trim()),
                                txtusername.Text.Trim(),
                                txtpassword.Text.Trim(),
                                txtddl.Text
                                );

                                clear();
                                lblSuccessMessage.Text = "User Added Successfully!";
                                lblErrorMessage.Text = "";
                            }
                        }
                    }
                }
            }
        }
        void clear()
        {
            txtusername.Text = txtpassword.Text = txtcpassword.Text = "";
            lblSuccessMessage.Text = lblErrorMessage.Text = "";
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("createnewobject.aspx");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtusername.Text.Trim() == "" || txtUserID.Text.Trim() == "" || txtpassword.Text.Trim() == "")
            {
                lblErrorMessage.Text = "Please fill in all mandatory fields.";
                lblErrorMessage.Visible = true;
            }
            else if (Convert.ToInt32(txtUserID.Text.Trim()) < 1)
            {
                lblErrorMessage.Text = "Unavailable User ID!!";
                lblErrorMessage.Visible = true;
            }
            else if (txtpassword.Text != txtcpassword.Text)
            {
                lblErrorMessage.Text = "The passwords do not match!!!";
            }
            else
            {
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    sqlcon.Open();
                    // Check if User_Name exists
                    string checkUserNameQuery = "SELECT COUNT(*) FROM IMS_Users WHERE username = @username and UserID != @UserID";
                    SqlCommand checkUserNameCmd = new SqlCommand(checkUserNameQuery, sqlcon);
                    checkUserNameCmd.Parameters.AddWithValue("@username", txtusername.Text.Trim());
                    checkUserNameCmd.Parameters.AddWithValue("@UserID", Convert.ToInt32(txtUserID.Text.Trim()));
                    int nameCount = (int)checkUserNameCmd.ExecuteScalar();
                    if (nameCount > 0)
                    {
                        lblErrorMessage.Text = "User Name Already Exists!!!";
                        lblErrorMessage.Visible = true;
                    }
                    else
                    {
                        UserHandler userHandler = new UserHandler(sqlcon);

                        userHandler.UpdateUser(
                        Convert.ToInt32(txtUserID.Text.Trim()),
                        txtusername.Text.Trim(),
                        txtpassword.Text.Trim(),
                        txtddl.Text
                        );

                        clear();
                        lblSuccessMessage.Text = "User Edited Successfully!";
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

                UserHandler userHandler = new UserHandler(sqlcon);

                userHandler.DeleteUser(Convert.ToInt32(txtUserID.Text.Trim()));

                clear();
                lblSuccessMessage.Text = "User Deleted Successfully!";
                lblErrorMessage.Text = "";
            }
        }
    }
}
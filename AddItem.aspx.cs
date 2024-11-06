using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using IT508_Project.Utilities;

namespace IT508_Project
{
    public partial class AddItem : System.Web.UI.Page
    {
        string connectionString = @"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;";
        private int userId;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Retrieve the username from the session
            string username = (string)Session["username"];

            // Use the utility class to get the UserID
            userId = UserDataHelper.GetUserID(username);

            if (!IsPostBack)
            {
                string itemIDQury = "SELECT MAX(Item_ID) FROM IMS_Items";
                IDGenerator idGenerator = new IDGenerator(connectionString);
                int startItemID = 100000;
                int nextItemID = idGenerator.GetNextID(itemIDQury, startItemID);
                txtItemID.Text = nextItemID.ToString();

                clear();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtItemID.Text.Trim() == "" || Convert.ToInt32(txtItemID.Text.Trim()) < 100001)
            {
                lblErrorMessage.Text = "Please inter a valid Item ID";
            }
            else
            {
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    sqlcon.Open();
                    // Check if Item_ID exists
                    string checkItemIDQuery = "SELECT COUNT(*) FROM IMS_Items WHERE Item_ID = @Item_ID";
                    SqlCommand checkItemIDCmd = new SqlCommand(checkItemIDQuery, sqlcon);
                    checkItemIDCmd.Parameters.AddWithValue("@Item_ID", Convert.ToInt32(txtItemID.Text.Trim()));
                    int count = (int)checkItemIDCmd.ExecuteScalar();
                    if (count > 0)
                    {
                        int itemID = Convert.ToInt32(txtItemID.Text.Trim());
                        string getItemDataQury = @"SELECT * FROM IMS_Items WHERE Item_ID = @Item_ID";
                        SqlDataAdapter sqlDa = new SqlDataAdapter(getItemDataQury, sqlcon);
                        sqlDa.SelectCommand.Parameters.AddWithValue("@Item_ID", itemID);
                        DataTable dtbl = new DataTable();
                        sqlDa.Fill(dtbl);

                        txtItemID.Text = itemID.ToString();
                        txtitemname.Text = dtbl.Rows[0][1].ToString();
                        txtddl2.Text = dtbl.Rows[0][2].ToString();
                        txtproddate.Text = Convert.ToDateTime(dtbl.Rows[0][3]).ToString("yyyy-MM-dd");
                        txtexpdate.Text = Convert.ToDateTime(dtbl.Rows[0][4]).ToString("yyyy-MM-dd");
                        txtreorder.Text = dtbl.Rows[0][5].ToString();
                        Textmeasureunit.Text = dtbl.Rows[0][6].ToString();

                        lblSuccessMessage.Text = "";
                        lblErrorMessage.Text = "";
                    }
                    else
                    {
                        if (txtitemname.Text == "" || txtreorder.Text == "")
                        {
                            lblErrorMessage.Text = "Please Fill Mandetory Fields!!";
                        }
                        else
                        {
                            // Check if Item_Name exists
                            string checkItemNameQuery = "SELECT COUNT(*) FROM IMS_Items WHERE Item_Name = @Item_Name";
                            SqlCommand checkItemNameCmd = new SqlCommand(checkItemNameQuery, sqlcon);
                            checkItemNameCmd.Parameters.AddWithValue("@Item_Name", txtitemname.Text.Trim());
                            int nameCount = (int)checkItemNameCmd.ExecuteScalar();
                            if (nameCount > 0)
                            {
                                lblErrorMessage.Text = "Item Name Already Exists!!!";
                            }
                            else
                            {
                                ItemHandler itemHandler = new ItemHandler(sqlcon, userId);

                                itemHandler.AddItem(
                                Convert.ToInt32(txtItemID.Text.Trim()),
                                txtitemname.Text.Trim(),
                                txtddl2.Text,
                                Convert.ToDateTime(txtproddate.Text),
                                Convert.ToDateTime(txtexpdate.Text),
                                Convert.ToInt32(txtreorder.Text),
                                Textmeasureunit.Text.Trim()
                                );

                                clear();
                                lblSuccessMessage.Text = "Item Added Successfully!";
                                lblErrorMessage.Text = "";
                            }
                        }
                    }
                }
            }
        }
        void clear()
        {
            txtitemname.Text = txtproddate.Text = txtexpdate.Text = txtreorder.Text = Textmeasureunit.Text = "";
            lblSuccessMessage.Text = lblErrorMessage.Text = "";
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("createnewobject.aspx");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtitemname.Text.Trim() == "" || txtItemID.Text.Trim() == "" || txtreorder.Text == "")
            {
                lblErrorMessage.Text = "Please fill in all mandatory fields.";
                lblErrorMessage.Visible = true;
            }
            else if (Convert.ToInt32(txtItemID.Text.Trim()) < 100001)
            {
                lblErrorMessage.Text = "Invalid Item ID!!";
                lblErrorMessage.Visible = true;
            }
            else
            {
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    sqlcon.Open();
                    // Check if Item_Name exists
                    string checkItemNameQuery = "SELECT COUNT(*) FROM IMS_Items WHERE Item_Name = @Item_Name and Item_ID != @Item_ID";
                    SqlCommand checkItemNameCmd = new SqlCommand(checkItemNameQuery, sqlcon);
                    checkItemNameCmd.Parameters.AddWithValue("@Item_Name", txtitemname.Text.Trim());
                    checkItemNameCmd.Parameters.AddWithValue("@Item_ID", Convert.ToInt32(txtItemID.Text.Trim()));
                    int nameCount = (int)checkItemNameCmd.ExecuteScalar();
                    if (nameCount > 0)
                    {
                        lblErrorMessage.Text = "Item Name Already Exists!!!";
                        lblErrorMessage.Visible = true;
                    }
                    else
                    {
                        ItemHandler itemHandler = new ItemHandler(sqlcon, userId);

                        itemHandler.UpdateItem(
                        Convert.ToInt32(txtItemID.Text.Trim()),
                        txtitemname.Text.Trim(),
                        txtddl2.Text,
                        Convert.ToDateTime(txtproddate.Text),
                        Convert.ToDateTime(txtexpdate.Text),
                        Convert.ToInt32(txtreorder.Text),
                        Textmeasureunit.Text.Trim()
                        );

                        clear();
                        lblSuccessMessage.Text = "Item Edited Successfully!";
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

                ItemHandler itemHandler = new ItemHandler(sqlcon, userId);

                itemHandler.DeleteItem(Convert.ToInt32(txtItemID.Text.Trim()));

                clear();
                lblSuccessMessage.Text = "Item Deleted Successfully!";
                lblErrorMessage.Text = "";
            }
        }
    }
}
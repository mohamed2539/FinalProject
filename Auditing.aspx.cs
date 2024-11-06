using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using IT508_Project.Utilities;

namespace IT508_Project
{
    public partial class Auditing : System.Web.UI.Page
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
                ViewState["SerialNumber"] = 1;
                txttransdate.Text = DateTime.Today.ToString("yyyy-MM-dd");
                string whQury = @"select WH_ID, WH_Name from IMS_WH";
                string tanstypeQury = @"SELECT Trans_Type_ID, Trans_Type_Name FROM Trans_Type WHERE Trans_Type_ID > 80;";
                string TransIDQury = @"SELECT MAX(Trans_ID) FROM Tran_Master where Trans_Type_ID > 80";

                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    int nxtTransID = 2000;
                    sqlcon.Open();
                    SqlCommand cmd = new SqlCommand(TransIDQury, sqlcon);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        if (reader.IsDBNull(0))
                        {
                            nxtTransID = 2000;
                        }
                        else
                        {
                            nxtTransID = Convert.ToInt32(reader[0]);
                        }
                        nxtTransID++;
                    }
                    transID.Text = nxtTransID.ToString();
                }
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    try
                    {
                        sqlcon.Open();
                        SqlCommand cmd = new SqlCommand(whQury, sqlcon);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            string optionValue = reader["WH_ID"].ToString();
                            string optionText = reader["WH_Name"].ToString();

                            int whIDInt = int.Parse(optionValue);

                            whddl.Items.Add(new ListItem(optionText, optionValue));
                        }
                        whddl.Items.Insert(0, new ListItem("Select WH", "0"));
                        whddl.SelectedIndex = 0;
                    }
                    catch
                    {

                    }
                    finally
                    {
                        sqlcon.Close();
                    }
                }
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    try
                    {
                        sqlcon.Open();
                        SqlCommand cmd = new SqlCommand(tanstypeQury, sqlcon);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            string optionValue = reader["Trans_Type_ID"].ToString();
                            string optionText = reader["Trans_Type_Name"].ToString();
                            transtypeddl.Items.Add(new ListItem(optionText, optionValue));
                        }
                        transtypeddl.Items.Insert(0, new ListItem("Select Type", "0"));
                        transtypeddl.SelectedIndex = 0;
                    }
                    catch
                    {

                    }
                    finally
                    {
                        sqlcon.Close();
                    }
                }

                BindGridView();
            }
        }
        private void BindGridView()
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    string gvQuery = "SELECT Item_ID, Quantity FROM Stock where WH_ID=@WH_ID";
                    sqlcon.Open();
                    SqlCommand cmd = new SqlCommand(gvQuery, sqlcon);
                    cmd.Parameters.AddWithValue("@WH_ID", Convert.ToInt32(whddl.SelectedItem.Value));
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    // Add a 'Serial' column to the DataTable
                    dt.Columns.Add("Serial", typeof(int));
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dt.Rows[i]["Serial"] = i + 1;
                    }

                    if (dt.Rows.Count > 0)
                    {
                        gvTransDetails.DataSource = dt;
                        gvTransDetails.DataBind();
                    }
                    else
                    {
                        gvTransDetails.DataSource = null;
                        gvTransDetails.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error : " + ex.Message);
            }
        }
        protected void gvTransDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Set the initial difference to 0
                Label lblDifference = (Label)e.Row.FindControl("lblDifference");
                lblDifference.Text = "0";
            }
        }
        protected void txtPhysicalQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtPhysicalQty = (TextBox)sender;
                GridViewRow row = (GridViewRow)txtPhysicalQty.NamingContainer;

                // Get the Quantity value from the GridView
                int quantity = Convert.ToInt32(gvTransDetails.DataKeys[row.RowIndex].Values["Quantity"]);

                // Calculate the difference
                int physicalQty;
                if (int.TryParse(txtPhysicalQty.Text, out physicalQty))
                {
                    int difference = physicalQty - quantity;

                    // Display the difference
                    Label lblDifference = (Label)row.FindControl("lblDifference");
                    lblDifference.Text = difference.ToString();
                }
                else
                {
                    // If parsing fails, set the difference to 0 or handle as needed
                    Label lblDifference = (Label)row.FindControl("lblDifference");
                    lblDifference.Text = "0";
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
        }

        protected void saveMaster_Click(object sender, EventArgs e)
        {
            TransMasterHandler auditboundTransMaster = new TransMasterHandler(connectionString);

            if (auditboundTransMaster.TransIDExists(transID.Text.Trim()))
            {
                string username = (string)Session["username"];

                if (auditboundTransMaster.IsUserAdmin(username))
                {
                    // Call the merged method FetchTranMasterData and specify which additional data to fetch
                    auditboundTransMaster.FetchTranMasterData(
                        transID.Text.Trim(),
                        out string whID,
                        out string transTypeID,
                        out DateTime transDate,
                        out string supID,
                        out string cusID,
                        fetchSupID: false,
                        fetchCusID: false
                    );

                    // Populate the UI fields with the fetched data
                    whddl.Text = whID;
                    transtypeddl.Text = transTypeID;
                    txttransdate.Text = transDate.ToString("yyyy-MM-dd");

                    BindGridView();
                    lblSuccessMessage.Text = "Transaction Data loaded for modification or deletion.";
                }
            }
            else
            {
                if (whddl.SelectedIndex == 0 || transtypeddl.SelectedIndex == 0)
                {
                    lblErrorMessage.Text = "Please Fill Mandatory Fields!!!";
                    lblSuccessMessage.Text = "";
                }
                else
                {
                    auditboundTransMaster.InsertTranMaster(transID.Text.Trim(), whddl.Text, transtypeddl.Text, txttransdate.Text, userId.ToString());
                    lblSuccessMessage.Text = "Transaction Master was added Successfully!";
                }
            }

            BindGridView();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            StockHandler stockUpdate = new StockHandler(connectionString);
            bool isValid = true;

            try
            {
                // First, check if any Physical Qty is less than 0
                foreach (GridViewRow row in gvTransDetails.Rows)
                {
                    TextBox txtPhysicalQty = (TextBox)row.FindControl("txtPhysicalQty");
                    int physicalQty;
                    if (txtPhysicalQty != null && int.TryParse(txtPhysicalQty.Text, out physicalQty))
                    {
                        if (physicalQty < 0)
                        {
                            lblErrorMessage.Text = "Physical Quantity cannot be less than zero!";
                            lblSuccessMessage.Text = "";
                            isValid = false;
                            break;
                        }
                    }
                }

                // If all quantities are valid, then proceed to update the stock
                if (isValid)
                {
                    foreach (GridViewRow row in gvTransDetails.Rows)
                    {
                        Label lblDifference = (Label)row.FindControl("lblDifference");
                        if (lblDifference != null && !string.IsNullOrEmpty(lblDifference.Text))
                        {
                            int difference;
                            if (int.TryParse(lblDifference.Text, out difference) && difference != 0)
                            {
                                TextBox txtPhysicalQty = (TextBox)row.FindControl("txtPhysicalQty");
                                int physicalQty;
                                if (txtPhysicalQty != null && int.TryParse(txtPhysicalQty.Text, out physicalQty) && physicalQty >= 0)
                                {
                                    // Get the Item_ID from DataKeys and WH_ID
                                    int itemID = Convert.ToInt32(gvTransDetails.DataKeys[row.RowIndex].Values["Item_ID"]);
                                    int whId = Convert.ToInt32(whddl.SelectedItem.Value);

                                    // Update the Quantity in the database
                                    stockUpdate.auditItemQuantity(itemID, whId, physicalQty);

                                    lblSuccessMessage.Text = "Transactions Done Successfully!";
                                    lblErrorMessage.Text = "";
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Response.Write("Error: " + ex.Message);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Dashboard.aspx");
        }

        private void returnItemQuantity(int itemID, int oldValue)
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    string updateQuery = "UPDATE Stock SET Quantity = @oldValue WHERE Item_ID = @ItemID";
                    sqlcon.Open();
                    SqlCommand cmd = new SqlCommand(updateQuery, sqlcon);
                    cmd.Parameters.AddWithValue("@oldValue", oldValue);
                    cmd.Parameters.AddWithValue("@ItemID", itemID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error updating database: " + ex.Message);
            }
        }
    }
}
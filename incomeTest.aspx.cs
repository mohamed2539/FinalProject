using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using IT508_Project.Utilities;

namespace IT508_Project
{
    public partial class incomeTest : System.Web.UI.Page
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
                SerialNum.Text = ViewState["SerialNumber"].ToString();
                txttransdate.Text = DateTime.Today.ToString("yyyy-MM-dd");
                string whQury = "select WH_ID, WH_Name from IMS_WH";
                string tanstypeQury = "SELECT Trans_Type_ID, Trans_Type_Name FROM Trans_Type WHERE Trans_Type_ID <=40;";
                string supQury = "select Supplier_ID, Supplier_Name from IMS_Supplier";
                string itemQury = "select Item_ID, Item_Name from IMS_Items";
                string TransIDQury = "SELECT MAX(Trans_ID) FROM Tran_Master where Trans_Type_ID < 40";
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    int nxtTransID = 0;
                    sqlcon.Open();
                    SqlCommand cmd = new SqlCommand(TransIDQury, sqlcon);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        if (reader.IsDBNull(0))
                        {
                            nxtTransID = 0;
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
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    try
                    {
                        sqlcon.Open();
                        SqlCommand cmd = new SqlCommand(supQury, sqlcon);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            string optionValue = reader["Supplier_ID"].ToString();
                            string optionText = reader["Supplier_Name"].ToString();
                            supddl.Items.Add(new ListItem(optionText, optionValue));
                        }
                        supddl.Items.Insert(0, new ListItem("Select Supplier", "0"));
                        supddl.SelectedIndex = 0;
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
                        SqlCommand cmd = new SqlCommand(itemQury, sqlcon);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            string optionValue = reader["Item_ID"].ToString();
                            string optionText = reader["Item_Name"].ToString();
                            ddlItemID.Items.Add(new ListItem(optionValue, optionText));
                        }
                        ddlItemID.Items.Insert(0, new ListItem("Select Item", "0"));
                        ddlItemID.SelectedIndex = 0;
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

        protected void saveMaster_Click(object sender, EventArgs e)
        {
            TransMasterHandler inboundTransMaster = new TransMasterHandler(connectionString);

            if (inboundTransMaster.TransIDExists(transID.Text.Trim()))
            {
                string username = (string)Session["username"];

                if (inboundTransMaster.IsUserAdmin(username))
                {
                    // Call the merged method FetchTranMasterData and specify which additional data to fetch
                    inboundTransMaster.FetchTranMasterData(
                        transID.Text.Trim(),
                        out string whID,
                        out string transTypeID,
                        out DateTime transDate,
                        out string supID,
                        out string cusID,
                        fetchSupID: true // Set to true to fetch Sup_ID
                    );

                    // Populate the UI fields with the fetched data
                    whddl.Text = whID;
                    transtypeddl.Text = transTypeID;
                    txttransdate.Text = transDate.ToString("yyyy-MM-dd");
                    supddl.Text = supID; // Use supID since fetchSupID is set to true

                    BindGridView();
                    lblSuccessMessage.Text = "Transaction Data loaded for modification or deletion.";
                }
                else
                {
                    lblErrorMessage.Text = "You do not have the necessary permissions to modify this transaction.";
                    lblSuccessMessage.Text = "";
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
                    inboundTransMaster.InsertTranMaster(transID.Text.Trim(), whddl.Text, transtypeddl.Text, txttransdate.Text, userId.ToString(), supID: supddl.Text);
                    lblSuccessMessage.Text = "Transaction Master was added Successfully!";
                }
            }
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            ValidationHelper validationHelper = new ValidationHelper(connectionString);
            StockHandler inboundHandler = new StockHandler(connectionString);
            DetailsHandler inboundDetails = new DetailsHandler(connectionString);

            int itemId = Convert.ToInt32(ddlItemID.SelectedItem.Text);
            int whId = Convert.ToInt32(whddl.SelectedItem.Value);

            if (validationHelper.CheckItemExists(itemId, whId))
            {
                try
                {
                    int serialNum = Convert.ToInt32(SerialNum.Text);
                    int transId = Convert.ToInt32(transID.Text.Trim());
                    int transTypeId = Convert.ToInt32(transtypeddl.SelectedItem.Value);
                    int quantity = Convert.ToInt32(txtQuantity.Text.Trim());
                    string position = txtPosition.Text;

                    inboundDetails.InsertTransaction(serialNum, transId, whId, transTypeId, itemId, quantity, position);
                    inboundHandler.UpdateInStockQuantity(itemId, whId, quantity, position);

                    lblSuccessMessage.Text = "Detailed Record added Successfully!";
                    lblErrorMessage.Text = "";
                }
                catch (Exception ex)
                {
                    lblErrorMessage.Text = "An error occurred: " + ex.Message;
                    lblSuccessMessage.Text = "";
                    //Response.Write(ex.Message);
                }
                finally
                {
                    BindGridView();
                }
            }
            else
            {
                lblErrorMessage.Text = "This Item Not Linked to this WH";
                lblSuccessMessage.Text = "";
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Dashboard.aspx");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Create an instance of the DeleteTrans class
                DeleteTrans deleteTrans = new DeleteTrans(connectionString);

                // Get necessary parameters
                int warehouseID = Convert.ToInt32(whddl.SelectedItem.Value);
                int transTypeID = Convert.ToInt32(transtypeddl.SelectedItem.Value); // Determine if inbound or outbound

                // Call the DeleteTransaction method from DeleteTrans class
                deleteTrans.DeleteTransaction(transID.Text.Trim(), transTypeID, warehouseID, gvTransDetails, lblSuccessMessage, lblErrorMessage);

                // Clear and rebind data
                clearMaster();
                BindGridView();
            }
            catch (Exception ex)
            {
                Response.Write("Error : " + ex.Message);
            }
        }

        protected void ddlItemID_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtItemName.Text = ddlItemID.SelectedItem.Value;
        }
        private void BindGridView()
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    string gvQuery = "SELECT Serial, Item_ID, Quantity, Position FROM Transactions where Trans_ID=@Trans_ID and WH_ID=@WH_ID and Trans_Type_ID=@Trans_Type_ID";
                    sqlcon.Open();
                    SqlCommand cmd = new SqlCommand(gvQuery, sqlcon);
                    cmd.Parameters.AddWithValue("@Trans_ID", Convert.ToInt32(transID.Text.Trim()));
                    cmd.Parameters.AddWithValue("@WH_ID", Convert.ToInt32(whddl.SelectedItem.Value));
                    cmd.Parameters.AddWithValue("@Trans_Type_ID", Convert.ToInt32(transtypeddl.SelectedItem.Value));
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        gvTransDetails.DataSource = dt;
                        gvTransDetails.DataBind();

                        int serialNumber = (int)ViewState["SerialNumber"];
                        serialNumber++;
                        ViewState["SerialNumber"] = serialNumber;
                        SerialNum.Text = serialNumber.ToString();

                        clearDetailRecord();
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error : " + ex.Message);
            }
        }
        protected void gvTransDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteRow")
            {
                int rowIndex;
                if (int.TryParse(e.CommandArgument.ToString(), out rowIndex))
                {
                    try
                    {
                        GridViewRow row = gvTransDetails.Rows[rowIndex];

                        // Retrieve serial from the DataKeys or other means if available
                        int serial = Convert.ToInt32(gvTransDetails.DataKeys[rowIndex].Values["Serial"]);
                        int itemID = Convert.ToInt32(gvTransDetails.DataKeys[rowIndex].Values["Item_ID"]);
                        int rowQuantity = Convert.ToInt32(gvTransDetails.DataKeys[rowIndex].Values["Quantity"]);


                        using (SqlConnection sqlcon = new SqlConnection(connectionString))
                        {
                            sqlcon.Open();

                            string getQntyQury = "SELECT Quantity FROM Stock WHERE Item_ID = @Item_ID and WH_ID = @WH_ID";
                            SqlCommand getQntyCmd = new SqlCommand(getQntyQury, sqlcon);
                            getQntyCmd.Parameters.AddWithValue("@Item_ID", itemID);
                            getQntyCmd.Parameters.AddWithValue("@WH_ID", Convert.ToInt32(whddl.SelectedItem.Value));
                            int currentQnty = (int)getQntyCmd.ExecuteScalar();

                            int updatedQuantity = currentQnty - rowQuantity;
                            string qntyUpdateQury = @"UPDATE Stock SET Quantity = @updatedQuantity WHERE Item_ID = @Item_ID and WH_ID = @WH_ID";
                            SqlCommand qntyUpdateCmd = new SqlCommand(qntyUpdateQury, sqlcon);
                            qntyUpdateCmd.Parameters.AddWithValue("@Item_ID", itemID);
                            qntyUpdateCmd.Parameters.AddWithValue("@WH_ID", Convert.ToInt32(whddl.SelectedItem.Value));
                            qntyUpdateCmd.Parameters.AddWithValue("@updatedQuantity", updatedQuantity);
                            qntyUpdateCmd.ExecuteNonQuery();

                            string deleteQuery = "DELETE FROM Transactions WHERE Serial=@Serial and Trans_ID=@Trans_ID and WH_ID=@WH_ID and Trans_Type_ID=@Trans_Type_ID";
                            SqlCommand cmd = new SqlCommand(deleteQuery, sqlcon);
                            cmd.Parameters.AddWithValue("@Serial", serial);
                            cmd.Parameters.AddWithValue("@Trans_ID", Convert.ToInt32(transID.Text.Trim()));
                            cmd.Parameters.AddWithValue("@WH_ID", Convert.ToInt32(whddl.SelectedItem.Value));
                            cmd.Parameters.AddWithValue("@Trans_Type_ID", Convert.ToInt32(transtypeddl.SelectedItem.Value));
                            cmd.ExecuteNonQuery();

                            lblSuccessMessage.Text = "Record Deleted Successfully!";
                            lblErrorMessage.Text = "";
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("Error : " + ex.Message);
                    }

                    BindGridView();
                }
            }
        }
        void clearDetailRecord ()
        {
            ddlItemID.SelectedIndex = 0;
            txtItemName.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            txtPosition.Text = string.Empty;
        }
        void clearMaster()
        {
            whddl.SelectedIndex = 0;
            transtypeddl.SelectedIndex = 0;
            supddl.SelectedIndex = 0;
        }
    }
}
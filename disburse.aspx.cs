using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Data;
using IT508_Project.Utilities;

namespace IT508_Project
{
    public partial class disburse : System.Web.UI.Page
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
                string whQury = @"select WH_ID, WH_Name from IMS_WH";
                string tanstypeQury = @"SELECT Trans_Type_ID, Trans_Type_Name FROM Trans_Type WHERE Trans_Type_ID between 41 and 80;";
                string cusQury = @"select Cus_ID, Cus_Name from IMS_Customer";
                string itemQury = @"select Item_ID, Item_Name from IMS_Items";
                string TransIDQury = @"SELECT MAX(Trans_ID) FROM Tran_Master where Trans_Type_ID between 41 and 80";

                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    int nxtTransID = 1000;
                    sqlcon.Open();
                    SqlCommand cmd = new SqlCommand(TransIDQury, sqlcon);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        if (reader.IsDBNull(0))
                        {
                            nxtTransID = 1000;
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
                        SqlCommand cmd = new SqlCommand(cusQury, sqlcon);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            string optionValue = reader["Cus_ID"].ToString();
                            string optionText = reader["Cus_Name"].ToString();
                            cusddl.Items.Add(new ListItem(optionText, optionValue));
                        }
                        cusddl.Items.Insert(0, new ListItem("Select Customer", "0"));
                        cusddl.SelectedIndex = 0;
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
        protected void ddlItemID_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtItemName.Text = ddlItemID.SelectedItem.Value;
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                string positionQury = @"select Position from Stock where Item_ID=@ItemID2 and WH_ID=@WHID2";
                SqlCommand pCmd = new SqlCommand(positionQury, sqlcon);
                pCmd.Parameters.AddWithValue("ItemID2", ddlItemID.SelectedItem.Text);
                pCmd.Parameters.AddWithValue("WHID2", whddl.SelectedItem.Value);
                SqlDataReader reader = pCmd.ExecuteReader();
                if (reader.Read())
                {
                    string iPosition = reader.GetString(0);
                    lblPosition.Text = iPosition;
                }
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

                            int updatedQuantity = currentQnty + rowQuantity;
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
        void clearDetailRecord()
        {
            ddlItemID.SelectedIndex = 0;
            txtItemName.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            lblPosition.Text = string.Empty;
        }
        void clearMaster()
        {
            whddl.SelectedIndex = 0;
            transtypeddl.SelectedIndex = 0;
            cusddl.SelectedIndex = 0;
        }

        protected void saveMaster_Click(object sender, EventArgs e)
        {
            TransMasterHandler outboundTransMaster = new TransMasterHandler(connectionString);

            if (outboundTransMaster.TransIDExists(transID.Text.Trim()))
            {
                string username = (string)Session["username"];

                if (outboundTransMaster.IsUserAdmin(username))
                {
                    // Call the merged method FetchTranMasterData and specify which additional data to fetch
                    outboundTransMaster.FetchTranMasterData(
                        transID.Text.Trim(),
                        out string whID,
                        out string transTypeID,
                        out DateTime transDate,
                        out string supID,
                        out string cusID,
                        fetchCusID: true // Fetch Cus_ID instead
                    );

                    // Populate the UI fields with the fetched data
                    whddl.Text = whID;
                    transtypeddl.Text = transTypeID;
                    txttransdate.Text = transDate.ToString("yyyy-MM-dd");
                    cusddl.Text = cusID; // Use cusID since fetchCusID is set to true

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
                    outboundTransMaster.InsertTranMaster(transID.Text.Trim(), whddl.Text, transtypeddl.Text, txttransdate.Text, userId.ToString(), cusID: cusddl.Text);
                    lblSuccessMessage.Text = "Transaction Master was added Successfully!";
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Dashboard.aspx");
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            ValidationHelper validationHelper = new ValidationHelper(connectionString);
            StockHandler outBoundHandler = new StockHandler(connectionString);
            DetailsHandler outboundDetails = new DetailsHandler(connectionString);

            // Get selected item ID and warehouse ID
            int itemId = Convert.ToInt32(ddlItemID.SelectedItem.Text);
            int whId = Convert.ToInt32(whddl.SelectedItem.Value);

            if (validationHelper.CheckItemExists(itemId, whId))
            {
                // Check available stock quantity
                string checkQntyQury = @"SELECT Quantity FROM Stock WHERE Item_ID = @ItemID2 AND WH_ID = @WHID2";
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    sqlcon.Open();
                    SqlCommand checkQntyCmd = new SqlCommand(checkQntyQury, sqlcon);
                    checkQntyCmd.Parameters.AddWithValue("@ItemID2", itemId);
                    checkQntyCmd.Parameters.AddWithValue("@WHID2", whId);

                    int itemQnty2 = (int)checkQntyCmd.ExecuteScalar();
                    int requiredQnty = Convert.ToInt32(txtQuantity.Text.Trim());

                    if (requiredQnty <= itemQnty2)
                    {
                        try
                        {
                            // Validate Quantity
                            if (requiredQnty <= 0)
                            {
                                throw new ArgumentException("Quantity must be greater than 0.", nameof(requiredQnty));
                            }

                            // Call the InsertTransaction method
                            outboundDetails.InsertTransaction(
                                Convert.ToInt32(SerialNum.Text),
                                Convert.ToInt32(transID.Text.Trim()),
                                whId,
                                Convert.ToInt32(transtypeddl.SelectedItem.Value),
                                itemId,
                                requiredQnty,
                                lblPosition.Text
                            );

                            // Update stock quantity
                            outBoundHandler.UpdateOutStockQuantity(
                                itemId,
                                whId,
                                requiredQnty,
                                lblPosition.Text
                            );

                            // Set success message
                            lblSuccessMessage.Text = "Detailed Record added Successfully!";
                            lblErrorMessage.Text = "";
                        }
                        catch (Exception ex)
                        {
                            // Display the error message in lblErrorMessage
                            lblErrorMessage.Text = "An error occurred: " + ex.Message;
                            lblSuccessMessage.Text = "";
                        }
                        finally
                        {
                            // Ensure the GridView is bound regardless of success or failure
                            BindGridView();
                        }
                    }
                    else
                    {
                        lblErrorMessage.Text = "Quantity exceeds available stock.";
                        lblSuccessMessage.Text = "";
                    }
                }
            }
            else
            {
                lblErrorMessage.Text = "This Item Not Linked to this WH";
                lblSuccessMessage.Text = "";
            }
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
    }
}
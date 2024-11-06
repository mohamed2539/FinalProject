using IT508_Project.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IT508_Project
{
    public partial class linkItemToWH : System.Web.UI.Page
    {
        string connectionString = @"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string itemQury = "select Item_ID, Item_Name from IMS_Items";
                string whQury = "select WH_ID, WH_Name from IMS_WH";
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

                            itemddl.Items.Add(new ListItem(optionText, optionValue));
                        }
                        itemddl.Items.Insert(0, new ListItem("Select Item", "0"));
                        itemddl.SelectedIndex = 0;
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
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                if (whddl.SelectedIndex == 0 || itemddl.SelectedIndex == 0)
                {
                    lblErrorMessage.Text = "Please fill in all mandatory fields.";
                    lblSuccessMessage.Text = "";
                }
                else
                {
                    sqlcon.Open();
                    string checkQury = @"select count(*) from Stock where Item_ID=@Item_ID and WH_ID=@WH_ID";
                    SqlCommand checkCmd = new SqlCommand(checkQury, sqlcon);
                    checkCmd.Parameters.AddWithValue("@Item_ID", itemddl.SelectedItem.Value);
                    checkCmd.Parameters.AddWithValue("@WH_ID", whddl.SelectedItem.Value);
                    int count = (int)checkCmd.ExecuteScalar();
                    if (count > 0)
                    {
                        string getLinkQury = @"select * from Stock where Item_ID=@Item_ID and WH_ID=@WH_ID";
                        SqlDataAdapter sqlDa = new SqlDataAdapter(getLinkQury, sqlcon);
                        sqlDa.SelectCommand.Parameters.AddWithValue("@Item_ID", itemddl.Text);
                        sqlDa.SelectCommand.Parameters.AddWithValue("@WH_ID", whddl.SelectedItem.Value);
                        DataTable dtbl = new DataTable();
                        sqlDa.Fill(dtbl);

                        itemddl.Text = itemddl.Text;
                        whddl.SelectedItem.Value = dtbl.Rows[0][1].ToString();
                        txtQuantity.Text = dtbl.Rows[0][2].ToString();
                        txtOpenBalance.Text = dtbl.Rows[0][3].ToString();
                        txtPosition.Text = dtbl.Rows[0][4].ToString();

                        // Disable the txtQuantity TextBox
                        txtQuantity.Enabled = false;
                    }
                    else
                    {
                        if (txtQuantity.Text.Trim() == "" || txtOpenBalance.Text.Trim() == "")
                        {
                            lblErrorMessage.Text = "Please Enter the quantity and open balance!!";
                            lblSuccessMessage.Text = "";
                        }
                        else if (Convert.ToInt32(txtQuantity.Text.Trim()) < 0 || Convert.ToInt32(txtOpenBalance.Text.Trim()) < 0)
                        {
                            lblErrorMessage.Text = "Please Enter a valid quantity and open balance!!";
                            lblSuccessMessage.Text = "";
                        }
                        else
                        {
                            StockHandler linkHandler = new StockHandler(connectionString);
                            linkHandler.InsertLinkItemWh(
                                itemddl.Text,
                                whddl.SelectedItem.Value,
                                Convert.ToInt32(txtQuantity.Text.Trim()),
                                Convert.ToInt32(txtOpenBalance.Text.Trim()),
                                txtPosition.Text.Trim()
                                );

                            clear();
                            lblSuccessMessage.Text = "Link Added Successfully!";
                            lblErrorMessage.Text = "";
                        }
                    }
                }
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("createnewobject.aspx");
        }
        void clear()
        {
            txtQuantity.Text = txtOpenBalance.Text = txtPosition.Text = "";
            itemddl.SelectedIndex = whddl.SelectedIndex = 0;
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                try
                {
                    if (Convert.ToInt32(txtOpenBalance.Text) >= 0)
                    {
                        sqlcon.Open();
                        StockHandler linkHandler = new StockHandler(connectionString);
                        linkHandler.UpdateLinkItemWh(
                            itemddl.Text,
                            whddl.SelectedItem.Value,
                            Convert.ToInt32(txtOpenBalance.Text.Trim()),
                            txtPosition.Text.Trim()
                            );

                        clear();
                        lblSuccessMessage.Text = "Link Edited Successfully!";
                        lblErrorMessage.Text = "";
                    }
                    else
                    {
                        lblErrorMessage.Text = "Enter a valid open balance!!";
                        lblSuccessMessage.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    
                }
            }
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                try
                {
                    sqlcon.Open();
                    string checkQury = @"select count(*) from Stock where Item_ID=@Item_ID and WH_ID=@WH_ID";
                    SqlCommand checkCmd = new SqlCommand(checkQury, sqlcon);
                    checkCmd.Parameters.AddWithValue("@Item_ID", itemddl.SelectedItem.Value);
                    checkCmd.Parameters.AddWithValue("@WH_ID", whddl.SelectedItem.Value);
                    int count = (int)checkCmd.ExecuteScalar();
                    if (count > 0)
                    {
                        StockHandler linkHandler = new StockHandler(connectionString);
                        linkHandler.DelLinkItemWh(
                            itemddl.Text,
                            whddl.SelectedItem.Value
                            );

                        clear();
                        lblSuccessMessage.Text = "Link Deleted Successfully!";
                        lblErrorMessage.Text = "";
                    }
                    else
                    {
                        lblErrorMessage.Text = "This item Not Linked to This WH!";
                        lblSuccessMessage.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    
                }
            }
        }
    }
}
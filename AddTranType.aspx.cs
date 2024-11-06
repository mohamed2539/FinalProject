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
    public partial class AddTranType : System.Web.UI.Page
    {
        string connectionString = @"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ttypeddl.Items.Add(new ListItem("Select Trans Type", ""));
                ttypeddl.Items.Add(new ListItem("Inbound", "inbound"));
                ttypeddl.Items.Add(new ListItem("Outbound", "outbound"));
                ttypeddl.Items.Add(new ListItem("Audit", "audit"));
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtTranTypeID.Text.Trim() == "" || Convert.ToInt32(txtTranTypeID.Text.Trim()) < 1)
            {
                lblErrorMessage.Text = "Please inter a valid TransType ID";
                lblSuccessMessage.Text = "";
            }
            else
            {
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    sqlcon.Open();

                    // Check if TransType ID exists
                    string checktTypeIDQuery = @"SELECT COUNT(*) FROM Trans_Type WHERE Trans_Type_ID = @Trans_Type_ID";
                    SqlCommand checktTypeIDCmd = new SqlCommand(checktTypeIDQuery, sqlcon);
                    checktTypeIDCmd.Parameters.AddWithValue("@Trans_Type_ID", Convert.ToInt32(txtTranTypeID.Text.Trim()));
                    int count = (int)checktTypeIDCmd.ExecuteScalar();
                    if (count > 0)
                    {
                        int TransTypeID = Convert.ToInt32(txtTranTypeID.Text.Trim());
                        string gettTypeDataQury = @"SELECT * FROM Trans_Type WHERE Trans_Type_ID = @Trans_Type_ID";
                        SqlDataAdapter sqlDa = new SqlDataAdapter(gettTypeDataQury, sqlcon);
                        sqlDa.SelectCommand.Parameters.AddWithValue("@Trans_Type_ID", TransTypeID);
                        DataTable dtbl = new DataTable();
                        sqlDa.Fill(dtbl);

                        txtTranTypeID.Text = TransTypeID.ToString();
                        txtTranTypeName.Text = dtbl.Rows[0][1].ToString();

                        lblSuccessMessage.Text = "";
                        lblErrorMessage.Text = "";
                    }
                    else
                    {
                        // Check if WH_Name exists
                        string checktTypeNameQuery = "SELECT COUNT(*) FROM Trans_Type WHERE Trans_Type_Name = @Trans_Type_Name";
                        SqlCommand checktTypeNameCmd = new SqlCommand(checktTypeNameQuery, sqlcon);
                        checktTypeNameCmd.Parameters.AddWithValue("@Trans_Type_Name", txtTranTypeName.Text.Trim());
                        int nameCount = (int)checktTypeNameCmd.ExecuteScalar();
                        if (nameCount > 0)
                        {
                            lblErrorMessage.Text = "TransType Name Already Exists!!!";
                            lblSuccessMessage.Text = "";
                        }
                        else
                        {
                            TransTypeHandler transTypeHandler = new TransTypeHandler(sqlcon);

                            transTypeHandler.AddTransType(
                            Convert.ToInt32(txtTranTypeID.Text.Trim()),
                            txtTranTypeName.Text.Trim()
                            );

                            clear();
                            lblSuccessMessage.Text = "TransType Added Successfully!";
                            lblErrorMessage.Text = "";
                        }
                    }
                }
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtTranTypeName.Text.Trim() == "" || txtTranTypeID.Text.Trim() == "")
            {
                lblErrorMessage.Text = "Please fill in all mandatory fields!!";
                lblErrorMessage.Visible = true;
            }
            else if (Convert.ToInt32(txtTranTypeID.Text.Trim()) < 1)
            {
                lblErrorMessage.Text = "Unavailable TransType ID!!";
                lblErrorMessage.Visible = true;
            }
            else
            {
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    sqlcon.Open();
                    // Check if WH_Name exists
                    string checktTypeNameQuery = @"SELECT COUNT(*) FROM Trans_Type WHERE Trans_Type_Name = @Trans_Type_Name and Trans_Type_ID != @Trans_Type_ID";
                    SqlCommand checktTypeNameCmd = new SqlCommand(checktTypeNameQuery, sqlcon);
                    checktTypeNameCmd.Parameters.AddWithValue("@Trans_Type_Name", txtTranTypeName.Text.Trim());
                    checktTypeNameCmd.Parameters.AddWithValue("@Trans_Type_ID", Convert.ToInt32(txtTranTypeID.Text.Trim()));
                    int tTypeCount = (int)checktTypeNameCmd.ExecuteScalar();
                    if (tTypeCount > 0)
                    {
                        lblErrorMessage.Text = "TransType Name Already Exists!!!";
                        lblErrorMessage.Visible = true;
                    }
                    else
                    {
                        TransTypeHandler transTypeHandler = new TransTypeHandler(sqlcon);

                        transTypeHandler.UpdateTransType(
                        Convert.ToInt32(txtTranTypeID.Text.Trim()),
                        txtTranTypeName.Text.Trim()
                        );

                        clear();
                        lblSuccessMessage.Text = "TransType Edited Successfully!";
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

                TransTypeHandler transTypeHandler = new TransTypeHandler(sqlcon);

                transTypeHandler.DeleteTransType(Convert.ToInt32(txtTranTypeID.Text.Trim()));

                clear();
                lblSuccessMessage.Text = "TransType Deleted Successfully!";
                lblErrorMessage.Text = "";
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("createnewobject.aspx");
        }

        protected void ddltTypeID_SelectedIndexChanged(object sender, EventArgs e)
        {
            int starttTypeID, nexttTypeID;

            if (ttypeddl.SelectedValue == "inbound")
            {
                string tranTypeIDQuery = "SELECT MAX(Trans_Type_ID) FROM Trans_Type WHERE Trans_Type_ID BETWEEN 1 AND 40;";
                IDGenerator idGenerator = new IDGenerator(connectionString);
                starttTypeID = 1;
                nexttTypeID = idGenerator.GetNextID(tranTypeIDQuery, starttTypeID);
                txtTranTypeID.Text = nexttTypeID.ToString();
            }
            else if (ttypeddl.SelectedValue == "outbound")
            {
                string tranTypeIDQuery = "SELECT MAX(Trans_Type_ID) FROM Trans_Type WHERE Trans_Type_ID BETWEEN 41 AND 80;";
                IDGenerator idGenerator = new IDGenerator(connectionString);
                starttTypeID = 41;
                nexttTypeID = idGenerator.GetNextID(tranTypeIDQuery, starttTypeID);
                txtTranTypeID.Text = nexttTypeID.ToString();
            }
            else if (ttypeddl.SelectedValue == "audit")
            {
                string tranTypeIDQuery = "SELECT MAX(Trans_Type_ID) FROM Trans_Type WHERE Trans_Type_ID > 80;";
                IDGenerator idGenerator = new IDGenerator(connectionString);
                starttTypeID = 81;
                nexttTypeID = idGenerator.GetNextID(tranTypeIDQuery, starttTypeID);
                txtTranTypeID.Text = nexttTypeID.ToString();
            }
        }

        void clear()
        {
            txtTranTypeID.Text = txtTranTypeName.Text = "";
            ttypeddl.SelectedIndex = 0;
        }
    }
}
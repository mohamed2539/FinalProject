using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using NPOI.SS.Formula.Functions;
using static NPOI.HSSF.Util.HSSFColor;
using System.Windows.Forms;

namespace IT508_Project
{
    public partial class Dashboard : System.Web.UI.Page
    {
        string connectionString = @"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string countUQuery = @"SELECT COUNT(*) FROM IMS_Users";
                string countIQuery = @"SELECT COUNT(*) FROM IMS_Items";
                string countWQuery = @"SELECT COUNT(*) FROM IMS_WH";
                string countTQuery = @"SELECT COUNT(*) FROM Tran_Master WHERE YEAR(Trans_Date) = YEAR(GETDATE()) AND MONTH(Trans_Date) = MONTH(GETDATE())";
                int usersCount, itemsCount, WHsCount, TMastersCount;
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    sqlcon.Open();
                    using (var sqlCmd = new SqlCommand(countUQuery, sqlcon))
                    {
                        var result = sqlCmd.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out usersCount))
                        {
                            lblUsersCount.Text = usersCount.ToString();
                        }
                        else
                        {
                            lblUsersCount.Text = "Error retrieving count";
                        }
                    }
                }
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    sqlcon.Open();
                    using (var sqlCmd = new SqlCommand(countIQuery, sqlcon))
                    {
                        var result = sqlCmd.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out itemsCount))
                        {
                            lblItemsCount.Text = itemsCount.ToString();
                        }
                        else
                        {
                            lblItemsCount.Text = "Error retrieving count";
                        }
                    }
                }
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    sqlcon.Open();
                    using (var sqlCmd = new SqlCommand(countWQuery, sqlcon))
                    {
                        var result = sqlCmd.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out WHsCount))
                        {
                            lblWHCount.Text = WHsCount.ToString();
                        }
                        else
                        {
                            lblWHCount.Text = "Error retrieving count";
                        }
                    }
                }
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    sqlcon.Open();
                    using (var sqlCmd = new SqlCommand(countTQuery, sqlcon))
                    {
                        var result = sqlCmd.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out TMastersCount))
                        {
                            lblTransCount.Text = TMastersCount.ToString();
                        }
                        else
                        {
                            lblTransCount.Text = "Error retrieving count";
                        }
                    }
                }

                BindGridView();
                BindGridView2();
            }
        }
        private void BindGridView()
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    string gvQuery = @"SELECT IMS_Items.Item_ID, Stock.WH_ID, IMS_Items.Exp_Date 
                                       FROM IMS_Items JOIN Stock ON IMS_Items.Item_ID = Stock.Item_ID 
                                       WHERE IMS_Items.Exp_Date <= DATEADD(DAY, 15, GETDATE()) order by Exp_Date;";
                    sqlcon.Open();
                    SqlCommand cmd = new SqlCommand(gvQuery, sqlcon);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        gvExpireItems.DataSource = dt;
                        gvExpireItems.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error : " + ex.Message);
            }
        }
        private void BindGridView2()
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    string gv2Query = @"SELECT IMS_Items.Item_ID, SUM(Stock.Quantity) AS TotalQuantity, IMS_Items.Reorder_Threshold 
                                        FROM IMS_Items JOIN Stock ON IMS_Items.Item_ID = Stock.Item_ID 
                                        GROUP BY IMS_Items.Item_ID, IMS_Items.Reorder_Threshold 
                                        HAVING SUM(Stock.Quantity) <= IMS_Items.Reorder_Threshold;";
                    sqlcon.Open();
                    SqlCommand cmd = new SqlCommand(gv2Query, sqlcon);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        gvReorder.DataSource = dt;
                        gvReorder.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error : " + ex.Message);
            }
        }
    }
}
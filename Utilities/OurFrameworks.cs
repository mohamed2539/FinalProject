using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;



namespace IT508_Project.Classes
{
    public class OurFrameworks
    {

        public OurFrameworks() { }

        public void SearchMethod()
        {


            if (SearchKayword.Text.Equals(""))
            {
                //Response.Write("You Sloud Fill The Search Box");
                SearchErorrMessage.InnerText = "You Sloud Fill The Search Box And ChackBox for your Search Type";
            }
            else
            {
                string RetriveData = "SELECT * FROM Stock WHERE ";
                bool isFirstCondition = true;
                SqlConnection mySqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");

                if (Quantity_Check.Checked)
                {
                    RetriveData += "Quantity = @Quantity";
                    isFirstCondition = false;
                }

                if (Item_Check.Checked)
                {
                    if (!isFirstCondition)
                    {
                        RetriveData += " AND ";
                    }
                    RetriveData += "Item_ID = @Item_ID";
                    isFirstCondition = false;
                }

                if (Wh_Check.Checked)
                {
                    if (!isFirstCondition)
                    {
                        RetriveData += " AND ";
                    }
                    RetriveData += "WH_ID = @WH_ID";
                    isFirstCondition = false;
                }

                if (Open_Balance_Check.Checked)
                {
                    if (!isFirstCondition)
                    {
                        RetriveData += " AND ";
                    }
                    RetriveData += "Open_Balance = @OpenBalance";
                    isFirstCondition = false;
                }

                if (Position_Check.Checked)
                {
                    if (!isFirstCondition)
                    {
                        RetriveData += " AND ";
                    }
                    RetriveData += "Position = @Position";
                }

                SqlCommand Command = new SqlCommand(RetriveData, mySqlConnection);
                if (Quantity_Check.Checked)
                {
                    Command.Parameters.AddWithValue("@Quantity", SearchKayword.Text);
                }
                if (Item_Check.Checked)
                {
                    Command.Parameters.AddWithValue("@Item_ID", SearchKayword.Text);
                }
                if (Wh_Check.Checked)
                {
                    Command.Parameters.AddWithValue("@WH_ID", SearchKayword.Text);
                }
                if (Open_Balance_Check.Checked)
                {
                    Command.Parameters.AddWithValue("@OpenBalance", SearchKayword.Text);
                }
                if (Position_Check.Checked)
                {
                    Command.Parameters.AddWithValue("@Position", SearchKayword.Text);
                }

                mySqlConnection.Open();
                SqlDataReader sda = Command.ExecuteReader();
                DataTable data = new DataTable();
                data.Columns.Add("Product Id");
                data.Columns.Add("Wharehouse");
                data.Columns.Add("Quantity");
                data.Columns.Add("Open_Balance");
                data.Columns.Add("Position");

                while (sda.Read())
                {
                    DataRow rows = data.NewRow();
                    rows["Product Id"] = sda["Item_ID"];
                    rows["Wharehouse"] = sda["WH_ID"];
                    rows["Quantity"] = sda["Quantity"];
                    rows["Open_Balance"] = sda["Open_Balance"];
                    rows["Position"] = sda["Position"];
                    data.Rows.Add(rows);
                }

                SearchGridView.DataSource = data;
                SearchGridView.DataBind();
                mySqlConnection.Close();
            }
        }
        }
         




    }
}
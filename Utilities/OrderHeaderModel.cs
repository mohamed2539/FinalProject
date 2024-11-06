using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace IT508_Project
{
    public class OrderHeaderModel
    {

        int     OrderId;          //AutoIncrement 
        int     WhereHouseId;    // DropDownList
        int     Transaction;    // DropDownList
        int     CustomerId;         // DropDownList
        string  OrderDate;         // Function Get Time And Data
        string  CreatedBy;         // Session ["Username"]


        // public int Id { get; set; }
        public void InsertOrderHeader(TextBox orderId, DropDownList WhereHouseId,  DropDownList Transaction, DropDownList CustomerId, TextBox CreatedBy)
        {
            SqlConnection mySqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; 
                                                                     Initial Catalog = IMSDB;
                                                                     Integrated Security=True;
                                                                     TrustServerCertificate=True;
                                                                     Encrypt=True;");
            string query = @"INSERT INTO OrderHeader(wh_id,
                                                    transaction_type,
                                                    Customer_id,
                                                    order_date,
                                                    created_by) 
                                             VALUES
                                                   (@Wh_ID,
                                                    @Trans_Type_Name,
                                                    @Customer_id,
                                                    @OrderTime,
                                                    @LoginUsername)";

            mySqlConnection.Open();
            SqlCommand cmd = new SqlCommand(query, mySqlConnection);

            cmd.Parameters.AddWithValue("@Wh_ID",                WhereHouseId.Text.Trim());
            cmd.Parameters.AddWithValue("@Trans_Type_Name",      Transaction.Text.Trim());
            cmd.Parameters.AddWithValue("@Customer_id",          CustomerId.Text.Trim());
            cmd.Parameters.AddWithValue("@OrderTime",            DateTime.Now);
            cmd.Parameters.AddWithValue("@LoginUsername",        CreatedBy.Text.Trim());
            cmd.ExecuteNonQuery();
            mySqlConnection.Close();


        }


        public void SetIdTimeInTextBox(TextBox SetOrderId, TextBox SetTime)
        {
            string id = "";
            SqlConnection mySqlConnection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");

            mySqlConnection.Open();
            string GetInfo = "Select order_id from orderheader";// this select to get order_id
            string GetTime = "Select order_date from orderheader";// this select to get time 

            SqlCommand IDcommand = new SqlCommand(GetInfo, mySqlConnection);
            SqlDataReader ReadId = IDcommand.ExecuteReader();


            while (ReadId.Read())
            {
                if(!string.IsNullOrEmpty(ReadId[("order_id")].ToString()))
                {
                    id = "1";
                }
                else
                {
                    id = ReadId[("order_id")].ToString();
                }

            }
            ReadId.Close();
            SqlCommand TimeCommand = new SqlCommand(GetTime, mySqlConnection);
            SetTime.Text = TimeCommand.ExecuteScalar().ToString();
            //
            mySqlConnection.Close();

            SetOrderId.Text = id;


        }


    }
}
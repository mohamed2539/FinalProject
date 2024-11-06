using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IT508_Project
{
    public class ShowData
    {
        //SqlConnection mySqlConnection =   =  Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");

        //protected void CreateConnetion(string DataSource , string DBName,SqlConnection mySqlConnection,bool )
        //{
        //    mySqlConnection = new SqlConnection = $"Data Source ={DataSource}; Initial Catalog = {DBName}";
        //}


        ////protected void ShowTableData(string TableName, SqlConnection ConnectionName)
        //{
        //     ConnectionName = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDBI; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");
        //    string SelectDataQuery = $"Select * from {TableName} WHERE item_id = @Item_id AND wh_id = @Wh_id";

        //    ConnectionName.Open();

        //    //This Command to get Quantity and add new Quantity with old to table 
        //    SqlCommand OpenBalancCommand = new SqlCommand(SelectDataQuery, ConnectionName);
        //    OpenBalancCommand.Parameters.AddWithValue("@Item_id", Item_ID.Text.Trim());
        //    OpenBalancCommand.Parameters.AddWithValue("@Wh_id", Wh_ID.Text.Trim());

        //}




    }

}
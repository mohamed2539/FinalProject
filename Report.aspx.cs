using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using OfficeOpenXml;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace IT508_Project
{
    public partial class Report : System.Web.UI.Page
    {
        string connectionString = @"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;";

        public int Item_ID { get; set; }
        public int WH_ID { get; set; }
        public int Quantity { get; set; }
        public int Open_Balance { get; set; }
        public String Position { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        /*protected void Btn_Search_Click(object sender, EventArgs e)
        {

        }*/

        //Function to fill DropDwonlist
        /*protected void GetTableNames() {

            SqlConnection mySqlConnection = new SqlConnection(connectionString);

            String RetriveData = "SELECT Item_ID ,WH_ID, Quantity ,Open_Balance FROM Stock";
            SqlCommand Command = new SqlCommand(RetriveData, mySqlConnection);
            mySqlConnection.Open();

            SqlDataReader sda = Command.ExecuteReader();

            while (sda.Read())
            {
                
            }

            //ReportGridView.DataSource = data;
            //ReportGridView.DataBind();

            mySqlConnection.Close();
        }*/
        protected void Btn_Stock_Click(object sender, EventArgs e)
        {
            // Retrieve Data from DDMS Database
            List<Report> reportData = GetDataFromDatabase(connectionString);

            // Generate Excel File
            string filePath = Path.Combine(Server.MapPath("~/ByClick/"), "Stock.xls");
            CreateExcelFile(reportData, filePath);

            // Download Excel File
            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("Content-Disposition", "attachment; filename=Stock.xls");
            Response.TransmitFile(filePath);
            Response.End();
        }

        private List<Report> GetDataFromDatabase(string connectionString)
        {
            List<Report> data = new List<Report>();
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            string query = "SELECT * FROM stock";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();
            DataTable tabledata = new DataTable();
            tabledata.Columns.Add("Product Id");
            tabledata.Columns.Add("Wharehouse");
            tabledata.Columns.Add("Quantity");
            tabledata.Columns.Add("Open_Balance");
            tabledata.Columns.Add("Position");

            while (reader.Read())
            {
                Report report = new Report();

                if (!reader.IsDBNull(0))
                    report.Item_ID = reader.GetInt32(0);
                if (!reader.IsDBNull(1))
                    report.WH_ID = reader.GetInt32(1);
                if (!reader.IsDBNull(2))
                    report.Quantity = reader.GetInt32(2);
                if (!reader.IsDBNull(3))
                    report.Open_Balance = reader.GetInt32(3);
                if (!reader.IsDBNull(4))
                    report.Position = reader.GetString(4);

                data.Add(report);

                DataRow rows = tabledata.NewRow();
                rows["Product Id"] = reader["Item_ID"];
                rows["Wharehouse"] = reader["WH_ID"];
                rows["Quantity"] = reader["Quantity"];
                rows["Open_Balance"] = reader["Open_Balance"];
                rows["Position"] = reader["Position"];
                tabledata.Rows.Add(rows);

            }

            return data;
        }

        private void CreateExcelFile(List<Report> data, string filePath)
        {
            // Create a new Excel workbook
            HSSFWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("Report");

            // Create header row
            IRow headerRow = sheet.CreateRow(0);
            headerRow.CreateCell(0).SetCellValue("Item_ID");
            headerRow.CreateCell(1).SetCellValue("WH_ID");
            headerRow.CreateCell(2).SetCellValue("Quantity");
            headerRow.CreateCell(3).SetCellValue("Open_Balance");
            headerRow.CreateCell(4).SetCellValue("Position");

            // Populate rows with data
            int rowNum = 1;
            foreach (Report item in data)
            {
                IRow dataRow = sheet.CreateRow(rowNum);
                dataRow.CreateCell(0).SetCellValue(item.Item_ID);
                dataRow.CreateCell(1).SetCellValue(item.WH_ID);
                dataRow.CreateCell(2).SetCellValue(item.Quantity);
                dataRow.CreateCell(3).SetCellValue(item.Open_Balance);
                dataRow.CreateCell(4).SetCellValue(item.Position);
                rowNum++;
            }

            // Save Excel file
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                workbook.Write(fileStream);
            }
        }
    }
}
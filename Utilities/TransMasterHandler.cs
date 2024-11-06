using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace IT508_Project.Utilities
{
    public class TransMasterHandler
    {
        private string connectionString;
        private ValidationHelper validationHelper;

        public TransMasterHandler(string connectionString)
        {
            this.connectionString = connectionString;
            this.validationHelper = new ValidationHelper(connectionString);
        }

        public void FetchTranMasterData(string transID, out string whID, out string transTypeID, out DateTime transDate, out string supID, out string cusID, bool fetchSupID = false,  bool fetchCusID = false)
        {
            // Initialize out parameters to default values
            whID = transTypeID = supID = cusID = null;
            transDate = DateTime.MinValue;

            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();

                // Construct the base query
                string query = "SELECT WH_ID, Trans_Type_ID, Trans_Date";

                // Add the additional fields conditionally
                if (fetchSupID)
                {
                    query += ", Sup_ID";
                }
                if (fetchCusID)
                {
                    query += ", Cus_ID";
                }

                query += " FROM Tran_Master WHERE Trans_ID = @Trans_ID";

                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.Parameters.AddWithValue("@Trans_ID", transID);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    whID = reader["WH_ID"].ToString();
                    transTypeID = reader["Trans_Type_ID"].ToString();
                    transDate = Convert.ToDateTime(reader["Trans_Date"]);

                    if (fetchSupID)
                    {
                        supID = reader["Sup_ID"]?.ToString();
                    }

                    if (fetchCusID)
                    {
                        cusID = reader["Cus_ID"]?.ToString();
                    }
                }
            }
        }

        public void InsertTranMaster(string transID, string whID, string transTypeID, string transDate, string createdBy, string supID = null, string cusID = null)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();

                // Start building the query
                string query = @"INSERT INTO Tran_Master (Trans_ID, WH_ID, Trans_Type_ID, Trans_Date, Created_By";

                // Add conditional columns based on provided parameters
                if (!string.IsNullOrEmpty(supID))
                {
                    query += ", Sup_ID";
                }

                if (!string.IsNullOrEmpty(cusID))
                {
                    query += ", Cus_ID";
                }

                query += ") VALUES (@Trans_ID, @WH_ID, @Trans_Type_ID, @Trans_Date, @Created_By";

                // Add conditional values placeholders
                if (!string.IsNullOrEmpty(supID))
                {
                    query += ", @Sup_ID";
                }

                if (!string.IsNullOrEmpty(cusID))
                {
                    query += ", @Cus_ID";
                }

                query += ")";

                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.Parameters.AddWithValue("@Trans_ID", transID);
                cmd.Parameters.AddWithValue("@WH_ID", whID);
                cmd.Parameters.AddWithValue("@Trans_Type_ID", transTypeID);
                cmd.Parameters.AddWithValue("@Trans_Date", transDate);
                cmd.Parameters.AddWithValue("@Created_By", createdBy);

                // Add conditional parameters
                if (!string.IsNullOrEmpty(supID))
                {
                    cmd.Parameters.AddWithValue("@Sup_ID", supID);
                }

                if (!string.IsNullOrEmpty(cusID))
                {
                    cmd.Parameters.AddWithValue("@Cus_ID", cusID);
                }

                cmd.ExecuteNonQuery();
            }
        }

        /*public void InsertTransaction(string transID, string whID, string transTypeID, string transDate, string supID, string createdBy)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                string query = @"INSERT INTO Tran_Master (Trans_ID, WH_ID, Trans_Type_ID, Trans_Date, Sup_ID, Created_By) 
                             VALUES (@Trans_ID, @WH_ID, @Trans_Type_ID, @Trans_Date, @Sup_ID, @Created_By)";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.Parameters.AddWithValue("@Trans_ID", transID);
                cmd.Parameters.AddWithValue("@WH_ID", whID);
                cmd.Parameters.AddWithValue("@Trans_Type_ID", transTypeID);
                cmd.Parameters.AddWithValue("@Trans_Date", transDate);
                cmd.Parameters.AddWithValue("@Sup_ID", supID);
                cmd.Parameters.AddWithValue("@Created_By", createdBy);
                cmd.ExecuteNonQuery();
            }
        }

        public void InsertOutTransaction(string transID, string whID, string transTypeID, string transDate, string cusID, string createdBy)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                string query = @"INSERT INTO Tran_Master (Trans_ID, WH_ID, Trans_Type_ID, Trans_Date, Cus_ID, Created_By) 
                             VALUES (@Trans_ID, @WH_ID, @Trans_Type_ID, @Trans_Date, @Cus_ID, @Created_By)";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.Parameters.AddWithValue("@Trans_ID", transID);
                cmd.Parameters.AddWithValue("@WH_ID", whID);
                cmd.Parameters.AddWithValue("@Trans_Type_ID", transTypeID);
                cmd.Parameters.AddWithValue("@Trans_Date", transDate);
                cmd.Parameters.AddWithValue("@Cus_ID", cusID);
                cmd.Parameters.AddWithValue("@Created_By", createdBy);
                cmd.ExecuteNonQuery();
            }
        }

        public void InsertAuditTransaction(string transID, string whID, string transTypeID, string transDate, string createdBy)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                string query = @"INSERT INTO Tran_Master (Trans_ID,WH_ID,Trans_Type_ID,Trans_Date,Created_By) 
                               VALUES (@Trans_ID,@WH_ID,@Trans_Type_ID,@Trans_Date,@Created_By)";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.Parameters.AddWithValue("@Trans_ID", transID);
                cmd.Parameters.AddWithValue("@WH_ID", whID);
                cmd.Parameters.AddWithValue("@Trans_Type_ID", transTypeID);
                cmd.Parameters.AddWithValue("@Trans_Date", transDate);
                cmd.Parameters.AddWithValue("@Created_By", createdBy);
                cmd.ExecuteNonQuery();
            }
        }*/

        public bool TransIDExists(string transID)
        {
            return validationHelper.TransIDExists(transID);
        }

        public bool IsUserAdmin(string username)
        {
            return validationHelper.IsUserAdmin(username);
        }
    }
}
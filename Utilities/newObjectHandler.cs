using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Web.UI;
using System.Linq;
using System.Web;
using Azure;

namespace IT508_Project.Utilities
{
    public class newObjectHandler
    {
        private SqlConnection sqlcon;
        private int userId;

        public newObjectHandler (SqlConnection connection, int user)
        {
            sqlcon = connection;
            userId = user;
        }

        public void AddNewCustomer (int cusId, string cusName, string cusContactNum, string cusEmail)
        {
            try
            {
                string query = @"INSERT INTO IMS_Customer (Cus_ID, Cus_Name, Cus_Contact_Num, Cus_Email, Created_By) VALUES (@Cus_ID, @Cus_Name, @Cus_Contact_Num, @Cus_Email, @Created_By)";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.Parameters.AddWithValue("@Cus_ID", cusId);
                cmd.Parameters.AddWithValue("@Cus_Name", cusName);
                cmd.Parameters.AddWithValue("@Cus_Contact_Num", cusContactNum);
                cmd.Parameters.AddWithValue("@Cus_Email", cusEmail);
                cmd.Parameters.AddWithValue("@Created_By", userId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                
            }
        }

        public void AddNewItem (int itemId, string itemName, string category, DateTime prodDate, DateTime expDate, int reorderThreshold, string measureUnit)
        {
            try
            {
                string query = @"INSERT INTO IMS_Items (Item_ID, Item_Name, Category, Prod_Date, Exp_Date, Reorder_Threshold, Measure_Unit, Created_By) VALUES (@Item_ID, @Item_Name, @Category, @Prod_Date, @Exp_Date, @Reorder_Threshold, @Measure_Unit, @Created_By)";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.Parameters.AddWithValue("@Item_ID", itemId);
                cmd.Parameters.AddWithValue("@Item_Name", itemName);
                cmd.Parameters.AddWithValue("@Category", category);
                cmd.Parameters.AddWithValue("@Prod_Date", prodDate);
                cmd.Parameters.AddWithValue("@Exp_Date", expDate);
                cmd.Parameters.AddWithValue("@Reorder_Threshold", reorderThreshold);
                cmd.Parameters.AddWithValue("@Measure_Unit", measureUnit);
                cmd.Parameters.AddWithValue("@Created_By", userId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                
            }
        }

        public void AddNewWarehouse (int whId, string whName)
        {
            try
            {
                string query = @"INSERT INTO IMS_WH (WH_ID, WH_Name, Created_By) VALUES (@WH_ID, @WH_Name, @Created_By)";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.Parameters.AddWithValue("@WH_ID", whId);
                cmd.Parameters.AddWithValue("@WH_Name", whName);
                cmd.Parameters.AddWithValue("@Created_By", userId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                
            }
        }

        public void AddNewUser (int userID, string userName, string pass, string userRole)
        {
            try
            {
                string addNewUserQury = @"INSERT INTO IMS_Users (UserID,username,password,Role) VALUES (@UserID,@username,@password,@Role)";
                SqlCommand addNewUsercmd = new SqlCommand(addNewUserQury, sqlcon);
                addNewUsercmd.Parameters.AddWithValue("@UserID", userID);
                addNewUsercmd.Parameters.AddWithValue("@username", userName);
                addNewUsercmd.Parameters.AddWithValue("@password", pass);
                addNewUsercmd.Parameters.AddWithValue("@Role", userRole);
                addNewUsercmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                
            }
        }

        public void AddNewSupplier (int supId, string supName, string supContactNum, string supEmail)
        {
            try
            {
                string addNewSupQury = @"INSERT INTO IMS_Supplier (Supplier_ID,Supplier_Name,Supplier_Contact_Num,Supplier_Email,Created_By) VALUES (@Supplier_ID,@Supplier_Name,@Supplier_Contact_Num,@Supplier_Email,@Created_By)";
                SqlCommand addNewSupcmd = new SqlCommand(addNewSupQury, sqlcon);
                addNewSupcmd.Parameters.AddWithValue("@Supplier_ID", supId);
                addNewSupcmd.Parameters.AddWithValue("@Supplier_Name", supName);
                addNewSupcmd.Parameters.AddWithValue("@Supplier_Contact_Num", supContactNum);
                addNewSupcmd.Parameters.AddWithValue("@Supplier_Email", supEmail);
                addNewSupcmd.Parameters.AddWithValue("@Created_By", userId);
                addNewSupcmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
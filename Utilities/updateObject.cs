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
    public class updateObject
    {
        private SqlConnection sqlcon;
        private int userId;

        public updateObject (SqlConnection connection, int user)
        {
            sqlcon = connection;
            userId = user;
        }

        public void updateCustomer(int cusId, string cusName, string cusContactNum, string cusEmail)
        {
            try
            {
                string updateCusDataQury = @"UPDATE IMS_Customer SET Cus_Name = @Cus_Name, Cus_Contact_Num = @Cus_Contact_Num, Cus_Email = @Cus_Email, Created_By = @Created_By WHERE Cus_ID = @Cus_ID";
                SqlCommand updateCusDataCmd = new SqlCommand(updateCusDataQury, sqlcon);
                updateCusDataCmd.Parameters.AddWithValue("@Cus_ID", cusId);
                updateCusDataCmd.Parameters.AddWithValue("@Cus_Name", cusName);
                updateCusDataCmd.Parameters.AddWithValue("@Cus_Contact_Num", cusContactNum);
                updateCusDataCmd.Parameters.AddWithValue("@Cus_Email", cusEmail);
                updateCusDataCmd.Parameters.AddWithValue("@Created_By", userId);
                updateCusDataCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
        }

        public void updateItem(int itemId, string itemName, string category, DateTime prodDate, DateTime expDate, int reorderThreshold, string measureUnit)
        {
            try
            {
                string updateItemDataQury = @"UPDATE IMS_Items SET Item_Name = @Item_Name, Category = @Category, Prod_Date = @Prod_Date, Exp_Date = @Exp_Date, Reorder_Threshold = @Reorder_Threshold, Measure_Unit = @Measure_Unit, Created_By = @Created_By WHERE Item_ID = @Item_ID";
                SqlCommand updateItemDataCmd = new SqlCommand(updateItemDataQury, sqlcon);
                updateItemDataCmd.Parameters.AddWithValue("@Item_ID", itemId);
                updateItemDataCmd.Parameters.AddWithValue("@Item_Name", itemName);
                updateItemDataCmd.Parameters.AddWithValue("@Category", category);
                updateItemDataCmd.Parameters.AddWithValue("@Prod_Date", prodDate);
                updateItemDataCmd.Parameters.AddWithValue("@Exp_Date", expDate);
                updateItemDataCmd.Parameters.AddWithValue("@Reorder_Threshold", reorderThreshold);
                updateItemDataCmd.Parameters.AddWithValue("@Measure_Unit", measureUnit);
                updateItemDataCmd.Parameters.AddWithValue("@Created_By", userId);
                updateItemDataCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
        }

        public void updateWarehouse(int whId, string whName)
        {
            try
            {
                string updateWHDataQury = @"UPDATE IMS_WH SET WH_Name = @WH_Name, Created_By = @Created_By WHERE WH_ID = @WH_ID";
                SqlCommand updateUWHDataCmd = new SqlCommand(updateWHDataQury, sqlcon);
                updateUWHDataCmd.Parameters.AddWithValue("@WH_ID", whId);
                updateUWHDataCmd.Parameters.AddWithValue("@WH_Name", whName);
                updateUWHDataCmd.Parameters.AddWithValue("@Created_By", userId);
                updateUWHDataCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
        }

        public void updateUser(int userID, string userName, string pass, string userRole)
        {
            try
            {
                string updateUserDataQury = @"UPDATE IMS_Users SET username = @username, password = @password, Role = @Role WHERE UserID = @UserID";
                SqlCommand updateUserDataCmd = new SqlCommand(updateUserDataQury, sqlcon);
                updateUserDataCmd.Parameters.AddWithValue("@UserID", userID);
                updateUserDataCmd.Parameters.AddWithValue("@username", userName);
                updateUserDataCmd.Parameters.AddWithValue("@password", pass);
                updateUserDataCmd.Parameters.AddWithValue("@Role", userRole);
                updateUserDataCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
        }

        public void updateSupplier(int supId, string supName, string supContactNum, string supEmail)
        {
            try
            {
                string updateSupDataQury = @"UPDATE IMS_Supplier SET Supplier_Name = @Supplier_Name, Supplier_Contact_Num = @Supplier_Contact_Num, Supplier_Email = @Supplier_Email, Created_By = @Created_By WHERE Supplier_ID = @Supplier_ID";
                SqlCommand updateSupDataCmd = new SqlCommand(updateSupDataQury, sqlcon);
                updateSupDataCmd.Parameters.AddWithValue("@Supplier_ID", supId);
                updateSupDataCmd.Parameters.AddWithValue("@Supplier_Name", supName);
                updateSupDataCmd.Parameters.AddWithValue("@Supplier_Contact_Num", supContactNum);
                updateSupDataCmd.Parameters.AddWithValue("@Supplier_Email", supEmail);
                updateSupDataCmd.Parameters.AddWithValue("@Created_By", userId);
                updateSupDataCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace IT508_Project
{
    public partial class PrivilegeCheckHandler : System.Web.UI.Page
    {
        string connectionString = @"Data Source = localhost\SQLEXPRESS; Initial Catalog = IMSDB; Integrated Security=True;";
        protected void Page_Load(object sender, EventArgs e)
        {
            string targetPage = Request.Form["targetPage"];
            string username = User.Identity.Name;

            if (HasPrivilege(username, targetPage))
            {
                Response.Redirect(targetPage);
            }
            else
            {
                Response.Write("<script>alert('You do not have the necessary permissions to access this page.');</script>");
                Response.Write("<script>window.location = 'Dashboard.aspx';</script>");
            }
        }
        private bool HasPrivilege(string username, string targetPage)
        {
            bool hasPrivilege = false;

            string query = "";
            if (targetPage == "createnewobject.aspx")
            {
                query = "SELECT COUNT(*) FROM IMS_Users WHERE username = @username AND Role = 'Admin'";
            }
            else
            {
                query = "SELECT COUNT(*) FROM IMS_Users WHERE username = @username AND Role IN( 'Admin' , 'User' )";
            }
            // Add similar conditions for other pages based on your privilege requirements

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", (string)Session["username"]);

                try
                {
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    if (count > 0)
                    {
                        hasPrivilege = true;
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception
                }
            }

            return hasPrivilege;
        }
    }
}
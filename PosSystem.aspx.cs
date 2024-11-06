using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IT508_Project
{
    public partial class PosSystem : System.Web.UI.Page
    {

        MyFramework framework = new MyFramework();
        protected void Page_Load(object sender, EventArgs e)
        {

        }






        protected void RedirectAdmin_Click(object sender, EventArgs e)
        {
            //string ShowAllUsername = ""; // Session(string)Session["username"];

            //if (string.IsNullOrEmpty(ShowAllUsername))
            //{

            //    CheckFillTextBox.Text = "Session is empty";
            //    //CheckAdminUserRole(ShowAllUsername); //  checking role method
            //}
            //else
            //{
            //    // Messsage When the user is not logged in
            //    CheckFillTextBox.Text = "User is not logged in.";
            //}
        }
    }
}
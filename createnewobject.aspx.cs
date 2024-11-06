using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IT508_Project
{
    public partial class createnewobject : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void addNewUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("addNewUser.aspx");
        }

        protected void addCustomer_Click(object sender, EventArgs e)
        {
            Response.Redirect("addCustomer.aspx");
        }

        protected void addWarehouse_Click(object sender, EventArgs e)
        {
            Response.Redirect("addWH.aspx");
        }

        protected void addSupplier_Click(object sender, EventArgs e)
        {
            Response.Redirect("addSupplier.aspx");
        }

        protected void addProduct_Click(object sender, EventArgs e)
        {
            Response.Redirect("addItem.aspx");
        }

        protected void linkItemToWH_Click(object sender, EventArgs e)
        {
            Response.Redirect("linkItemToWH.aspx");
        }

        protected void addTranType_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddTranType.aspx");
        }
    }
}
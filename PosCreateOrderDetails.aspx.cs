using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IT508_Project
{
    public partial class PosCreateOrderDetails : System.Web.UI.Page
    {


        CreateOrderDetailsModel OrderDetails = new CreateOrderDetailsModel();
        MyFramework framework = new MyFramework();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                GetOrderId.Text      = "Order Id is         -> " + OrderDetails.GetOrderID().ToString()     + "<br>";
                GetWhareHouseId.Text = "WareHouse Id is     -> " + OrderDetails.GetWareHouseID().ToString() + "<br>";
                GetTransId.Text      = "Trans Id is         ->" + OrderDetails.GetTransID().ToString()      + "<br>";
                QtyWhareHouseId.Text = "Pos Store WH Id is  ->" + framework.PosStoreWHId().ToString()    + "<br>";

                FillDropList();
               


            }
        }

        protected void CreatePosOrder_Click(object sender, EventArgs e)
        {
            int GetOrderId          = OrderDetails.GetOrderID();
            int GetWhareHouseId     = OrderDetails.GetWareHouseID();
            int GetTransId          = OrderDetails.GetTransID();
            int QtyWhareHouseId     = framework.PosStoreWHId();


            OrderDetails.InsertOrderInfo(OrderSrial, GetOrderId, GetWhareHouseId, GetTransId, Item_ID, ItemQuantity, Price, OrderErrorMessage);
            //InsertedMessage.Text = "Order Details Data Is Inserted . . . ";
            framework.CalculateQtyAfterOrder(Item_ID, QtyWhareHouseId, ItemQuantity, OrderErrorMessage);
            //framework.FillInvoiceGrid(InvoiceLabelGrid, InoviceId);
            ItemQuantity.Text = "";
            Price.Text = "";
            AutoIcreementSerail();
        }


        protected void Item_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            framework.GetPriceToSelectedItem(Item_ID, Price);
        }

        protected void FillDropList()
        {
            framework.FileItemsList(Item_ID);
        }

        public void AutoIcreementSerail()
        {
            int OrderId = OrderDetails.GetOrderID();
            framework.IncrementSerial(OrderSrial, OrderId);
        }

        protected void InvoiceButton_Click(object sender, EventArgs e)
        {

            int GetOrderId = OrderDetails.GetOrderID();
            framework.GenerateInvoice(GetOrderId, InvoiceLabel);
            string script = "setTimeout(function() { window.location.href = 'PosSystem.aspx'; }, 4000);";
            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
        }
    }
}
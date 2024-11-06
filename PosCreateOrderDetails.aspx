<%@ Page Title="" Language="C#" MasterPageFile="~/IMS.Master" AutoEventWireup="true" CodeBehind="PosCreateOrderDetails.aspx.cs" Inherits="IT508_Project.PosCreateOrderDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    

    <form ID="CreateOrderDetailsForm"  class="CreateOrderForm" runat="server">
        <asp:Label ID="OrderErrorMessage" runat="server" Text=""></asp:Label>
        <asp:Label ID="GetOrderId" runat="server" Text=""></asp:Label>
        <asp:Label ID="GetWhareHouseId" runat="server" Text=""></asp:Label>
        <asp:Label ID="GetTransId" runat="server" Text=""></asp:Label>
        <asp:Label ID="QtyWhareHouseId" runat="server" Text=""></asp:Label>
        <div class="OrderDetailsSection"> 
            <asp:TextBox ID="OrderSrial" class="GeneralInputTow TinyInput OrderID" runat="server" placeholder="Order Serial" readonly="true"></asp:TextBox><br>

         <div class="formGroup">
           <asp:Label ID="OrderItemId" class="OrderLabels " runat="server" Text="Choose item id"></asp:Label>
            <asp:DropDownList ID="Item_ID" class="Select" runat="server" AutoPostBack="True"  OnSelectedIndexChanged="Item_ID_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:IMSDBIConnectionString %>" SelectCommand="SELECT [item_id] FROM [PosStoree]"></asp:SqlDataSource>
         </div>
   

            <div class="formGroup">
                <asp:TextBox ID="ItemQuantity"  class="GeneralInputTow StanderInputSize"  placeholder="Enter the Quantity"  runat="server" required="true"></asp:TextBox>
                <asp:TextBox ID="Price" class="GeneralInputTow TinyInput"  placeholder="Enter Price" runat="server" readonly="true"></asp:TextBox><br>
            </div>
           

            <div class="formGroup">
                <asp:Button ID="CreatePosOrder" runat="server" class="GeneralButton" Text="Create Order" OnClick="CreatePosOrder_Click" />
                <asp:Button ID="InvoiceButton" runat="server" class="GeneralButton" Text="Create Invoice" OnClick="InvoiceButton_Click" />

            </div>
        </div> <%-- End Order Details Section Tag --%>
        <asp:Label ID="InvoiceLabel" runat="server" />

        <asp:GridView ID="InvoiceLabelGrid" runat="server"></asp:GridView>

    </form>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>

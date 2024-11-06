<%@ Page Title="" Language="C#" MasterPageFile="~/IMS.Master" AutoEventWireup="true" CodeBehind="EditOrderDetail.aspx.cs" Inherits="IT508_Project.EditOrderDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="UpadateOrderForm" class="GeneralForm  DisbursingForm UpadateOrderForm" method="post"  runat="server">
        <div class="OrderHeaderInfo">
            <asp:TextBox         ID="UpdateOrderId"      class="GeneralInputTow OrderInput EidtInput InputID" runat="server"></asp:TextBox>
            <asp:Label ID="UpdateWhIDLabel" runat="server" Text="Whereouse id"></asp:Label>  
            <asp:DropDownList    ID="UpdateWhID"         class="Select" runat="server"  DataTextField="WH_ID" DataValueField="WH_ID"></asp:DropDownList>
            <asp:SqlDataSource   ID="WhDataSource"       runat="server" ConnectionString="<%$ ConnectionStrings:IMSDBConnectionString %>" SelectCommand="SELECT [WH_ID] FROM [IMS_WH]"></asp:SqlDataSource>
            <asp:Label ID="UpdateTransIDLabel" runat="server" Text="Transaction id"></asp:Label> 
            <asp:DropDownList    ID="UpdateTransID"      class="Select" runat="server"  DataTextField="Trans_Type_ID" DataValueField="Trans_Type_ID"></asp:DropDownList>
            <asp:SqlDataSource   ID="TransDataSource"    runat="server" ConnectionString="<%$ ConnectionStrings:IMSDBConnectionString %>" SelectCommand="SELECT [Trans_Type_ID] FROM [Tran_Master]"></asp:SqlDataSource>
            <asp:Label ID="UpdateCustomerIDLabel" runat="server" Text="Customer id"></asp:Label> 
            <asp:DropDownList    ID="UpdateCustomerID"   class="Select" runat="server"  DataTextField="Customer_id" DataValueField="Customer_id"></asp:DropDownList>
            <asp:SqlDataSource   ID="CustomerID_DataSource" runat="server" ConnectionString="<%$ ConnectionStrings:IMSDBConnectionString %>" SelectCommand="SELECT [Customer_id] FROM [POSCustomer]"></asp:SqlDataSource>
            <asp:TextBox         ID="UpdateTime"         class="GeneralInputTow OrderInput EidtInput"  runat="server"></asp:TextBox>
            <asp:TextBox         ID="UpdateUserName"     class="GeneralInputTow OrderInput EidtInput"  runat="server"></asp:TextBox>
    
            <asp:Button ID="UpadateHeader" runat="server"  class="GeneralButton CreateOrderButton" Text="Upadate Header" OnClick="UpadateHeader_Click" />
    
        </div>
  <%-- -------------------------------------------Order Details Section------------------------------------------- --%>
        <div class="OrderDetailsInfo">
            <asp:Label ID="ErrorMessageOrder" runat="server" Text="Whereouse id"></asp:Label> 
            <asp:TextBox         ID="UpdataSerial"     class="GeneralInputTow OrderInput EidtInput"  runat="server"></asp:TextBox>
            <asp:TextBox         ID="UpdateDeOrderId"  class="GeneralInputTow OrderInput EidtInput"  runat="server"></asp:TextBox>
            <asp:DropDownList    ID="UpdataItemID"     class="Select" runat="server"  DataTextField="Item_ID" DataValueField="Item_ID"></asp:DropDownList>
            <asp:SqlDataSource   ID="ItemID_DataSource" runat="server" ConnectionString="<%$ ConnectionStrings:IMSDBConnectionString %>" SelectCommand="SELECT [Item_ID] FROM [IMS_Items]"></asp:SqlDataSource>
            <asp:TextBox         ID="UpdataQuantity"   class="GeneralInputTow OrderInput EidtInput"  runat="server"></asp:TextBox>
            <asp:TextBox         ID="UpdataPrice"      class="GeneralInputTow OrderInput EidtInput"  runat="server"></asp:TextBox>

            <asp:Button       ID="UpadateDeatails"  class="GeneralButton CreateOrderButton"       runat="server" Text="Upadate Deatails" OnClick="UpadateDeatails_Click" />
        </div>
    </form>
</asp:Content>
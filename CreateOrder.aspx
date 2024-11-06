<%@ Page Title="" Language="C#" MasterPageFile="~/IMS.Master" AutoEventWireup="true" CodeBehind="CreateOrder.aspx.cs" Inherits="IT508_Project.CreateOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="OrderHeaderForm" class="CreateOrderForm" runat="server">
        <div class="orderFormHeaderContainer">
                    <h3>Create New Order</h3>
      <%-- <asp:Label ID="QuantityLable" class="OrderLabels " runat="server"></asp:Label><br>--%>
        <asp:Label ID="OrderErrorMessage" class="OrderLabels    ErroLabel" runat="server" Text=""></asp:Label>
        <asp:Label ID="InsertedMessage"  class="OrderLabels    GeneralSuccess" runat="server" Text=""></asp:Label>
        </div>
         
    <div class="OrderHeaderSection"><%-- Start Order Header Section Tag --%>
       
    <%--  <div class="OrderDropList">OrderDropList Tag --%>

        
    <asp:TextBox            ID="OrderID"  class="GeneralInputTow OrderInput TinyInput OrderID" runat="server" placeholder="Order ID" readonly="true"></asp:TextBox>
      <div class="formGroup">
        <asp:Label          ID="OrderWhID"    class="OrderLabels "   runat="server" Text="Choose WhereHouse Id"></asp:Label>
        <asp:DropDownList   ID="Wh_ID" class="Select" runat="server" DataSourceID="SqlDataSource" DataTextField="WH_ID" DataValueField="WH_ID">
        </asp:DropDownList>
        <asp:SqlDataSource  ID="SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:IMSDBConnectionString5 %>" SelectCommand="SELECT [WH_ID] FROM [IMS_WH]"></asp:SqlDataSource>

        <asp:Label ID="TransType" runat="server" class="OrderLabels " Text="Choose Transaction Type"></asp:Label>
        <asp:DropDownList   ID="Trans_Type_Name" class="Select" runat="server" DataSourceID="SqlDataSource1" DataTextField="Trans_Type_ID" DataValueField="Trans_Type_ID">
        </asp:DropDownList>
        <asp:SqlDataSource  ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:IMSDBConnectionString5 %>" SelectCommand="SELECT [Trans_Type_ID] FROM [Trans_Type]"></asp:SqlDataSource>

        <asp:Label ID="OrderCustomerID" class="OrderLabels " runat="server" Text="Choose Customer Id"></asp:Label>
        <asp:DropDownList   ID="Customer_id" class="Select" runat="server" DataSourceID="SqlDataSource2" DataTextField="Customer_id" DataValueField="Customer_id">
        </asp:DropDownList>
        <asp:SqlDataSource  ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:IMSDBConnectionString5 %>" SelectCommand="SELECT [Customer_id] FROM [POSCustomer]"></asp:SqlDataSource>
     </div>

        <div class="formGroup">
            <asp:TextBox    ID="OrderTime" textMode="DateTime" class="GeneralInputTow OrderInput" runat="server" readonly="true"></asp:TextBox>
     <%--</div>End OrderDropList Tag --%>       
            <asp:TextBox    ID="LoginUsername" class="GeneralInputTow StanderInputSize" runat="server" readonly="true"></asp:TextBox>
        </div>
       
        <div class="formGroup">
            <asp:Button Text="Save" ID="SaveOrderHeader" CssClass="GeneralButton " runat="server" OnClick="SaveOrderHeader_Click" />
        </div>
          
    </div> <%-- End Order Header Section Tag --%>


        <%-- ********************** **************** This IS Order details Form ******************  readonly="true"****************--%>
        <div class="OrderDetailsSection"> <%-- Start Order Details Section Tag --%>
          <asp:TextBox ID="OrderSrial" class="GeneralInputTow TinyInput OrderID" runat="server" placeholder="Order Serial"  readonly="true"></asp:TextBox><br>

         <div class="formGroup">
           <asp:Label ID="OrderItemId" class="OrderLabels " runat="server" Text="Choose item id"></asp:Label>
            <asp:DropDownList ID="Item_ID" class="Select" runat="server" AutoPostBack="True"  OnSelectedIndexChanged="Item_ID_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:IMSDBConnectionString %>" SelectCommand="SELECT [item_id] FROM [PosStoree]"></asp:SqlDataSource>
         </div>
   <%--  disabled="disabled" --%>

            <div class="formGroup">
                <asp:TextBox ID="ItemQuantity"  class="GeneralInputTow StanderInputSize"  placeholder="Enter the Quantity"  runat="server"></asp:TextBox>
                <asp:TextBox ID="Price" class="GeneralInputTow TinyInput"  placeholder="Enter Price" runat="server" readonly="true"></asp:TextBox><br>
            </div>
           

            <div class="formGroup">
                <asp:Button ID="CreateOrderButton" class="GeneralButton CreateOrderButton" runat="server" Text="Create Order" OnClick="CreateOrderButton_Click" />
                <%-- <asp:Button ID="PrintButton" runat="server" Text="Print Invoice" OnClick="PrintButton_Click" />--%>
            </div>
        </div> <%-- End Order Details Section Tag --%>
        <%-- ******************** **************** This IS Order details Form ************** **************** --%>
        <asp:Label ID="InvoiceLabel" runat="server" />

        <asp:GridView ID="InvoiceLabelGrid" runat="server"></asp:GridView>
  </form>


    <script src="Scripts/Script.js"></script>
    



</asp:Content>
<%@ Page Title="" Language="C#" MasterPageFile="~/IMS.Master" AutoEventWireup="true" CodeBehind="SaleByOneStore.aspx.cs" Inherits="IT508_Project.SaleByOneStore" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


            
        

    <%-- required="true" --%>

    <form id="SaleOnePiceStorage" class="GeneralForm StoreOnePiceForm" runat="server">
        <h3>Add Data To POS Store </h3>
        <h4><asp:Label ID="ErrorMessage"  class="ErrorClass" runat ="server" Text=""></asp:Label></h4>
        <h4><asp:Label ID="SuccessMessage"  class="SuccessLabel" runat ="server" Text=""></asp:Label></h4>
         <asp:Label ID="Item_idLabel"  class="ErrorClass" runat ="server" Text="">Choose Item</asp:Label>
        <asp:DropDownList ID="Item_id" class="Select" runat="server" DataSourceID="SqlDataSource1" DataTextField="Item_ID" DataValueField="Item_ID" >
        </asp:DropDownList><br>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:IMSDBIConnectionString4 %>" SelectCommand="SELECT [Item_ID] FROM [IMS_Items]"></asp:SqlDataSource>
        <asp:Label ID="Wh_idLabel"  class="ErrorClass" runat ="server" Text="">Choose WhereHouse</asp:Label>
        <asp:DropDownList ID="Wh_id" class="Select" runat="server" DataSourceID="SqlDataSource2" DataTextField="WH_ID" DataValueField="WH_ID">
        </asp:DropDownList><br>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:IMSDBIConnectionString %>" SelectCommand="SELECT [WH_ID] FROM [IMS_WH]"></asp:SqlDataSource>
        <asp:TextBox ID="OpenBalance" AutoComplete="false"  class="GeneralInputTow " runat="server" placeholder="Open Balance"></asp:TextBox>
        <asp:TextBox ID="Quantity" AutoComplete="false"  class="GeneralInputTow " runat="server" placeholder="Quantity"></asp:TextBox>
        <%--<asp:TextBox ID="RealQuantity"  class="StoreOnePiceInput" runat="server"  placeholder="Real Quantity"></asp:TextBox>--%>
         <asp:TextBox ID="ItemPosition" AutoComplete="false"  class="GeneralInputTow" runat="server"  placeholder="Itme Position"></asp:TextBox>
        <asp:TextBox ID="ItemPrice" AutoComplete="false"   class="GeneralInputTow" runat="server"  placeholder="Price"></asp:TextBox>
        <asp:Button ID="OnePiceButton"  class="GeneralButton" runat="server" Text="Store" OnClick="OnePiceButton_Click" />
        <asp:Button ID="ShowAllStoreData"  class="GeneralButton PosShowAllButton" runat="server" Text="Show All Store Data" OnClick="ShowAllStoreData_Click" /><br>
        <asp:Label ID="RedirectQtyBtn" class="Generl-Label ShowAllLable" runat="server" Text="Label">From this Blow Button You Can Go To Add Quantity To Store</asp:Label>
        <asp:Button ID="IcrementButton" class="GeneralButton BigButton"  runat="server" Text="Increment POS item Quantity" OnClick="IcrementButton_Click" />
        
            
    </form>


            
        



</asp:Content>

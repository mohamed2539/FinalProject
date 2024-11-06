<%@ Page Title="" Language="C#" MasterPageFile="~/IMS.Master" AutoEventWireup="true" CodeBehind="POSIncoming.aspx.cs" Inherits="IT508_Project.POSIncoming" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" class="GeneralForm" runat="server">
        <asp:Label ID="IncomingPosMessage" runat="server" Text=""></asp:Label>
        <asp:DropDownList ID="IncomingItemID" class="Select" runat="server" DataSourceID="ItemIDSourceData" DataTextField="Item_ID" DataValueField="Item_ID">
        </asp:DropDownList>
        <asp:SqlDataSource ID="ItemIDSourceData" runat="server" ConnectionString="<%$ ConnectionStrings:IMSDBIConnectionString %>" SelectCommand="SELECT [Item_ID] FROM [IMS_Items]"></asp:SqlDataSource>
        <asp:DropDownList ID="IncomingWhID" class="Select" runat="server" DataSourceID="WhIDSourceData" DataTextField="WH_ID" DataValueField="WH_ID">
        </asp:DropDownList>
        <asp:SqlDataSource ID="WhIDSourceData" runat="server" ConnectionString="<%$ ConnectionStrings:IMSDBIConnectionString %>" SelectCommand="SELECT [WH_ID] FROM [IMS_WH]"></asp:SqlDataSource>
        <asp:TextBox ID="IncreemntalQuantity" required="true" class="GeneralInputTow"  placeholder="Enter Quantity" runat="server"></asp:TextBox>
        <asp:Button ID="AddQuantity"  class="GeneralButton" runat="server" Text="Add Quantity" OnClick="AddQuantity_Click" />
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>

<%@ Page Title="IMS Live Search" Language="C#" MasterPageFile="~/IMS.Master" AutoEventWireup="true" CodeBehind="SearchPage.aspx.cs" Inherits="IT508_Project.SearchPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form id="ReportPageFrom" runat="server">

        <div class="checkContainer">
            <asp:CheckBox ID="Item_Check" Text="Item" runat="server" />
            <asp:CheckBox ID="Wh_Check" Text="WH" runat="server" />
            <asp:CheckBox ID="Quantity_Check" Text="Quantity" runat="server" />
            <asp:CheckBox ID="Open_Balance_Check" Text="Balance" runat="server" />
            <asp:CheckBox ID="Position_Check"  Text="Position" runat="server" />
        </div>

        <div class="SearchCompenetContainer">
            <h3 class="SearchHeader">Here, you have the ability to search for any object</h3>
            <asp:TextBox ID="SearchKayword"  placeholder="What you Search For" class="SearchInput" runat="server"></asp:TextBox>
            <asp:Button ID="SearchButton" runat="server" class="Btn_Search" Text="Search" OnClick="SearchButton_Click" />
        </div>
  
        <div class="GridViewContainer" style="margin-left: 20%;">
            <asp:GridView ID="SearchGridView" CssClass="ReprotTable" runat="server" GridLines="None">
                <AlternatingRowStyle BorderStyle="None" />
                <HeaderStyle BorderStyle="None" BorderWidth="0px" />
            </asp:GridView>
        </div>
        
    </form>
    
    







</asp:Content>

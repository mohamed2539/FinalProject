<%@ Page Title="" Language="C#" MasterPageFile="~/IMS.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="IT508_Project.Report" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Reportes</title>
    <link href="style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <div class="Header">
            <div class="ReportCompContainer">
                <h4>Welcome in Reprots Page Here You Can Export any wherehouse Report In Excel Sheet</h4>
                <%-- <asp:TextBox ID="ReportNameFaild"  class="ReportNameFaild" placeholder="Enter Table name to see Report" runat="server"></asp:TextBox>
                <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="DropDwonDataSource" DataTextField="Item_ID" DataValueField="Item_ID">
                </asp:DropDownList>
                <asp:SqlDataSource ID="DropDwonDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:IMSDBConnectionString %>" SelectCommand="SELECT [Item_ID] FROM [IMS_Items]"></asp:SqlDataSource>--%>
                <ul class="ReportButtonsContainer">

                    <div class="GroupOne">
                        <li><asp:Button ID="Btn_Customer" class="Btn_Report" runat="server" Text="Customer Report" /></li>
                        <li><asp:Button ID="Btn_Items" class="Btn_Report" runat="server" Text="Items Report" /></li>
                        <li><asp:Button ID="Btn_Stock" class="Btn_Report" runat="server" Text="Stock Report" OnClick="Btn_Stock_Click" /></li>
                    </div>

                    <div class="GroupTow">
                        <li><asp:Button ID="Btn_Supplier" class="Btn_Report" runat="server" Text="Supplier Report" /></li>
                        <li><asp:Button ID="Btn_Tran_Master" class="Btn_Report" runat="server" Text="Tran Master Report" /></li>
                        <li><asp:Button ID="Btn_Trans_Type" class="Btn_Report" runat="server" Text="Trans Type Report" /></li>
                        <li><asp:Button ID="Btn_Transactions" class="Btn_Report" runat="server" Text="Transactions Report" /></li>
                    </div>
      
                </ul>

            </div>
           
        </div>
        <%--<asp:GridView ID="ReportGridView" CssClass="ReprotTable" runat="server" GridLines="None">
            <AlternatingRowStyle BorderStyle="None" />
            <HeaderStyle BorderStyle="None" BorderWidth="0px" />
        </asp:GridView>--%>
    </form>
</asp:Content>

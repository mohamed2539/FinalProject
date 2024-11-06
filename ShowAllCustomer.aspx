<%@ Page Title="" Language="C#" MasterPageFile="~/IMS.Master" AutoEventWireup="true" CodeBehind="ShowAllCustomer.aspx.cs" Inherits="IT508_Project.ShowAllCustomer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <asp:Label ID="DeleteCustomerErrorMessage" class="GeneralErrorMessage" runat="server" Text=""></asp:Label>
        <%-- DataSourceID="GetAllCustomer" --%>

        <asp:GridView ID="ShowCustomerGraid" runat="server" AutoGenerateColumns="False" 
            OnRowDeleting="ClickedRowDeleting" 
            OnRowCommand="ShowCustomer_RowCommand" DataKeyNames="Customer_id">
            <Columns>
                <asp:BoundField DataField="Customer_id"         HeaderText="Customer_id" InsertVisible="False" ReadOnly="True" SortExpression="Customer_id" />
                <asp:BoundField DataField="Customer_name"       HeaderText="Customer_name"      SortExpression="Customer_name" />
                <asp:BoundField DataField="Customer_Phone"      HeaderText="Customer_Phone"     SortExpression="Customer_Phone" />
                <asp:BoundField DataField="Customer_Address"    HeaderText="Customer_Address"   SortExpression="Customer_Address" />
                <asp:ButtonField ButtonType="Button" ControlStyle-CssClass="EditButton" Text="Edit" CommandName="Edit" />
                <asp:ButtonField ButtonType="Button" ControlStyle-CssClass="EditButton DeleteButton" Text="Delete" CommandName="Delete" />
            </Columns>
        </asp:GridView>
        <%--<asp:SqlDataSource ID="GetAllCustomer" runat="server" ConnectionString="<%$ ConnectionStrings:IMSDBConnectionString %>" SelectCommand="SELECT * FROM [POSCustomer]"></asp:SqlDataSource>--%>
    </form>
</asp:Content>
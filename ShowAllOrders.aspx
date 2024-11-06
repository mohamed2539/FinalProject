<%@ Page Title="" Language="C#" MasterPageFile="~/IMS.Master" AutoEventWireup="true" CodeBehind="ShowAllOrders.aspx.cs" Inherits="IT508_Project.ShowAllOrders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">






    <form id="EditOrderDetail"  runat="server">

                                     <h3>This Table Display Order Header Data To Edit Or Delete Any Record</h3>
        <div>
            <asp:GridView ID="GridViewOrders" runat="server"  class="GridViewOrders"
                AutoGenerateColumns="False" 
                OnRowCommand="GridViewOrders_RowCommand"
                OnRowDeleting="ClickedOrderHeaderRowDeleting" 
                DataKeyNames="Order_id,Wh_id,transaction_type">
                <Columns>
                    <asp:BoundField DataField="Order_id" HeaderText="Order ID" />
                    <asp:BoundField DataField="Wh_id" HeaderText="WH ID" />
                    <asp:BoundField DataField="transaction_type" HeaderText="trans type" />
                    <asp:BoundField DataField="customer_id" HeaderText="customer id" />
                    <asp:BoundField DataField="order_date" HeaderText="order date" />
                    <asp:BoundField DataField="created_by" HeaderText="created by" />
                  <%--  <asp:BoundField  HeaderText="Edit" />
                    <asp:BoundField  HeaderText="Delete" />--%>
                    <asp:ButtonField ButtonType="Button" ControlStyle-CssClass="EditButton" Text="Edit" CommandName="Edit" />
                    <asp:ButtonField ButtonType="Button" ControlStyle-CssClass="EditButton DeleteButton" Text="Delete" CommandName="Delete" />
                </Columns>
            </asp:GridView>
        </div>
                                        <h3>This Table Display Data</h3>
         <div>
            <asp:GridView ID="OrderDetailsGrid" runat="server" class="OrderDetailsGrid" 
                AutoGenerateColumns="False" 
                OnRowDeleting="ClickedRowDeleting" 
                OnRowCommand="OrderDetailsGrid_RowCommand" 
                DataKeyNames="Srial,Order_id,Wh_id,trans_type">
                <Columns>
                    <%-- CommandArgument="<%# Container.DataItemIndex %>" --%>
                    <asp:BoundField DataField="Srial"       HeaderText="Order Serial" />
                    <asp:BoundField DataField="Order_id"    HeaderText="Order ID" />
                    <asp:BoundField DataField="Wh_ID"       HeaderText="WhareHouse ID" />
                    <asp:BoundField DataField="trans_type"  HeaderText="Trans Id" />
                    <asp:BoundField DataField="item_id"     HeaderText="Item ID" />
                    <asp:BoundField DataField="Quantity"    HeaderText="Quantity" />
                    <asp:BoundField DataField="price"       HeaderText="Price" />

                   <%-- <asp:ButtonField ButtonType="Button" Text="Edit" CommandName="Edit" CommandArgument="<%# Container.DataItemIndex %>" />--%>
                    <asp:ButtonField ButtonType="Button" ControlStyle-CssClass="EditButton" Text="Edit" CommandName="Edit"/>
                    <asp:ButtonField ButtonType="Button" ControlStyle-CssClass="EditButton DeleteButton" Text="Delete" CommandName="Delete" />
                </Columns>
            </asp:GridView>
        </div>
    </form>



     <script src="Scripts/Script.js"></script>

</asp:Content>

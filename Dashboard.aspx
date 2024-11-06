<%@ Page Title="IMS Dashboard" Language="C#" MasterPageFile="~/IMS.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="IT508_Project.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
    .gridview {
        width: 100%;
        border: 1px solid #000;
    }
    .gridview th {
        background-color: #f1f1f1;
        color: #333;
        padding: 10px;
    }
    .gridview td {
        border: 1px solid #000;
        padding: 8px;
    }
    .gridview .one-column {
        width: 30%;
    }
    .gridview .two-column {
        width: 40%;
    }
    .gridview-caption {
        font-weight: bold;
        font-size: 18px;
        margin-bottom: 10px;
        text-align: center;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <div class="Container">
            <div class="title-info">
                <p>IMS-StockUP Dashboard</p>
                <i class="fa fa-bar-chart"></i>
            </div>
            <div class="data-info">
                <div class="box">
                    <i class="fa fa-user DataIcon"></i>
                    <div class="data">
                        <p>User</p>
                        <span><asp:Label ID="lblUsersCount" CssClass="Count" style="width: 50%; border: none; outline: none; color: #31344b; font-size: 50px; background-color: transparent; text-align: center" runat="server"/></span>
                    </div>
                </div>
                <div class="box">
                    <i class="fa fa-table DataIcon"></i>
                    <div class="data">
                        <p>Items</p>
                        <span><asp:Label ID="lblItemsCount" CssClass="Count" style="width: 50%; border: none; outline: none; color: #31344b; font-size: 50px; background-color: transparent; text-align: center" runat="server"/></span>
                    </div>
                </div>
                <div class="box">
                    <i class="fa fa-bank DataIcon"></i>
                    <div class="data">
                        <p>Warehouse</p>
                        <span><asp:Label ID="lblWHCount" CssClass="Count" style="width: 50%; border: none; outline: none; color: #31344b; font-size: 50px; background-color: transparent; text-align: center" runat="server"/></span>
                    </div>
                </div>
                <div class="box">
                    <i class="fa fa-retweet DataIcon"></i>
                    <div class="data">
                        <p>Transactions</p>
                        <span><asp:Label ID="lblTransCount" CssClass="Count" style="width: 50%; border: none; outline: none; color: #31344b; font-size: 50px; background-color: transparent; text-align: center" runat="server"/></span>
                    </div>
                </div>
            </div>
            <div class="title-info">
                <p>Products</p>
                <i class="fa fa-table"></i>
            </div>
        </div>
        <div style="display: flex; width: 100%;gap: 5%; align-items: center; margin-left: 5%;">
            <div style="width: 40%;">
                <asp:GridView ID="gvExpireItems" runat="server" AutoGenerateColumns="False" DataKeyNames="Item_ID,WH_ID,Exp_Date" CssClass="gridview" Caption="Expired Items" CaptionStyle-CssClass="gridview-caption">
                    <Columns>
                        <asp:BoundField DataField="Item_ID" HeaderText="Item ID" HeaderStyle-CssClass="item-id-column" ItemStyle-CssClass="one-column" />
                        <asp:BoundField DataField="WH_ID" HeaderText="WH ID" HeaderStyle-CssClass="WH_ID-column" ItemStyle-CssClass="one-column" />
                        <asp:TemplateField HeaderText="Exp. Date" HeaderStyle-CssClass="Exp_Date-column" ItemStyle-CssClass="two-column">
                            <ItemTemplate>
                                <%# Eval("Exp_Date", "{0:MM/dd/yyyy}") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div style="width: 40%;">
                <asp:GridView ID="gvReorder" runat="server" AutoGenerateColumns="False" DataKeyNames="Item_ID,TotalQuantity,Reorder_Threshold" CssClass="gridview" Caption="Reorder Items" CaptionStyle-CssClass="gridview-caption">
                    <Columns>
                        <asp:BoundField DataField="Item_ID" HeaderText="Item ID" HeaderStyle-CssClass="item-id-column" ItemStyle-CssClass="one-column" />
                        <asp:BoundField DataField="TotalQuantity" HeaderText="Total Qty" HeaderStyle-CssClass="tquantity-column" ItemStyle-CssClass="one-column" />
                        <asp:BoundField DataField="Reorder_Threshold" HeaderText="Reorder Threshold" HeaderStyle-CssClass="reorder-column" ItemStyle-CssClass="two-column" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</asp:Content>
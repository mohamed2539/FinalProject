<%@ Page Title="Create New Object" Language="C#" MasterPageFile="~/IMS.Master" AutoEventWireup="true" CodeBehind="createnewobject.aspx.cs" Inherits="IT508_Project.createnewobject" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <div class="title-info2">
            <p>Here you can Add New Object</p>
            <i class="fa fa-hand-o-down"></i>
        </div>
        <div class="ButtonsContainer">
            <div class="data-info2">
                <div class="box">
                    <i class="fa fa-user"></i>
                    <div class="data">
                        <asp:Button ID="addNewUser" class="Btn_Report" runat="server" Text="Add New User" OnClick="addNewUser_Click" />
                    </div>
                </div>
                <div class="box">
                    <i class="fa fa-table"></i>
                    <div class="data">
                        <asp:Button ID="addProduct" class="Btn_Report" runat="server" Text="Add New Item" OnClick="addProduct_Click" />
                    </div>
                </div>
                <div class="box">
                    <i class="fa fa-bank"></i>
                    <div class="data">
                        <asp:Button ID="addWarehouse" class="Btn_Report" runat="server" Text="Add Warehouse" OnClick="addWarehouse_Click" />
                    </div>
                </div>
                <div class="box">
                    <i class="fa fa-handshake-o"></i>
                    <div class="data">
                        <asp:Button ID="addSupplier" class="Btn_Report" runat="server" Text="Add Supplier" OnClick="addSupplier_Click" />
                    </div>
                </div>
                <div class="box">
                    <i class="fa fa-child"></i>
                    <div class="data">
                        <asp:Button ID="addCustomer" class="Btn_Report" runat="server" Text="Add Customer" OnClick="addCustomer_Click" />
                    </div>
                </div>
                <div class="box">
                    <i class="fa fa-cube"></i>
                    <div class="data">
                        <asp:Button ID="addTranType" class="Btn_Report" runat="server" Text="Add TransType" OnClick="addTranType_Click" />
                    </div>
                </div>
                <div class="box">
                    <i class="fa fa-compress"></i>
                    <div class="data">
                        <asp:Button ID="linkItemToWH" class="Btn_Report" runat="server" Text="Link Item To WH" OnClick="linkItemToWH_Click" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>

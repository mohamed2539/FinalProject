<%@ Page Title="Add New Warehouse" Language="C#" MasterPageFile="~/IMS.Master" AutoEventWireup="true" CodeBehind="addWH.aspx.cs" Inherits="IT508_Project.addWH" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="title-info2" style="text-align: center;">
        <p>Here you can Add New WareHouse</p>
        <i class="fa fa-hand-o-down"></i>
    </div>
    <div class="addBox">
        <form runat="server" class="GeneralForm">
            <h3>Add New WareHouse</h3>
            <div>
                <asp:Label Text="WareHouse ID" CssClass="lblWHID" runat="server" style="width: 20%; margin-right: 10px;"/>
                <asp:TextBox runat="server" ID="txtWHID" CssClass="txtBx01 wharehouseInput" placeholder="Enter WH ID" />
            </div>
            <div>
                <asp:Label Text="WareHouse Name" CssClass="lblWHName" runat="server" style="width: 20%; margin-right: 10px;"/>
                <asp:TextBox runat="server" ID="txtWHName" CssClass="txtBx01 wharehouseInput" placeholder="Enter WH Name" />
            </div>
            <div>
                <div class="twoBtn"">
                    <asp:Button Text="Submit" CssClass=" GeneralButton wharehouseButton" runat="server" ID="btnSubmit" OnClick="btnSubmit_Click" />
                    <asp:Button Text="Edit" CssClass=" GeneralButton wharehouseButton" ID="btnEdit"  runat="server" OnClick="btnEdit_Click" />
                    <asp:Button Text="Delete" CssClass="GeneralButton" ID="btnDel" runat="server" OnClick="btnDel_Click" />
                </div>
                <div class="twoBtn">
                    <asp:Button Text="Cancel" class="btnBack" CssClass=" GeneralButton wharehouseButton" ID="btnBack"  runat="server" OnClick="btnBack_Click" />
                    <input id="Reset1"  class="GeneralButton danger wharehouseButton" type="reset" value="Reset" />
                </div>
                <asp:Label style="padding:10px;" Text="WareHouse Already Exists!!" ID="lblErrorMessage" runat="server" CssClass="lblErrorMsg" />
                <asp:Label ID="lblSuccessMessage" runat="server" Text="" ForeColor="Green"></asp:Label>
            </div>
        </form>
    </div>
</asp:Content>
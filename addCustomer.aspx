<%@ Page Title="Add New Customer" Language="C#" MasterPageFile="~/IMS.Master" AutoEventWireup="true" CodeBehind="addCustomer.aspx.cs" Inherits="IT508_Project.addCustomer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="title-info2">
        <p>Here you can Add New Customer</p>
        <i class="fa fa-hand-o-down"></i>
    </div>
    <div class="addBox">
        <form runat="server" class="GeneralForm">
            <h3>Add New Customer</h3>
            <div>
                <asp:Label Text="Customer ID" CssClass="lblCusID" runat="server" style="width: 20%; margin-right: 10px;"/>
                <asp:TextBox runat="server" ID="txtCusID" CssClass="GeneralInputTow" placeholder="Enter Customer ID" />
            </div>
            <div>
                <asp:Label Text="Customer Name" CssClass="lblCusName" runat="server" style="width: 20%; margin-right: 10px;"/>
                <asp:TextBox runat="server" ID="txtCusName" CssClass="GeneralInputTow" placeholder="Enter Customer Name" />
            </div>
            <div>
                <asp:Label Text="Contact Num" CssClass="lblContactNum" runat="server" style="width: 20%; margin-right: 10px;"/>
                <asp:TextBox runat="server" ID="txtContactNum" CssClass="GeneralInputTow" placeholder="01XXXXXXXXX" />
            </div>
            <div>
                <asp:Label Text="E-Mail" CssClass="lblCusEmail" runat="server" style="width: 20%; margin-right: 10px;"/>
                <asp:TextBox runat="server" ID="txtCusEmail" CssClass="GeneralInputTow" TextMode="Email" placeholder="XXX@XXX.COM" />
            </div>
            <div>
                <div>
                    <asp:Button ID="btnSubmit" Text="Submit" CssClass="GeneralButton " runat="server" OnClick="btnSubmit_Click" />
                    <asp:Button Text="Edit" CssClass="GeneralButton" ID="btnEdit" runat="server" OnClick="btnEdit_Click" />
                    <asp:Button Text="Delete" CssClass="GeneralButton" ID="btnDel" runat="server" OnClick="btnDel_Click" />
                </div>
                <div class="twoBtn">
                    <asp:Button Text="Cancel"  CssClass="GeneralButton" ID="btnBack" runat="server" onmouseover="this.style.backgroundColor=Yellow;" onmouseout="this.style.backgroundColor=darkkhaki;" OnClick="btnBack_Click"/>
                    <input id="Reset1" class="GeneralButton" type="reset" value="reset"/>
                </div>
                <asp:Label style="padding:10px;" Text="Customer Already Exists!!" ID="lblErrorMessage" runat="server" CssClass="lblErrorMsg" />
                <asp:Label ID="lblSuccessMessage" runat="server" Text="" ForeColor="Green"></asp:Label>
            </div>
        </form>
    </div>
</asp:Content>
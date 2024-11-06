<%@ Page Title="Add New Supplier" Language="C#" MasterPageFile="~/IMS.Master" AutoEventWireup="true" CodeBehind="addSupplier.aspx.cs" Inherits="IT508_Project.addSupplier" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="title-info2" style="text-align: center;">
        <p>Here you can Add New Supplier</p>
        <i class="fa fa-hand-o-down"></i>
    </div>
    <div class="addBox">
        <form runat="server" class="GeneralForm">
             <h3>Add New Supplier</h3>
            <div>
                <asp:Label Text="Supplier ID" CssClass="lblSupID" runat="server" style="width: 20%; margin-right: 10px;"/>
                <asp:TextBox runat="server" ID="txtSupID" CssClass="GeneralInputTow" placeholder="Enter Supplier ID" />
            </div>
            <div>
                <asp:Label Text="Supplier Name" CssClass="lblSupName" runat="server" style="width: 20%; margin-right: 10px;"/>
                <asp:TextBox runat="server" ID="txtSupName" CssClass="GeneralInputTow" placeholder="Enter Supplier Name" />
            </div>
            <div>
                <asp:Label Text="Contact Num" CssClass="lblContactNum" runat="server" style="width: 20%; margin-right: 10px;"/>
                <asp:TextBox runat="server" ID="txtContactNum" CssClass="GeneralInputTow" placeholder="01XXXXXXXXX" />
            </div>
            <div>
                <asp:Label Text="E-Mail" CssClass="lblCusEmail" runat="server" style="width: 20%; margin-right: 10px;"/>
                <asp:TextBox runat="server" ID="txtSupEmail" TextMode="Email" CssClass="GeneralInputTow" placeholder="XXX@XXX.COM" />
            </div>
            <div>
                <div class="twoBtn">
                    <asp:Button Text="Submit" ID="btnSubmit" CssClass="GeneralButton" runat="server" OnClick="btnSubmit_Click" />
                    <asp:Button Text="Edit" CssClass="GeneralButton" ID="btnEdit" runat="server" OnClick="btnEdit_Click" />
                    <asp:Button Text="Delete" CssClass="GeneralButton" ID="btnDel" runat="server" OnClick="btnDel_Click" />
                </div>
                <div class="twoBtn">
                    <asp:Button Text="Cancel"  CssClass="GeneralButton" ID="btnBack" runat="server" OnClick="btnBack_Click" />
                    <input id="Reset1" class="GeneralButton" type="reset" value="Reset"  />
                </div>
                <asp:Label style="padding:10px;" Text="Supplier Already Exists!!" ID="lblErrorMessage" runat="server" CssClass="lblErrorMsg" />
                <asp:Label ID="lblSuccessMessage" runat="server" Text="" ForeColor="Green"></asp:Label>
            </div>
        </form>
    </div>
</asp:Content>
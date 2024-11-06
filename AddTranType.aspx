<%@ Page Title="Add Transaction Type" Language="C#" MasterPageFile="~/IMS.Master" AutoEventWireup="true" CodeBehind="AddTranType.aspx.cs" Inherits="IT508_Project.AddTranType" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="title-info2" style="text-align: center;">
        <p>Here you can Add Transaction Type</p>
        <i class="fa fa-hand-o-down"></i>
    </div>
    <div class="addBox">
        <form runat="server" class="GeneralForm ItemsForm">
            <h3>Add New Transaction Type</h3>
            <div>
                <asp:Label Text="Transaction Type" CssClass="lblTransType" runat="server" style="width: 20%; margin-right: 10px;"/>
                <asp:DropDownList ID="ttypeddl" CssClass="GeneralInputTow" runat="server" OnSelectedIndexChanged="ddltTypeID_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
            </div>
            <div>
                <asp:Label Text="Transaction Type ID" CssClass="lblTransTypeID" runat="server" style="width: 20%; margin-right: 10px;"/>
                <asp:TextBox runat="server" ID="txtTranTypeID" CssClass="GeneralInputTow" placeholder="Enter TranType ID" />
            </div>
            <div>
                <asp:Label Text="Transaction Type Name" CssClass="lblTransTypeName" runat="server" style="width: 20%; margin-right: 10px;"/>
                <asp:TextBox runat="server" ID="txtTranTypeName" CssClass="GeneralInputTow" placeholder="Enter TranType Name" />
            </div>
            <div>
                <div class="twoBtn"">
                    <asp:Button Text="Submit" CssClass="GeneralButton" runat="server" ID="btnSubmit" OnClick="btnSubmit_Click"/>
                    <asp:Button Text="Edit" CssClass="GeneralButton" ID="btnEdit"  runat="server" OnClick="btnEdit_Click" />
                    <asp:Button Text="Delete" CssClass="GeneralButton" ID="btnDel" runat="server" OnClick="btnDel_Click" />
                </div>
                <div class="twoBtn">
                    <asp:Button Text="Cancel" class="btnBack" CssClass="GeneralButton" ID="btnBack"  runat="server" OnClick="btnBack_Click" />
                    <input id="Reset1"  class="danger GeneralButton" type="reset" value="Reset" />
                </div>
                <asp:Label style="padding:10px;" Text="" ID="lblErrorMessage" runat="server" CssClass="lblErrorMsg" />
                <asp:Label ID="lblSuccessMessage" runat="server" Text="" ForeColor="Green"></asp:Label>
            </div>
        </form>
    </div>
</asp:Content>

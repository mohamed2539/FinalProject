<%@ Page Title="" Language="C#" MasterPageFile="~/IMS.Master" AutoEventWireup="true" CodeBehind="linkItemToWH.aspx.cs" Inherits="IT508_Project.linkItemToWH" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<link rel="preconnect" href="https://fonts.googleapis.com">
<link rel="preconnect" href="https://fonts.gstatic.com" crossorigin="">
<link href="https://fonts.googleapis.com/css2?family=Arsenal+SC:ital,wght@0,400;0,700;1,400;1,700&family=Lilita+One&display=swap" rel="stylesheet">

    <div class="title-info2" style="text-align: center;">
        <p>Here you can Link Item To WH</p>
        <i class="fa fa-hand-o-down"></i>
    </div>
    <div class="addBox">
        <form runat="server" class="GeneralForm ItemsForm">
             <h2>Link Item To WH</h2>
            <div>
                <asp:Label Text="Item ID" CssClass="lblItemID" runat="server" style="width: 20%; margin-right: 10px;"/>
                <asp:DropDownList ID="itemddl" CssClass="GeneralInputTow" runat="server">
                </asp:DropDownList>
            </div>
            <div>
                <asp:Label Text="WH ID" CssClass="lblWHID" runat="server" style="width: 20%; margin-right: 10px;"/>
                <asp:DropDownList ID="whddl" CssClass="GeneralInputTow" runat="server">
                </asp:DropDownList>
            </div>
            <div>
                <asp:Label Text="Quantity" CssClass="lblQtyID" runat="server" style="width: 20%; margin-right: 10px;"/>
                <asp:TextBox runat="server" ID="txtQuantity" CssClass="GeneralInputTow" placeholder="Quantity"/>
            </div>
            <div>
                <asp:Label Text="Open Balance" CssClass="lblOpenBalance" runat="server" style="width: 20%; margin-right: 10px;"/>
                <asp:TextBox runat="server" ID="txtOpenBalance" CssClass="GeneralInputTow" placeholder="Open Balance"/>
            </div>
            <div>
                <asp:Label Text="Position" CssClass="lblPosition" runat="server" style="width: 20%; margin-right: 10px;"/>
                <asp:TextBox runat="server" ID="txtPosition" CssClass="GeneralInputTow" placeholder="Item Position"/>
            </div>
            <div>
                <div class="twoBtn" >
                    <asp:Button Text="Submit" ID="btnSubmit" CssClass="GeneralButton" runat="server" OnClick="btnSubmit_Click"/>
                    <asp:Button Text="Edit" ID="btnEdit" CssClass="GeneralButton" runat="server" OnClick="btnEdit_Click"/>
                    <asp:Button Text="Delete" ID="btnDel" CssClass="GeneralButton" runat="server" OnClick="btnDel_Click"/>
                </div>
                <div class="twoBtn">
                    <asp:Button Text="Cancel"  CssClass="GeneralButton" ID="btnBack" runat="server" OnClick="btnBack_Click"/>
                    <input id="Reset1" class="danger GeneralButton"  type="reset" value="Reset" />
                </div>
                <asp:Label ID="lblErrorMessage" runat="server" Text="" style="padding:10px;" CssClass="lblErrorMsg" />
                <asp:Label ID="lblSuccessMessage" runat="server" Text="" ForeColor="Green"></asp:Label>
            </div>
        </form>
    </div>
</asp:Content>
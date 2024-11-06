<%@ Page Title="" Language="C#" MasterPageFile="~/IMS.Master" AutoEventWireup="true" CodeBehind="AddNewCustomer.aspx.cs" Inherits="IT508_Project.AddNewCustomer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="AddCustomerForm"     class="GeneralForm AddCustomerForm" runat="server">

        <h4 class="AddPosCustomerHeader">Add New POS Customer Form</h4>
        <asp:Label   ID="SessionInfoLabel" runat="server" Text=""></asp:Label>
        <%-- This Lbale to Show To user Error Message About what he Enter --%>
        <asp:Label   ID="CheckFillTextBox"     class="GeneralErrorMessage"    runat="server" Text=""></asp:Label>
         <%-- This Lbale to Show To user Success Message If Insert Is Done With no Error --%>
        <asp:Label   ID="SuccessInserting"      class="SuccessLabel"           runat="server" Text=""></asp:Label>
        <asp:Label   ID="CheckDBQuary"          class="GeneralErrorMessage"    runat="server" Text=""></asp:Label>
        <asp:TextBox ID="CustomerName"          class="GeneralInputTow  AddCustomerInput"  placeholder="Customer Name"    runat="server" ></asp:TextBox>
        <asp:TextBox ID="CustomerPhone"         class="GeneralInputTow  AddCustomerInput"  placeholder="Customer Phone"   runat="server" ></asp:TextBox>
        <asp:TextBox ID="CustomerAddress"       class="GeneralInputTow  AddCustomerInput"  placeholder="Customer Address" runat="server"></asp:TextBox>
        <asp:Button  ID="AddCustomer"           class="GeneralButton    AddCustomerButton"  runat ="server"    Text="Add New Curstomer"       OnClick="AddCustomer_Click"/>
        <asp:Button  ID="ShowCustomer"          class="GeneralButton   MediumButton GeneralShowAllButton" Text="Show All Customer"       runat="server"  OnClick="ShowCustomer_Click" />
    </form>
</asp:Content>


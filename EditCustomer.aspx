<%@ Page Title="" Language="C#" MasterPageFile="~/IMS.Master" AutoEventWireup="true" CodeBehind="EditCustomer.aspx.cs" Inherits="IT508_Project.EditCustomer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="UpdateCustomerForm"  class="GeneralForm AddCustomerForm" runat="server">
        <asp:Label ID="CheckFaildsError"      runat="server" Text=""></asp:Label><br>
        <asp:Label ID="UpdateCustomerLabel"  class="Generl-Label"    runat="server" Text=""></asp:Label>
        <h4 class="AddPosCustomerHeader">This Form To Update Customer Information</h4>
        <asp:TextBox ID="EditCustomerID"        class="GeneralInputTow AddCustomerInput"    placeholder="Customer ID"               runat="server"></asp:TextBox>
        <asp:TextBox ID="EditCustomerName"      class="GeneralInputTow AddCustomerInput"    placeholder="Customer Name"             runat="server" ></asp:TextBox>
        <asp:TextBox ID="EditCustomerPhone"     class="GeneralInputTow AddCustomerInput"    placeholder="Customer Phone"            runat="server" ></asp:TextBox>
        <asp:TextBox ID="EditCustomerAddress"   class="GeneralInputTow AddCustomerInput"    placeholder="Customer Address"          runat="server"></asp:TextBox>
        <%--<asp:Button  ID="UpdateCustomer"        class="GeneralButton AddCustomerButton  MediumButton"  Text="Update Curstomer Info" runat ="server"       OnClick="UpdateCustomer_Click"/>--%>
        <asp:Button ID="UpdateCustomerInfo" class="GeneralButton AddCustomerButton  MediumButton"  runat="server" Text="Update Curstomer Info" OnClick="UpdateCustomerInfo_Click" />
    </form>
</asp:Content>
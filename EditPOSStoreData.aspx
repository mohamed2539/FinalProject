<%@ Page Title="" Language="C#" MasterPageFile="~/IMS.Master" AutoEventWireup="true" CodeBehind="EditPOSStoreData.aspx.cs" Inherits="IT508_Project.EditPOSStoreData" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

            <form   ID="POsStoreEditForm"    class="StoreOnePiceForm" runat="server">
                <h4>Update Pos Store Data Form</h4>
                <asp:Label        ID="ErrorValidNumber"     class="GeneralErrorMessage"       runat="server" Text=""></asp:Label><br>
                <asp:Label        ID="UpdatePOsStoreLabel"  class="GeneralSuccess"       runat="server" Text=""></asp:Label><br>

                <asp:Label        ID="ItemIdLabel"          class="TinyLeble"           runat="server" Text="Choose Item Id" ></asp:Label>
                <asp:DropDownList ID="ItemId"               class="Select"              runat="server"></asp:DropDownList><br>

                <asp:Label        ID="whIdLabel"            class="TinyLeble LebleWithMoreWidth"           runat="server" Text="Choose WhereHouse Id"></asp:Label>
                <asp:DropDownList ID="whId"                 class="Select"              runat="server"></asp:DropDownList><br>

                <asp:TextBox      ID="OpenBalance"          class="GeneralInputTow "    runat="server"></asp:TextBox>
                <asp:TextBox      ID="quantity"             class="GeneralInputTow "    runat="server"></asp:TextBox>
                <asp:TextBox      ID="ItemPosition"         class="GeneralInputTow "    runat="server"></asp:TextBox>
                <asp:TextBox      ID="Price"                class="GeneralInputTow "    runat="server"></asp:TextBox> 
                    
                <asp:Button ID="POsStoreButton" class="GeneralButton   MediumButton" runat="server" Text="Update Data" OnClick="POsStoreButton_Click" />
            </form>

</asp:Content>
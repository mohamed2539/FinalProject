<%@ Page Title="" Language="C#" MasterPageFile="~/IMS.Master" AutoEventWireup="true" CodeBehind="ShowPOSStoreData.aspx.cs" Inherits="IT508_Project.ShowPOSStoreData" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="ShowPosStoresDataForm"  runat="server">
        <asp:GridView ID="ShowPosStoresData"  AutoGenerateColumns="False" 
            OnRowDeleting="ClickedRowDeleting" 
            OnRowCommand="EditOrDelete_RowCommand" DataKeyNames="item_id,wh_id"
            class="ShowPosStoresData" runat="server">
             <Columns>
                <asp:BoundField  DataField="item_id"         HeaderText="Item id" InsertVisible="False" ReadOnly="True" />
                <asp:BoundField  DataField="wh_id"           HeaderText="WhereHouse id"  />
                <asp:BoundField  DataField="Open_Balance"    HeaderText="Open Balance"/>
                <asp:BoundField  DataField="quantity"        HeaderText="quantity" />
                <asp:BoundField  DataField="Item_Position"   HeaderText="Item Position" />
                <asp:BoundField  DataField="Price"           HeaderText="Price"/>
                <asp:ButtonField ButtonType="Button" ControlStyle-CssClass="EditButton" Text="Edit" CommandName="Edit" />
                <asp:ButtonField ButtonType="Button" ControlStyle-CssClass="EditButton DeleteButton" Text="Delete" CommandName="Delete" />
            </Columns>

        </asp:GridView>
        <asp:Button ID="BackButton" class="GeneralButton BigButton" runat="server" Text="Back" OnClick="BackButton_Click" />

    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>

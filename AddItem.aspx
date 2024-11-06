<%@ Page Title="Add New Item" Language="C#" MasterPageFile="~/IMS.Master" AutoEventWireup="true" CodeBehind="AddItem.aspx.cs" Inherits="IT508_Project.AddItem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="title-info2">
        <p>Here you can Add New Item</p>
        <i class="fa fa-hand-o-down"></i>
    </div>
    <div>
        <form runat="server" class="GeneralForm ItemsForm">
            <h2>Add New Item</h2>
            <div>
                <asp:Label Text="Item ID" CssClass="lblItemID" runat="server" style="width: 20%; margin-right: 10px;"/>
                <asp:TextBox runat="server" ID="txtItemID" CssClass=" GeneralInput wharehouseInput ItemsInput" placeholder="Enter Item ID" />
            </div>
            <div>
                <asp:Label Text="Item Name" CssClass="lblItemName" runat="server" style="width: 20%; margin-right: 10px;"/>
                <asp:TextBox runat="server" ID="txtitemname" CssClass=" GeneralInput wharehouseInput ItemsInput" placeholder="Enter Item Name"/>
            </div>
            <div>
                <asp:Label Text="Category" CssClass="lblCategory" runat="server" style="width: 20%; margin-right: 10px;"/>
                <asp:DropDownList ID="txtddl2" CssClass="txtBx01 itemsSelectInput" runat="server">
                    <asp:ListItem>Men</asp:ListItem>
                    <asp:ListItem Selected="True">Women</asp:ListItem>
                    <asp:ListItem>Girly</asp:ListItem>
                    <asp:ListItem>Kids</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div>
                <asp:Label Text="Production Date" CssClass="lblProdDate" runat="server" style="width: 20%; margin-right: 10px;"/>
                <asp:TextBox runat="server" ID="txtproddate" CssClass=" GeneralInput wharehouseInput ItemsInput" TextMode="Date" placeholder="01/01/2001" />
            </div>
            <div>
                <asp:Label Text="Expire Date" CssClass="lblExDate" runat="server" style="width: 20%; margin-right: 10px;"/>
                <asp:TextBox runat="server" ID="txtexpdate" CssClass=" GeneralInput wharehouseInput ItemsInput" TextMode="Date" placeholder="01/01/2001" />
            </div>
            <div>
                <asp:Label Text="Re-Order" CssClass="lblReorder" runat="server" style="width: 20%; margin-right: 10px;"/>
                <asp:TextBox runat="server" ID="txtreorder" CssClass=" GeneralInput wharehouseInput ItemsInput"  TextMode="Number" placeholder="1234" />
            </div>
            <div >
                <asp:Label Text="Measure Unit" CssClass="lblMeasureUnit" runat="server" style="width: 20%; margin-right: 10px;"/>
                <asp:TextBox runat="server" ID="Textmeasureunit" CssClass="GeneralInput wharehouseInput ItemsInput"  placeholder="Piece" />
            </div>
            <div>
                <div class="twoBtn">
                    <asp:Button Text="Submit" ID="btnSubmit" CssClass="GeneralButton" runat="server" OnClick="btnSubmit_Click" />
                    <asp:Button Text="Edit" CssClass="GeneralButton" ID="btnEdit" runat="server" OnClick="btnEdit_Click" />
                    <asp:Button Text="Delete" CssClass="GeneralButton" ID="btnDel" runat="server" OnClick="btnDel_Click" />
                </div>
                <div >
                    <asp:Button Text="Cancel" class="btnBack" CssClass="GeneralButton" ID="btnBack" runat="server" OnClick="btnBack_Click" />
                    <input id="Reset1" class="danger GeneralButton"  type="reset" value="Reset" />
                </div>
                <asp:Label style="padding:10px;" Text="Item Already Exists!!" ID="lblErrorMessage" runat="server" CssClass="lblErrorMsg" />
                <asp:Label ID="lblSuccessMessage" runat="server" Text="" ForeColor="Green"></asp:Label>
            </div>
        </form>
    </div>
</asp:Content>

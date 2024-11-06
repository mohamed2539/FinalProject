﻿<%@ Page Title="" Language="C#" MasterPageFile="~/IMS.Master" AutoEventWireup="true" CodeBehind="incomeTest.aspx.cs" Inherits="IT508_Project.incomeTest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="title-info2">
        <p>Here you can Add New Inbound Inventory</p>
        <i class="fa fa-hand-o-down"></i>
    </div>
    <div class="TmasterBox InComingContainer" >
        <h2 class="DisbursingHeader">Create Inbound Inventory</h2>
        <form runat="server" class="InComingPageForm">
        <div class="sub1Box">
            <div>
                <asp:Label Text="Transaction ID" CssClass="lbl02" runat="server" style="width: 20%;"/>
                <asp:TextBox runat="server" ID="transID" CssClass="txtBx01" placeholder="0000" />
                <asp:Label Text="Trans Type" CssClass="lbl02" runat="server" style="width: 20%;"/>
                <asp:DropDownList ID="transtypeddl" CssClass="txtBx01 Select" runat="server">
                </asp:DropDownList>
            </div>
            <div>
                <asp:Label Text="WareHouse ID" CssClass="lbl02" runat="server" style="width: 20%;"/>
                <asp:DropDownList ID="whddl" CssClass="txtBx01" runat="server">
                </asp:DropDownList>
                <asp:Label Text="Trans Date:" CssClass="lbl02" runat="server" style="width: 20%;"/>
                <asp:TextBox runat="server" ID="txttransdate" CssClass="txtBx01" placeholder="Enter Trans Date" textMode="Date"/>
            </div>
            <div>
                <asp:Label Text="Supplier ID" CssClass="lbl02" runat="server" style="width: 20%;"/>
                <asp:DropDownList ID="supddl" CssClass="txtBx01 Select" runat="server">
                </asp:DropDownList>
                <asp:Button ID="saveMaster" runat="server" Text="Save" CssClass="btnSubmit2"  OnClick="saveMaster_Click"/>
            </div>
        </div>
        <div class="sub2Box">
            <div>
                <table>
                    <colgroup>
                        <col span="1" style="width: 10%;">
                        <col span="1" style="width: 15%;">
                        <col span="1" style="width: 30%;">
                        <col span="1" style="width: 20%;">
                        <col span="1" style="width: 25%;">
                    </colgroup>
                    <thead>
                        <tr>
                            <td><asp:Label Text="Serial" style="width:10%" CssClass="lbl02" runat="server" /></td>
                            <td><asp:Label Text="Item ID" style="width:15%" CssClass="lbl02" runat="server" /></td>
                            <td><asp:Label Text="Item Name" style="width:30%" CssClass="lbl02" runat="server" /></td>
                            <td><asp:Label Text="Quantity" style="width:20%" CssClass="lbl02" runat="server" /></td>
                            <td><asp:Label Text="Position" style="width:25%" CssClass="lbl02" runat="server" /></td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td><asp:Label runat="server" ID="SerialNum" CssClass="lbl02" Text="1" /></td>
                            <td><asp:DropDownList ID="ddlItemID" CssClass="txtBx01 Select" runat="server" OnSelectedIndexChanged="ddlItemID_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                            <td><asp:TextBox runat="server" ID="txtItemName" CssClass="txtBx01" /></td>
                            <td><asp:TextBox runat="server" ID="txtQuantity" CssClass="txtBx01" TextMode="Number" /></td>
                            <td><asp:TextBox runat="server" ID="txtPosition" CssClass="txtBx01" /></td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="twoBtn">
                <asp:Button Text="Insert" ID="btnInsert" CssClass="btnSubmit2 InconingCustomButton" runat="server"  OnClick="btnInsert_Click" />
                <asp:Button Text="Cancel" class="btnBack" CssClass="btnSubmit2 InconingCustomButton" ID="btnBack" runat="server" OnClick="btnBack_Click" />
                <asp:Button Text="Delete" CssClass="btnSubmit2 danger" ID="btnDelete"  runat="server" OnClick="btnDelete_Click" />
            </div>
            <div>
                <asp:GridView ID="gvTransDetails" runat="server" AutoGenerateColumns="False" DataKeyNames="Serial,Item_ID,Quantity" OnRowCommand="gvTransDetails_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="Serial" HeaderText="Serial" />
                        <asp:BoundField DataField="Item_ID" HeaderText="Item ID" />
                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                        <asp:BoundField DataField="Position" HeaderText="Position" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="img/delete.png" style="width: 16px; height: 16px; border: none; padding: 0; margin: 0; background: none;" CommandName="DeleteRow" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div>
                <asp:Label ID="lblErrorMessage" style="padding:10px;" Text="" runat="server" CssClass="lblErrorMsg" />
                <asp:Label ID="lblSuccessMessage" runat="server" Text="" ForeColor="Green"></asp:Label>
            </div>
        </div>
        </form>
    </div>
</asp:Content>
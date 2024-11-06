<%@ Page Title="" Language="C#" MasterPageFile="~/IMS.Master" AutoEventWireup="true" CodeBehind="Auditing.aspx.cs" Inherits="IT508_Project.Auditing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="title-info2">
        <p>Here you can Add New Audit</p>
        <i class="fa fa-hand-o-down"></i>
    </div>
    <div class="TmasterBox">
        <h2>Create Audit</h2>
        <form runat="server" class="GeneralForm DisbursingForm">
        <div class="sub1Box">
            <div>
                <asp:Label Text="Transaction ID" CssClass="lbl02" runat="server" style="width: 20%;"/>
                <asp:TextBox runat="server" ID="transID" CssClass="txtBx01 GeneralInput" placeholder="0000" />
                <asp:Label Text="Trans Type" CssClass="lbl02" runat="server" style="width: 20%;"/>
                <asp:DropDownList ID="transtypeddl" CssClass="txtBx01 Select" runat="server">
                </asp:DropDownList>
            </div>
            <div style="display: flex; gap: 20px; align-items: center; margin-bottom: 10px;">
                <asp:Label Text="WareHouse ID" CssClass="lbl02" runat="server" style="width: 20%;"/>
                <asp:DropDownList ID="whddl" CssClass="txtBx01 Select" runat="server" style="width: 25%;">
                </asp:DropDownList>
                <asp:Label Text="Trans Date:" CssClass="lbl02" runat="server" style="width: 20%;"/>
                <asp:TextBox runat="server" ID="txttransdate" CssClass="txtBx01 GeneralInput" placeholder="Enter Trans Date" textMode="Date"/>
            </div>
            <div>
                <asp:Button ID="saveMaster" runat="server" Text="Proceed" CssClass="btnSubmit2 GeneralButton"  OnClick="saveMaster_Click"/>
            </div>
        </div>
        <div class="sub2Box">
            <div>
                <asp:GridView ID="gvTransDetails" runat="server" AutoGenerateColumns="False" DataKeyNames="Serial,Item_ID,Quantity" OnRowDataBound="gvTransDetails_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="Serial" HeaderText="Serial" />
                        <asp:BoundField DataField="Item_ID" HeaderText="Item ID"/>
                        <asp:BoundField DataField="Quantity" HeaderText="Quantity"/>
                        <asp:TemplateField HeaderText="Physical Qty">
                            <ItemTemplate>
                                <asp:TextBox ID="txtPhysicalQty" runat="server" CssClass="txtBx01" AutoPostBack="True" OnTextChanged="txtPhysicalQty_TextChanged" RowIndex='<%# Container.DataItemIndex %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Difference">
                            <ItemTemplate>
                                <asp:Label ID="lblDifference" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="twoBtn">
                <asp:Button Text="Submit" ID="btnSubmit" CssClass="btnSubmit2 GeneralButton" runat="server"  OnClick="btnSubmit_Click"/>
                <asp:Button Text="Cancel" class="btnBack" CssClass="btnSubmit2 GeneralButton" ID="btnBack" runat="server"  OnClick="btnBack_Click" />
            </div>
            <div>
                <asp:Label ID="lblErrorMessage" style="padding:10px;" Text="" runat="server" ForeColor="Red" CssClass="lblErrorMsg" />
                <asp:Label ID="lblSuccessMessage" runat="server" Text="" ForeColor="Green"></asp:Label>
            </div>
        </div>
        </form>
    </div>
</asp:Content>

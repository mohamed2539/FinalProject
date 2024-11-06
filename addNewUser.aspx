<%@ Page Title="Add New User" Language="C#" MasterPageFile="~/IMS.Master" AutoEventWireup="true" CodeBehind="addNewUser.aspx.cs" Inherits="IT508_Project.addNewUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="title-info2">
        <p>Here you can Add New User</p>
        <i class="fa fa-hand-o-down"></i>
    </div>
    <div class="addBox">
        <form runat="server" class="addNewUserForm">
            <h2 class="AddNewUserHeader">Add new user</h2>
            <div>
                <asp:Label Text="User ID" CssClass="lblUserID" runat="server" style="width: 20%; margin-right: 10px;"/>
                <asp:TextBox runat="server" ID="txtUserID" CssClass="txtBx01 inputFaild"/>
            </div>
            <div>
                <asp:Label Text="User Name" CssClass="lblUserName" runat="server" style="width: 20%; margin-right: 10px;"/>
                <asp:TextBox runat="server" ID="txtusername" CssClass="txtBx01 inputFaild" placeholder="Enter User Name"/>
            </div>
            <div>
                <asp:Label Text="Password" CssClass="lblPassword" runat="server" style="width: 20%; margin-right: 10px;"/>
                <asp:TextBox runat="server" ID="txtpassword" CssClass="txtBx01 inputFaild" TextMode="Password" placeholder="********" />
            </div>
            <div>
                <asp:Label Text="Verify" CssClass="lblVerify" runat="server" style="width: 20%; margin-right: 10px;"/>
                <asp:TextBox runat="server" ID="txtcpassword" CssClass="txtBx01 inputFaild" TextMode="Password" placeholder="********" />
            </div>
            <div>
                <asp:Label Text="User Role" CssClass="lblUserRole" runat="server" style="width: 20%; margin-right: 10px;"/>
                <asp:DropDownList ID="txtddl" CssClass="txtBx01" style="margin-bottom: 35px;" runat="server">
                    <asp:ListItem style="color: #123;">Admin</asp:ListItem>
                    <asp:ListItem style="color: #123;" Selected="True">User</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div>
                <div class="twoBtn">
                    <asp:Button Text="Submit" ID="btnSubmit" CssClass="GeneralButton" runat="server" OnClick="btnsubmit_Click" />
                    <asp:Button Text="Edit" CssClass="GeneralButton" ID="btnEdit" runat="server" OnClick="btnEdit_Click" />
                    <asp:Button Text="Delete" CssClass="GeneralButton" ID="btnDel" runat="server" OnClick="btnDel_Click" />
                </div>
                <div class="twoBtn">
                    <asp:Button Text="Cancel" class="btnBack" CssClass="GeneralButton" ID="btnBack" runat="server" OnClick="btnBack_Click"/>
                    <input id="Reset1" class="danger GeneralButton" type="reset" value="Reset"/>
                </div>
                <asp:Label style="padding:10px;margin-top: 15px;" Text="Customer Already Exists!!" ForeColor="Red" ID="lblErrorMessage" runat="server" CssClass="lblErrorMsg" />
                <asp:Label ID="lblSuccessMessage" Style="margin-top: 15px;" runat="server" Text="" ForeColor="Green"></asp:Label>
            </div>
        </form>
    </div>
</asp:Content>

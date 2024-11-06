<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="IT508_Project.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>IMS Login Form</title>
    <link href="Css/loginStyle.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css">
</head>
<body>
    <div class="container" id="container">
        <div class="form-container sign-up">
            <h2>IMS Login Form</h2>
        </div>
        <div class="form-container sign-in">
            <form runat="server">
                <h1>sign in</h1>
                <div class="social-icons">
                    <a href="#" class="icon"><i class="fa-brands fa-github"></i></a>
                    <a href="#" class="icon"><i class="fa-brands fa-facebook"></i></a>
                    <a href="#" class="icon"><i class="fa-brands fa-twitter"></i></a>
                    <a href="#" class="icon"><i class="fa-brands fa-telegram"></i></a>
                </div>
                <asp:TextBox  runat="server" ID="txtUsername" type="text" name="username" placeholder="enter your name" />
                <asp:TextBox runat="server" ID="txtPassword" textmode="Password" placeholder="enter your password" />
                <asp:Button Text="Login" CssClass="btnLogin" runat="server" OnClick="btnLogin_Click" />
                <asp:Label Text="Invalid. Please check your credentials, and try again." ID="lblErrorMessage" runat="server" ForeColor="Red" CssClass="lblErrorMsg" />
            </form>
        </div>
        <div class="toggle-container">
            <div class="toggle">
                <div class="toggle-panel toggle-left">
                    <h2>Welcome Back</h2>
                    <p>please fill all field</p>
                    <button class="heddin" id="Login">Sign In</button>
                </div>
                <div class="toggle-panel toggle-right">
                    <h2>Hello There</h2>
                    <p>Register for new usre</p>
                    <button class="heddin" id="register">Sign Up</button>
                </div>
            </div>
        </div>
    </div>
    <script src="Scripts/Script.js"></script>
</body>
</html>

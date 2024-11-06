<%@ Page Title="" Language="C#" MasterPageFile="~/IMS.Master" AutoEventWireup="true" CodeBehind="PosSystem.aspx.cs" Inherits="IT508_Project.PosSystem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    
      <ul>
        
           <li>
                <button type="submit" class="btn" data-target="AddNewCustomer.aspx" id="Dash"><i class="fa fa-person-skating HomeIcon"></i>  Add New Customer </button>
            </li>

            <li>
                <button type="submit" class="btn" data-target="CreateOrder.aspx"><i class="fa-solid fa-cart-shopping AddnewIcon"></i>  Create New Order </button>
            </li>

            <li>
                <button type="submit" class="btn" data-target="SaleByOneStore.aspx"><i class="fa fa-store IncomingIcon"></i> Add Item To Pos Store</button>
            </li>  
          
             <li>
                <button type="submit" class="btn" data-target="ShowAllOrders.aspx"><i class="fa-regular fa-eye IncomingIcon"></i>  Show All Orders </button>
            </li>


    </ul>



</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddItemDiscrepancy.aspx.cs" Inherits="AddItemDiscrepancy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="updateDeptHead"><h2 class="mainPageHeader">Add to Discrepancy List</h2></div>
    <br />
    <p>Item Code: <asp:Label ID="lblItemCode" runat="server" Text=""></asp:Label></p>
    <p>Item Name: <asp:Label ID="lblItemName" runat="server" Text=""></asp:Label></p>
    <p>Unit of Measure: <asp:Label ID="lblUom" runat="server" Text=""></asp:Label></p>
    <p>Quantity in Stock: <asp:Label ID="lblStock" runat="server" Text=""></asp:Label></p>
    <p>Adjustment Amount: <asp:TextBox ID="txtAdj" runat="server"></asp:TextBox></p>
    <br /> <br />
    <asp:Button ID="Button1" runat="server" Text="Add Item" CssClass="button" OnClick="Button1_Click" />

&nbsp;&nbsp; 
    <asp:Label ID="Label1" runat="server" ForeColor="Red" Text=""></asp:Label>
</asp:Content>


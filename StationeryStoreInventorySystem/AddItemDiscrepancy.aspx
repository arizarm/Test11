<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddItemDiscrepancy.aspx.cs" Inherits="AddItemDiscrepancy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2>Add to Discrepancy List</h2>
    Item Code: <asp:Label ID="lblItemCode" runat="server" Text=""></asp:Label>
    <br />
    Item Name: <asp:Label ID="lblItemName" runat="server" Text=""></asp:Label>
    <br />
    Unit of Measure: <asp:Label ID="lblUom" runat="server" Text=""></asp:Label>
    <br />
    Quantity in Stock: <asp:Label ID="lblStock" runat="server" Text=""></asp:Label>
    <br />
    Adjustment Amount: <asp:TextBox ID="txtAdj" runat="server"></asp:TextBox>
    <br /> <br />
    <asp:Button ID="Button1" runat="server" Text="Add Item" CssClass="button" OnClick="Button1_Click" />

&nbsp;&nbsp; 
    <asp:Label ID="Label1" runat="server" ForeColor="Red" Text=""></asp:Label>
</asp:Content>


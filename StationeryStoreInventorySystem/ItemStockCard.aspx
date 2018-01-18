<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ItemStockCard.aspx.cs" Inherits="ItemStockCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div style ="text-align: center">
   <h2 class="mainPageHeader">Stock Card</h2></div>
    <br />
    <br />
    <asp:Label ID="Label2" runat="server" Text="Item Code: P085" Font-Size="Medium" ></asp:Label><br />
    <asp:Label ID="Label3" runat="server" Text="Item Description: PENCIL 2B, Eraser end" Font-Size="Medium"></asp:Label><br />
    <asp:Label ID="Label4" runat="server" Text="Bin#: A7" Font-Size="Medium"></asp:Label><br />
    <asp:Label ID="Label5" runat="server" Text="UOM: Box" Font-Size="Medium"></asp:Label><br />
    <asp:Label ID="Label6" runat="server" Text="1st Supplier: BANES" Font-Size="Medium"></asp:Label><br />
    <asp:Label ID="Label7" runat="server" Text="2nd Supplier: CHEP" Font-Size="Medium"></asp:Label><br />
    <asp:Label ID="Label8" runat="server" Text="3rd Supplier: ALPHA" Font-Size="Medium" ></asp:Label><br />
    <br />
    <table border="1">
        <tr>
            <td style="width:10%">Transaction Date</td>
            <td style="width:10%">Transaction Details</td>
            <td style="width:10%">Quantity</td>
            <td style="width:10%">Balance</td>
        </tr>
        <tr aria-readonly="true">
            <td>02/01/2000</td>
            <td>Supplier - BANE</td>
            <td>+ 500</td>
            <td>550</td>
        </tr>
        <tr>
            <td>03/01/2000</td>
            <td>English Department</td>
            <td>- 20</td>
            <td>530</td>
        </tr>
        <tr>
            <td>08/01/2000</td>
            <td>Stock Adjustment 001/001/2000</td>
            <td>ADJ + 4</td>
            <td>534</td>
        </tr>
        <tr>
            <td>09/01/2000</td>
            <td>Electronic/Electric Engineering Department</td>
            <td>- 30</td>
            <td>504</td>
        </tr>
        <tr>
            <td>09/01/2000</td>
            <td>Admninistration Department.</td>
            <td>- 50</td>
            <td>454</td>
        </tr>
        <tr>
            <td>14/01/2000</td>
            <td>Supplier - BANE</td>
            <td>+ 500</td>
            <td>954</td>
       
    </table>
    <br />
     <br />
    
</asp:Content>


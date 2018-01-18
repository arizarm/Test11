<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReorderReport.aspx.cs" Inherits="ReorderReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="Content/JavaScript.js"></script> 
    <h2 class="mainPageHeader">
        Reorder Report

    </h2>
    Start Date: <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
    <br />
    End Date:&nbsp; <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
     <br />
    <asp:Button ID="Button1" runat="server" Text="Generate Report" CssClass="button" />
    <br />
    <h4>Reorder Report from... to... </h4>
    <br />
     <asp:Table ID="Table1" runat="server" Width="591px">
        <asp:TableHeaderRow>
            <asp:TableHeaderCell ColumnSpan="8" Font-Bold="true" Font-Underline="true">The following items have fallen below re-order level</asp:TableHeaderCell>
        </asp:TableHeaderRow>
        <asp:TableHeaderRow HorizontalAlign="Left" BorderStyle="Solid" BorderColor="Black">
            <asp:TableHeaderCell>S/N</asp:TableHeaderCell>
            <asp:TableHeaderCell>Item Code</asp:TableHeaderCell> 
            <asp:TableHeaderCell>Description</asp:TableHeaderCell>
            <asp:TableHeaderCell>Quantity on hand</asp:TableHeaderCell>
            <asp:TableHeaderCell>Re-order Level</asp:TableHeaderCell>
            <asp:TableHeaderCell>Re-order Quantity</asp:TableHeaderCell>
            <asp:TableHeaderCell>Purchase Order</asp:TableHeaderCell>
            <asp:TableHeaderCell>Expected Delivery</asp:TableHeaderCell>
        </asp:TableHeaderRow>

        <asp:TableRow>
            <asp:TableCell>1</asp:TableCell>
            <asp:TableCell>P040</asp:TableCell>
            <asp:TableCell>Pen WhiteBoard Marker Green</asp:TableCell>
            <asp:TableCell>98</asp:TableCell>
            <asp:TableCell>100</asp:TableCell>
            <asp:TableCell>50</asp:TableCell>
            <asp:TableCell>01-1234/A</asp:TableCell>
            <asp:TableCell>15/1/2018</asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell>2</asp:TableCell>
            <asp:TableCell>T001</asp:TableCell>
            <asp:TableCell>Thumb Tacks Large</asp:TableCell>
            <asp:TableCell>8</asp:TableCell>
            <asp:TableCell>10</asp:TableCell>
            <asp:TableCell>10</asp:TableCell>
            <asp:TableCell>01-1234/A</asp:TableCell>
            <asp:TableCell>15/1/2018</asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell>3</asp:TableCell>
            <asp:TableCell>P040</asp:TableCell>
            <asp:TableCell>Folder Plastic Blue</asp:TableCell>
            <asp:TableCell>180</asp:TableCell>
            <asp:TableCell>150</asp:TableCell>
            <asp:TableCell>50</asp:TableCell>
            <asp:TableCell>01-1234/A</asp:TableCell>
            <asp:TableCell>15/1/2018</asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell>4</asp:TableCell>
            <asp:TableCell>F004</asp:TableCell>
            <asp:TableCell>Folder Plastic Yellow</asp:TableCell>
            <asp:TableCell>90</asp:TableCell>
            <asp:TableCell>100</asp:TableCell>
            <asp:TableCell>50</asp:TableCell>
            <asp:TableCell>01-1234/A</asp:TableCell>
            <asp:TableCell>15/1/2018</asp:TableCell>
        </asp:TableRow>

     </asp:Table>

</asp:Content>


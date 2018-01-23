<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReorderReport.aspx.cs" Inherits="ReorderReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
    <link type="text/css" href="css/smoothness/jquery-ui-1.7.1.custom.css" rel="stylesheet" />
    <script src="_scripts/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="_scripts/jquery-ui-1.7.1.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript">
     $(document).ready(function () {
         $('#txtDate').datepicker({ minDate: -2, maxDate: -0 });
         $('#txtEDate').datepicker({ minDate: -2});
     });
    </script>    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--<script src="Content/JavaScript.js"></script> --%>
    <h2 class="mainPageHeader">
        Reorder Report

    </h2>
    Start Date: <asp:TextBox ID="txtDate" CssClass ="datepicker" runat="server" Text=""></asp:TextBox>
    <br />
    <br />
    End Date:&nbsp; <asp:TextBox ID="txtEDate"  CssClass ="datepicker" runat="server" Text=""></asp:TextBox>
     <br />
    <asp:Button ID="GenerateBtn" runat="server" Text="Generate Report" CssClass="button" OnClick="GenerateBtn_Click" />
    <br />
    <h4>Reorder Report between <asp:Label ID="sDate" runat="server" Text=""></asp:Label> -<asp:Label ID="eDate"  runat="server" Text=""></asp:Label> </h4>
    <br />
    
        <asp:Label runat ="server" Text="" ForeColor="Red" ID="txtLbl"></asp:Label>
        <br />
        <br />
    <asp:GridView runat="server" AutoGenerateColumns ="False" ID="gvPurchasedreoderItem" EmptyDataText="No records founds within the dates !!" EmptyDataRowStyle-BackColor="Window" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal">
             <Columns>                 
                 <asp:BoundField HeaderText="ItemCode"  DataField="ItemCode">
                 <HeaderStyle Height="35px" Width="55px" />
                 </asp:BoundField>
                 <asp:BoundField HeaderText="Description" DataField="Description">
                 <HeaderStyle Height="35px" Width="75px" />
                 </asp:BoundField>
                 <asp:BoundField HeaderText="QuantityOnHand" DataField="Balance">
                 <HeaderStyle Width="55px" />
                 </asp:BoundField>
                 <asp:BoundField HeaderText="ReorderLevel" DataField="ReorderLevel">
                 <HeaderStyle Width="55px" />
                 </asp:BoundField>
                 <asp:BoundField HeaderText="ReorderQuantity" DataField="ReorderQuantity">
                 <HeaderStyle Width="55px" />
                 </asp:BoundField>
                 <asp:BoundField HeaderText="PurchaseOrderNo" DataField="PurchaseOrderNo">
                 <HeaderStyle Width="55px" />
                 </asp:BoundField>
                 <asp:BoundField HeaderText="ExpectedDate" DataField="FormattedExpectedDate">
                 <HeaderStyle Width="65px" />
                 </asp:BoundField>
             </Columns>

             <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
             <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
             <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
             <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
             <SortedAscendingCellStyle BackColor="#F7F7F7" />
             <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
             <SortedDescendingCellStyle BackColor="#E5E5E5" />
             <SortedDescendingHeaderStyle BackColor="#242121" />

         </asp:GridView>
         <asp:GridView runat="server" AutoGenerateColumns ="False" ID="gvShortfallItems" EmptyDataText="No records founds within the dates !!" EmptyDataRowStyle-BackColor="Window" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal">
             <Columns>                 
                 <asp:BoundField HeaderText="ItemCode"  DataField="ItemCode">
                 <HeaderStyle Height="35px" Width="55px" />
                 </asp:BoundField>
                 <asp:BoundField HeaderText="Description" DataField="Description">
                 <HeaderStyle Height="35px" Width="75px" />
                 </asp:BoundField>
                 <asp:BoundField HeaderText="QuantityOnHand" DataField="Balance">
                 <HeaderStyle Width="55px" />
                 </asp:BoundField>
                 <asp:BoundField HeaderText="ReorderLevel" DataField="ReorderLevel">
                 <HeaderStyle Width="55px" />
                 </asp:BoundField>
                 <asp:BoundField HeaderText="ReorderQuantity" DataField="ReorderQuantity">
                 <HeaderStyle Width="55px" />
                 </asp:BoundField>
                 <asp:BoundField HeaderText="PurchaseOrderNo" DataField="NullablePurchaseOrderNo">
                 <HeaderStyle Width="55px" />
                 </asp:BoundField>
                 <asp:BoundField HeaderText="ExpectedDate" DataField="FormattedExpectedDate">
                 <HeaderStyle Width="65px" />
                 </asp:BoundField>
             </Columns>

             <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
             <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
             <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
             <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
             <SortedAscendingCellStyle BackColor="#F7F7F7" />
             <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
             <SortedDescendingCellStyle BackColor="#E5E5E5" />
             <SortedDescendingHeaderStyle BackColor="#242121" />

         </asp:GridView>

</asp:Content>


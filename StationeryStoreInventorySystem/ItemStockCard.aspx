<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ItemStockCard.aspx.cs" Inherits="ItemStockCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div style ="text-align: center">
   <h2 class="mainPageHeader">Stock Card</h2></div>
    <br />
    <br />
    Item Code: <asp:Label ID="lblItemCode" runat="server" Text="" Font-Size="Medium" ></asp:Label><br />
    Item Name: <asp:Label ID="lblItemName" runat="server" Text="" Font-Size="Medium"></asp:Label><br />
    Bin: <asp:Label ID="lblBin" runat="server" Text="" Font-Size="Medium"></asp:Label><br />
    Unit of Measure: <asp:Label ID="lblUom" runat="server" Text="" Font-Size="Medium"></asp:Label><br />
    1st Supplier: <asp:Label ID="lblSupp1" runat="server" Text="" Font-Size="Medium"></asp:Label><br />
    2nd Supplier: <asp:Label ID="lblSupp2" runat="server" Text="" Font-Size="Medium"></asp:Label><br />
    3rd Supplier: <asp:Label ID="lblSupp3" runat="server" Text="" Font-Size="Medium" ></asp:Label>
    <br />
    <%--
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
       
    </table>--%>
    <br />
     <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:TemplateField HeaderText="Transaction Date">
                <ItemTemplate>
                    <asp:Label ID="lblDate" runat="server" Text='<%# Bind("TransDate") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Transaction Details">
                <ItemTemplate>
                    <asp:Label ID="lblDetails" runat="server" Text='<%# Bind("TransDetails") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Quantity">
                <ItemTemplate>
                    <asp:Label ID="lblQty" runat="server" Text='<%# Bind("Quantity") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Balance">
                <ItemTemplate>
                    <asp:Label ID="lblBalance" runat="server" Text='<%# Bind("Balance") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    
</asp:Content>


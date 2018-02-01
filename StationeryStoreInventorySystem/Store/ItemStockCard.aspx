<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ItemStockCard.aspx.cs" Inherits="ItemStockCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="updateDeptHead"><h2 class="mainPageHeader">Stock Card</h2></div>
    <br />
    <br />
    <p>Item Code: <asp:Label ID="lblItemCode" runat="server" Text="" Font-Size="Medium" ></asp:Label></p>
    <p>Item Name: <asp:Label ID="lblItemName" runat="server" Text="" Font-Size="Medium"></asp:Label></p>
    <p>Bin: <asp:Label ID="lblBin" runat="server" Text="" Font-Size="Medium"></asp:Label></p>
    <p>Unit of Measure: <asp:Label ID="lblUom" runat="server" Text="" Font-Size="Medium"></asp:Label></p>
    <p>1st Supplier: <asp:Label ID="lblSupp1" runat="server" Text="" Font-Size="Medium"></asp:Label></p>
    <p>2nd Supplier: <asp:Label ID="lblSupp2" runat="server" Text="" Font-Size="Medium"></asp:Label></p>
    <p>3rd Supplier: <asp:Label ID="lblSupp3" runat="server" Text="" Font-Size="Medium" ></asp:Label></p>
    <br />
    <br />
     <br />
    <%if (GridView1.Rows.Count > 0)
        { %>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:TemplateField HeaderText="Transaction Date">
                <ItemTemplate>
                    <asp:Label ID="lblDate" runat="server" Text='<%# Bind("TransDate","{0:dddd, dd MMMM yyyy}") %>'></asp:Label>
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
    <%}
    else
    { %>
    <h4>No transaction history found</h4>
    <%} %>
</asp:Content>


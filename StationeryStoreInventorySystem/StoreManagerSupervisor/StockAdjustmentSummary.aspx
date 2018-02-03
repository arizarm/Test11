<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StockAdjustmentSummary.aspx.cs" Inherits="StockAdjustmentSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="updateDeptHead"><h2 class="mainPageHeader">Stock Adjustment Approval Summary</h2></div>
    <br />
    <asp:GridView ID="gvActionSummary" runat="server" AutoGenerateColumns="False" OnRowDataBound="GvActionSummary_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="Discrepancy ID" Visible="False">
                <ItemTemplate>
                    <asp:Label ID="lblDiscID" runat="server" Text='<%# Bind("Key.Key.DiscrepencyID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Item Code">
                <ItemTemplate>
                    <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("Key.Value.ItemCode") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Item Name">
                <ItemTemplate>
                    <asp:Label ID="lblItemName" runat="server" Text='<%# Bind("Key.Value.Description") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Adjustment">
                <ItemTemplate>
                    <asp:Label ID="lblAdj" runat="server" Text='<%# Bind("Key.Key.AdjustmentQty") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Unit of Measure">
                <ItemTemplate>
                    <asp:Label ID="lblUom" runat="server" Text='<%# Bind("Key.Value.UnitOfMeasure") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Total Discrepancy Amount ($)">
                <ItemTemplate>
                    <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("Key.Key.TotalDiscrepencyAmount") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="150px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Reason">
                <ItemTemplate>
                    <asp:Label ID="lblReason" runat="server" Text='<%# Bind("Key.Key.Remarks") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                    <asp:Label ID="lblAction" runat="server" Text='<%# Bind("Value") %>' Font-Bold="true"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <br /><br />
    <asp:Button ID="btnProcess" runat="server" Text="Process Discrepancies" CssClass="button" OnClick="BtnProcess_Click"/>
</asp:Content>


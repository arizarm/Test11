<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StockAdjustment.aspx.cs" Inherits="StockAdjustment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div class="updateDeptHead"><h2 class="mainPageHeader">Stock Adjustment Approval</h2></div>
    <br />
    <%if (gvMonthly.Rows.Count > 0)
        { %>
    <h3>Monthly Inventory Check Discrepancies</h3>
    <asp:GridView ID="gvMonthly" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
        <Columns>
            <asp:TemplateField HeaderText="Discrepancy ID" Visible="False">
                <ItemTemplate>
                    <asp:Label ID="lblDiscID" runat="server" Text='<%# Bind("Key.DiscrepencyID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:RadioButtonList ID="rblAction" runat="server" Width="100%" Height="100%">
                        <asp:ListItem Text="Approve"></asp:ListItem>
                        <asp:ListItem Text="Reject"></asp:ListItem>
                    </asp:RadioButtonList>
                </ItemTemplate>

<ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Item Code">
                <ItemTemplate>
                    <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("Value.ItemCode") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="90px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Item Name">
                <ItemTemplate>
                    <asp:Label ID="lblItemName" runat="server" Text='<%# Bind("Value.Description") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="175px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Adjustment">
                <ItemTemplate>
                    <asp:Label ID="lblAdj" runat="server" Text='<%# Bind("Key.AdjustmentQty") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="90px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Unit of Measure">
                <ItemTemplate>
                    <asp:Label ID="lblUom" runat="server" Text='<%# Bind("Value.UnitOfMeasure") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="90px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Total Discrepancy Amount ($)">
                <ItemTemplate>
                    <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("Key.TotalDiscrepencyAmount") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="150px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Reason">
                <ItemTemplate>
                    <asp:Label ID="lblReason" runat="server" Text='<%# Bind("Key.Remarks") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="400px" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <%} %>
    <%if (gvPending.Rows.Count > 0)
        { %>
    <h3>Discrepancies Pending Approval</h3>
    <asp:GridView ID="gvPending" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvPending_RowDataBound" CssClass="mGrid">
        <Columns>
            <asp:TemplateField HeaderText="Discrepancy ID" Visible="False">
                <ItemTemplate>
                    <asp:Label ID="lblDiscID" runat="server" Text='<%# Bind("Key.DiscrepencyID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:RadioButtonList ID="rblAction" runat="server" Width="100%" Height="100%">
                        <asp:ListItem Text="Approve"></asp:ListItem>
                        <asp:ListItem Text="Reject"></asp:ListItem>
                    </asp:RadioButtonList>
                </ItemTemplate>

<ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Item Code">
                <ItemTemplate>
                    <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("Value.ItemCode") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="90px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Item Name">
                <ItemTemplate>
                    <asp:Label ID="lblItemName" runat="server" Text='<%# Bind("Value.Description") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="175px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Adjustment">
                <ItemTemplate>
                    <asp:Label ID="lblAdj" runat="server" Text='<%# Bind("Key.AdjustmentQty") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="90px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Unit of Measure">
                <ItemTemplate>
                    <asp:Label ID="lblUom" runat="server" Text='<%# Bind("Value.UnitOfMeasure") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="90px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Total Discrepancy Amount ($)">
                <ItemTemplate>
                    <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("Key.TotalDiscrepencyAmount") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" Width="150px" Wrap="True" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Reason">
                <ItemTemplate>
                    <asp:Label ID="lblReason" runat="server" Text='<%# Bind("Key.Remarks") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="400px" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <br />
    <asp:Button ID="btnProcess" runat="server" Text="Process Pending Discrepancies" CssClass="button" OnClick="btnProcess_Click"/>
    &nbsp&nbsp
    <asp:Button ID="btnClear" runat="server" Text="Clear Selections" CssClass="rejectBtn" OnClick="btnClear_Click"/>
    <%} %>
    </asp:Content>


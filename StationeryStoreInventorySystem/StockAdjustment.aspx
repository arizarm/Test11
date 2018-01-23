<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StockAdjustment.aspx.cs" Inherits="StockAdjustment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--<h1 class="mainPageHeader">Adjustment Voucher</h1>
    <p>&nbsp;</p>
    <p>
        Voucher : A192746 <br />
         Date issued : <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label><br />
        Requested By : John
    </p>
    <p>
        &nbsp;</p>
    <p>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
            <Columns>
                 <asp:TemplateField HeaderText="Item Code">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1"  runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1"  Text='<%# Bind("itemCode") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Item Description">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1"  runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1"  Text='<%# Bind("itemCode") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quantity Adjusted">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" Text='<%# Bind("quantity") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reason">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" Text='<%# Bind("reason") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                
            </Columns>
        </asp:GridView>
    </p>
    <p>
        <asp:Button ID="Button1" runat="server" Text="Issue Ticket" CssClass="button" />
    </p>--%>

    <h1 class="mainPageHeader">Stock Adjustment Approval</h1>
    <br />
    <%if (GridView1.Rows.Count > 0)
        { %>
    <h4>Monthly Inventory Check Discrepancies</h4>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:TemplateField HeaderText="Discrepancy ID" Visible="False">
                <ItemTemplate>
                    <asp:Label ID="lblDiscID" runat="server" Text='<%# Bind("Key.DiscrepencyID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server">
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
    <br />
    <asp:Button ID="Button1" runat="server" Text="Process Monthly Discrepancies" CssClass="button" OnClick="Button1_Click"/>
    <br />
    <br />
    <%} %>
    <%if (GridView2.Rows.Count > 0)
        { %>
    <h4>Discrepancies Pending Approval</h4>
    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView2_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="Discrepancy ID" Visible="False">
                <ItemTemplate>
                    <asp:Label ID="lblDiscID" runat="server" Text='<%# Bind("Key.DiscrepencyID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server">
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
    <asp:Button ID="Button2" runat="server" Text="Process Pending Discrepancies" CssClass="button" OnClick="Button2_Click"/>
    <%} %>
    </asp:Content>


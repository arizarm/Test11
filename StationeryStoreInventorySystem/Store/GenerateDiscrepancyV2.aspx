<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="GenerateDiscrepancyV2.aspx.cs" Inherits="GenerateDiscrepancyV2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <div class="updateDeptHead"><h2 class="mainPageHeader">Report Discrepancies</h2></div>


        <% if (gvDiscrepancyList.Rows.Count > 0)
            {%>
        
        <h3>Discrepancy List</h3>
        <asp:GridView ID="gvDiscrepancyList" runat="server" AutoGenerateColumns="False" CssClass="mGrid mGrid60percent" EmptyDataText ="Nothing added yet">
            <Columns>
                <asp:TemplateField HeaderText="Item Code">
                    <ItemTemplate>
                        <asp:Label ID="lblItemCodeDisc" runat="server" Text='<%# Bind("Key.ItemCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Item Name">
                    <ItemTemplate>
                        <asp:Label ID="lblDesc" runat="server" Text='<%# Bind("Key.Description") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quantity in Stock" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblStockDisc" runat="server" Text='<%# Bind("Key.BalanceQty") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Unit of Measure">
                    <ItemTemplate>
                        <asp:Label ID="lblUomDisc" runat="server" Text='<%# Bind("Key.UnitOfMeasure") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Actual Quantity / Adjustment Quantity" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblActual" runat="server" Text='<%# Bind("Value") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Button ID="btnFinalise" runat="server" Text="Finalise Discrepancy List"  CssClass="button" OnClick="BtnFinalise_Click"/>
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnClearList" runat="server" Text="Clear List" CssClass="rejectBtn" OnClick="BtnClearList_Click"/>
        <% } %>

        <%if (lblErrorMissedItems.Text != "")
            { %>
        <br />
        <br />
        <asp:Label ID="lblErrorFinalise" runat="server" Text="" CssClass="errorfont"></asp:Label>
        <asp:Label ID="lblErrorMissed" runat="server" Text="" CssClass="errorfont"></asp:Label>
        <asp:Label ID="lblErrorBase" runat="server" Text="" CssClass="errorfont"></asp:Label>
        <br />
        <asp:Label ID="lblErrorMissedItems" runat="server" Text="" CssClass="errorfont"></asp:Label>
        <br />
        <br />
        <%} %>

        <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch">
        <br />
        <h3>Search by Item Code or Name</h3>
        <asp:TextBox ID="txtSearch" runat="server" Width="212px" ValidationGroup="Search"></asp:TextBox>&nbsp;
        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="BtnSearch_Click" ValidationGroup="Search" CssClass="button"/>&nbsp;
        <asp:Button ID="btnDisplayAll" runat="server" Text="Display All" OnClick="BtnDisplayAll_Click" CssClass="button"/>
            <asp:RequiredFieldValidator ID="reqSearch" runat="server" ErrorMessage="" ControlToValidate="txtSearch" ValidationGroup="Search"></asp:RequiredFieldValidator>


        <% if (gvItemList.Rows.Count > 0)
            {%>
        <h3>Item List</h3>
            <%} %>
        <asp:GridView ID="gvItemList" runat="server" AutoGenerateColumns="False" CssClass="mGrid" EmptyDataText="No items matching your search found">
            <Columns>
                
                <asp:TemplateField HeaderText="Item Code">
                    <ItemTemplate>
                        <asp:Label ID="lblItemCodeItem" runat="server" Text='<%# Bind("Key.ItemCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Item Name">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlkDesc" runat="server" NavigateUrl="" Text='<%# Bind("Key.Description") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Unit of Measure">
                    <ItemTemplate>
                        <asp:Label ID="lblUomItem" runat="server" Text='<%# Bind("Key.UnitOfMeasure") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                    <asp:TemplateField HeaderText="Qty in stock (excluding pending discrepancies)" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblStockItem" runat="server" Text='<%# Bind("Key.BalanceQty") %>'></asp:Label>
                    </ItemTemplate>
<ItemStyle HorizontalAlign="Center" Width="160px"></ItemStyle>
                </asp:TemplateField>
               
                <asp:TemplateField HeaderText="Pending Adjustments"  ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblPendingAdj" runat="server" Text='<%# Bind("Value") %>'></asp:Label>
                    </ItemTemplate>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
               
                <asp:TemplateField HeaderText="Amount Correct" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:CheckBox ID="cbxCorrect" runat="server" />
                    </ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Actual Quantity">
                    <ItemTemplate>
                        <asp:TextBox ID="txtActual" runat="server" Width="100%"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                
            </Columns>

        </asp:GridView>
            <% if (gvItemList.Rows.Count > 0)
            {%>
        <br />
        <asp:Button ID="btnGenerateDiscrepancy" runat="server" Text="Generate Discrepancy List" OnClick="BtnGenerateDiscrepancy_Click" CssClass="button" />
            <%-- Check and uncheck all buttons only for testing --%>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         <asp:Button ID="btnCheckAll" runat="server" Text="Check All" OnClick="BtnCheckAll_Click" CssClass="button"/>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         <asp:Button ID="btnUncheckAll" runat="server" Text="Uncheck All" CssClass="button" OnClick="BtnUncheckAll_Click"/>
            
        <%} %>

        </asp:Panel>
       
    </div>
</asp:Content>


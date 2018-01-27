<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="GenerateDiscrepancyV2.aspx.cs" Inherits="GenerateDiscrepancyV2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <h2 class="mainPageHeader">Report Discrepancy</h2>
        <%--<br />
        Selection Type: 
        <asp:RadioButtonList ID="RadioButtonList1" runat="server">
            <asp:ListItem Selected="True">Monthly Inventory Check</asp:ListItem>
            <asp:ListItem>Adhoc</asp:ListItem>
        </asp:RadioButtonList> --%>
        <% if (GridView2.Rows.Count > 0)
           {%>
        
        <h2>Discrepancy List</h2>
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
            <Columns>
                <asp:TemplateField HeaderText="Item Code">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode2" runat="server" Text='<%# Bind("Key.ItemCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Item Name">
                    <ItemTemplate>
                        <asp:Label ID="lblDesc" runat="server" Text='<%# Bind("Key.Description") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quantity in Stock" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblStock" runat="server" Text='<%# Bind("Key.BalanceQty") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Unit of Measure">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblUom" runat="server" Text='<%# Bind("Key.UnitOfMeasure") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Actual Quantity / Adjustment Quantity" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblActual" runat="server" Text='<%# Bind("Value") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
        <asp:Button ID="Button2" runat="server" Text="Finalise Discrepancy List"  CssClass="button" OnClick="Button2_Click"/>
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button6" runat="server" Text="Clear List" CssClass="button" OnClick="Button6_Click"/>
        <% } %>

        <%if (Label8.Text != "")
            { %>
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red"></asp:Label>
        <asp:Label ID="Label5" runat="server" Text="" ForeColor="Red"></asp:Label>
        <asp:Label ID="Label7" runat="server" Text="" ForeColor="Red"></asp:Label>
        <br />
        <asp:Label ID="Label8" runat="server" Text="" ForeColor="Red"></asp:Label>
        <br />
        <br />
        <%} %>

        <asp:Panel ID="Panel1" runat="server" DefaultButton="Button4">

        <h4>Search by Item Code or Name</h4>
        <br />
        <asp:TextBox ID="txtSearch" runat="server" Width="212px"></asp:TextBox>&nbsp;
        <asp:Button ID="Button4" runat="server" Text="Search" OnClick="Button4_Click" />&nbsp;
        <asp:Button ID="Button5" runat="server" Text="Display All" OnClick="Button5_Click" />


        <% if (GridView1.Rows.Count > 0)
           {%>
        <h2>Item List</h2>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="mGrid" >
            <Columns>
                
                <asp:TemplateField HeaderText="Item Code">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode1" runat="server" Text='<%# Bind("Key.ItemCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Item Name">
                    <ItemTemplate>
                        <asp:HyperLink ID="lnkItem" runat="server" NavigateUrl="" Text='<%# Bind("Key.Description") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Unit of Measure">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblUom" runat="server" Text='<%# Bind("Key.UnitOfMeasure") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                    <asp:TemplateField HeaderText="Qty in stock (excluding pending discrepancies)" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblStock" runat="server" Text='<%# Bind("Key.BalanceQty") %>'></asp:Label>
                    </ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
               
                <asp:TemplateField HeaderText="Pending Adjustments"  ItemStyle-HorizontalAlign="Center">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("Value") %>'></asp:Label>
                    </ItemTemplate>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
               
                <asp:TemplateField HeaderText="Amount Correct" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                    <EditItemTemplate>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Actual Quantity">
                    <EditItemTemplate>
                        
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:TextBox ID="txtActual" runat="server" Width=""></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                
            </Columns>

        </asp:GridView>
        <br />
        <asp:Button ID="Button1" runat="server" Text="Generate Discrepancy List" OnClick="Button1_Click" CssClass="button" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button3" runat="server" Text="Check All" OnClick="Button3_Click" CssClass="button"/>
        <%} %>

        </asp:Panel>
       
    </div>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="GenerateDiscrepancyV2.aspx.cs" Inherits="GenerateDiscrepancyV2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <h2 class="mainPageHeader">Generate Discrepancy CheckList</h2>
        <br />
        Selection Type: 
        <asp:RadioButtonList ID="RadioButtonList1" runat="server">
            <asp:ListItem Selected="True">Monthly Inventory Check</asp:ListItem>
            <asp:ListItem>Adhoc</asp:ListItem>
        </asp:RadioButtonList>
        <% if (GridView2.Rows.Count > 0)
           {%>
        
        <h2>Discrepancy List</h2>
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField HeaderText="Item Code">
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode2" runat="server" Text='<%# Bind("Key.I.ItemCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Item Name">
                    <ItemTemplate>
                        <asp:Label ID="lblDesc" runat="server" Text='<%# Bind("Key.I.Description") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quantity in Stock" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblStock" runat="server" Text='<%# Bind("Key.Stock") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Unit of Measure">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblUom" runat="server" Text='<%# Bind("Key.I.UnitOfMeasure") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Adjustment Amount" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblAdj" runat="server" Text='<%# Bind("Value") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
        <asp:Button ID="Button2" runat="server" Text="Submit Discrepancy List"  CssClass="button" OnClick="Button2_Click"/>
        <br />
        <br />
        <asp:Label ID="Label5" runat="server" Text="" ForeColor="Red"></asp:Label>
        <asp:Label ID="Label7" runat="server" Text="" ForeColor="Red"></asp:Label>
        <br />
        <asp:Label ID="Label8" runat="server" Text="" ForeColor="Red"></asp:Label>
        <% } %>
        <h2>Item List</h2>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="mGrid" >
            <Columns>
                
                <asp:TemplateField HeaderText="Item Description">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode1" runat="server" Text='<%# Bind("I.ItemCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Item Name">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("I.Description") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Unit of Measure">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("I.UnitOfMeasure") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                    <asp:TemplateField HeaderText="Quantity in stock"  ItemStyle-HorizontalAlign="Center">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("Stock") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
               
                <asp:TemplateField HeaderText="Amount Correct" ItemStyle-HorizontalAlign="Center">
                    <EditItemTemplate>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Actual Quantity">
                    <EditItemTemplate>
                        
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:TextBox ID="txtActual" runat="server"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Remarks">
                    <EditItemTemplate>
                        
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:TextBox ID="txtRemarks" runat="server"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                
            </Columns>

        </asp:GridView>
        <br />
        &nbsp;<asp:Button ID="Button1" runat="server" Text="Generate Discrepancy List" OnClick="Button1_Click" CssClass="button" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button3" runat="server" Text="Check All" OnClick="Button3_Click" CssClass="button"/>
        
       
    </div>
</asp:Content>


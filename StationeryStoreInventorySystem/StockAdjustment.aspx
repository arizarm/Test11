<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StockAdjustment.aspx.cs" Inherits="StockAdjustment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <h1 class="mainPageHeader">Adjustment Voucher</h1>
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
    </p>
    </asp:Content>


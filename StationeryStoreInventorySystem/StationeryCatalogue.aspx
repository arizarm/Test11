<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StationeryCatalogue.aspx.cs" Inherits="StationeryCatalogue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
        <h2 class="mainPageHeader">Logic University Stationery Catalogue</h2>
        <table>
            <tr>
                <td>Item Number:</td>
                <td> <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox> <br /> <br />
                </td>
            </tr>
            <tr>
                <td>Category:</td>
                <td>
                    <asp:DropDownList ID="DropDownList2" runat="server">
                        <asp:ListItem>Others</asp:ListItem>
                        <asp:ListItem>Clip</asp:ListItem>
                        <asp:ListItem>File</asp:ListItem>
                    </asp:DropDownList><br />
                    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox><br /><br />
                </td>
            </tr>
            <tr>
                <td>Item Description : </td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox><br /><br /></td>
            </tr>
            <tr>
                <td>Unit of Measure:</td>
                <td><asp:DropDownList ID="DropDownList1" runat="server">
                        <asp:ListItem>Others</asp:ListItem>
                        <asp:ListItem>Box</asp:ListItem>
                        <asp:ListItem>Each</asp:ListItem>
                    </asp:DropDownList><br />
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox><br /><br /></td>
            </tr>
            <tr>
                <td>Reorder Level : </td>
                <td><asp:TextBox ID="TextBox5" runat="server"></asp:TextBox><br /><br />
                    </td>
            </tr>
            <tr>
                <td>Reorder Quantity : </td>
                <td><asp:TextBox ID="TextBox11" runat="server"></asp:TextBox>
                    </td>
            </tr>
        </table>


        <asp:Button ID="Button1" runat="server" Text="Add" CssClass="button" />
        <br />
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField HeaderText="Item Number">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("itemNumber") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Category">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("category") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("description") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reorder Level">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("reorderLevel") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reorder Qty">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("reorderQty") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Unit Of Measure">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("unitOfMeasure") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <EditItemTemplate>
                        <asp:Button ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:Button>
                        &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Button ID="LinkButton3" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit" CssClass="button" ></asp:Button>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:Button ID="LinkButton4" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" CssClass="rejectBtn" ></asp:Button>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" EnableEventValidation="true" CodeFile="StationeryCatalogue.aspx.cs" Inherits="StationeryCatalogue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <h2 class="mainPageHeader">Logic University Stationery Catalogue</h2>
    <table>
        <tr>
            <td>Item Number:</td>
            <td>
                <asp:textbox id="TextBox1" runat="server"></asp:textbox>
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td>Category:</td>
            <td>
                <asp:dropdownlist id="DropDownList2" runat="server">
                        <asp:ListItem>Others</asp:ListItem>
                        <asp:ListItem>Clip</asp:ListItem>
                        <asp:ListItem>File</asp:ListItem>
                    </asp:dropdownlist>
                <br />
                <asp:textbox id="TextBox4" runat="server"></asp:textbox>
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td>Item Description : </td>
            <td>
                <asp:textbox id="TextBox2" runat="server"></asp:textbox>
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td>Unit of Measure:</td>
            <td>
                <asp:dropdownlist id="DropDownList1" runat="server">
                        <asp:ListItem>Others</asp:ListItem>
                        <asp:ListItem>Box</asp:ListItem>
                        <asp:ListItem>Each</asp:ListItem>
                    </asp:dropdownlist>
                <br />
                <asp:textbox id="TextBox3" runat="server"></asp:textbox>
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td>Reorder Level : </td>
            <td>
                <asp:textbox id="TextBox5" runat="server"></asp:textbox>
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td>Reorder Quantity : </td>
            <td>
                <asp:textbox id="TextBox11" runat="server"></asp:textbox>
            </td>
        </tr>
    </table>


    <asp:button id="Button1" runat="server" text="Add" cssclass="button" />
    <br />
    <br />
    <asp:gridview id="GridView1" runat="server" autogeneratecolumns="False" OnRowCommand="GridView1_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="Item Number">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("itemCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Category">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList3" runat="server"></asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("category.CategoryName")  %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("description") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("description") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reorder Level">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("reorderLevel") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("reorderLevel") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reorder Qty">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("reorderQty") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("reorderQty") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Unit Of Measure">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList4" runat="server"></asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("unitOfMeasure") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="UpdateRow" Text="Update" CommandArgument ="<%# ((GridViewRow) Container).RowIndex %>"></asp:LinkButton>
                        &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="True" CommandName="CancelEdit" Text="Cancel" CommandArgument ="<%# ((GridViewRow) Container).RowIndex %>"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="True" CommandName="EditRow" Text="Edit" CssClass="button" CommandArgument ="<%# ((GridViewRow) Container).RowIndex %>" ></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="True" CommandName="RemoveRow" Text="Remove" CssClass="rejectBtn" CommandArgument ="<%# ((GridViewRow) Container).RowIndex %>" ></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:gridview>
</asp:Content>


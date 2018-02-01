<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RequisitionForm.aspx.cs" Inherits="RequisitionForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 268435904px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="updateDeptHead">
        <h2 class="mainPageHeader">Requisition Form</h2>
    </div>
    <table>
        <tr>
            <td>
                <h3>
                    <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label></h3>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
            <td>
                <asp:Label ID="Label1" runat="server"></asp:Label></td>
        </tr>
    </table>
    <table>
        <tr>
            <td>Item Description:</td>
            <td>
                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True">
                </asp:DropDownList>
                <asp:Label ID="Label4" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>&nbsp; </td>
        </tr>

        <tr>
            <td>Quantity:</td>
            <td>
                <asp:TextBox ID="TextBox4" runat="server" Text="1" TextMode="Number" Width="74px"></asp:TextBox>

                <asp:Label ID="Label2" runat="server"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                    ControlToValidate="TextBox4" ForeColor="Red"
                    ErrorMessage="RequiredFieldValidator" ValidationGroup="addValid">Enter quantity.</asp:RequiredFieldValidator>
                <asp:RangeValidator ID="RangeValidator1" runat="server"
                    ErrorMessage="RangeValidator" ControlToValidate="TextBox4"
                    ForeColor="Red" Type="Integer"
                    MaximumValue="1000" MinimumValue="1" ValidationGroup="addValid">Invalid Quantity</asp:RangeValidator>
            </td>
        </tr>

    </table>


    <asp:Button ID="Add" runat="server" Text="Add" OnClick="Add_Click" CssClass="button" ValidationGroup="addValid" />
    <br />
    <br />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ValidationGroup="editValid" />
    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" DataKeyNames="Code" OnRowEditing="RowEdit" OnRowCancelingEdit="RowCancelingEdit" OnRowUpdating="ReqRow_Updating" CssClass="mGrid mGrid60percent" RowStyle-Height="50px">

        <Columns>
            <asp:TemplateField HeaderText="Code" SortExpression="Code" Visible="False">
                <ItemTemplate>
                    <asp:Label ID="code" runat="server" Text='<%# Bind("Code") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Item" SortExpression="Description">
                <ItemTemplate>
                    <asp:Label ID="itemDes" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Amount" SortExpression="RequestedQty" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("Quantity") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="qtyText" runat="server" Text='<%# Bind("Quantity") %>' TextMode="Number" Width="60px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                        ControlToValidate="qtyText" ForeColor="Red"
                        ErrorMessage="Enter quantity" Display="None" Enabled="True" ValidationGroup="editValid"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RangeValidator3" runat="server"
                        ErrorMessage="Invalid Quantity" ControlToValidate="qtyText"
                        ForeColor="Red" Type="Integer"
                        MaximumValue="1000" MinimumValue="1" Display="None" Enabled="True" EnableViewState="False" ValidationGroup="editValid"></asp:RangeValidator>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="UOM" SortExpression="UnitOfMeasure">
                <ItemTemplate>
                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("Uom") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="ItemEdit" runat="server" Text="  Edit  " CssClass="alert-success" CommandName="Edit" ValidationGroup="editValid" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Button ID="EditItemSave" runat="server" Text=" Save " CommandName="Update" CssClass="alert-success" ValidationGroup="editValid" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="ItemDelete" runat="server" Text="Delete " OnClick="Delete_Click" CssClass="alert-warning" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Button ID="CancelItemEdit" runat="server" Text="Cancel" CommandName="Cancel" CssClass="alert-warning" />
                </EditItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <br />
    <asp:Button ID="Submit" runat="server" Text="Submit to Approve" CssClass="button" OnClick="Submit_Click" />
    <br />
    <br />

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RequisitionForm.aspx.cs" Inherits="RequisitionForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 268435904px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--AUTHOR : APRIL SHAR--%>
    <div class="updateDeptHead">
        <h2 class="mainPageHeader">Requisition Form</h2>
    </div>
    <table>
        <tr>
            <td>
                <h3>
                    <asp:Label ID="lblFormTitle" runat="server" Text="Label"></asp:Label></h3>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
            <td>
                <asp:Label ID="lblDate" runat="server"></asp:Label></td>
        </tr>
    </table>
    <table>
        <tr>
            <td>Item Description:</td>
            <td>
                <asp:DropDownList ID="ddlItem" runat="server" AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>&nbsp; </td>
        </tr>

        <tr>
            <td>Quantity:</td>
            <td>
                <asp:TextBox ID="txtQuantity" runat="server" Text="1" TextMode="Number" Width="74px"></asp:TextBox>

                <asp:Label ID="lblUom" runat="server"></asp:Label>
                <asp:RequiredFieldValidator ID="rfvQuantity" runat="server"
                    ControlToValidate="txtQuantity" ForeColor="Red"
                    ErrorMessage="RequiredFieldValidator" ValidationGroup="addValid">Enter quantity.</asp:RequiredFieldValidator>
                <asp:RangeValidator ID="rvQuantity" runat="server"
                    ErrorMessage="RangeValidator" ControlToValidate="txtQuantity"
                    ForeColor="Red" Type="Integer"
                    MaximumValue="1000" MinimumValue="1" ValidationGroup="addValid">Invalid Quantity</asp:RangeValidator>
            </td>
        </tr>

    </table>


    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="BtnAdd_Click" CssClass="button" ValidationGroup="addValid" />
    <br />
    <br />
    <asp:ValidationSummary ID="vsQuantity" runat="server" ForeColor="Red" ValidationGroup="editValid" />
    <asp:GridView ID="gvItemList" runat="server" AutoGenerateColumns="false" DataKeyNames="Code" OnRowEditing="RowEdit" OnRowCancelingEdit="RowCancelingEdit" OnRowUpdating="ReqRow_Updating" CssClass="mGrid mGrid60percent" RowStyle-Height="50px">

        <Columns>
            <asp:TemplateField HeaderText="Code" SortExpression="Code" Visible="False">
                <ItemTemplate>
                    <asp:Label ID="lblCode" runat="server" Text='<%# Bind("Code") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Item" SortExpression="Description">
                <ItemTemplate>
                    <asp:Label ID="lblItemDes" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Amount" SortExpression="RequestedQty" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="lblQty" runat="server" Text='<%# Bind("Quantity") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtQty" runat="server" Text='<%# Bind("Quantity") %>' TextMode="Number" Width="60px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvQty" runat="server"
                        ControlToValidate="txtQty" ForeColor="Red"
                        ErrorMessage="Enter quantity" Display="None" Enabled="True" ValidationGroup="editValid"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="rvQty" runat="server"
                        ErrorMessage="Invalid Quantity" ControlToValidate="txtQty"
                        ForeColor="Red" Type="Integer"
                        MaximumValue="1000" MinimumValue="1" Display="None" Enabled="True" EnableViewState="False" ValidationGroup="editValid"></asp:RangeValidator>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="UOM" SortExpression="UnitOfMeasure">
                <ItemTemplate>
                    <asp:Label ID="lblUnitOfMeasure" runat="server" Text='<%# Bind("Uom") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="btnEdit" runat="server" Text="  Edit  " CssClass="alert-success" CommandName="Edit" ValidationGroup="editValid" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Button ID="btnSave" runat="server" Text=" Save " CommandName="Update" CssClass="alert-success" ValidationGroup="editValid" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="btnDelete" runat="server" Text="Delete " OnClick="BtnDelete_Click" CssClass="alert-warning" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CommandName="Cancel" CssClass="alert-warning" />
                </EditItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <br />
    <asp:Button ID="Submit" runat="server" Text="Submit to Approve" CssClass="button" OnClick="BtnSubmit_Click" />
    <br />
    <br />

</asp:Content>

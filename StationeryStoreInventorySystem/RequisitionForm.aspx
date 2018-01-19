<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RequisitionForm.aspx.cs" Inherits="RequisitionForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2 class="mainPageHeader">Stationary Requisition Form  </h2>

    <table>
        <tr>
            <td> 
                <h3>
                    <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label></h3>
            </td>
            <td colsp>&nbsp;</td>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
            <td>
                <asp:Label ID="Label1" runat="server"></asp:Label></td>
        </tr>
    </table>
    <table>
        <tr><td colspan="2">---------------------------------------------------------------------------------------</td></tr>
        <tr>
        <tr>
            <td>Item Description:.....</td>
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
                <asp:TextBox ID="TextBox4" runat="server" TextMode="Number" Width="74px" Text="1"></asp:TextBox>

                <asp:Label ID="Label2" runat="server"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                    ControlToValidate="TextBox4" ForeColor="Red"
                    ErrorMessage="RequiredFieldValidator">Enter quantity.</asp:RequiredFieldValidator>
                <asp:RangeValidator ID="RangeValidator1" runat="server"
                    ErrorMessage="RangeValidator" ControlToValidate="TextBox4"
                    ForeColor="Red" Type="Integer"
                    MaximumValue="1000" MinimumValue="1">Invalid Quantity</asp:RangeValidator>
            </td>
        </tr>
        <tr><td colspan="2">---------------------------------------------------------------------------------------</td></tr>
        <tr>
            <td></td>
            <td draggable="false">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Button ID="Add" runat="server" Text="Add" OnClick="Add_Click" CssClass="btn-success" /></td>
        </tr>
    </table>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <br />
&nbsp;
    <asp:Label ID="Label5" runat="server" Text="Label" Visible="False"></asp:Label>
    <br />

    <asp:GridView ID="GridView1" runat="server" CssClass="mGrid" Width="60%">
    </asp:GridView>


     <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" DataKeyNames="Code" OnRowEditing="RowEdit" OnRowCancelingEdit="RowCancelingEdit" OnRowUpdating="ReqRow_Updating" CssClass="mGrid" Width="40%">
                        
                        <Columns>
                            <asp:TemplateField HeaderText="Cpde" SortExpression="Code">
                                <ItemTemplate>
                                    <asp:Label ID="code" runat="server" Text='<%# Bind("Code") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="UOM" SortExpression="Description">
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
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UOM" SortExpression="UnitOfMeasure">
                                <ItemTemplate>
                                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("Uom") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="ItemEdit" runat="server" Text="  Edit  " CssClass="alert-success" CommandName="Edit" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Button ID="EditItemSave" runat="server" Text=" Save " CommandName="Update" CssClass="alert-success" />
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

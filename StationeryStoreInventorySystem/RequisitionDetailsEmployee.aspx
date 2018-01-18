<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RequisitionDetailsEmployee.aspx.cs" Inherits="RequisitionDetails" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2 class="mainPageHeader">Stationary Requisition Detail</h2>
    <br />
    <br />
    <a href="ReqisitionListDepartment.aspx"><-Back</a>
    <br />
    <br />
    <h2>
        <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label></h2>
    Requested By:
    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
    <br />
    Requested Date:
    <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
    <br />
    <strong>Status:
    <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
    </strong>
    <asp:Table runat="server">
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label1" runat="server" Text="Remarks: " Visible="False"></asp:Label>
            </asp:TableCell>
            <asp:TableCell Width="200px">
                <asp:Label ID="remarks" runat="server" ></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <br />

    <asp:Button ID="Add" runat="server" CssClass="btn-link" Text="Add More Item" OnClick="Add_Click" />
    <asp:Button ID="Close" runat="server" CssClass="btn-link" Text="Close" OnClick="Close_Click" Visible="False" />

    <asp:Panel ID="Panel1" runat="server" Visible="False">
        <asp:Table runat="server">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell>New Item</asp:TableHeaderCell>
            </asp:TableHeaderRow>
            <asp:TableRow>
                <asp:TableCell>Item: </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True">
                    </asp:DropDownList>

                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Quantity: </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TextBox1" runat="server" TextMode="Number" Width="74px" Text="1"></asp:TextBox>
                    <asp:Label ID="Label6" runat="server"></asp:Label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                        ControlToValidate="TextBox1" ForeColor="Red"
                        ErrorMessage="RequiredFieldValidator">Enter quantity.</asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RangeValidator2" runat="server"
                        ErrorMessage="RangeValidator" ControlToValidate="TextBox1"
                        ForeColor="Red" Type="Integer"
                        MaximumValue="1000" MinimumValue="1">Invalid Quantity</asp:RangeValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2">
                    <asp:Button ID="Button2" runat="server" OnClick="New_Click" Text="Save" CssClass="btn-success" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </asp:Panel>
    <br />
    <br />
    <asp:Table runat="server">
        <asp:TableRow>
            <asp:TableCell>
                <asp:Panel ID="Panel3" runat="server">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" DataKeyNames="Description" OnRowEditing="RowEdit" OnRowCancelingEdit="RowCancelingEdit" OnRowUpdating="ReqRow_Updating">
                        <%--CssClass="mGrid"--%>
                        <Columns>
                            <asp:TemplateField HeaderText="Item" SortExpression="Description">
                                <ItemTemplate>
                                    <asp:Label ID="itemDes" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount" SortExpression="RequestedQty">
                                <ItemTemplate>
                                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("RequestedQty") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="qtyText" runat="server" Text='<%# Bind("RequestedQty") %>' TextMode="Number" Width="60px"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UOM" SortExpression="UnitOfMeasure">
                                <ItemTemplate>
                                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("UnitOfMeasure") %>'></asp:Label>
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
                </asp:Panel>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <asp:Button ID="Cancel" runat="server" Text="Cancel Request" CssClass="rejectBtn" OnClick="Cancel_Click" />
</asp:Content>


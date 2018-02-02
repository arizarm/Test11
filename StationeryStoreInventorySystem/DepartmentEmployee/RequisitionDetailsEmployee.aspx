<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RequisitionDetailsEmployee.aspx.cs" Inherits="RequisitionDetails" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <div class="updateDeptHead"><h2 class="mainPageHeader">Stationary Requisition Detail</h2></div>
 <br />
    <br />
    <asp:Button ID="btnBack" runat="server" Text="Back To List" OnClick="BtnBack_Click" CssClass="alert-warning"/>
    <br />

    <h3>
    Requested Date:
    <asp:Label ID="lblDate" runat="server" Text="Label"></asp:Label></h3>
    <br />
    <strong>Status:
    <asp:Label ID="lblStatus" runat="server" Text="Label"></asp:Label>
    </strong>
    <br />
    <asp:Label ID="lblRemarkText" runat="server" Text="Remarks: "></asp:Label><asp:Label ID="lblRemarks" runat="server" Text="-" Width="200px"></asp:Label>
    <br />

    <asp:Button ID="btnAdd" runat="server" CssClass="btn-link" Text="Add More Item" OnClick="BtnAdd_Click" Visible="false" />

    <asp:Panel ID="pnlAddNew" runat="server" Visible="False">
        <asp:Table runat="server">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell>New Item</asp:TableHeaderCell>
            </asp:TableHeaderRow>
            <asp:TableRow>
                <asp:TableCell>Item: </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="ddlItem" runat="server" AutoPostBack="True">
                    </asp:DropDownList>

                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Quantity: </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txtQty" runat="server" TextMode="Number" Width="74px" Text="1"></asp:TextBox>
                    <asp:Label ID="lblUom" runat="server"></asp:Label>
                    <asp:RequiredFieldValidator ID="rfvQty" runat="server"
                        ControlToValidate="txtQty" ForeColor="Red"
                        ErrorMessage="RequiredFieldValidator" Enabled="true" ValidationGroup="addValid">Enter quantity.</asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="rvQty" runat="server"
                        ErrorMessage="RangeValidator" ControlToValidate="txtQty"
                        ForeColor="Red" Type="Integer"
                        MaximumValue="1000" MinimumValue="1" Enabled="true" ValidationGroup="addValid">Invalid Quantity</asp:RangeValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" HorizontalAlign="Right">
                    <asp:Button ID="btnNew" runat="server" OnClick="BtnNew_Click" Text="Save" CssClass="alert-success" ValidationGroup="addValid" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnHide" runat="server" OnClick="BtnHide_Click" Text="Close" CssClass="alert-warning" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </asp:Panel>
    <br />
    <br />
                <asp:Panel ID="pnlGridView" runat="server">
                    <asp:ValidationSummary ID="vsQty" runat="server" ForeColor="Red"  ValidationGroup="editValid" />
                    <asp:GridView ID="gvItemList" runat="server" AutoGenerateColumns="false" Visible="false" DataKeyNames="Code" OnRowEditing="RowEdit" OnRowCancelingEdit="RowCancelingEdit" OnRowUpdating="ReqRow_Updating" CssClass="mGrid mGrid60percent" RowStyle-Height="50px">
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
                                    <asp:Label ID="lblQty" runat="server" Text='<%# Bind("RequestedQty") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtQuantity" runat="server" Text='<%# Bind("RequestedQty") %>' TextMode="Number" Width="60px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvQuantity" runat="server"
                                        ControlToValidate="txtQuantity" ForeColor="Red"
                                        ErrorMessage="Enter quantity" Display="None" Enabled="True" ValidationGroup="editValid"></asp:RequiredFieldValidator>
                                    <asp:RangeValidator ID="rvQuantity" runat="server"
                                        ErrorMessage="Invalid Quantity" ControlToValidate="txtQuantity"
                                        ForeColor="Red" Type="Integer"
                                        MaximumValue="1000" MinimumValue="1" Display="None" Enabled="True" EnableViewState="False" ValidationGroup="editValid"></asp:RangeValidator>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UOM" SortExpression="UnitOfMeasure">
                                <ItemTemplate>
                                    <asp:Label ID="lblUOM" runat="server" Text='<%# Bind("UnitOfMeasure") %>'></asp:Label>
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

                    <asp:GridView ID="gvItemListView" runat="server" AutoGenerateColumns="false" Visible="true" DataKeyNames="Code" OnRowEditing="RowEdit" OnRowCancelingEdit="RowCancelingEdit" OnRowUpdating="ReqRow_Updating" CssClass="mGrid mGrid60percent" RowStyle-Height="50px" >
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
                                    <asp:Label ID="lblQty" runat="server" Text='<%# Bind("RequestedQty") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UOM" SortExpression="UnitOfMeasure">
                                <ItemTemplate>
                                    <asp:Label ID="lblUOM" runat="server" Text='<%# Bind("UnitOfMeasure") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
    <asp:Button ID="btnCancel" runat="server" Text="Cancel Request" CssClass="rejectBtn" OnClick="BtnCancel_Click" />
    <asp:Button ID="btnUpdate" runat="server" Text="Update Request" CssClass="button" OnClick="BtnUpdate_Click" />
    <asp:Button ID="btnSave" runat="server" Text="Save Request" CssClass="button" OnClick="BtnSave_Click" Visible="false" />
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RequisitionDetailsEmployee.aspx.cs" Inherits="RequisitionDetails" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2 class="mainPageHeader">Stationary Requisition Detail</h2>
    <br />
    <br />
    <a href="RequisitionListDepartment.aspx"><-Back</a>
    <br />
    <h2>
        <%--<asp:Label ID="Label5" runat="server" Text="Label"></asp:Label></h2>--%>
        <h3>Requested By:
    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label10" runat="server" Text="Label" Visible="False"></asp:Label>
            <br />
        </h3>
        Requested Date:
    <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
        <br />
        <strong>Status:
    <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
        </strong>
        <br />
        <asp:Label ID="Label9" runat="server" Text="Remarks: "></asp:Label><asp:Label ID="Label8" runat="server" Text="-" Width="200px"></asp:Label>
        <br />

        <asp:Button ID="Add" runat="server" CssClass="alert-success" Text="Add More Item" OnClick="Add_Click" Visible="False" />


        <%--<asp:Label ID="Label10" runat="server" Visible="False"></asp:Label>--%>

        <asp:Panel ID="Panel1" runat="server" Visible="False">
            <asp:Table runat="server">
                <asp:TableHeaderRow>
                    <asp:TableHeaderCell HorizontalAlign="Center" Font-Underline="true"><h4>New Item</h4></asp:TableHeaderCell>
                </asp:TableHeaderRow>
                <asp:TableRow>
                    <asp:TableCell>Item: </asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True">
                        </asp:DropDownList>

                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>To Request: </asp:TableCell>
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
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Right">
                        <asp:Button ID="Button2" runat="server" OnClick="New_Click" Text="Save" CssClass="btn-success" />
                        &nbsp &nbsp
                    <asp:Button ID="Close" runat="server" CssClass="alert-warning" Text="Close" OnClick="Close_Click" />
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
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" Visible="false" DataKeyNames="Description" OnRowEditing="RowEdit" OnRowCancelingEdit="RowCancelingEdit" OnRowUpdating="ReqRow_Updating" CssClass="mGrid" Width="150%">
                            <Columns>
                                <asp:TemplateField HeaderText="Item" SortExpression="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="itemDes" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount" SortExpression="RequestedQty" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
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

                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Button ID="ItemEdit" runat="server" Text="  Edit  " CssClass="alert-success" CommandName="Edit" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Button ID="EditItemSave" runat="server" Text=" Save " CommandName="Update" CssClass="alert-success" />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
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
                <asp:TableCell>
                    <asp:GridView ID="GridView2" runat="server" Visible="false" AutoGenerateColumns="false" CssClass="mGrid" Width="200%">
                        <Columns>
                            <asp:TemplateField HeaderText="Item" SortExpression="Description">
                                <ItemTemplate>
                                    <asp:Label ID="itemDes" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount" SortExpression="RequestedQty" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
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
                        </Columns>
                    </asp:GridView>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Button ID="Update" runat="server" Text="Update Request" CssClass="button" OnClick="Update_Click" />
        <asp:Button ID="Save" runat="server" Text="Save Request" CssClass="button" OnClick="Save_Click" Visible="False" />
        <asp:Button ID="Cancel" runat="server" Text="Cancel Request" CssClass="rejectBtn" OnClick="Cancel_Click" />
</asp:Content>


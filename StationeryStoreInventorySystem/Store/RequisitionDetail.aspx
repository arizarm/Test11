<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RequisitionDetail.aspx.cs" Inherits="Store_RequisitionDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <h2 class="mainPageHeader">Approve / Reject Requisition Details</h2>
    <br />
    <br />
    <br />
    <br />

    Requested By:
    <strong>
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    </strong>
    <br />
    Requested Date:
    <strong>
    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
    </strong>
    <br />
    Status:
    <strong>
    <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
    </strong>

    <br />
    <asp:Table runat="server">
        <asp:TableRow>
            <asp:TableCell>
                <asp:Panel ID="Panel3" runat="server">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="mGrid">
                        <%--CssClass="mGrid"--%>
                        <Columns>
                            <asp:TemplateField HeaderText="Item" SortExpression="Description">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount" SortExpression="RequestedQty">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("RequestedQty") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="qty" runat="server" TextMode="Number" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UOM" SortExpression="UnitOfMeasure">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("UnitOfMeasure") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>

</asp:Content>


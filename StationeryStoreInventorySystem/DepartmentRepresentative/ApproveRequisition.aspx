<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ApproveRequisition.aspx.cs" Inherits="ApproveRequisition" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--AUTHOR : YIMON SOE--%>
    <div class="updateDeptHead">
        <br />
        <h2 class="mainPageHeader">Stationary Requisition Detail</h2>
    </div>

    <br />
    <br />
    <asp:Button ID="btnBack" runat="server" Text="Back To List" OnClick="BtnBack_Click" CssClass="alert-success" />
    <br />
    <br />

    Requested By:
    <asp:Label ID="lblRequestBy" runat="server" Text="Label"></asp:Label>
    <br />
    Requested Date:
    <asp:Label ID="lblDate" runat="server" Text="Label"></asp:Label>
    <br />
    <strong>Status:
    <asp:Label ID="lblStatus" runat="server" Text="Label"></asp:Label>
    </strong>

    <br />
    <asp:Panel ID="pnlGridView" runat="server">
        <asp:GridView ID="gvItemList" runat="server" AutoGenerateColumns="false" CssClass="mGrid mGrid60percent" RowStyle-Height="50px">
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
                </asp:TemplateField>
                <asp:TemplateField HeaderText="UOM" SortExpression="UnitOfMeasure">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("UnitOfMeasure") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>
</asp:Content>


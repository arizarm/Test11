<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ApproveRequisition.aspx.cs" Inherits="ApproveRequisition" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="updateDeptHead"> <br /><h2 class="mainPageHeader">Stationary Requisition Detail</h2></div>

    <br />
    <br />
    <asp:Button ID="Button1" runat="server" Text="Back To List" OnClick="Button1_Click" />
    <br />
    <br />

    Requested By:
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <br />
    Requested Date:
    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
    <br />
    <strong>Status:
    <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
    </strong>

    <br />
                <asp:Panel ID="Panel3" runat="server">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="mGrid mGrid60percent" RowStyle-Height="50px">
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
    <div>
        <asp:Label ID="ReasonLabel" runat="server" Text="Reason"></asp:Label>
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
    </div>

    <asp:Button ID="ApproveButton" runat="server" Text="Approve" CssClass="button" OnClick="ApproveButton_Click" />
    <asp:Button ID="RejectButton" runat="server" Text="Reject" CssClass="rejectBtn" OnClick="RejectButton_Click"  />
    <asp:Label ID="approveSuccess" runat="server"></asp:Label>
</asp:Content>


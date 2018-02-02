<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ApproveRequisition.aspx.cs" Inherits="ApproveRequisition" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <div class="updateDeptHead">
        <h2 class="mainPageHeader">Stationary Requisition Detail</h2>
    </div>
    <br />
    <br />
    <asp:Button ID="btnBack" runat="server" Text="Back To List" OnClick="BtnBack_Click" CssClass="alert-success" />
    <br />
    <br />
    Requested By:
    <asp:Label ID="lblReqname" runat="server"></asp:Label>
    <br />
    Requested Date:
    <asp:Label ID="lblReqDate" runat="server"></asp:Label>
    <br />
    <strong>Status:
    <asp:Label ID="lblStatus" runat="server"></asp:Label>
    </strong>
    <br />

                <asp:Panel ID="Panel3" runat="server">
                    <asp:GridView ID="gvRequisitionDetailList" runat="server" AutoGenerateColumns="false" CssClass="mGrid mGrid60percent" RowStyle-Height="50px">
                        <%--CssClass="mGrid"--%>
                        <Columns>
                            <asp:TemplateField HeaderText="Item" SortExpression="Description">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount" SortExpression="Requested Qty">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("RequestedQty") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="qty" runat="server" TextMode="Number" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UOM" SortExpression="Unit Of Measure">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("UnitOfMeasure") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
    <div>
        <asp:Label ID="ReasonLabel" runat="server" Text="Reason: "></asp:Label>   
       <textarea id="txtReason" cols="20" rows="2"  runat="server"></textarea>
   
    </div>

    <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="button" OnClick="BtnApprove_Click" />
    <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="rejectBtn" OnClick="BtnReject_Click"  />
    <asp:Label ID="approveSuccess" runat="server"></asp:Label>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RequisitionListDepEmp.aspx.cs" Inherits="ReqisitionListEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSearch">
        <div class="updateDeptHead">
            <h2 class="mainPageHeader">Requisition List</h2>
        </div>
        <div>
            <asp:TextBox ID="txtSearch" runat="server" Width="311px"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClick="BtnSearch_Click" />
            <asp:Button ID="btnDisplay" runat="server" Text="Display All" CssClass="button" OnClick="BtnDisplay_Click" />
        </div>
        <div>
            <asp:Label ID="lblText" runat="server" Text="To View Requisition by Status : "></asp:Label>
            <asp:DropDownList ID="ddlStatus" runat="server" OnSelectedIndexChanged="DdlStatus_SelectedIndexChanged" AutoPostBack="True">
                <asp:ListItem Selected="True">Select Status</asp:ListItem>
                <asp:ListItem Value="Pending">Pending</asp:ListItem>
                <asp:ListItem Value="Approved">Approved</asp:ListItem>
                <asp:ListItem Value="InProgress">In Progress</asp:ListItem>
                <asp:ListItem Value="Rejected">Rejected</asp:ListItem>
                <asp:ListItem Value="Closed">Closed</asp:ListItem>
            </asp:DropDownList>
        </div>
        <h3>Your Requested Requisition list</h3>

        <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Size="XX-Large" ForeColor="#999999" Text="Label" Visible="False"></asp:Label>
        <div>
            <asp:GridView ID="gvRequisitionList" runat="server" AutoGenerateColumns="False"
                DataKeyNames="RequisitionNo" CssClass="mGrid mGrid60percent" AllowPaging="true" PageSize="15" OnPageIndexChanging="GVRequisitionList_PageIndexChanging" RowStyle-Height="50px" OnRowDataBound="GVRquisitionList_RowDataBound">
                <PagerStyle BackColor="#424242" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle Height="50px" Font-Size="Large" />
                <Columns>

                    <asp:TemplateField HeaderText="Request Date">
                        <ItemTemplate>
                            <asp:Label ID="lblRequestDate" runat="server" Text='<%# Bind("Date") %>'></asp:Label>

                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:HyperLinkField HeaderText="Detail" DataNavigateUrlFields="RequisitionNo"
                        DataNavigateUrlFormatString="RequisitionDetailsEmployee.aspx?requisitionNo={0}" Text="Detail" />
                </Columns>
            </asp:GridView>
        </div>
    </asp:Panel>
</asp:Content>



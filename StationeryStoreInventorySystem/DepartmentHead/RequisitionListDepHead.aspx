﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RequisitionListDepHead.aspx.cs" Inherits="ReqisitionListEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--AUTHOR : APRIL SHAR--%>
    <%--AUTHOR : YIMON SOE--%>
    <div class="updateDeptHead">
        <h2 class="mainPageHeader">Requisition List</h2>
    </div>

    <div>
        <asp:TextBox ID="txtSearch" runat="server" Width="311px"></asp:TextBox>
        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClick="BtnSearch_Click" />
        <asp:Button ID="btnDisplayAll" runat="server" Text="Display All" CssClass="button" OnClick="BtnDisplayAll_Click" />
    </div>
    <div>
        <asp:Label ID="Label2" runat="server" Text="To View Requisition by Status : "></asp:Label>
        <asp:DropDownList ID="ddlStatus" runat="server" OnSelectedIndexChanged="DdlStatus_SelectedIndexChanged" AutoPostBack="True">
            <asp:ListItem Selected="True">Select Status</asp:ListItem>
            <asp:ListItem Value="Pending">Pending</asp:ListItem>
            <asp:ListItem Value="Approved">Approved</asp:ListItem>
            <asp:ListItem Value="InProgress">In Progress</asp:ListItem>
            <asp:ListItem Value="Rejected">Rejected</asp:ListItem>
            <asp:ListItem Value="Closed">Closed</asp:ListItem>
        </asp:DropDownList>
    </div>
    <h2><strong>
            <asp:Label ID="lblPendingCount" runat="server" Font-Underline="True"></asp:Label></strong></h2>
     <asp:Label ID="lblNoList" runat="server" Font-Bold="True" Font-Size="XX-Large" ForeColor="#999999" Visible="False"></asp:Label>

    <div>
        <h3>All Requisition list for Department Head</h3>
        <asp:GridView ID="gvRequisitionForm" runat="server" AutoGenerateColumns="False"
            DataKeyNames="RequisitionNo" CssClass="mGrid mGrid60percent" AllowPaging="true" PageSize="10" OnPageIndexChanging="GvRequisitionForm_PageIndexChanging" OnRowDataBound="GvRequisitionForm_RowDataBound" RowStyle-Height="50px">
            <HeaderStyle Height="50px" Font-Size="Large" />
            <PagerStyle BackColor="#424242" ForeColor="White" HorizontalAlign="Center" />
            <Columns>

                <asp:TemplateField HeaderText="RequestDate">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Date") %>'></asp:Label>

                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="RequestedBy">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("EmployeeName") %>'></asp:Label>

                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Status">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:HyperLinkField HeaderText="Detail" DataNavigateUrlFields="RequisitionNo"
                    DataNavigateUrlFormatString="ApproveRequisition.aspx?id={0}" Text="Detail" />
            </Columns>

        </asp:GridView>
    </div>
</asp:Content>



﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RequisitionDetail.aspx.cs" Inherits="Store_RequisitionDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--AUTHOR : CHOU MING SHENG--%>
    <div class="row updateDeptHead">
        <h2 class="mainPageHeader">Requisition Details</h2>
    </div>
    <br />
    <br />
    <br />

    Requested By:
    <strong>
        <asp:Label ID="lblRequestedBy" runat="server" Text="Label"></asp:Label>
    </strong>
    <br />
    Requested Date:
    <strong>
        <asp:Label ID="lblDate" runat="server" Text="Label"></asp:Label>
    </strong>
    <br />
    Status:
    <strong>
        <asp:Label ID="lblStatus" runat="server" Text="Label"></asp:Label>
    </strong>

    <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="false" CssClass="mGrid mGrid60percent">
        <%--CssClass="mGrid"--%>
        <Columns>
            <asp:TemplateField HeaderText="Item Description" SortExpression="Description">
                <ItemTemplate>
                    <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Amount" SortExpression="RequestedQty">
                <ItemTemplate>
                    <asp:Label ID="lblRequestedQty" runat="server" Text='<%# Bind("RequestedQty") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtQty" runat="server" TextMode="Number" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="UOM" SortExpression="UnitOfMeasure">
                <ItemTemplate>
                    <asp:Label ID="lblUnitOfMeasure" runat="server" Text='<%# Bind("UnitOfMeasure") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>


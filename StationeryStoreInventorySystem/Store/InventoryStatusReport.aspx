﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="InventoryStatusReport.aspx.cs" Inherits="InventoryStatusReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="/Content/JavaScript.js"></script>
<%--    <style type="text/css">
        .auto-style1 {
            height: 40px;
        }

        .headerrow {
            color: white;
            background-color: grey;
            height: 35px;
        }
    </style>--%>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2 class="mainPageHeader">Logic University Inventory Status Report</h2>


    <%--<CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />--%>
    <div class=" row">
        <div class="col-md-12 pull-right">
            <button type="button" class="btn btn-default pull-right" onclick="printDiv()">Print Inventory Status</button>
        </div>
    </div>
    <div id="printable">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="90%" HorizontalAlign="Center">
            <Columns>
                <asp:BoundField DataField='itemCode' HeaderText="Item Code" />
                <asp:BoundField DataField='description' HeaderText="Description" />
                <asp:BoundField DataField='bin' HeaderText="Location" />
                <asp:BoundField DataField='unitOfMeasurement' HeaderText="Unit of measurement" />
                <asp:BoundField DataField='currentQty' HeaderText="Quantity on hand" />
                <asp:BoundField DataField='reorderLevel' HeaderText="Reorder level" />
            </Columns>
        </asp:GridView>
    </div>

    <br />
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="InventoryStatusReport.aspx.cs" Inherits="InventoryStatusReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--AUTHOR : TAN WEN SONG--%>
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
    <div class="row updateDeptHead">
        <h2 class="mainPageHeader">Logic University Inventory Status</h2>
    </div>



    <%--<CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />--%>
    <div class=" row">
        <div class="col-md-6">
            <h3>Search By Item Code or Name</h3>
        </div>
        <div class="col-md-6 pull-right" >
            <button type="button" class="btn btn-default pull-right" onclick="printDiv()">Print Inventory Status</button>
        </div>
    </div>
    <div class="row">
        <asp:TextBox ID="SearchBox" runat="server" Width="300px" MaxLength="50"></asp:TextBox>
        &nbsp;
           <asp:Button ID="SearchBtn" runat="server" Text="Search" CssClass="button" OnClick="SearchBtn_Click" />
        &nbsp;
           <asp:Button ID="Display" runat="server" Text="Display All" CssClass="button" OnClick="Display_Click" />
    </div>
    <div id="printable">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="mGrid" RowStyle-Height=" 50px"  HorizontalAlign="Center">
            <Columns>
                <asp:TemplateField HeaderText="Item Code">
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("ItemCode") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Item Description">
                    <ItemTemplate>
                        <asp:HyperLink ID="lnkStockCard" runat="server" NavigateUrl="" Text='<%# Bind("Description") %>'></asp:HyperLink>
                    </ItemTemplate>
                    <ItemStyle Width="400px" />
                </asp:TemplateField>
                <asp:BoundField DataField='bin' HeaderText="Location" />
                <asp:BoundField DataField='unitOfMeasurement' HeaderText="Unit of measurement" />
                <asp:BoundField DataField='currentQty' HeaderText="Quantity on hand" />
                <asp:BoundField DataField='reorderLevel' HeaderText="Reorder level" />
            </Columns>
            <EmptyDataTemplate>
                Item is not found
            </EmptyDataTemplate>
        </asp:GridView>
    </div>

    <br />
</asp:Content>


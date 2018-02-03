<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StationCatalogueEmp.aspx.cs" Inherits="Department_StationCatalogueEmp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="/Content/JavaScript.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row updateDeptHead">
        <h2 class="mainPageHeader">Logic University Stationery Catalogue</h2>
    </div>
    <div class="row">

        <div class="col-md-12 pull-right">
            <button type="button" id="btnForPrint" runat="server" class="btn btn-default pull-left" onclick="printDiv()" aria-hidden="true">Print Catalogue</button>
        </div>
    </div>

    <br />
    <div id="printable">
        <asp:gridview id="gvForStationCatalogue" runat="server" cssclass="mGrid mGrid60percent" RowStyle-height="50px" autogeneratecolumns="False" OnRowDataBound="GvForStationCatalogue_RowDataBound">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="lblNumber" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Category">
                    <ItemTemplate>
                        <asp:Label ID="lblCategory" runat="server" Text='<%# Bind("category.CategoryName")  %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description">
                    <ItemTemplate>
                        <asp:Label ID="lblDescription" runat="server" Visible="true" Text='<%# Bind("description") %>'></asp:Label>
                        <asp:HyperLink ID="lnkStockCard" runat="server" Visible="false" NavigateUrl="" Text='<%# Bind("Description") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Unit Of Measure">
                    <ItemTemplate>
                        <asp:Label ID="lblUOM" runat="server" Text='<%# Bind("unitOfMeasure") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

<RowStyle Height="50px"></RowStyle>
        </asp:gridview>
    </div>
</asp:Content>




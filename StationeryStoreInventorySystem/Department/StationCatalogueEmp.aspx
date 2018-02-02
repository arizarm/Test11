<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StationCatalogueEmp.aspx.cs" Inherits="Department_StationCatalogueEmp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="/Content/JavaScript.js"></script>
    <script>
        function hideColumn() {
            col_num = [document.getElementById("LinkButton1").value,
                document.getElementById("LinkButton2").value,
                document.getElementById("LinkButton3").value,
            document.getElementById("LinkButton4").value];
            rows = document.getElementById("GridView1").rows;
            for (j = 0; j < col_num.length; i++) {
                for (i = 0; i < rows.length; i++) {
                    rows[i].cells[col_num[j]].style.display = "none";
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row updateDeptHead">
        <h2 class="mainPageHeader">Logic University Stationery Catalogue</h2>
    </div>
    <div class="row">

        <div class="col-md-12 pull-right">
            <asp:button id="btnViewPrint" runat="server" text="View Printable Version" class="btn btn-default pull-right" onclick="BtnViewPrint_Click" />
            <button type="button" id="btnForPrint" runat="server" class="btn btn-default pull-right" onclick="printDiv()" aria-hidden="true" visible="false">Print Catalogue</button>
        </div>
    </div>

    <br />
    <div id="printable">
        <asp:gridview id="gvForStationCatalogue" runat="server" cssclass="mGrid mGrid60percent" RowStyle-height="50px" autogeneratecolumns="False" horizontalalign="Center" OnRowDataBound="GvForStationCatalogue_RowDataBound">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="LabelNumber" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Category">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownListCat" runat="server"></asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("category.CategoryName")  %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBoxDesc" runat="server" MaxLength="50" Text='<%# Bind("description") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="errorfont" ErrorMessage="Field cannot be left blank" ControlToValidate="TextBoxDesc" ValidationGroup="validateItemGroup" Display="Dynamic"></asp:RequiredFieldValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="LabelDescription" runat="server" Visible="true" Text='<%# Bind("description") %>'></asp:Label>
                        <asp:HyperLink ID="lnkStockCard" runat="server" Visible="false" NavigateUrl="" Text='<%# Bind("Description") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Unit Of Measure">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownListUOM" runat="server"></asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("unitOfMeasure") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

<RowStyle Height="50px"></RowStyle>
        </asp:gridview>
    </div>
</asp:Content>




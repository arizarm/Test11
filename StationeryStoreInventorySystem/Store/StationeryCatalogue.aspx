<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" EnableEventValidation="true" CodeFile="StationeryCatalogue.aspx.cs" Inherits="StationeryCatalogue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--AUTHOR : TAN WEN SONG--%>
    <script src="/Content/JavaScript.js"></script>
    <script>
        function hideColumn() {
            col_num = [document.getElementById("LbtnUpdate").value,
                document.getElementById("LbtnEdit").value,
                document.getElementById("LbtnCancel").value,
            document.getElementById("LbtnRemove").value];
            rows = document.getElementById("gvForStationeryCatalogue").rows;
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

        <div class="col-md-12 pull-left">
            <button type="button" id="BtnPrint" runat="server" class="btn btn-default pull-left" onclick="printDiv()" aria-hidden="true" visible="false">Print Catalogue</button>
            <asp:Button ID="BtnPrintView" runat="server" Text="View Printable Version" class="btn btn-default pull-left" OnClick="BtnPrintView_Click" />
        </div>
    </div>

    <br />
    <div id="printable">
        <asp:GridView ID="gvForStationeryCatalogue" runat="server" CssClass="mGrid" RowStyle-Height="50px" AutoGenerateColumns="False" OnRowCommand="GvForStationeryCatalogue_RowCommand" HorizontalAlign="Center">
            <Columns>
                <asp:TemplateField HeaderText="Item Number">
                    <ItemTemplate>
                        <asp:Label ID="LblItemCode" runat="server" Text='<%# Bind("itemCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Category">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DdlCategory" runat="server"></asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="LblCategory" runat="server" Text='<%# Bind("category.CategoryName")  %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description">
                    <EditItemTemplate>
                        <asp:TextBox ID="TxtDescription" runat="server" MaxLength="50" Text='<%# Bind("description") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ReqTxtDescription" runat="server" CssClass="errorfont" ErrorMessage="Field cannot be left blank" ControlToValidate="TxtDescription" ValidationGroup="validateItemGroup" Display="Dynamic"></asp:RequiredFieldValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="LblDescription" runat="server" Visible="true" Text='<%# Bind("description") %>'></asp:Label>
                        <asp:HyperLink ID="lnkStockCard" runat="server" Visible="false" NavigateUrl="" Text='<%# Bind("Description") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reorder Level">
                    <EditItemTemplate>
                        <asp:TextBox ID="TxtReorderLvl" runat="server" MaxLength="10" Width="100%" Text='<%# Bind("reorderLevel") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ReqTxtReorderLvl" runat="server" CssClass="errorfont" ErrorMessage="Field cannot be left blank" ControlToValidate="TxtReorderLvl" ValidationGroup="validateItemGroup" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RngTxtReorderLvl" runat="server" CssClass="errorfont" ErrorMessage="Reorder Level must be a positive number" ValidationGroup="validateItemGroup" ControlToValidate="TxtReorderLvl" MaximumValue="1000000000" MinimumValue="0" Type="Integer" Display="Dynamic"></asp:RangeValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="LblReorderLvl" runat="server" Text='<%# Bind("reorderLevel") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reorder Qty">
                    <EditItemTemplate>
                        <asp:TextBox ID="TxtReorderQty" runat="server" MaxLength="10" Width="100%" Text='<%# Bind("reorderQty") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ReqTxtReorderQty" runat="server" CssClass="errorfont" ErrorMessage="Description cannot be left blank" ControlToValidate="TxtReorderQty" ValidationGroup="validateItemGroup" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RngTxtReorderQty" runat="server" CssClass="errorfont" ErrorMessage="Reorder Qty must be a positive number" ValidationGroup="validateItemGroup" ControlToValidate="TxtReorderQty" MaximumValue="1000000000" MinimumValue="0" Type="Integer" Display="Dynamic"></asp:RangeValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="LblReorderQty" runat="server" Text='<%# Bind("reorderQty") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Unit Of Measure">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DdlUOM" runat="server"></asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="LblUOM" runat="server" Text='<%# Bind("unitOfMeasure") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Bin">
                    <EditItemTemplate>
                        <asp:TextBox ID="TxtBin" runat="server" MaxLength="10" Width="100%" Text='<%# Bind("bin") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ReqTxtBin" runat="server" CssClass="errorfont" ErrorMessage="Bin number cannot be left blank" ControlToValidate="TxtBin" ValidationGroup="validateItemGroup" Display="Dynamic"></asp:RequiredFieldValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="LblBin" runat="server" Text='<%# Bind("bin") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False" ItemStyle-BorderColor="Transparent" ControlStyle-BorderColor="Transparent" HeaderStyle-BorderColor="Transparent" FooterStyle-BorderColor="Transparent">
                    <EditItemTemplate>
                        <asp:LinkButton ID="LbtnUpdate" runat="server" CausesValidation="True" CommandName="UpdateRow" Text="Update" CssClass="button" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ValidationGroup="validateItemGroup"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="LbtnEdit" runat="server" CausesValidation="False" CommandName="EditRow" Text="Edit" CssClass="button" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False" ItemStyle-BorderColor="Transparent" ControlStyle-BorderColor="Transparent" HeaderStyle-BorderColor="Transparent" FooterStyle-BorderColor="Transparent">
                    <EditItemTemplate>
                        <asp:LinkButton ID="LbtnCancel" runat="server" CausesValidation="False" CommandName="CancelEdit" CssClass="rejectBtn" Text="Cancel" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="LbtnRemove" runat="server" CausesValidation="False" CommandName="RemoveRow" Text="Remove" CssClass="rejectBtn" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="return confirm('Confirm Remove?');"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>


            </Columns>
        </asp:GridView>
    </div>
</asp:Content>


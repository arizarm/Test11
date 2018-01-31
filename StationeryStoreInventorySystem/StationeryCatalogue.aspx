<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" EnableEventValidation="true" CodeFile="StationeryCatalogue.aspx.cs" Inherits="StationeryCatalogue" %>

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
    <h2 class="mainPageHeader">Logic University Stationery Catalogue</h2>
    <div class="row">
        <div class="col-md-6 pull-left">
            <asp:LinkButton ID="LinkButton5" runat="server" CssClass="pull-left" OnClick="LinkButton5_Click">&lt &lt Create New Item &gt &gt</asp:LinkButton>
        </div>
        
        <div class="col-md-6 pull-right">
            <asp:Button ID="PrintViewButton" runat="server" Text="View Printable Version" class="btn btn-default pull-right" OnClick="PrintViewButton_Click" />
            <button type="button" id="PrintButton" runat="server" class="btn btn-default pull-right" onclick="printDiv()" aria-hidden="true" visible="false">Print Catalogue</button>
        </div>
    </div>

    <br />
    <div id="printable">
        <asp:GridView ID="GridView1" runat="server" CssClass="mGrid" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" Width="90%" HorizontalAlign="Center">
            <Columns>
                <asp:TemplateField HeaderText="Item Number">
                    <ItemTemplate>
                        <asp:Label ID="LabelICode" runat="server" Text='<%# Bind("itemCode") %>'></asp:Label>
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
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="LabelDescription" runat="server" Visible="true" Text='<%# Bind("description") %>'></asp:Label>
                        <asp:HyperLink ID="lnkStockCard" runat="server" Visible="false" NavigateUrl="" Text='<%# Bind("Description") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reorder Level">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBoxReLvl" runat="server" MaxLength="10" Text='<%# Bind("reorderLevel") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="errorfont" ErrorMessage="Field cannot be left blank" ControlToValidate="TextBoxReLvl" ValidationGroup="validateItemGroup" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RangeValidator1" runat="server" CssClass="errorfont" ErrorMessage="Reorder Level must be a positive number" ValidationGroup="validateItemGroup" ControlToValidate="TextBoxReLvl" MaximumValue="1000000000" MinimumValue="0" Type="Integer" Display="Dynamic"></asp:RangeValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("reorderLevel") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reorder Qty">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBoxReQty" runat="server" MaxLength="10" Text='<%# Bind("reorderQty") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="errorfont" ErrorMessage="Description cannot be left blank" ControlToValidate="TextBoxReQty" ValidationGroup="validateItemGroup" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RangeValidator2" runat="server" CssClass="errorfont" ErrorMessage="Reorder Qty must be a positive number" ValidationGroup="validateItemGroup" ControlToValidate="TextBoxReQty" MaximumValue="1000000000" MinimumValue="0" Type="Integer" Display="Dynamic"></asp:RangeValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("reorderQty") %>'></asp:Label>
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
                <asp:TemplateField HeaderText="Bin">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBoxBin" runat="server" MaxLength="10" Text='<%# Bind("bin") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="errorfont" ErrorMessage="Bin number cannot be left blank" ControlToValidate="TextBoxBin" ValidationGroup="validateItemGroup" Display="Dynamic"></asp:RequiredFieldValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("bin") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False" ItemStyle-BorderColor="Transparent" ControlStyle-BorderColor="Transparent" HeaderStyle-BorderColor="Transparent" FooterStyle-BorderColor="Transparent">
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="UpdateRow" Text="Update" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ValidationGroup="validateItemGroup"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="CancelEdit" Text="Cancel" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CommandName="EditRow" Text="Edit" CssClass="button" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False" ItemStyle-BorderColor="Transparent" ControlStyle-BorderColor="Transparent" HeaderStyle-BorderColor="Transparent" FooterStyle-BorderColor="Transparent">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="False" CommandName="RemoveRow" Text="Remove" CssClass="rejectBtn" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="return confirm('Confirm Delete?');"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>


            </Columns>
        </asp:GridView>
    </div>
</asp:Content>


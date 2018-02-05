<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" EnableEventValidation="true" CodeFile="StationeryCatalogueDetail.aspx.cs" Inherits="StationeryCatalogueDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--AUTHOR : TAN WEN SONG--%>
    <div class="row updateDeptHead">
        <h2 class="mainPageHeader">Logic University Stationery Catalogue (New Item)</h2>
    </div>
    <br />
    <asp:Panel ID="pnlAdd" runat="server" DefaultButton ="BtnAdd">
        <table>
            <tr>
                <td>Item Code:</td>
                <td>
                    <asp:TextBox ID="TxtItemCode" MaxLength="10" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqTxtItemCode" runat="server" ControlToValidate="TxtItemCode" CssClass="errorfont" ErrorMessage="Item Code cannot be left blank" ValidationGroup="validateItemGroup" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="CstTxtItemCode" runat="server" ControlToValidate="TxtItemCode" CssClass="errorfont" ErrorMessage="Item Code exists in the database" OnServerValidate="CstTxtItemCode_ServerValidate" ValidationGroup="validateItemGroup" Display="Dynamic"></asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td>Category:</td>
                <td>
                    <asp:DropDownList ID="DdlCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DdlCategory_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:TextBox ID="TxtCategory" runat="server" MaxLength="10" ReadOnly="true"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqTxtCategory" runat="server" ControlToValidate="TxtCategory" CssClass="errorfont" ErrorMessage="Category cannot be left blank" ValidationGroup="validateItemGroup" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td>Item Description : </td>
                <td>
                    <asp:TextBox ID="TxtDescription" runat="server" MaxLength="50"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqTxtDescription" runat="server" ControlToValidate="TxtDescription" CssClass="errorfont" ErrorMessage="Description cannot be left blank" ValidationGroup="validateItemGroup" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="CstTxtDescription" runat="server" ControlToValidate="TxtDescription" CssClass="errorfont" ErrorMessage="Description currently used for current items" OnServerValidate="CstTxtDescription_ServerValidate" ValidationGroup="validateItemGroup"></asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td>Unit of Measure:</td>
                <td>
                    <asp:DropDownList ID="DdlUOM" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DdlUOM_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:TextBox ID="TxtUOM" runat="server" ReadOnly="true" Visible="false" MaxLength="10"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqTxtUOM" runat="server" ControlToValidate="TxtUOM" CssClass="errorfont" ErrorMessage="Unit of Measure cannot be left blank" ValidationGroup="validateItemGroup" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td>Reorder Level : </td>
                <td>
                    <asp:TextBox ID="TxtReorderLvl" runat="server" MaxLength="10"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqTxtReorderLvl" runat="server" ControlToValidate="TxtReorderLvl" CssClass="errorfont" ErrorMessage="Reorder Level cannot be left blank" ValidationGroup="validateItemGroup" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RngTxtReorderLvl" runat="server" CssClass="errorfont" ErrorMessage="Reorder Level must be a positive number" ValidationGroup="validateItemGroup" ControlToValidate="TxtReorderLvl" MaximumValue="1000000000" MinimumValue="0" Type="Integer" Display="Dynamic"></asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td>Reorder Quantity : </td>
                <td>
                    <asp:TextBox ID="TxtReorderQty" runat="server" MaxLength="10"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqTxtReorderQty" runat="server" ControlToValidate="TxtReorderQty" CssClass="errorfont" ErrorMessage="Reorder Quantity cannot be left blank" ValidationGroup="validateItemGroup" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RngTxtReorderQty" runat="server" CssClass="errorfont" ErrorMessage="Reorder Qty must be a positive number" ValidationGroup="validateItemGroup" ControlToValidate="TxtReorderQty" MaximumValue="1000000000" MinimumValue="0" Type="Integer" Display="Dynamic"></asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td>Bin No:
                </td>
                <td>
                    <asp:TextBox ID="TxtBin" MaxLength="10" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="BtnAdd" runat="server" Text="Add" CssClass="button" OnClick="BtnAdd_Click" ValidationGroup="validateItemGroup" /></td>
                <td>
                    <asp:Label ID="LblMessage" runat="server" Text="" Visible="false"></asp:Label>
                </td>
            </tr>

        </table>
    </asp:Panel>
    <br />
    <asp:Label ID="LblSubtitle" runat="server" Text="Items Added" CssClass="h3"></asp:Label>
    <br />
    <asp:GridView ID="gvItemAdded" runat="server" AutoGenerateColumns="False" RowStyle-Height="50px" CssClass="mGrid">
        <Columns>
            <asp:TemplateField HeaderText="Item Code">
                <ItemTemplate>
                    <asp:Label ID="LblItemCode" runat="server" Text='<%# Bind("itemCode") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Category">
                <ItemTemplate>
                    <asp:Label ID="LblCategory" runat="server" Text='<%# Bind("category.CategoryName")  %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Description">
                <ItemTemplate>
                    <asp:Label ID="LblDescription" runat="server" Text='<%# Bind("description") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Reorder Level">
                <ItemTemplate>
                    <asp:Label ID="LblReorderLvl" runat="server" Text='<%# Bind("reorderLevel") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Reorder Qty">
                <ItemTemplate>
                    <asp:Label ID="LblReorderQty" runat="server" Text='<%# Bind("reorderQty") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Unit Of Measure">
                <ItemTemplate>
                    <asp:Label ID="LblUOM" runat="server" Text='<%# Bind("unitOfMeasure") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Bin">
                <ItemTemplate>
                    <asp:Label ID="LblBin" runat="server" Text='<%# Bind("bin") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <br />
</asp:Content>


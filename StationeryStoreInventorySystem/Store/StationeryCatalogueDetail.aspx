<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" EnableEventValidation="true" CodeFile="StationeryCatalogueDetail.aspx.cs" Inherits="StationeryCatalogueDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <h2 class="mainPageHeader">Logic University Stationery Catalogue (New Item)</h2>

    <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="~/StationeryCatalogue.aspx" >&lt &lt Return to Catalogue List &gt &gt</asp:HyperLink>
    <asp:panel id="Panel1" runat="server">
            <table>
        <tr>
            <td>Item Number:</td>
            <td>
                <asp:textbox id="TextBoxItemNo" runat="server"></asp:textbox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxItemNo" ErrorMessage="Item Number cannot be left blank" ValidationGroup="validateItemGroup" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="TextBoxItemNo" ErrorMessage="Item Code exists in the database" OnServerValidate="CustomValidator1_ServerValidate" ValidationGroup="validateItemGroup"></asp:CustomValidator>
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td>Category:</td>
            <td>
                <asp:dropdownlist id="DropDownListCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListCategory_SelectedIndexChanged">
                    </asp:dropdownlist>
                <br />
                <asp:textbox id="TextBoxCategory" runat="server"></asp:textbox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBoxCategory" ErrorMessage="Category cannot be left blank" ValidationGroup="validateItemGroup"></asp:RequiredFieldValidator>
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td>Item Description : </td>
            <td>
                <asp:textbox id="TextBoxDesc" runat="server"></asp:textbox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBoxDesc" ErrorMessage="Description cannot be left blank" ValidationGroup="validateItemGroup"></asp:RequiredFieldValidator>
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td>Unit of Measure:</td>
            <td>
                <asp:dropdownlist id="DropDownListUOM" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListUOM_SelectedIndexChanged">
                    </asp:dropdownlist>
                <br />
                <asp:textbox id="TextBoxUOM" runat="server"></asp:textbox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBoxUOM" ErrorMessage="Unit of Measure cannot be left blank" ValidationGroup="validateItemGroup"></asp:RequiredFieldValidator>
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td>Reorder Level : </td>
            <td>
                <asp:textbox id="TextBoxReLvl" runat="server"></asp:textbox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TextBoxReLvl" ErrorMessage="Reorder Level cannot be left blank" ValidationGroup="validateItemGroup"></asp:RequiredFieldValidator>
                <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Reorder Level must be a positive number" ValidationGroup="validateItemGroup" ControlToValidate="TextBoxReLvl" MaximumValue="100000000" MinimumValue="0" Type="Integer"></asp:RangeValidator>
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td>Reorder Quantity : </td>
            <td>
                <asp:textbox id="TextBoxReQty" runat="server"></asp:textbox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TextBoxReQty" ErrorMessage="Reorder Quantity cannot be left blank" ValidationGroup="validateItemGroup"></asp:RequiredFieldValidator>
                <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="Reorder Qty must be a positive number" ValidationGroup="validateItemGroup" ControlToValidate="TextBoxReQty" MaximumValue="100000000" MinimumValue="0" Type="Integer"></asp:RangeValidator>
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td>
                Bin No:
            </td>
                    <td>
                        <asp:textbox id="TextBoxBin" runat="server"></asp:textbox>
                                        <br />
                <br />
                    </td>
                </tr>
                <tr>
            <td> <asp:button id="Button1" runat="server" text="Add" cssclass="button" OnClick="Button1_Click" ValidationGroup="validateItemGroup" /></td>
        </tr>
                
    </table>
    </asp:panel>

    <br />
    <asp:gridview id="GridView1" runat="server" autogeneratecolumns="False" Width = "90%" >
            <Columns>
                <asp:TemplateField HeaderText="Item Number">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("itemCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Category">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList3" runat="server"></asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("category.CategoryName")  %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("description") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("description") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reorder Level">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("reorderLevel") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("reorderLevel") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reorder Qty">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("reorderQty") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("reorderQty") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Unit Of Measure">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList4" runat="server"></asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("unitOfMeasure") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Bin">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text ='<%# Bind("bin") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label7" runat="server" Text ='<%# Bind("bin") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>


            </Columns>
        </asp:gridview>
</asp:Content>


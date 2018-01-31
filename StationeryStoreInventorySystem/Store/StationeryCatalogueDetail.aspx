<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" EnableEventValidation="true" CodeFile="StationeryCatalogueDetail.aspx.cs" Inherits="StationeryCatalogueDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <h2 class="mainPageHeader">Logic University Stationery Catalogue (New Item)</h2>
    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">&lt &lt Return to Catalogue List &gt &gt</asp:LinkButton>
    <asp:panel id="Panel1" runat="server">
            <table>
        <tr>
            <td>Item Number:</td>
            <td>
                <asp:textbox id="TextBoxItemNo" MaxLength="10" runat="server"></asp:textbox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxItemNo" ErrorMessage="Item Number cannot be left blank" ValidationGroup="validateItemGroup" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="TextBoxItemNo" ErrorMessage="Item Code exists in the database" OnServerValidate="CustomValidator1_ServerValidate" ValidationGroup="validateItemGroup" Display="Dynamic"></asp:CustomValidator>
            </td>
        </tr>
                <tr><td><br /></td><td><br /></td></tr>
        <tr>
            <td>Category:</td>
            <td>
                <asp:dropdownlist id="DropDownListCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListCategory_SelectedIndexChanged">
                    </asp:dropdownlist>
                <asp:textbox id="TextBoxCategory" runat="server" MaxLength="10" readonly="true"></asp:textbox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBoxCategory" ErrorMessage="Category cannot be left blank" ValidationGroup="validateItemGroup" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
                <tr><td><br /></td><td><br /></td></tr>
        <tr>
            <td>Item Description : </td>
            <td>
                <asp:textbox id="TextBoxDesc" runat="server" MaxLength="50"></asp:textbox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBoxDesc" ErrorMessage="Description cannot be left blank" ValidationGroup="validateItemGroup" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
                <tr><td><br /></td><td><br /></td></tr>
        <tr>
            <td>Unit of Measure:</td>
            <td>
                <asp:dropdownlist id="DropDownListUOM" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListUOM_SelectedIndexChanged">
                    </asp:dropdownlist>
                <asp:textbox id="TextBoxUOM" runat="server" readonly="true" Visible="false" MaxLength="10"></asp:textbox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBoxUOM" ErrorMessage="Unit of Measure cannot be left blank" ValidationGroup="validateItemGroup" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
                <tr><td><br /></td><td><br /></td></tr>
        <tr>
            <td>Reorder Level : </td>
            <td>
                <asp:textbox id="TextBoxReLvl" runat="server" MaxLength="10"></asp:textbox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TextBoxReLvl" ErrorMessage="Reorder Level cannot be left blank" ValidationGroup="validateItemGroup" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Reorder Level must be a positive number" ValidationGroup="validateItemGroup" ControlToValidate="TextBoxReLvl" MaximumValue="1000000000" MinimumValue="0" Type="Integer" Display="Dynamic"></asp:RangeValidator>
            </td>
        </tr>
                <tr><td><br /></td><td><br /></td></tr>
        <tr>
            <td>Reorder Quantity : </td>
            <td>
                <asp:textbox id="TextBoxReQty" runat="server" MaxLength="10"></asp:textbox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TextBoxReQty" ErrorMessage="Reorder Quantity cannot be left blank" ValidationGroup="validateItemGroup" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="Reorder Qty must be a positive number" ValidationGroup="validateItemGroup" ControlToValidate="TextBoxReQty" MaximumValue="1000000000" MinimumValue="0" Type="Integer" Display="Dynamic"></asp:RangeValidator>
            </td>
        </tr>
                <tr><td><br /></td><td><br /></td></tr>
        <tr>
            <td>
                Bin No:
            </td>
                    <td>
                        <asp:textbox id="TextBoxBin" MaxLength="10" runat="server"></asp:textbox>
                    </td>
                </tr>
                <tr><td><br /></td><td><br /></td></tr>
                <tr>
            <td> <asp:button id="Button1" runat="server" text="Add" cssclass="button" OnClick="Button1_Click" ValidationGroup="validateItemGroup" /></td>
                    <td>
                        <asp:Label ID="LabelMessage" runat="server" Text="" Visible="false"></asp:Label>
                    </td>
        </tr>
                
    </table>
    </asp:panel>
    <br />
    <asp:Label ID="LabelSubtitle" runat="server"  Text="Items Added" CssClass="h3"></asp:Label>
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
                        <asp:TextBox ID="TextBox6" runat="server" MaxLength="50" Text='<%# Bind("description") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("description") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reorder Level">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox9" runat="server" MaxLength="10" Text='<%# Bind("reorderLevel") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("reorderLevel") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reorder Qty">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox8" runat="server" MaxLength="10" Text='<%# Bind("reorderQty") %>'></asp:TextBox>
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


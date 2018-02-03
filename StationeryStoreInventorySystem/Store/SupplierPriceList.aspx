<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SupplierPriceList.aspx.cs" Inherits="SupplierPriceList" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:Panel ID="PnlMain" runat="server">
        <div class="updateDeptHead">
            <h2 class="mainPageHeader">Supplier Profile</h2>
        </div>
        <%--<asp:Label ID="Label1" runat="server" Text="Label" Style="font-weight: 700" ForeColor="#76b432"><h2>Supplier Profile</h2></asp:Label>--%>
        <br />
        <asp:Table ID="TblMain" runat="server">
            <asp:TableRow>
                <asp:TableCell>
                    <strong>
                        <asp:Label ID="LblSupCode" runat="server" Text="Supplier Code: "></asp:Label></strong>
                </asp:TableCell>
                <asp:TableCell>
                    <strong>
                        <asp:TextBox ID="TxtSupCode" runat="server" Width="130%" Enabled="false"></asp:TextBox></strong>
                    <asp:RequiredFieldValidator ID="ReqSupCode" runat="server" ErrorMessage="Supplier Code is required!" ControlToValidate="TxtSupCode" ForeColor="Red"></asp:RequiredFieldValidator>
                    <br />
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <strong>
                        <asp:Label ID="LblSupName" runat="server" Text="Supplier Name: "></asp:Label></strong>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TxtSupName" runat="server" Width="130%" ValidationGroup="SupplierName"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqSupName" runat="server" ValidationGroup="SupplierName" ErrorMessage="Supplier Name is required!" ControlToValidate="TxtSupName" ForeColor="Red"></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <strong>
                        <asp:Label ID="LblContactName" runat="server" Text="Contact Name: "></asp:Label></strong>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TxtContactName" runat="server" Width="130%"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqContactName" runat="server" ErrorMessage="Contact Name is required!" ControlToValidate="TxtContactName" ForeColor="Red"></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <strong>
                        <asp:Label ID="LblPhoneNo" runat="server" Text="Phone No.: "></asp:Label></strong>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TxtSupplierPhoneNo" runat="server" TextMode="SingleLine" Width="130%" OnTextChanged="TxtSupplierPhoneNo_TextChanged"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqPhoneNo" runat="server" ErrorMessage="Phone Number is required!" ControlToValidate="TxtSupplierPhoneNo" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegPhoneNo" runat="server" ErrorMessage="Please enter a valid 8-digit phone number" ControlToValidate="TxtSupplierPhoneNo" ValidationExpression="[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]" Enabled="false" ForeColor="Red"></asp:RegularExpressionValidator>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <strong>
                        <asp:Label ID="LblFaxNo" runat="server" Text="Fax No.: "></asp:Label></strong>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TxtFaxNo" runat="server" TextMode="SingleLine" Width="130%"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqFaxNo" runat="server" ErrorMessage="Fax Number is required!" ControlToValidate="TxtFaxNo" ForeColor="Red"></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <strong>
                        <asp:Label ID="LblAddress" runat="server" Text="Address: "></asp:Label></strong>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TxtAddress" runat="server" TextMode="MultiLine" Height="90px" Width="70%"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqAddress" runat="server" ErrorMessage="Address is required!" ControlToValidate="TxtAddress" ForeColor="Red"></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <strong>
                        <asp:Label ID="LblEmail" runat="server" Text="Email: "></asp:Label></strong>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TxtEmail" runat="server" TextMode="SingleLine" Width="130%"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <strong>
                        <asp:Label ID="LblActive" runat="server" Text="Active: "></asp:Label></strong>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TxtActive" runat="server" TextMode="SingleLine" Width="130%" Enabled="false"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" HorizontalAlign="Right">
                    <asp:Button ID="BtnDelete" runat="server" Text="Set To Inactive" CssClass="rejectBtn" OnClick="BtnDelete_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="BtnUpdate" runat="server" Text="Update" CssClass="button" OnClick="BtnUpdate_Click" Enabled="false" Visible="false" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        &nbsp;
    </asp:Panel>
    <br />

    <asp:Panel ID="PnlAddNewItem" runat="server" Width="444px" Height="857px">
        <strong><asp:Label ID="LblSPL" runat="server" Text="Label" ForeColor="#76b432" Font-Underline="true" Font-Bold="true"><strong><h2>Supplier PriceList</h2></strong></asp:Label></strong>
        <asp:Button ID="BtnAddNewItem" runat="server" Text="+ Add New" Font-Underline="True" OnClick="BtnAddNewItem_Click" CssClass="alert-success"/>

        <br />

        <asp:Panel ID="PnlNewItem" runat="server">
            <asp:Table ID="TblNewItem" runat="server" HorizontalAlign="Center" Visible="False" Width="560px">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" Font-Size="Large"><strong>New Item</strong></asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell>Category: </asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList runat="server" ID="DDLCategory" Width="100%" OnSelectedIndexChanged="DDLCategory_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow />
                <asp:TableRow>
                    <asp:TableCell>Item: </asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList runat="server" ID="DDLItem" Width="180%" OnSelectedIndexChanged="DDLItem_SelectedIndexChanged" AutoPostBack="true" DataTextField="ItemDescription" DataValueField="ItemCode">
                        </asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow />
                <asp:TableRow>
                    <asp:TableCell>Price: </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="TxtAddNewItem" runat="server" ValidationGroup="AddNewItem"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ReqItem" runat="server" ErrorMessage="Enter a value" ControlToValidate="TxtAddNewItem" ForeColor="Red" ValidationGroup="AddNewItem" />
                        <asp:RegularExpressionValidator ID="RegNewItemPriceRange" runat="server" ErrorMessage="Correct format: 12.00" ControlToValidate="TxtAddNewItem" ForeColor="Red" ValidationExpression="(\d{1,4})([.]{0,1}\d{0,2})" ValidationGroup="AddNewItem" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow />
                <asp:TableRow>
                    <asp:TableCell>Supplier Priority for this item: </asp:TableCell><asp:TableCell>
                        <asp:DropDownList runat="server" ID="DDLPriorityRank" Width="50%">
                        </asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow />
                <asp:TableRow>
                    <asp:TableCell>Year(Tender): </asp:TableCell><asp:TableCell>
                        <asp:TextBox ID="TxtTenderYear" runat="server"></asp:TextBox><br />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Right">
                        <asp:Button ID="BtnAddItem" runat="server" Text="Add" OnClick="BtnAddItem_Click" CssClass="alert-success"/>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </asp:Panel>

        <asp:Label ID="LblItemDisplay" runat="server" Text="*Items displayed are only for current year" Font-Italic="true" Font-Size="Small"></asp:Label>
        <asp:GridView ID="DDLTenderPrice" runat="server" BorderStyle="Double" AllowPaging="true" PageSize="10" AutoGenerateColumns="False" Width="120%" DataKeyNames="ItemCode" OnPageIndexChanging="DDLTenderPrice_PageIndexChanging" 
            OnRowEditing="DDLTenderPrice_RowEditing" OnRowCancelingEdit="DDLTenderPrice_RowCancelingEdit" OnRowUpdating="DDLTenderPrice_RowUpdating" CssClass="mGrid mGrid60percent" RowStyle-Height="50px">
           <PagerStyle BackColor="#424242" ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle Height="50px" Font-Size="Large" /> 
            <Columns>
                <asp:TemplateField HeaderText="Item Description" ItemStyle-Width="80%">
                    <ItemTemplate>
                        <asp:Label ID="LblItemDes" runat="server" Text='<%# Eval("ItemDescription") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Item Price" ItemStyle-Width="80%">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("ItemPrice") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="TxtNewPrice" runat="server" Text='<%# Eval("ItemPriceOnly") %>'></asp:TextBox><asp:RegularExpressionValidator ID="NewPriceRangeValidator" runat="server" ErrorMessage="Input correct format eg:12.00" ControlToValidate="TxtNewPrice" ValidationExpression="(\d{1,4})([.]{0,1}\d{0,2})" ForeColor="Red" />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="BtnItemEdit" runat="server" Text="  Edit  " CommandName="Edit" CssClass="alert-success" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Button ID="BtnItemSave" runat="server" Text="Save" CommandName="Update" CssClass="alert-success"/>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="BtnItemDelete" runat="server" Text="Delete" OnClientClick="return confirm('Confirm Delete?');" OnClick="BtnItemDelete_Click" CssClass="alert-warning"/>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Button ID="BtnCancelItemEdit" runat="server" Text="Cancel" CommandName="Cancel" CssClass="alert-warning"/>
                    </EditItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
    </asp:Panel>
</asp:Content>


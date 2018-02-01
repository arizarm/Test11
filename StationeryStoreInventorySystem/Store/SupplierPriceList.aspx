<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SupplierPriceList.aspx.cs" Inherits="SupplierPriceList" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:Panel ID="Panel1" runat="server">
        <div class="updateDeptHead">
            <br />
            <h2 class="mainPageHeader">Supplier Profile</h2>
        </div>
        <%--<asp:Label ID="Label1" runat="server" Text="Label" Style="font-weight: 700" ForeColor="#76b432"><h2>Supplier Profile</h2></asp:Label>--%>
        <br />
        <asp:Table ID="Table2" runat="server">
            <asp:TableRow>
                <asp:TableCell>
                    <strong>
                        <asp:Label ID="Label2" runat="server" Text="Supplier Code: "></asp:Label></strong>
                </asp:TableCell>
                <asp:TableCell>
                    <strong>
                        <asp:TextBox ID="TextBox1" runat="server" Width="130%" Enabled="false"></asp:TextBox></strong>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Supplier Code is required!" ControlToValidate="TextBox1" ForeColor="Red"></asp:RequiredFieldValidator>
                    <br />
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <strong>
                        <asp:Label ID="Label4" runat="server" Text="Supplier Name: "></asp:Label></strong>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TextBox2" runat="server" Width="130%" ValidationGroup="SupplierName"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="SupplierName" ErrorMessage="Supplier Name is required!" ControlToValidate="TextBox2" ForeColor="Red"></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <strong>
                        <asp:Label ID="Label5" runat="server" Text="Contact Name: "></asp:Label></strong>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TextBox3" runat="server" Width="130%"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Contact Name is required!" ControlToValidate="TextBox3" ForeColor="Red"></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <strong>
                        <asp:Label ID="Label6" runat="server" Text="Phone No.: "></asp:Label></strong>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="SupplierPhoneNoTextBox" runat="server" TextMode="SingleLine" Width="130%" OnTextChanged="SupplierPhoneNoTextBox_TextChanged"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Phone Number is required!" ControlToValidate="SupplierPhoneNoTextBox" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="PhoneNoValidator" runat="server" ErrorMessage="Please enter a valid 8-digit phone number" ControlToValidate="SupplierPhoneNoTextBox" ValidationExpression="[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]" Enabled="false" ForeColor="Red"></asp:RegularExpressionValidator>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <strong>
                        <asp:Label ID="Label7" runat="server" Text="Fax No.: "></asp:Label></strong>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TextBox5" runat="server" TextMode="SingleLine" Width="130%"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Fax Number is required!" ControlToValidate="TextBox5" ForeColor="Red"></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <strong>
                        <asp:Label ID="Label8" runat="server" Text="Address: "></asp:Label></strong>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TextBox6" runat="server" TextMode="MultiLine" Height="90px" Width="70%"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Address is required!" ControlToValidate="TextBox6" ForeColor="Red"></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <strong>
                        <asp:Label ID="Label9" runat="server" Text="Email: "></asp:Label></strong>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TextBox8" runat="server" TextMode="SingleLine" Width="130%"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <strong>
                        <asp:Label ID="Label10" runat="server" Text="Active: "></asp:Label></strong>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TextBox9" runat="server" TextMode="SingleLine" Width="130%"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="ActiveStatusValidator" runat="server" ErrorMessage="Enter Y or N" ValidationExpression="[YN]" ControlToValidate="TextBox9"></asp:RegularExpressionValidator>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" HorizontalAlign="Right">
                    <asp:Button ID="DeleteButton" runat="server" Text="Set To Inactive" CssClass="rejectBtn" OnClick="DeleteButton_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="UpdateButton" runat="server" Text="Update" CssClass="button" OnClick="UpdateButton_Click" Enabled="false" Visible="false" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        &nbsp;
    </asp:Panel>
    <br />

    <asp:Panel ID="Panel2" runat="server" Width="444px" Height="857px">
        <strong><asp:Label ID="Label3" runat="server" Text="Label" ForeColor="#76b432"><strong><h2>Supply Tender Quotation Form</h2></strong></asp:Label></strong>
        <asp:Button ID="AddNewItemButton" runat="server" Text="+ Add New" Font-Underline="True" OnClick="AddNewItemButton_Click" CssClass="alert-success"/>

        <br />

        <br />

        <asp:Panel ID="Panel3" runat="server">
            <asp:Table ID="Table3" runat="server" HorizontalAlign="Center" Visible="False" Width="560px">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" Font-Size="Large"><strong>New Item</strong></asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell>Category: </asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList runat="server" ID="CategoryDropDownList" Width="100%" OnSelectedIndexChanged="CategoryDropDownList_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow />
                <asp:TableRow>
                    <asp:TableCell>Item: </asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList runat="server" ID="ItemDropDownList" Width="180%" OnSelectedIndexChanged="ItemDropDownList_SelectedIndexChanged" AutoPostBack="true" DataTextField="ItemDescription" DataValueField="ItemCode">
                        </asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow />
                <asp:TableRow>
                    <asp:TableCell>Price: </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="TextBox7" runat="server" ValidationGroup="AddNewItem"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Enter a value" ControlToValidate="TextBox7" ForeColor="Red" ValidationGroup="AddNewItem" />
                        <asp:RegularExpressionValidator ID="NewItemPriceRangeValidator" runat="server" ErrorMessage="Correct format: 12.00" ControlToValidate="TextBox7" ForeColor="Red" ValidationExpression="(\d{1,4})([.]{0,1}\d{0,2})" ValidationGroup="AddNewItem" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow />
                <asp:TableRow>
                    <asp:TableCell>Supplier Priority for this item: </asp:TableCell><asp:TableCell>
                        <asp:DropDownList runat="server" ID="PriorityRankList" Width="50%">
                        </asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow />
                <asp:TableRow>
                    <asp:TableCell>Year(Tender): </asp:TableCell><asp:TableCell>
                        <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox><br />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Right">
                        <asp:Button ID="AddItemButton" runat="server" Text="Add" OnClick="AddItemButton_Click" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </asp:Panel>
        <br />
        <asp:Label ID="ItemDisplayDesc" runat="server" Text="*Items displayed are only for current year" Font-Italic="true" Font-Size="Small"></asp:Label>
        <asp:GridView ID="TenderPriceDropDownList" runat="server" BorderStyle="Double" AllowPaging="true" PageSize="10" AutoGenerateColumns="False" Width="120%" DataKeyNames="ItemCode" OnPageIndexChanging="TenderPriceDropDownList_PageIndexChanging" 
            OnRowEditing="TenderPriceDropDownList_RowEditing" OnRowCancelingEdit="TenderPriceDropDownList_RowCancelingEdit" OnRowUpdating="TenderPriceDropDownList_RowUpdating" CssClass="mGrid mGrid60percent" RowStyle-Height="50px">
           <PagerStyle BackColor="#424242" ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle Height="50px" Font-Size="Large" /> 
            <Columns>
                <asp:TemplateField HeaderText="Item Description" ItemStyle-Width="80%">
                    <ItemTemplate>
                        <asp:Label ID="ItemDesLabel" runat="server" Text='<%# Eval("ItemDescription") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Item Price" ItemStyle-Width="80%">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("ItemPrice") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="NewPriceTextBox" runat="server" OnTextChanged="NewPriceTextBox_TextChanged"></asp:TextBox><asp:RegularExpressionValidator ID="NewPriceRangeValidator" runat="server" ErrorMessage="Input correct format eg:12.00" ControlToValidate="NewPriceTextBox" ValidationExpression="(\d{1,4})([.]{0,1}\d{0,2})" ForeColor="Red" />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="ItemEdit" runat="server" Text="  Edit  " CommandName="Edit" CssClass="alert-success" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Button ID="EditItemSave" runat="server" Text="Save" CommandName="Update" CssClass="alert-success"/>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="ItemDelete" runat="server" Text="Delete" OnClick="ItemDelete_Click" CssClass="alert-warning"/>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Button ID="CancelItemEdit" runat="server" Text="Cancel" CommandName="Cancel" CssClass="alert-warning"/>
                    </EditItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
    </asp:Panel>
</asp:Content>


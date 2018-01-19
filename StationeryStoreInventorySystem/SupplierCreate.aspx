<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SupplierCreate.aspx.cs" Inherits="SupplierPriceList" MaintainScrollPositionOnPostback="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:Panel ID="Panel1" runat="server">
        <asp:Label ID="Label1" runat="server" Text="Label" Style="font-weight: 700" ForeColor="#76b432"><h2>New Supplier</h2></asp:Label>
        <asp:Table ID="Table2" runat="server">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="Label2" runat="server" Text="Supplier Code: "></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TextBox1" runat="server" Width ="130%"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TextBox1" runat="server" ForeColor="Red" ErrorMessage="Supplier Code is required!"></asp:RequiredFieldValidator>
                    <br />
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="Label4" runat="server" Text="Supplier Name: "></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TextBox2" runat="server" Width ="130%"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"  ControlToValidate="TextBox2"  ForeColor="Red" ErrorMessage="Supplier Name is required!"></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="Label5" runat="server" Text="Contact Name: "></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TextBox3" runat="server" Width ="130%"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox3"  ForeColor="Red" ErrorMessage="Contact Name is required!"></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="Label6" runat="server" Text="Phone No.: "></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TextBox4" runat="server" TextMode="SingleLine" Width ="130%"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TextBox4"  ForeColor="Red" ErrorMessage="Phone Number is required!"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="PhoneNoValidator" runat="server" ErrorMessage="Please enter a valid Singapore phone number" ControlToValidate="TextBox4" ValidationExpression="[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]"></asp:RegularExpressionValidator>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="Label7" runat="server" Text="Fax No.: "></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TextBox5" runat="server" TextMode="SingleLine" Width ="130%"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBox5"  ForeColor="Red" ErrorMessage="Fax Number is required!"></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="Label8" runat="server" Text="Address: "></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TextBox6" runat="server" TextMode="MultiLine" Height="86px" Width="186px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TextBox6"  ForeColor="Red" ErrorMessage="Address is required!"></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>

                        <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="Label3" runat="server" Text="Email: "></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TextBox7" runat="server"  Width ="130%"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

                        <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="Label9" runat="server" Text="Active: "></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TextBox8" runat="server"  Width ="130%" MaxLength="1"></asp:TextBox>
                    <asp:RegularExpressionValidator runat="server" ErrorMessage="Enter Y or N" ControlToValidate="TextBox8" ValidationExpression="^[Y|N]*$" ForeColor="Red"></asp:RegularExpressionValidator>
                </asp:TableCell>
            </asp:TableRow>


            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" HorizontalAlign="Right">
                    <asp:Button ID="SaveButton" runat="server" Text="Save"  CssClass="button" OnClick="SaveButton_Click"/>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        &nbsp;</asp:Panel>
    <br />
</asp:Content>


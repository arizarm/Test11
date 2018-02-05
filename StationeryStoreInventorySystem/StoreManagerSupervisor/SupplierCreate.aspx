<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SupplierCreate.aspx.cs" Inherits="SupplierPriceList" MaintainScrollPositionOnPostback="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--AUTHOR : ARIZ ARMAND BIN ABDUL RAHMAN--%>
      <div class="updateDeptHead"><h2 class="mainPageHeader">New Supplier</h2></div>
 
    <asp:Panel ID="PnlMain" runat="server">
        <%--<asp:Label ID="Label1" runat="server" Text="Label" Style="font-weight: 700" ForeColor="#76b432"><h2>New Supplier</h2></asp:Label>--%>
        <br />
        <asp:Table ID="TblMain" runat="server">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="LblSupCode" runat="server" Text="Supplier Code: "></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TxtSupCode" runat="server" Width ="130%"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqSupCode" ControlToValidate="TxtSupCode" runat="server" ForeColor="Red" ErrorMessage="Supplier Code is required!"></asp:RequiredFieldValidator>
                    <br />
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="LblSupName" runat="server" Text="Supplier Name: "></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TxtSupName" runat="server" Width ="130%"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqSupName" runat="server"  ControlToValidate="TxtSupName"  ForeColor="Red" ErrorMessage="Supplier Name is required!"></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="LblContactName" runat="server" Text="Contact Name: "></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TxtContactName" runat="server" Width ="130%"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqContactName" runat="server" ControlToValidate="TxtContactName"  ForeColor="Red" ErrorMessage="Contact Name is required!"></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="LblPhoneNo" runat="server" Text="Phone No.: "></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TxtPhoneNo" runat="server" TextMode="SingleLine" Width ="130%"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqPhoneNo" runat="server" ControlToValidate="TxtPhoneNo"  ForeColor="Red" ErrorMessage="Phone Number is required!"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegPhoneNo" runat="server" ErrorMessage="Please enter a valid Singapore phone number" ControlToValidate="TxtPhoneNo" ValidationExpression="[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]"></asp:RegularExpressionValidator>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="LblFaxNo" runat="server" Text="Fax No.: "></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TxtFaxNo" runat="server" TextMode="SingleLine" Width ="130%"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqFaxNo" runat="server" ControlToValidate="TxtFaxNo"  ForeColor="Red" ErrorMessage="Fax Number is required!"></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="LblAddress" runat="server" Text="Address: "></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TxtAddress" runat="server" TextMode="MultiLine" Height="90px" Width="70%"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqAddress" runat="server" ControlToValidate="TxtAddress"  ForeColor="Red" ErrorMessage="Address is required!"></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>

                        <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="LblEmail" runat="server" Text="Email: "></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TxtEmail" runat="server"  Width ="130%"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

                        <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="LblActive" runat="server" Text="Active: "></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TxtActive" runat="server"  Width ="130%" MaxLength="1"></asp:TextBox>
                    <asp:RegularExpressionValidator runat="server" ErrorMessage="Enter Y or N" ControlToValidate="TxtEmail" ValidationExpression="^[Y|N]*$" ForeColor="Red"></asp:RegularExpressionValidator>
                </asp:TableCell>
            </asp:TableRow>


            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" HorizontalAlign="Right">
                    <asp:Button ID="BtnSave" runat="server" Text="Save"  CssClass="button" OnClick="BtnSave_Click"/>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        &nbsp;</asp:Panel>
    <br />
</asp:Content>


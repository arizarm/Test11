<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CollectionPointUpdate.aspx.cs" Inherits="CollectionPointUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="Content/JavaScript.js"></script>
    <script type="text/javascript">
        $(function () {

            $("#txtSDate").datepicker({
                changeMonth: true,
                changeYear: true,
                yearRange: "-0:+2", // You can set the year range as per as your need
                dateFormat: 'yy-mm-dd'

            }).val()
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row updateDeptHead">
        <h2 class="mainPageHeader">Collection Point Update</h2>
    </div>
    <br />
    <br />
    <br />
    <p>
        <b>Collection Date:</b>

        <asp:TextBox ID="txtSDate" AutoPostBack="true" runat="server" ValidationGroup="1" ClientIDMode="Static"></asp:TextBox>
        <asp:RequiredFieldValidator ID="req" Display="Dynamic" ValidationGroup="1" runat="server" ErrorMessage="Please select the date" ControlToValidate="txtSDate" Style="color: red"></asp:RequiredFieldValidator>
        <asp:CustomValidator runat="server"  ControlToValidate="txtSDate" ValidationGroup="1" ErrorMessage="Please select a future date." Display="Dynamic" ForeColor="Red" OnServerValidate="DateValidator"></asp:CustomValidator>
      
        <asp:GridView ID="gvCollectionPoint" runat="server" AutoGenerateColumns="False" CssClass="mGrid mGrid60percent" RowStyle-Height="50px">
            <Columns>
                <asp:TemplateField HeaderText="Collection Point">
                    <ItemTemplate>
                        <asp:Label ID="lblCollectionPoint" runat="server" Text='<%# Bind("CollectionPoint") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Collection Time (HHMM, 24 hour format)">
                    <ItemTemplate>
                        <asp:TextBox ID="txtTime" runat="server" Text='<%# Bind("DefaultCollectionTime") %>'></asp:TextBox>
                        <asp:RegularExpressionValidator
                            ID="reg" ControlToValidate="txtTime" Display="Dynamic" ValidationGroup="1"
                            runat="server" ErrorMessage="You must enter 4 digits."
                            ValidationExpression="\d{4}$" ForeColor="Red"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="req" Display="Dynamic" ValidationGroup="1" runat="server" ErrorMessage="Please enter 4 digits" ControlToValidate="txtTime" Style="color: red"></asp:RequiredFieldValidator>

                         </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </p>
    <asp:Button ID="btnSubmit" runat="server" ValidationGroup="1" Text="Submit" CssClass="button" OnClick="BtnSubmit_Click" />
</asp:Content>



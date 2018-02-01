<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RetrievalListDetail.aspx.cs" Inherits="RetrievalForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row updateDeptHead">
        <h2 class="mainPageHeader">Retrieval List Detail</h2>
    </div>
    <br />
    <br />
    <br />


    <asp:GridView ID="gvRe" runat="server" AutoGenerateColumns="False" CssClass="mGrid " width="70%" RowStyle-Height="50px">
        <Columns>
            <asp:TemplateField HeaderText="Bin#">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("bin") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Item Description">
                <ItemTemplate>
                    <asp:HiddenField ID="hdnflditemCode" runat="server" Value='<%# Bind("itemCode") %>' />
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("description") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Total Quantity Requested">
                <ItemTemplate>
                    <asp:Label ID="labTotalRequestedQty" runat="server" Text='<%# Bind("totalRequestedQty") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Total Quantity Retrieved">
                <ItemTemplate>
                    <asp:TextBox ID="txtRetrieved" ValidationGroup="1" runat="server" Text='<%# Bind("retrievedQty") %>'></asp:TextBox>
                    <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator1" ValidationGroup="1" runat="server" ErrorMessage="Please enter the Qty" ForeColor="Red" ControlToValidate="txtRetrieved"></asp:RequiredFieldValidator>

                    <asp:RangeValidator ID="RangeValidator1" Display="Dynamic" ValidationGroup="1" runat="server" ErrorMessage="Invalid Quantity!" ControlToValidate="txtRetrieved" MinimumValue="0" Style="color: red" Type="Integer"></asp:RangeValidator>

                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <asp:Button ID="Save" runat="server" Text="Save" ValidationGroup="1"  CssClass="button" OnClick="Save_Click" />&nbsp;&nbsp;&nbsp;
            <asp:Button ID="FinalizeDisbursmentList" ValidationGroup="1"  runat="server" Text="Generate Disbursment List" CssClass="button" OnClick="FinalizeDisbursmentList_Click" Style="height: 47px" />

</asp:Content>


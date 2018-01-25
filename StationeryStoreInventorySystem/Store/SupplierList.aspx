<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SupplierList.aspx.cs" Inherits="SupplierList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="Label1" runat="server" Text="Supplier List" CssClass="mainPageHeader" Font-Bold="true" Font-Size="X-Large"/>
    <br /><br /> 
    <asp:Button ID="AddSupplierButton" runat="server" Text="Add New Supplier" OnClick="AddSupplierButton_Click" Visible="false" Enabled="false"/>
    <br /> <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" >
        <Columns>
            <asp:BoundField DataField="SupplierCode" HeaderText="Supplier Code" ItemStyle-Width="7.5%">
<ItemStyle Width="7.5%"></ItemStyle>
            </asp:BoundField>
            <asp:HyperLinkField DataNavigateUrlFields="SupplierCode" DataNavigateUrlFormatString="~\Store\SupplierPriceList.aspx?SupplierCode={0}" DataTextField="SupplierName" HeaderText="Supplier Name" />
            <asp:BoundField DataField="SupplierContactName" HeaderText="Supplier Contact Name" ControlStyle-Width="7.5%" >
<ControlStyle Width="7.5%"></ControlStyle>
            </asp:BoundField>
            <asp:BoundField DataField="SupplierPhone" HeaderText="Supplier Phone" ItemStyle-Width="7.5%" >
<ItemStyle Width="7.5%"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="SupplierFax" HeaderText="Supplier Fax" ItemStyle-Width="7.5%" >
<ItemStyle Width="7.5%"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="SupplierAddress" HeaderText="Supplier Address" ItemStyle-Width="7.5%" >
<ItemStyle Width="7.5%"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="SupplierEmail" HeaderText="Supplier Email" ItemStyle-Width="7.5%" >
<ItemStyle Width="7.5%"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="ActiveStatus" HeaderText="Active Status" ItemStyle-Width="7.5%" >
<ItemStyle Width="7.5%"></ItemStyle>
            </asp:BoundField>
        </Columns>
    </asp:GridView>
    <br />
</asp:Content>


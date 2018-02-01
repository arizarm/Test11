<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PurchaseOrderForm.aspx.cs" Inherits="PurchaseOrderForm" Culture="en-SG" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title></title>

    <style type="text/css">
        .textboxStyle {
            text-align: center;
        }

        .labelStyle {
            font-weight: bold;
            font-Size: medium;
        }

        .gvHeaderColumn {
            text-align: center;
        }
    </style>

    <link type="text/css" href="css/smoothness/jquery-ui-1.7.1.custom.css" rel="stylesheet" />
    <script src="_scripts/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="_scripts/jquery-ui-1.7.1.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("#txtDate").datepicker({
                changeMonth: true,
                changeYear: true,

            });
        });
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row updateDeptHead">
        <h2 class="mainPageHeader">Purchase Order Form</h2>
    </div>
    <br />
    <br />
    <div>
        <asp:Label ID="Label5" runat="server" Text="Deliver to : Logic University Stationery Store" CssClass="labelStyle"></asp:Label>
    </div>
    <br />
    Please supply the following items by: &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtDate" runat="server" Text=""></asp:TextBox>
    <asp:RequiredFieldValidator runat="server" ErrorMessage="Please enter a date" ControlToValidate="txtDate" ValidationGroup="PurchaseOrderValidationGrp" Display="Dynamic" ForeColor="Red" />
    <asp:CustomValidator runat="server" ControlToValidate="txtDate" ErrorMessage="Date cannot be lesser than today." ValidationGroup="PurchaseOrderValidationGrp" Display="Dynamic" ForeColor="Red" OnServerValidate="DateValidator"></asp:CustomValidator>
        <br />
        <asp:GridView ID="gvPurchaseItems" runat="server" AutoGenerateColumns="False" CssClass="mGrid" DataKeyNames="ItemCode" RowStyle-Height="50px" 
            OnRowDataBound="reoderItems_RowDataBound">
            <Columns>

                <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="gvHeaderColumn">
                    <HeaderTemplate>
                        <asp:CheckBox runat="server" ID="CheckAll" Width="60px" OnCheckedChanged="CheckAll_CheckedChanged" AutoPostBack="true" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox runat="server" ID="CheckBox" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ItemCode" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="ItemCode" runat="server" Text='<%# Bind("ItemCode") %>' ForeColor="Black"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Description" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="gvHeaderColumn">
                    <ItemTemplate>
                        <asp:Label ID="Label9" runat="server" Text='<%# Bind("Description") %>' ForeColor="Black"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Reorder Level" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="gvHeaderColumn">
                    <ItemTemplate>
                        <asp:Label ID="Label10" runat="server" Text='<%# Bind("ReorderLevel") %>' ForeColor="Black"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Available Quantity" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="gvHeaderColumn">
                    <ItemTemplate>
                        <asp:Label ID="Label11" runat="server" Text='<%# Bind("Balance") %>' ForeColor="Black"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Re-order Quantity" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="gvHeaderColumn">
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="ReorderQty" Text='<%# Bind("ReorderQty") %>' Height="38px" Width="80px" ForeColor="Black" CausesValidation="True" CssClass="textboxStyle" AutoPostBack="true" OnTextChanged="orderQtyTxtBx_TextChanged" />
                        <asp:RequiredFieldValidator runat="server" ErrorMessage="Please enter Quantity" ControlToValidate="ReorderQty" Display="Dynamic" ForeColor="Red" />
                        <asp:RegularExpressionValidator runat="server" ErrorMessage="Please Enter digits " ControlToValidate="ReorderQty" ValidationExpression="[0-9]+" Display="Dynamic" ForeColor="Red"></asp:RegularExpressionValidator>
                        <%-- <asp:CustomValidator ID="ReorderQtyVal" runat="server" ErrorMessage="Quantity cannot be less than Reorder Quantity" ValidateEmptyText="true"  Display="Dynamic" ForeColor="Red" ValidationGroup="PurchaseOrderValidationGrp"  OnServerValidate="ReorderQtyValidation"/>--%>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Unit Of Measure" ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="gvHeaderColumn">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="Label12" Text='<%# Bind("UnitOfMeasure") %>' Height="38px" Width="100px" ForeColor="Black" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Supplier" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="gvHeaderColumn">
                    <ItemTemplate>
                        <asp:DropDownList ID="SupplierList" runat="server" Width="250px" ForeColor="Black" OnSelectedIndexChanged="SupplierList_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Price" ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="gvHeaderColumn">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="Price" Text='<%# Bind("Price") %>' Height="38px" Width="100px" ForeColor="Black" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Amount" ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="gvHeaderColumn">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="Amount" Text='<%# Bind("Amount") %>' Height="38px" Width="100px" ForeColor="Black" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
            <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#242121" />
        </asp:GridView>
        <br />
        <br />

    Item Description&nbsp;&nbsp;
    <asp:DropDownList ID="AddNewItemDropDown" runat="server" Height="25px" Width="201px">
    </asp:DropDownList>
    &nbsp;&nbsp;&nbsp;
   <asp:Button ID="Button4" runat="server" Text="Add Item" CssClass="button" OnClick="AddItem_Click" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
   <br />
    <br />
    <br />
    <asp:Label ID="Label16" runat="server" Text="Approver" Font-Size="Medium"></asp:Label>
    &nbsp;&nbsp;&nbsp;&nbsp;       
    <asp:DropDownList ID="supervisorNamesDropDown" runat="server" Height="25px" Width="201px">
    </asp:DropDownList>
    <br />
    <br />
    <br />
    <asp:Button ID="Reset" runat="server" Text="Reset" CssClass="rejectBtn" OnClick="Reset_Click" />
    &nbsp;&nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
    <asp:Button ID="ProceedBtn" runat="server" Text="Buy" CssClass="button" OnClick="ProceedBtn_Click" ValidationGroup="PurchaseOrderValidationGrp" />
    &nbsp;&nbsp;
</asp:Content>


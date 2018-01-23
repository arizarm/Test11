<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PurchaseOrderForm.aspx.cs" Inherits="PurchaseOrderForm" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            color: rgba(118,180,50,1);
            margin-left: 210px;
        }
    </style>
    <style type="text/css">
        .auto-style2 {
            margin-left: 210px;
        }
    </style>
    <style type="text/css">
        .table2style {
            padding-left: 150px;
        }

        .gridviewStyle {
            margin-left: 30px;
        }

        .orderNoStyle {
            padding-left: 405px;
        }

        .auto-style3 {
            margin-left: 40px;
            margin-top: 0px;
        }

        .buttonstyle {
            margin-left: 350px;
        }

        .textboxStyle {
            text-align: center;
        }
        .labelStyle{
            font-weight:bold;
           font-Size:medium;
        }
    </style>

    <link type="text/css" href="css/smoothness/jquery-ui-1.7.1.custom.css" rel="stylesheet" />
    <script src="_scripts/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="_scripts/jquery-ui-1.7.1.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("#txtDate").datepicker();
        });

    </script>
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <br />
        <h2 class="auto-style1">Purchase Order </h2>
        <br />
    </div>
   
    <br />
    <div>
        <asp:Label ID="Label5" runat="server" Text="Deliver to : Logic University Stationery Store"  CssClass="labelStyle" ></asp:Label>
    </div>
    <br />
    <br />
    Please supply the following items by: &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtDate" runat="server" Text=""></asp:TextBox>
    <asp:RequiredFieldValidator runat="server" ErrorMessage="Please enter a date" ControlToValidate="txtDate" ValidationGroup="PurchaseOrderValidationGrp" Display="Dynamic" ForeColor="Red"/>
    <asp:CustomValidator runat="server" ControlToValidate="txtDate" ErrorMessage ="Date cannot be lesser than today." ValidationGroup="PurchaseOrderValidationGrp" Display="Dynamic" ForeColor="Red" OnServerValidate="DateValidator"></asp:CustomValidator>
 

    <div>

        <br />
        <br />
        <asp:GridView ID="gvPurchaseItems" runat="server" AutoGenerateColumns="False" CssClass="mGrid" Height="109px" DataKeyNames="ItemCode" 
            Width="858px" Font-Size="Large" OnRowDataBound="reoderItems_RowDataBound" OnRowCommand="gvreoderItems_RowCommand" 
            OnRowDeleting="gvreoderItems_RowDeleting" >
            <Columns>
                <asp:TemplateField HeaderText="ItemCode">
                    <ItemTemplate>
                        <asp:Label ID="ItemCode" runat="server" Text='<%# Bind("ItemCode") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Font-Size="10pt" />
                    <HeaderStyle Font-Size="11pt" HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle Font-Size="Smaller" HorizontalAlign="Center" Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description">
                    <ItemTemplate>
                        <asp:Label ID="Label9" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="135px" />
                    <HeaderStyle Font-Size="11pt" HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle Font-Size="10pt" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reorder Level">
                    <ItemTemplate>
                        <asp:Label ID="Label10" runat="server" Text='<%# Bind("ReorderLevel") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Size="11pt" HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle Font-Size="10pt" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Available Quantity">
                    <ItemTemplate>
                        <asp:Label ID="Label11" runat="server" Text='<%# Bind("Balance") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Size="11pt" HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle Font-Size="10pt" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Re-order Quantity">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="ReorderQty" Text='<%# Bind("ReorderQty") %>' Height="38px" Width="80px" ForeColor="Black"  CssClass="textboxStyle" />
                    </ItemTemplate>
                    <HeaderStyle Font-Size="11pt" HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle Font-Size="10pt" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Unit Of Measure" ControlStyle-Width="100px">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="Label12" Text='<%# Bind("UnitOfMeasure") %>' Height="38px" Width="100px" ForeColor="Black" />
                    </ItemTemplate>

<ControlStyle Width="100px"></ControlStyle>

                    <HeaderStyle Font-Size="11pt" HorizontalAlign="Center" Wrap="False" VerticalAlign="Middle" />
                    <ItemStyle Font-Size="10pt" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Supplier / Price / Amount">
                    <ItemTemplate>
                        <asp:DropDownList ID="SupplierList" runat="server" Width="270px">
                        </asp:DropDownList>
                    </ItemTemplate>
                    <ControlStyle Width="355px" />
                    <HeaderStyle Font-Size="11pt" HorizontalAlign="Center" Wrap="False" Width="250px" />
                    <ItemStyle Font-Size="10pt" HorizontalAlign="Center" Width="200px" />
                </asp:TemplateField>
                <%--<asp:TemplateField HeaderText="Amount" ControlStyle-Width="100px">                   
                    <ItemTemplate >
                        <asp:Label runat="server" ID="Label12" Text='<%# Bind("Amount") %>' Height="38px" Width="100px" ForeColor="Black" />
                    </ItemTemplate>
                     <HeaderStyle Font-Size="11pt" HorizontalAlign="Center" Wrap="False" VerticalAlign="Middle" />
                     <ItemStyle Font-Size="10pt" HorizontalAlign="Center" />
                </asp:TemplateField> --%>
                <asp:CommandField ShowDeleteButton="true" />

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

    </div>

    Item Description&nbsp;&nbsp;
    <asp:DropDownList ID="AddNewItemDropDown" runat="server" Height="25px" Width="201px">
    </asp:DropDownList>
    &nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button4" runat="server" Text="Add Item" CssClass="button" OnClick="AddItem_Click" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
   <br />
    <br />
    <br />
    <asp:Label ID="Label16" runat="server" Text="Supervisor" Font-Size="Medium"></asp:Label>
    &nbsp;
     <br />
    <br />
    <asp:DropDownList ID="supervisorNamesDropDown" runat="server" Height="25px" Width="201px">
    </asp:DropDownList>
    <br />
    <br />
    <br />
    <asp:Button ID="Reset" runat="server" Text="Reset" Height="35px" Width="93px" CssClass="button" OnClick="Reset_Click" />
    &nbsp;&nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
    <asp:Button ID="ProceedBtn" runat="server" Text="Proceed" Width="93px" Height="35px" CssClass="button" OnClick="ProceedBtn_Click" ValidationGroup="PurchaseOrderValidationGrp"/>
    &nbsp;&nbsp;
</asp:Content>


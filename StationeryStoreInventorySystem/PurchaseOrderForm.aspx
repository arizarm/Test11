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
    <div>
        <asp:Label ID="Label3" runat="server" Text="PO Number : 200947574" Font-Size="Medium" Font-Bold="true"></asp:Label> 
    </div>
    <br />
    <div>
        <asp:Label ID="Label5" runat="server" Text="Deliver to : Logic University Stationery Store" Font-Size="Medium"></asp:Label>
    </div>
    <br />
    <br />
    Please supply the following items by: &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtDate" runat="server"></asp:TextBox>

    </div>
    <div>

        <br />
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="mGrid"  Height="109px" Width="858px" Font-Size="Large">
            <Columns>
                <asp:TemplateField HeaderText="ItemNo">
                    <ItemTemplate>
                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("ItemCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description">
                    <ItemTemplate>
                        <asp:Label ID="Label9" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Re-order Quantity">
                    <ItemTemplate>
                        <asp:Label ID="Label10" runat="server" Text='<%# Bind("CurrentQuantity") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Actual Quantity">
                    <ItemTemplate>
                        <asp:Label ID="Label11" runat="server" Text='<%# Bind("OrderQuantity") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="TextBox3" Text='<%# Bind("OrderQuantity") %>' Height="68px" Width="191px" />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Supplier">
                    <ItemTemplate>
                        <asp:DropDownList ID="DropDownList1" runat="server">
                            <asp:ListItem Text="BANES Shop"></asp:ListItem>
                            <asp:ListItem Text="Alpha"></asp:ListItem>
                            <asp:ListItem Text="CHEP"></asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList4" runat="server">
                            <asp:ListItem Text="BANES Shop"></asp:ListItem>
                            <asp:ListItem Text="Alpha"></asp:ListItem>
                            <asp:ListItem Text="CHEP"></asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Price">
                    <ItemTemplate>
                        <asp:Label ID="Label12" runat="server" Text='<%# Bind("Price") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Amount">
                    <ItemTemplate>
                        <asp:Label ID="Label13" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:ButtonField Text="Delete" />
            </Columns>
        </asp:GridView>
        <br />
        <br />

    </div>

    Item Description&nbsp;&nbsp;
    <asp:DropDownList ID="DropDownList3" runat="server" Height="25px" Width="201px">
        <asp:ListItem Text="Clips Double 1"></asp:ListItem>
        <asp:ListItem Text="File Seperator"></asp:ListItem>
        <asp:ListItem Text="File-Blue with Logo"></asp:ListItem>
    </asp:DropDownList>
    &nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button4" runat="server" Text="Add Item" CssClass="button"/>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
   <br />
    <br />
    <br />
    <asp:Label ID="Label16" runat="server" Text="Supervisor" Font-Size="Medium"></asp:Label>
    &nbsp;
                    <br />
    <br />
    <asp:DropDownList ID="DropDownList2" runat="server" Height="25px" Width="201px">
        <asp:ListItem Text="Tan ah Kow"></asp:ListItem>
        <asp:ListItem Text="Helen Ho"></asp:ListItem>
        <asp:ListItem Text="Peter Tan Ah Menh"></asp:ListItem>
    </asp:DropDownList>
    <br />
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" Text="Reset" Height="35px" Width="93px" CssClass="button"/>
    &nbsp;&nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
    <asp:Button ID="Button3" runat="server" Text="Proceed" Width="93px" Height="35px" CssClass="button"/>
    &nbsp;&nbsp;
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PurchaseOrderDetail.aspx.cs" Inherits="PurchaseOrderDetail" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
           color :  rgba(118,180,50,1); 
           
             margin-left:210px;
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
        .buttonstyle {
            margin-left: 350px;
        }
        </style>
    <script>

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <br />
        <h2 class="auto-style1">
            Purchase Order
            Detail</h2>
       <%-- <asp:Label ID="Label1" runat="server" Text="Purchase Order"  Font-Size="X-Large" class="mainPageHeader"></asp:Label>--%>
    </div>
    <div >
        <br />
    <asp:Label ID="Label16" runat="server" Text="Supervisor:Tan ah Kow" Font-Size="Medium"></asp:Label>
        <asp:Label ID="Label3" runat="server" CssClass="orderNoStyle" Text="PO Number : 200947574" Font-Size="Medium" Font-Bold="true"></asp:Label>
        <br />
    </div>
    <div>
        <table>
            <tr>
                
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Deliver to Logic University" Font-Size="Medium"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;</td>
            </tr>           
            <tr>                             
                <td>
                    <asp:Label ID="Label7" runat="server" Text="Address: 25 Heng Mui Keng Terrace Singapore 119615" Font-Size="14px" />
                    <br /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>              
            </tr>
        </table>

        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="mGrid"  Height="109px" Width="858px" Font-Size="Large">
            <Columns>
                <asp:TemplateField HeaderText="ItemNo">
                    <ItemTemplate>
                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("ItemCode") %>' ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description">
                    <ItemTemplate>
                        <asp:Label ID="Label9" runat="server" Text='<%# Bind("Description") %>' ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Re-order Quantity">
                    <ItemTemplate>
                        <asp:Label ID="Label10" runat="server" Text='<%# Bind("CurrentQuantity") %>' ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Actual Quantity">
                    <ItemTemplate>
                        <asp:Label ID="Label11" runat="server" Text='<%# Bind("OrderQuantity") %>' ></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox  runat="server" ID="TextBox3" Text='<%# Bind("OrderQuantity") %>' Height="68px" Width="191px"/>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Price">
                    <ItemTemplate>
                        <asp:Label ID="Label12" runat="server" Text='<%# Bind("Price") %>' ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Amount">
                    <ItemTemplate>
                        <asp:Label ID="Label13" runat="server" Text='<%# Bind("Amount") %>' ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
         Total Amount:<asp:Label ID="Label17" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
         <asp:Label ID="Label1" runat="server" Text="Label">Remarks :</asp:Label>
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
     &nbsp; &nbsp; &nbsp; 
    <asp:Button ID="btnReject" runat="server" Text="Approve" CssClass="button" />
      
         &nbsp; &nbsp; &nbsp; 
         <asp:Button ID="btnApprove" runat="server" Text="Reject" CssClass="rejectBtn" />
    </div>
</asp:Content>



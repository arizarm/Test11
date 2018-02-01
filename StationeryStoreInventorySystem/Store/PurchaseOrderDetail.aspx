﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PurchaseOrderDetail.aspx.cs" Inherits="PurchaseOrderDetail" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title></title>  
    <style type="text/css">
        .auto-style1 {
            color: rgba(118,180,50,1);
            margin-left: 210px;
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

        .labelStyle {
            font-size: medium;
            font-weight:bold;
             font-size: 10pt;
        }

        .orderInfoStyle {
            width: 1260px;
            height: 10px;
            
        }
        .gvHeaderColumn{           
            text-align:center;
        }
    </style>
    <script>

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

   <div class="row updateDeptHead">
        <h2 class="mainPageHeader">Purchase Order Detail</h2>
    </div>
    <br />
    <br />
    <table>
         <tr >
            <td class="orderInfoStyle">
                <h3 class="labelStyle">Deliver To:
                                <asp:Label ID="Label2" runat="server" Text=" Logic University" CssClass="labelStyle"></asp:Label></h3>
             
            </td>  
            <td class="orderInfoStyle">
                <h3 class="labelStyle">PurchaseOrder ID:
                    <asp:Label ID="OrderID" runat="server" Text="" CssClass="labelStyle"></asp:Label></h3>

            </td>
          </tr>
        <tr>         
            <td class="orderInfoStyle">
                <h3 class="labelStyle">
                    <asp:Label ID="Label3" runat="server" Text="Address: 25 Heng Mui Keng Terrace Singapore 119615" CssClass="labelStyle" />
                </h3>

            </td>
            <td class="orderInfoStyle">
                <h3 class="labelStyle">Supplier Name:<asp:Label ID="SupplierName" runat="server" Text=" " CssClass="labelStyle"></asp:Label></h3>

            </td>
        </tr>                       
        <tr>
            <td class="orderInfoStyle">
                <h3 class="labelStyle">Supervisor Name:<asp:Label ID="supervisorName" runat="server" Text="" CssClass="labelStyle"></asp:Label></h3>

            </td>
        </tr>      
    </table>
   
    <br />
    <br />
    <asp:GridView ID="gvPurchaseDetail" runat="server" AutoGenerateColumns="False" Width="60%" RowStyle-Height="50px" 
        EmptyDataText="No items haven been ordered for this order" OnRowEditing="gvPurchaseDetail_RowEditing"
        OnRowUpdating="gvPurchaseDetail_RowUpdating" OnRowCancelingEdit="gvPurchaseDetail_RowCancelingEdit">
        <Columns>
            <asp:TemplateField HeaderText="ItemCode" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="gvHeaderColumn">
                <HeaderStyle Width="90px" />
                <ItemTemplate>
                    <asp:Label ID="ItemCode" runat="server" Text='<%# Bind("ItemCode") %>' Width="90px" ForeColor="Black"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Description" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-CssClass="gvHeaderColumn">
               <HeaderStyle Width="350px" />
                 <ItemTemplate>
                    <asp:Label ID="Description" runat="server" Text='<%# Bind("Description") %>' Width="250px" ForeColor="Black"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Order Quantity" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-CssClass="gvHeaderColumn">
                <HeaderStyle Width="170px" />
                <ItemTemplate>
                    <asp:Label ID="orderQtyLbl" runat="server"  Width="170px" Text='<%# Bind("OrderQty") %>' ForeColor="Black"></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="orderQtyTxtBx" runat="server" Width="60px" Text='<%# Bind("OrderQty") %>' MaxLength="4" ForeColor="Black" OnTextChanged="orderQtyTxtBx_TextChanged"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ErrorMessage="Cannot be blank" Display="Dynamic" ForeColor="Red" ControlToValidate="orderQtyTxtBx" ValidationGroup="PurchaseDetailGrp" />
                    <asp:RegularExpressionValidator runat="server" ErrorMessage="Invalid.Please enter only the digits" ForeColor="Red" ControlToValidate="orderQtyTxtBx" ValidationExpression="^[0-9]+$" ValidationGroup="PurchaseDetailGrp" />
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Price" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-CssClass="gvHeaderColumn">
                <HeaderStyle Width="6px" />
                <ItemTemplate>
                    <asp:Label ID="Price" runat="server" Text='<%# Bind("FormattedPrice") %>' Width="60px" ForeColor="Black"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-CssClass="gvHeaderColumn">
                <HeaderStyle Width="70px" />
                <ItemTemplate>
                    <asp:Label ID="Amount" runat="server" Text='<%# Bind("FormattedAmount") %>' Width="70px" ForeColor="Black"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-CssClass="gvHeaderColumn">
                <HeaderStyle Width="130px" />                
                <ItemTemplate>
                    <asp:Button ID="btn_Edit" runat="server" Text="Edit" CommandName="Edit" CssClass="gridviewEditbutton"  Width="130px"/>
                </ItemTemplate>
                <EditItemTemplate>
                    
                    <asp:Button ID="btn_Update" runat="server" Text="Update" CommandName="Update" ValidationGroup="PurchaseDetailGrp" Width="130px" CssClass="gridviewEditbutton" />
                    <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" CommandName="Cancel" CssClass="gridviewEditbutton"  Width="130px" />
                </EditItemTemplate>               
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
    <h3 class="labelStyle">Total Amount:
                    <asp:Label ID="TotalAmount" runat="server" Text=""></asp:Label></h3>

    <br />
     <h3 class="labelStyle">Status:
                    <asp:Label ID="orderStatus" runat="server" Text=""></asp:Label></h3>
    <br />
    <br />
    <asp:Label ID="RemarkLbl" runat="server" Text="Remarks :"></asp:Label>
    <asp:TextBox ID="RemarkTxtBx" runat="server"></asp:TextBox>
    &nbsp; &nbsp; &nbsp; 
                <asp:Button ID="ApproveBtn" runat="server" Text="Approve" CssClass="button" OnClick="ApproveBtn_Click" ValidationGroup="ApproveValidationGrp" />
    &nbsp; &nbsp; &nbsp; 
                 <asp:Button ID="RejectBtn" runat="server" Text="Reject" CssClass="rejectBtn" OnClick="RejectBtn_Click" ValidationGroup="ApproveValidationGrp" />
    <br />
    <asp:RegularExpressionValidator runat="server" ValidationExpression="^[a-zA-Z'.-_ ]{1,100}$" ControlToValidate="RemarkTxtBx" ErrorMessage="Invalid input.Please enter alphabets only" ForeColor="Red" ValidationGroup="ApproveValidationGrp" Display="Dynamic"></asp:RegularExpressionValidator>

    <br />
    <br />
    <asp:Label ID="deliveryLbl" runat="server" Text="Deliver Order No:"></asp:Label>
    &nbsp;&nbsp;
                 <asp:TextBox ID="DeliveryOrderIDTxtBx" runat="server"></asp:TextBox>
    <br />
    <asp:RegularExpressionValidator runat="server" ControlToValidate="DeliveryOrderIDTxtBx" ErrorMessage="Only letter and digits allowed" ValidationGroup="DeliveryOrderValidationGrp" Display="Dynamic" ForeColor="Red" ValidationExpression="^[a-zA-Z0-9]+$"></asp:RegularExpressionValidator>
    <asp:RequiredFieldValidator runat="server" ControlToValidate="DeliveryOrderIDTxtBx" ErrorMessage="Please enter DeliveryOrder No" Display="Dynamic" ValidationGroup="DeliveryOrderValidationGrp" ForeColor="Red" />
    <br />
    <br />
    <asp:Button ID="CloseOrderBtn" runat="server" Text="Close Purchase Order" CssClass="button" OnClick="CloseOrderBtn_Click" ValidationGroup="DeliveryOrderValidationGrp" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
     <asp:Button ID="BackBtn" runat="server" Text="Back" CssClass="button" OnClick="BackBtn_Click"  />
</asp:Content>



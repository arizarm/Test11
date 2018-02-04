<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PurchaseOrderDetail.aspx.cs" Inherits="PurchaseOrderDetail" %>


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
                                <asp:Label ID="lblDeliver" runat="server" Text=" Logic University" CssClass="labelStyle"></asp:Label></h3>
             
            </td>  
            <td class="orderInfoStyle">
                <h3 class="labelStyle">PurchaseOrder ID:
                    <asp:Label ID="lblOrderID" runat="server" Text="" CssClass="labelStyle"></asp:Label></h3>

            </td>
          </tr>
        <tr>         
            <td class="orderInfoStyle">
                <h3 class="labelStyle">
                    <asp:Label ID="lblAddress" runat="server" Text="Address: 25 Heng Mui Keng Terrace Singapore 119615" CssClass="labelStyle" />
                </h3>

            </td>
            <td class="orderInfoStyle">
                <h3 class="labelStyle">Supplier Name:<asp:Label ID="lblSupplierName" runat="server" Text=" " CssClass="labelStyle"></asp:Label></h3>

            </td>
        </tr>                       
        <tr>
            <td class="orderInfoStyle">
                <h3 class="labelStyle">Supervisor Name:<asp:Label ID="lblsupervisorName" runat="server" Text="" CssClass="labelStyle"></asp:Label></h3>

            </td>
        </tr>      
    </table>
   
    <br />
    <br />
    <asp:GridView ID="GvPurchaseDetail" runat="server" AutoGenerateColumns="False" RowStyle-Height="50px" CssClass="mGrid60percent"
        EmptyDataText="No items haven been ordered for this order" OnRowEditing="GvPurchaseDetail_RowEditing"
        OnRowUpdating="GvPurchaseDetail_RowUpdating" OnRowCancelingEdit="GvPurchaseDetail_RowCancelingEdit">
        <Columns>
            <asp:TemplateField HeaderText="ItemCode" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="gvHeaderColumn">
                <HeaderStyle Width="90px" />
                <ItemTemplate>
                    <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("ItemCode") %>' Width="90px" ForeColor="Black"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Description" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-CssClass="gvHeaderColumn">
               <HeaderStyle Width="350px" />
                 <ItemTemplate>
                    <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Description") %>' Width="250px" ForeColor="Black"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Order Quantity" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-CssClass="gvHeaderColumn">
                <HeaderStyle Width="170px" />
                <ItemTemplate>
                    <asp:Label ID="lblorderQty" runat="server"  Width="170px" Text='<%# Bind("OrderQty") %>' ForeColor="Black"></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtorderQty" runat="server" Width="60px" Text='<%# Bind("OrderQty") %>' MaxLength="4" ForeColor="Black" OnTextChanged="OrderQtyTxtBx_TextChanged"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ErrorMessage="Cannot be blank" Display="Dynamic" ForeColor="Red" ControlToValidate="txtorderQty" ValidationGroup="PurchaseDetailGrp" />
                    <asp:RegularExpressionValidator runat="server" ErrorMessage="Invalid.Please enter only the digits" ForeColor="Red" ControlToValidate="txtorderQty" ValidationExpression="^[0-9]+$" ValidationGroup="PurchaseDetailGrp" />
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Price" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-CssClass="gvHeaderColumn">
                <HeaderStyle Width="6px" />
                <ItemTemplate>
                    <asp:Label ID="lblPrice" runat="server" Text='<%# Bind("FormattedPrice") %>' Width="60px" ForeColor="Black"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-CssClass="gvHeaderColumn">
                <HeaderStyle Width="70px" />
                <ItemTemplate>
                    <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("FormattedAmount") %>' Width="70px" ForeColor="Black"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-CssClass="gvHeaderColumn">
                <HeaderStyle Width="130px" />                
                <ItemTemplate>
                    <asp:Button ID="BtnEdit" runat="server" Text="Edit" CommandName="Edit" CssClass="gridviewEditbutton"  Width="130px"/>
                </ItemTemplate>
                <EditItemTemplate>
                    
                    <asp:Button ID="BtnUpdate" runat="server" Text="Update" CommandName="Update" ValidationGroup="PurchaseDetailGrp" Width="130px" CssClass="gridviewEditbutton" />
                    <asp:Button ID="BtnCancel" runat="server" Text="Cancel" CommandName="Cancel" CssClass="gridviewEditbutton"  Width="130px" />
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
                    <asp:Label ID="lblTotalAmount" runat="server" Text=""></asp:Label></h3>

    <br />
     <h3 class="labelStyle">Status:
                    <asp:Label ID="lblorderStatus" runat="server" Text=""></asp:Label></h3>
    <br />
    <br />
    <asp:Label ID="lblRemark" runat="server" Text="Remarks :"></asp:Label>
    <asp:TextBox ID="txtRemark" runat="server"></asp:TextBox>
    &nbsp; &nbsp; &nbsp; 
                <asp:Button ID="BtnApprove" runat="server" Text="Approve" CssClass="button" OnClick="BtnApprove_Click" ValidationGroup="ApproveValidationGrp" />
    &nbsp; &nbsp; &nbsp; 
                 <asp:Button ID="BtnReject" runat="server" Text="Reject" CssClass="rejectBtn" OnClick="BtnReject_Click" ValidationGroup="ApproveValidationGrp" />
    <br />
    <asp:RegularExpressionValidator runat="server" ValidationExpression="^[a-zA-Z'.-_ ]{1,100}$" ControlToValidate="txtRemark" ErrorMessage="Invalid input.Please enter alphabets only" ForeColor="Red" ValidationGroup="ApproveValidationGrp" Display="Dynamic"></asp:RegularExpressionValidator>

    <br />
    <br />
    <asp:Label ID="lbldelivery" runat="server" Text="Deliver Order No:"></asp:Label>
    &nbsp;&nbsp;
                 <asp:TextBox ID="txtDeliveryOrderID" runat="server"></asp:TextBox>
    <br />
    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtDeliveryOrderID" ErrorMessage="Only letter and digits allowed" ValidationGroup="DeliveryOrderValidationGrp" Display="Dynamic" ForeColor="Red" ValidationExpression="^[a-zA-Z0-9]+$"></asp:RegularExpressionValidator>
    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDeliveryOrderID" ErrorMessage="Please enter DeliveryOrder No" Display="Dynamic" ValidationGroup="DeliveryOrderValidationGrp" ForeColor="Red" />
    <br />
    <br />
    <asp:Button ID="BtnCloseOrder" runat="server" Text="Close Purchase Order" CssClass="button" OnClick="BtnCloseOrder_Click" ValidationGroup="DeliveryOrderValidationGrp" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
     <asp:Button ID="BtnBack" runat="server" Text="Back" CssClass="button" OnClick="BtnBack_Click"  />
</asp:Content>



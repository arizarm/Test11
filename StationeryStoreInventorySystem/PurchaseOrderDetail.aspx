<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PurchaseOrderDetail.aspx.cs" Inherits="PurchaseOrderDetail" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            color: rgba(118,180,50,1);
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

        .buttonstyle {
            margin-left: 350px;
        }

        .labelStyle {
            font-size: medium;
            font-weight: bold;
        }
    </style>
    <script>

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
       
            <div>
                <br />
                <h2 class="auto-style1">Purchase Order   Detail</h2>

            </div>
            <div>
                <br />
                <h3 class="labelStyle">Supervisor Name:<asp:Label ID="SupervisorName" runat="server" Text="" CssClass="labelStyle"></asp:Label></h3>

                <h3 class="labelStyle">PurchaseOrder ID:
                    <asp:Label ID="OrderID" runat="server" Text="" CssClass="labelStyle"></asp:Label></h3>

            </div>
            <div>
                <table>
                    <tr>

                        <td>
                            <h3 class="labelStyle">Deliver To:
                                <asp:Label ID="Label3" runat="server" Text="Deliver to Logic University" CssClass="labelStyle"></asp:Label></h3>

                            <h3 class="labelStyle">Supplier Name:<asp:Label ID="SuplierName" runat="server" Text="" CssClass="labelStyle"></asp:Label></h3>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h3 class="labelStyle">Address:
                                <asp:Label ID="Label6" runat="server" Text="Address: 25 Heng Mui Keng Terrace Singapore 119615" CssClass="labelStyle" /></h3>
                            <br />
                        </td>
                    </tr>
                </table>

                <br />
                <asp:GridView ID="gvPurchaseDetail" runat="server" AutoGenerateColumns="False" CssClass="mGrid" Height="109px" Width="858px" 
                    EmptyDataText="No items haven been ordered for this order" Font-Size="Large"  OnRowEditing="gvPurchaseDetail_RowEditing"
                    OnRowUpdating="gvPurchaseDetail_RowUpdating" OnRowCancelingEdit="gvPurchaseDetail_RowCancelingEdit">
                    <Columns>
                        <asp:TemplateField HeaderText="ItemNo">
                            <ItemTemplate>
                                <asp:Label ID="ItemCode" runat="server" Text='<%# Bind("ItemCode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description">
                            <ItemTemplate>
                                <asp:Label ID="Description" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>                      
                        <asp:TemplateField HeaderText="Order Quantity">                            
                            <ItemTemplate>
                                <asp:Label ID="orderQtyLbl" runat="server" Height="38px" Width="80px" Text='<%# Bind("OrderQty") %>' ></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="orderQtyTxtBx" runat="server" Height="38px" Width="80px" Text='<%# Bind("OrderQty") %>' OnTextChanged="orderQtyTxtBx_TextChanged"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ErrorMessage="Cannot be blank" Display="Dynamic" ForeColor="Red" ControlToValidate="orderQtyTxtBx" ValidationGroup="PurchaseDetailGrp"/>
                                <asp:RegularExpressionValidator runat ="server" ErrorMessage="Invalid.Please enter only the digits" ForeColor="Red" ControlToValidate="orderQtyTxtBx" ValidationExpression="^[0-9]" ValidationGroup="PurchaseDetailGrp" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Price">
                            <ItemTemplate>
                                <asp:Label ID="Price" runat="server" Text='<%# Bind("FormattedPrice") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount">
                            <ItemTemplate>
                                <asp:Label ID="Amount" runat="server" Text='<%# Bind("FormattedAmount") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>  
                            <ItemTemplate>  
                                <asp:Button ID="btn_Edit" runat="server" Text="Edit" CommandName="Edit" />  
                            </ItemTemplate>  
                            <EditItemTemplate>  
                                <asp:Button ID="btn_Update" runat="server" Text="Update" CommandName="Update" ValidationGroup="PurchaseDetailGrp"/>  
                                <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" CommandName="Cancel"/>  
                            </EditItemTemplate>  
                            <ControlStyle Width="85px" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField> 
                    </Columns>
                </asp:GridView>
                <br />
                <h3 class="labelStyle">Total Amount:
                    <asp:Label ID="TotalAmount" runat="server" Text=""></asp:Label></h3>
               
                <br />
                <br />
                <br />                
                <asp:Label ID="RemarkLbl" runat="server" Text="Remarks :" ></asp:Label>
                <asp:TextBox ID="RemarkTxtBx" runat="server" ></asp:TextBox>    
                &nbsp; &nbsp; &nbsp; 
                <asp:Button ID="ApproveBtn" runat="server" Text="Approve" CssClass="button" OnClick="ApproveBtn_Click" ValidationGroup="ApproveValidationGrp"/>
                &nbsp; &nbsp; &nbsp; 
                 <asp:Button ID="RejectBtn" runat="server" Text="Reject" CssClass="rejectBtn" OnClick="RejectBtn_Click"  ValidationGroup="ApproveValidationGrp"/> 
                <br />         
                <asp:RegularExpressionValidator runat ="server" ValidationExpression="^[a-zA-Z'.]{1,100}$" ControlToValidate="RemarkTxtBx" ErrorMessage="Invalid input.Please enter alphabets only" ForeColor="Red" ValidationGroup="ApproveValidationGrp" Display="Dynamic"></asp:RegularExpressionValidator>               
                
                <br />
                <br />
                Deliver Order No:
                                 &nbsp;&nbsp;
                 <asp:TextBox ID="DeliveryOrderIDTxtBx" runat="server" ></asp:TextBox>
                <br />
                <asp:RegularExpressionValidator runat="server" ControlToValidate="DeliveryOrderIDTxtBx" ErrorMessage="Only letter and digits allowed" ValidationGroup="DeliveryOrderValidationGrp"  Display="Dynamic"  ForeColor="Red" ValidationExpression="^[a-zA-Z0-9]+$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="DeliveryOrderIDTxtBx" ErrorMessage="Please enter DeliveryOrder No"  Display="Dynamic" ValidationGroup="DeliveryOrderValidationGrp"  ForeColor="Red"/>
                <br />
                <br />
                <asp:Button ID="CloseOrderBtn" runat="server" Text="Close Purchase Order" CssClass="button" OnClick="CloseOrderBtn_Click" ValidationGroup="DeliveryOrderValidationGrp" />
            </div>



       


</asp:Content>



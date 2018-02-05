<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PurchaseOrderList.aspx.cs" Inherits="PurchaseOrderList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   <%--AUTHOR : KIRUTHIKA VENKATESH--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row updateDeptHead">
        <h2 class="mainPageHeader">Purchase Order List</h2>
    </div>
    <div>
           <asp:TextBox ID="txtSearch" runat="server" Width="311px" MaxLength="10"></asp:TextBox>   
          <%--<asp:RegularExpressionValidator runat="server" ControlToValidate ="SearchTxtBx" ErrorMessage="Invalid PurchaseOrder No" ValidationGroup="SearchValidationGrp" ValidationExpression="^[0-9]*$" ForeColor="Red" Display="Dynamic" />--%>
           <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="SearchTxtBx" ErrorMessage="Please enter the PurchaseOrder No" ForeColor="Red" ValidationGroup="SearchValidationGrp" Display="Dynamic" />--%>        
           <asp:Button ID="BtnSearch" runat="server" Text="Search" OnClick="BtnSearch_Click" ValidationGroup="SearchValidationGrp" CssClass="buttonformid"/>
           <asp:Button ID="BtnDisplayAll" runat="server" Text="Display All" OnClick="BtnDisplayAll_Click" CssClass="buttonformid"/>
            &nbsp;&nbsp;&nbsp;
         
           Filter Status by :&nbsp;&nbsp;<asp:DropDownList ID="ddlOrderStatus" runat="server" OnSelectedIndexChanged="OrderStatusDrpdwn_SelectedIndexChanged" AutoPostBack="true" >             
                 <asp:ListItem Selected="True">--Select Status--</asp:ListItem>
                <asp:ListItem>Pending</asp:ListItem>
                <asp:ListItem>Approved</asp:ListItem>
                <asp:ListItem>Rejected</asp:ListItem>
                <asp:ListItem>Closed</asp:ListItem>
            </asp:DropDownList>
        
    </div>
 
    <div>
        <asp:GridView ID="GvPurchaseOrder" runat="server" CssClass="mGrid" EmptyDataText="No records found"  RowStyle-Height="50px" 
            EmptyDataRowStyle-BackColor="Window" AutoGenerateColumns="False" PageSize="15"  AllowPaging="True" DataKeyNames="PurchaseOrderID"
            OnPageIndexChanging="GvPurchaseOrder_PageIndexChanging"  OnRowDataBound="GvPurchaseOrder_RowDataBound" 
        >
            <Columns>
                
            <asp:TemplateField HeaderText="Date">
                   <%-- <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    </EditItemTemplate>--%>
                    <ItemTemplate>
                         
                        <asp:Label ID="lblorderDate" runat="server" Text='<%# Bind("OrderDate","{0:MM/dd/yyyy}") %>' Font-Bold="true" Width="180px"></asp:Label>
                    </ItemTemplate> 
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Purchase Order">
                    <%--<EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </EditItemTemplate>--%>
                    <ItemTemplate>
                        <asp:LinkButton ID="LbtnPurchaseOrderID" runat="server" Text='<%# Eval("PurchaseOrderID") %>' CommandArgument='<%# Eval("PurchaseOrderID") %>' Width="100px" OnClick="LbtnPurchaseOrderID_Click" Font-Bold="true"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

                
                <asp:TemplateField HeaderText="Requested by">
                    <%--<EditItemTemplate>
                        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                    </EditItemTemplate>--%>
                    <ItemTemplate>
                        <asp:Label ID="lblReqstdBy" runat="server" Text='<%# Bind("Employee1.EmpName") %>' Font-Bold="true" Width="120px"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Supplier">
                    <%--<EditItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                    </EditItemTemplate>--%>
                    <ItemTemplate>
                        <asp:Label ID="lblSupplierCode" runat="server" Text='<%# Bind("Supplier.SupplierName") %>' Font-Bold="true" Width="250px" ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Status">
                   <%-- <EditItemTemplate>
                        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                    </EditItemTemplate>--%>
                    <ItemTemplate>
                        <asp:Label ID="lblOrderStatus" runat="server" Text='<%# Bind("Status") %>' Font-Bold="true" Width="140px" ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Delete" >
                   <%-- <EditItemTemplate>
                        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                    </EditItemTemplate>--%>
                    <ItemTemplate >
                        <asp:Button ID="BtnDelete" runat="server" Text="Delete" CommandName="Delete"  OnClick="BtnDelete_Click" OnClientClick="return confirm('Are you sure you want to delete this item?');" CssClass="deletebutton" CommandArgument='<%#Eval("PurchaseOrderID")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>


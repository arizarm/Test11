﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PurchaseOrderList.aspx.cs" Inherits="PurchaseOrderList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <h3>Purchase Orders</h3>
           <asp:TextBox ID="SearchTxtBx" runat="server" Width="311px" placeholder="Search by Purchase Order" ></asp:TextBox>           
           <asp:Button ID="SearchBtn" runat="server" Text="Search" OnClick="SearchBtn_Click" ValidationGroup="SearchValidationGrp" CssClass="buttonformid"/>
           <asp:Button ID="DisplayAllBtn" runat="server" Text="Display All" OnClick="DisplayAllBtn_Click" CssClass="buttonformid"/>
            <br />
           <asp:RegularExpressionValidator runat="server" ControlToValidate ="SearchTxtBx" ErrorMessage="Invalid PurchaseOrder No" ValidationGroup="SearchValidationGrp" ValidationExpression="^[0-9]*$" ForeColor="Red" Display="Dynamic" />
           <asp:RequiredFieldValidator runat="server" ControlToValidate="SearchTxtBx" ErrorMessage="Please enter the PurchaseOrder No" ForeColor="Red" ValidationGroup="SearchValidationGrp" Display="Dynamic" />
          

    </div>
    <div>
        <asp:DropDownList ID="OrderStatusDrpdwn" runat="server" CssClass="pull-right" OnSelectedIndexChanged="OrderStatusDrpdwn_SelectedIndexChanged" AutoPostBack="true" Width="96px">             
             <asp:ListItem Selected="True">Pending</asp:ListItem>
            <asp:ListItem>Approved</asp:ListItem>
            <asp:ListItem>Rejected</asp:ListItem>
            <asp:ListItem>Closed</asp:ListItem>
        </asp:DropDownList>
    </div>
    <div>
        <asp:GridView ID="gvPurchaseOrder" runat="server" CssClass="mGrid" EmptyDataText="PurchaseOrder No not found"  
            EmptyDataRowStyle-BackColor="Window" AutoGenerateColumns="False" PageSize="10"  AllowPaging="True" DataKeyNames="PurchaseOrderID"
            OnPageIndexChanging="gvPurchaseOrder_PageIndexChanging"  OnRowDataBound="gvPurchaseOrder_RowDataBound" 
        >
            <Columns>
                
            <asp:TemplateField HeaderText="Date">
                   <%-- <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    </EditItemTemplate>--%>
                    <ItemTemplate>
                         
                        <asp:Label ID="orderDate" runat="server" Text='<%# Bind("OrderDate") %>' Font-Bold="true" Width="180px"></asp:Label>
                    </ItemTemplate> 
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Purchase Order">
                    <%--<EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </EditItemTemplate>--%>
                    <ItemTemplate>
                        <asp:LinkButton ID="purchaseDetailLinkBtn" runat="server" Text='<%# Eval("PurchaseOrderID") %>' CommandArgument='<%# Eval("PurchaseOrderID") %>' Width="100px" OnClick="purchaseDetailLinkBtn_Click" Font-Bold="true"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

                
                <asp:TemplateField HeaderText="Requested by">
                    <%--<EditItemTemplate>
                        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                    </EditItemTemplate>--%>
                    <ItemTemplate>
                        <asp:Label ID="ReqstdBy" runat="server" Text='<%# Bind("Employee.EmpName") %>' Font-Bold="true" Width="120px"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Supplier">
                    <%--<EditItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                    </EditItemTemplate>--%>
                    <ItemTemplate>
                        <asp:Label ID="SupplierCode" runat="server" Text='<%# Bind("Supplier.SupplierName") %>' Font-Bold="true" Width="250px" ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Status">
                   <%-- <EditItemTemplate>
                        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                    </EditItemTemplate>--%>
                    <ItemTemplate>
                        <asp:Label ID="OrderStatus" runat="server" Text='<%# Bind("Status") %>' Font-Bold="true" Width="140px" ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Delete" >
                   <%-- <EditItemTemplate>
                        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                    </EditItemTemplate>--%>
                    <ItemTemplate >
                        <asp:Button ID="btn_Delete" runat="server" Text="Delete" CommandName="Delete"  OnClick="btn_Delete_Click" CssClass="deletebutton" CommandArgument='<%#Eval("PurchaseOrderID")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>


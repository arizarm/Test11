﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReqisitionListDepartment.aspx.cs" Inherits="ReqisitionListEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div>
        <h2 class="mainPageHeader">Requisition List</h2>
           <asp:TextBox ID="SearchBox" runat="server" Width="311px"></asp:TextBox>
           <asp:Button ID="SearchBtn" runat="server" Text="Search"/>
           <asp:Button ID="Display" runat="server" Text="Display All" />

    </div>
    <div>
        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="align-right" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True" >
            <asp:ListItem Selected="True" Value="Pending">Pending Approval</asp:ListItem>
            <asp:ListItem  Value="Approved">Approved</asp:ListItem>
            <asp:ListItem  Value="Rejected">Rejected</asp:ListItem>
            <asp:ListItem  Value="Closed">Closed</asp:ListItem>
        </asp:DropDownList>
    </div>
    <div>
        <asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="False" 
DataKeyNames="RequisitionID" >
             <Columns>  
                       
                 <asp:TemplateField HeaderText="RequestDate">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton2" runat="server" Text='<%# Bind("RequestDate") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>


                 <asp:TemplateField HeaderText="RequisitionID">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton2" runat="server" Text='<%# Bind("RequisitionID") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                  
                <asp:TemplateField HeaderText="RequestedBy">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton2" runat="server" Text='<%# Bind("RequestedBy") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

                
                <asp:TemplateField HeaderText="Status">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                   <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>    
                 
                  <asp:HyperLinkField HeaderText="View Details" DataNavigateUrlFields="RequisitionID" 
                      DataNavigateUrlFormatString="RequisitionDetailsEmployee.aspx?id={0}" Text="View Details"/>            

                
            </Columns>

        </asp:GridView>
    </div>
</asp:Content>



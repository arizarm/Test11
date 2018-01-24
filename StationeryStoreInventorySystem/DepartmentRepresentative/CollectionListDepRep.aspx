﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CollectionListDepRep.aspx.cs" Inherits="ReqisitionListEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <h2 class="mainPageHeader">Requisition List</h2>
      <asp:TextBox ID="SearchBox" runat="server" Width="311px"></asp:TextBox>
           <asp:button ID="SearchBtn" runat="server" Text="Search"  CssClass="button" OnClick="SearchBtn_Click"/>
           <asp:button ID="DisplayBtn" runat="server" Text="Display All" CssClass="button" OnClick="DisplayBtn_Click"/>
    </div>
    <div>
        <h3>All Approved Requisition list</h3>
        <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
            DataKeyNames="RequisitionNo" CssClass="mGrid">
            <Columns>

                <asp:TemplateField HeaderText="RequestDate">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Date") %>'></asp:Label>

                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="RequestedBy">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("EmployeeName") %>'></asp:Label>

                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Status">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:HyperLinkField HeaderText="Reject/Approve" DataNavigateUrlFields="RequisitionNo"
                    DataNavigateUrlFormatString="ApproveRequisition.aspx?id={0}" Text="Details"/>
            </Columns>

        </asp:GridView>
        <hr />
        <div>
        <asp:Label ID="Label2" runat="server" Text="To View Your Requested Requisition by Status : "></asp:Label>
        <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
            <asp:ListItem Selected="True" >Select Status</asp:ListItem>
            <asp:ListItem Value="Pending">Pending Approval</asp:ListItem>
            <asp:ListItem Value="Approved">Approved</asp:ListItem>
            <asp:ListItem Value="Rejected">Rejected</asp:ListItem>
            <asp:ListItem Value="Closed">Closed</asp:ListItem>
        </asp:DropDownList>
        </div>      
         <h3>Your Requested Requisition list</h3>
         <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
         <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False"
            DataKeyNames="RequisitionNo" CssClass="mGrid">
            <Columns>

                <asp:TemplateField HeaderText="RequestDate">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Date") %>'></asp:Label>

                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="RequestedBy">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("EmployeeName") %>'></asp:Label>

                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Status">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:HyperLinkField HeaderText="View" DataNavigateUrlFields="RequisitionNo"
                    DataNavigateUrlFormatString="ApproveRequisition.aspx?id={0}" Text="Approve/Reject"/>
            </Columns>

        </asp:GridView>
         <hr />
        <h3>All Short fall Requisition list</h3>
        <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label>
        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False"
            DataKeyNames="RequisitionNo" CssClass="mGrid">
            <Columns>

                <asp:TemplateField HeaderText="RequestDate">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Date") %>'></asp:Label>

                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="RequestedBy">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("EmployeeName") %>'></asp:Label>

                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Status">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:HyperLinkField HeaderText="Reject/Approve" DataNavigateUrlFields="RequisitionNo"
                    DataNavigateUrlFormatString="ApproveRequisition.aspx?id={0}" Text="Details"/>
            </Columns>

        </asp:GridView>
    </div>
</asp:Content>


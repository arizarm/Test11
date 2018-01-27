﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CollectionListDepRep.aspx.cs" Inherits="ReqisitionListEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <h2 class="mainPageHeader">Collection List</h2>
      <asp:TextBox ID="SearchBox" runat="server" Width="311px"></asp:TextBox>
           <asp:button ID="SearchBtn" runat="server" Text="Search"  CssClass="button" OnClick="SearchBtn_Click"/>
           <asp:button ID="DisplayBtn" runat="server" Text="Display All" CssClass="button" OnClick="DisplayBtn_Click"/>
    </div>
    <div>
        <h3>All Collection list</h3>
        <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
            DataKeyNames="RequisitionNo" CssClass="mGrid" Width="60%" >
            <Columns>

                <asp:TemplateField HeaderText="RequestDate">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Date") %>'></asp:Label>

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
    </div>
</asp:Content>



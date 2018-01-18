<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RegenerateRequest.aspx.cs" Inherits="RegenerateRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
     <h2 class="mainPageHeader">
       Regenerate Requisition 
    </h2>
    <h3>Shortfall Item List</h3>
    <asp:Label ID="Label2" runat="server" Text="Requested Date : 12 Jan 2018 <br /> Department : Register Department"></asp:Label> <br /><br />

    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="mGrid" >
            <Columns>                
                <asp:TemplateField HeaderText="###">
                    <ItemTemplate>
                       <asp:CheckBox ID="CheckBox1" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>                
                <asp:TemplateField HeaderText="Item Description">                                   
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("description") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Shortfall Quantity">                                   
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("quantity") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
             </Columns>
        </asp:GridView>
    <asp:Button ID="Button1" runat="server" Text="Generate Requisition" CssClass="button"/>
</asp:Content>


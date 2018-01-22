<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StationaryCatalogueEmployee.aspx.cs" Inherits="StationaryCatalogueEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <h2 class="mainPageHeader">Stationary Catalogue List</h2>
    <br />
         <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
            <Columns>                
                
                <asp:TemplateField HeaderText="Description">                                   
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("description") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
               
                <asp:TemplateField HeaderText="Unit Of Measure">                                   
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("unitOfMeasure") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>    
             </Columns>
        </asp:GridView>    
</asp:Content>


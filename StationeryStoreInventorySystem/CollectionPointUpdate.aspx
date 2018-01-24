<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CollectionPointUpdate.aspx.cs" Inherits="CollectionPointUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <script src="Content/JavaScript.js"></script>    
      <h1 class="mainPageHeader">Collection Point</h1>
     <p class="mainPageHeader">&nbsp;</p>
     <p class="mainPageHeader">
         <asp:GridView ID="gvCollectionPoint" runat="server" AutoGenerateColumns="False">
             <Columns>
                 <asp:TemplateField HeaderText="Collection Point">
                     <EditItemTemplate>
                         <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                     </EditItemTemplate>
                     <ItemTemplate>
                         <asp:Label ID="labCollectionPoint" runat="server" Text='<%# Bind("CollectionPoint") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Date">
                     <EditItemTemplate>
                         <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                     </EditItemTemplate>
                     <ItemTemplate>                         
                    <asp:TextBox ID="txtDate" runat="server" ClientIDMode="Static"></asp:TextBox>
                     </ItemTemplate>
                 </asp:TemplateField>


                 <asp:TemplateField HeaderText="Time">
                     <EditItemTemplate>
                         <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                     </EditItemTemplate>
                     <ItemTemplate>
                       <asp:TextBox ID="time" runat="server" Text='<%# Bind("DefaultCollectionTime") %>'></asp:TextBox>
                     </ItemTemplate>
                 </asp:TemplateField>
             </Columns>
         </asp:GridView>
     </p>      
        <asp:Button ID="Submit" runat="server" Text="Submit" CssClass="button" OnClick="Submit_Click" />
</asp:Content>


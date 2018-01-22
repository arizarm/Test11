<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ItemStockCardList.aspx.cs" Inherits="ItemStockCardList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <h2 class="mainPageHeader">Stock Card List</h2>
    <br />
    <h4>Search By Item Code or Name</h4>
      <asp:TextBox ID="SearchBox" runat="server" Width="311px"></asp:TextBox>
           &nbsp;
           <asp:Button ID="SearchBtn" runat="server" Text="Search" OnClick="SearchBtn_Click"/>
           &nbsp;
           <asp:Button ID="Display" runat="server" Text="Display All" OnClick="Display_Click" /> <br /><br />
         <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
            <Columns>              
                <asp:TemplateField HeaderText="Item Code">                                   
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("ItemCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>                 
                <asp:TemplateField HeaderText="Item Description">                                   
                    <ItemTemplate>
                        <asp:HyperLink ID="lnkStockCard" runat="server" NavigateUrl="" Text='<%# Bind("Description") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
               <asp:TemplateField HeaderText="Bin#">                                   
                    <ItemTemplate>
                        <asp:Label ID="lblBin" runat="server" Text='<%# Bind("Bin") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField> 
               
             </Columns>
        </asp:GridView>  
</asp:Content>


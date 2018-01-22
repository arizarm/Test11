<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ItemStockCardList.aspx.cs" Inherits="ItemStockCardList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <h2 class="mainPageHeader">Stock Card List</h2>
    <br />
      <asp:TextBox ID="SearchBox" runat="server" Width="311px"></asp:TextBox>
           &nbsp;
           <asp:Button ID="SearchBtn" runat="server" Text="Search"/>
           &nbsp;
           <asp:Button ID="Display" runat="server" Text="Display All" /> <br /><br />
         <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
            <Columns>              
                <asp:TemplateField HeaderText="Item Code">                                   
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/StockCard.aspx">001</asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>                 
                <asp:TemplateField HeaderText="Item Description">                                   
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='2B'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
               <asp:TemplateField HeaderText="Bin#">                                   
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='001'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField> 
               
             </Columns>
        </asp:GridView>  
</asp:Content>


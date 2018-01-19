<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RetrievalList.aspx.cs" Inherits="RetrievalList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div>
        <h2 class="mainPageHeader">Pending Retrieval List</h2>
     <asp:TextBox ID="SearchBox" runat="server" Width="311px"></asp:TextBox>
           <asp:button ID="SearchBtn" runat="server" Text="Search"  CssClass="button" OnClick="SearchBtn_Click"/>
           <asp:button ID="DisplayBtn" runat="server" Text="Display All" CssClass="button" OnClick="DisplayBtn_Click"/>
    </div>
       <div>        
         <asp:GridView ID="gvReq" runat="server" Width="100%" CssClass="mGrid"  AutoGenerateColumns="False">
            <Columns>
               
            <asp:TemplateField HeaderText="Date">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("RetrievedDate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Retrieval No">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("RetrievalID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                
                <asp:TemplateField HeaderText="Retrieved By">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                   <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("RetrievedBy") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>                

                <asp:TemplateField HeaderText="Detail">
                    <EditItemTemplate>
                        <asp:Button ID="Button1" runat="server" Text="Detail" />
                    </EditItemTemplate>
                   <ItemTemplate>
                       <asp:Button ID="gvDetailBtn" runat="server" OnClick="gvDetailBtn_Click" Text="Detail" />
                    </ItemTemplate>
                </asp:TemplateField>              

                
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>



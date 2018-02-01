<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DisbursementList.aspx.cs" Inherits="DisbursementList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

     <div class="updateDeptHead"><h2 class="mainPageHeader">Pending Disbursement List</h2></div>
        
    <br />

    <asp:Label ID="lblNoPending" runat="server" Visible="false" ForeColor="Red" Font-Size="40px">There is no pending disbursement!</asp:Label>

    <asp:GridView ID="gdvDisbList" runat="server" CssClass="mGrid" RowStyle-Height="50px" AutoGenerateColumns="false" AllowSorting="true" OnSorting="gdvDisbList_Sorting">      
         <Columns>                
                <asp:TemplateField HeaderText="Disbursement No">
                    <ItemTemplate>
                       <asp:Label ID="lbldisbId" runat="server" Text='<%# Bind("disbId") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>                
                <asp:TemplateField HeaderText="Collection Date">                                   
                    <ItemTemplate>
                        <asp:Label ID="lblcollectionDate" runat="server" Text='<%# Bind("collectionDate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Collection Time">                                   
                    <ItemTemplate>
                        <asp:Label ID="lblcollectionTime" runat="server" Text='<%# Bind("collectionTime") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
             <asp:TemplateField HeaderText="Department" SortExpression="depName" >                                   
                    <ItemTemplate>
                        <asp:Label ID="lbldepName" runat="server" Text='<%# Bind("depName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
              <asp:TemplateField HeaderText="Collection Point">                                   
                    <ItemTemplate>
                        <asp:Label ID="lblcollectionPoint" runat="server" Text='<%# Bind("collectionPoint") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
              <asp:TemplateField HeaderText="View">                                   
                    <ItemTemplate>
                        <asp:Button ID="btnDetail" CssClass="alert-success" runat="server" Text="Details" OnClick="btnDetail_Click" />
                    </ItemTemplate>
                </asp:TemplateField>
             </Columns>
    </asp:GridView>
</asp:Content>


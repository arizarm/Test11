<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DepartmentList.aspx.cs" Inherits="DepartmentList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        
         .middlerow {
           background-color:ButtonHighlight;
        }
        .auto-style1 {
            width: 54px;
        }
        .text2			{ font-family: Arial;  font-size  :10pt; color : #000000; }
		.auto-style2 {
            width: 392px;
        }
		</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
        <h2 class="auto-style2">
            <asp:Label ID="Label1" runat="server" Font-Size="Larger" ForeColor="#666666" Text="Department List"></asp:Label>
        </h2>
        <p class="auto-style1">
            <asp:GridView ID="GridViewDept" runat="server" CellPadding="100" ForeColor="Black" GridLines="Horizontal" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" Height="237px" Width="1037px">
                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                <SortedDescendingHeaderStyle BackColor="#242121" />
            </asp:GridView>
        </p>
  
    <p>
        
        &nbsp;</p>
    <p>
        
    
</asp:Content>


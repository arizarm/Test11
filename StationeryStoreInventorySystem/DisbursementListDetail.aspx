<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DisbursementListDetail.aspx.cs" Inherits="DisbursementListDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <h2 class="mainPageHeader">Details Disbursement List</h2>

                Date : <asp:Label ID="lblDate" runat="server" Text="Label"></asp:Label><br /><br />

                Time : <asp:Label ID="lblTime" runat="server" Text="Label"></asp:Label><br /><br />

                Department : <asp:Label ID="lblDepartment" runat="server" Text="Label"></asp:Label><br /><br />

                Collection Point : <asp:Label ID="lblColPoint" runat="server" Text="Label"></asp:Label><br /><br /><br />


    <asp:GridView ID="gvDisbDetail" runat="server" CssClass="mGrid"></asp:GridView>

    <asp:GridView ID="gvTestDisb" runat="server" CssClass="mGrid"></asp:GridView>

     <asp:Table ID="Table1" runat="server" Width="591px">
        <asp:TableHeaderRow>
            <asp:TableHeaderCell ColumnSpan="3" Font-Bold="true">
                <h2 class="auto-style1">
                     Details Disbursement List 
                 </h2>

                Date : <br /><br />

                Time : <br /><br />

                Department : <br /><br />

                Collection Point : <br /><br /><br />

            </asp:TableHeaderCell>
        </asp:TableHeaderRow>
        <asp:TableHeaderRow HorizontalAlign="Left" BorderStyle="Solid" BorderColor="Black">
            <asp:TableHeaderCell >Item Description </asp:TableHeaderCell>
            <asp:TableHeaderCell >Requested Quantity </asp:TableHeaderCell>
            <asp:TableHeaderCell > Actual Quantity </asp:TableHeaderCell>
            <asp:TableHeaderCell > Remarks </asp:TableHeaderCell>
        </asp:TableHeaderRow>
        
        <asp:TableRow>
            <asp:TableCell>Clips Double 1"</asp:TableCell>
            <asp:TableCell>15</asp:TableCell>
            <asp:TableCell>10</asp:TableCell>
            <asp:TableCell> <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></asp:TableCell>
           
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell>File Blue Plain</asp:TableCell>
            <asp:TableCell>20</asp:TableCell>
            <asp:TableCell>20</asp:TableCell>
            <asp:TableCell> <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell>File Separator</asp:TableCell>
            <asp:TableCell>60</asp:TableCell>
            <asp:TableCell>60</asp:TableCell>
            <asp:TableCell> <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell>Stapler No.36</asp:TableCell>
            <asp:TableCell>20</asp:TableCell>
            <asp:TableCell>15</asp:TableCell>
            <asp:TableCell> <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <br /><br />
    <asp:Label ID="Label1" runat="server" Text="Enter PIN : "></asp:Label>
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <br /> <br /> <br />
    <asp:Button ID="Button2" runat="server" Text="Acknowledge" CssClass="button"/>
</asp:Content>


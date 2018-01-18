<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReorderTrendReport.aspx.cs" Inherits="StationeryReorderReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <h2 class="mainPageHeader">Reorder Trend Report</h2>
    
    <br />
    <br />
    <table>
        <tr>

             <td>
                 <asp:RadioButton ID="RadioButton3" runat="server" OnCheckedChanged="RadioButton3_CheckedChanged" text="Duration"
                      /><asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                 <br />
                 <asp:RadioButton ID="RadioButton4" runat="server" text="Month"
                     />
    &nbsp;<asp:DropDownList ID="DropDownList4" runat="server">
    </asp:DropDownList>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button5" runat="server" Text="Add" />
            </td>
            <td class="auto-style1"></td>


            <td>
                <br />
    Catergory:
    <asp:DropDownList ID="DropDownList1" runat="server">
    </asp:DropDownList>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button1" runat="server" Text="Add" />
            </td>
            <td class="auto-style1"></td>
            <td>
                <br />
                 Supplier:
    <asp:DropDownList ID="DropDownList2" runat="server">
    </asp:DropDownList>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button3" runat="server" Text="Add" />
            </td>
        </tr>

        <tr>
            <td>
                  <asp:GridView ID="GridView1" runat="server"></asp:GridView>
            </td>
            <td class="auto-style1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
            <td>
                <asp:GridView ID="GridView2" runat="server"></asp:GridView>
            </td>
              <td class="auto-style1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
            <td>
                <asp:GridView ID="GridView3" runat="server"></asp:GridView>
            </td>
        </tr>
    </table>
    
    <br />
    <br />
  
    Split report by: <br />
    <asp:RadioButton ID="RadioButton1" runat="server" Text="Category" />
    <br />
    <asp:RadioButton ID="RadioButton2" runat="server" Text="Supplier" />
  <br />
    <br />
    <asp:Button ID="Button2" runat="server" CssClass="button" Text="Generate Report" />
    <br />
    <br />
</asp:Content>


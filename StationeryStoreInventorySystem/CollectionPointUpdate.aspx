<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CollectionPointUpdate.aspx.cs" Inherits="CollectionPointUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <script src="Content/JavaScript.js"></script>    
      <h1 class="mainPageHeader">Collection Point</h1>      
        <table border="1">
            <tr class="headerrow" >
                <th>Collection Point </th>
                <th class="auto-style14">Date</th>
                <th class="auto-style14">Time</th>
            </tr>
            <tr>
                <td class="auto-style1">Biz2</td>
                <td> <asp:TextBox ID="txtDate" runat="server"></asp:TextBox></td>
                  <td> <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
            </tr>
            <tr class="middlerow">
                <td class="auto-style1">Central Library</td>
              <td> <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
                  <td> <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>
            </tr>
        </table>
      <br />
           <asp:Button ID="Button1" runat="server" Text="Submit" CssClass="button" />
</asp:Content>


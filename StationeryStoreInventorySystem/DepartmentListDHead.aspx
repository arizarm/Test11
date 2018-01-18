<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DepartmentListDHead.aspx.cs" Inherits="DepartmentListDHead" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            height: 40px;
        }
        .auto-style2 {
            width: 422px;
        }
        .auto-styledd {
            width: 155px;
        }
        .auto-style3 {
            width: 422px;
            height: 40px;
        }
        .auto-style4 {
            height: 40px;
        }
        .auto-style5 {
            width: 400px;
            height: 400px;
            
        }
        .auto-style6 {
            width: 422px;
            height: 12px;
        }
        .auto-style7 {
            height: 12px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="Content/JavaScript.js"></script>
        <table class="auto-style5">
            <tr>
                <th colspan="2" > <h2 class="auto-style1">Update Department Info</h2></th>
            </tr>            
            <tr>
                <td class="auto-style2">Department Name :</td>
                <td>English Dept</td>
            </tr>
            <tr>
                <td class="auto-style2">Contact Name :</td>
                <td>Mrs. Pamela Kow </td>
            </tr>
            <tr>
                <td class="auto-style3">Telephone No :</td>
                <td class="auto-style4">8742234 </td>
            </tr>
            <tr>
                <td class="auto-style2">Fax No :</td>
                <td>8921456</td>
            </tr>
            <tr>
                <td class="auto-style2">Head&#39;s Name :</td>
                <td>Prof. Ezra Pound</td>
            </tr>
            <tr>
                <td class="auto-style2">Acting Department Head :</td>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server" class="auto-styledd">
                        <asp:ListItem Selected="True">Michelle</asp:ListItem>
                        <asp:ListItem>John</asp:ListItem>
                        <asp:ListItem>Cheryl</asp:ListItem>
                        <asp:ListItem>Sarah</asp:ListItem>
                        <asp:ListItem>lyana</asp:ListItem>
                    </asp:DropDownList>
                </td>
               
            </tr>  
            <tr>
                <td class="auto-style2">Assignment Start Date :</td>
                <td> <asp:TextBox ID="txtDate" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="auto-style2">Assignment End Date :</td>
                <td> <asp:TextBox ID="txtDate1" runat="server"></asp:TextBox></td>
            </tr>          
            <tr>
                <td class="auto-style6">Representative Name :</td>
                <td class="auto-style7">
                    <asp:DropDownList ID="DropDownList3" runat="server" class="auto-styledd">
                        <asp:ListItem Selected="True">Lim</asp:ListItem>
                        <asp:ListItem>Eliza</asp:ListItem>
                        <asp:ListItem>Kelly</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Collection Point :</td>
                <td>
                    <asp:DropDownList ID="DropDownList4" runat="server" class="auto-styledd">
                        <asp:ListItem>Stationery Store</asp:ListItem>
                        <asp:ListItem>Management School</asp:ListItem>
                        <asp:ListItem>Medical School</asp:ListItem>
                        <asp:ListItem>Engineering School</asp:ListItem>
                        <asp:ListItem>Science School</asp:ListItem>
                        <asp:ListItem Selected="True">University Hospital</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td></td>
                <td class="auto-style2">
                    <asp:Button ID="Button1" runat="server" Text="Update" CssClass="button"/>
                </td>
                
            </tr>
            
        </table>
    </p>
</asp:Content>


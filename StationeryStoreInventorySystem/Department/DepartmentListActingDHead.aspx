<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DepartmentListActingDHead.aspx.cs" Inherits="DepartmentListActingDHead" %>

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
        .auto-style5 {
            width: 697px;
            height: 452px;
            
        }
        .auto-style8 {
            width: 422px;
            height: 25px;
        }
        .auto-style9 {
            height: 25px;
        }
        .auto-style10 {
            width: 422px;
            height: 31px;
        }
        .auto-style11 {
            height: 31px;
        }
        .auto-style12 {
            width: 422px;
            height: 30px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="Content/JavaScript.js"></script>
   
    <table class="auto-style5">
            <tr>
                <th colspan="2" class="updateDeptHead"> <h2 class="auto-style1">Update Department Info</h2>
                    </th>
            </tr> 
            <tr><th><p class="auto-style1">Tenure of office：<asp:Label ID="lblStartD" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Medium" ForeColor="#666666"></asp:Label>
&nbsp;to
                        <asp:Label ID="lblEndD" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Medium" ForeColor="#666666"></asp:Label>
                    </p></th></tr>           
            <tr>
                <td class="auto-style8">Department Name :</td>
                <td class="auto-style9">
                    <asp:Label ID="lblDeptName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style8">Contact Name :</td>
                <td class="auto-style9">
                    <asp:Label ID="lblContactName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style8">Telephone No :</td>
                <td class="auto-style9">
                    <asp:Label ID="lblPhone" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style8">Fax No :</td>
                <td class="auto-style9">
                    <asp:Label ID="lblFax" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style8">Head&#39;s Name :</td>
                <td class="auto-style9">
                    <asp:Label ID="lblHeadname" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style10">Acting Department Head :</td>
                <td class="auto-style11"><asp:Label ID="lblActingDHead" runat="server"></asp:Label></td>
            </tr>            
            <tr>
                <td class="auto-style12">Department Representative :</td>
                <td class="contentMenu">
                    <asp:DropDownList ID="DropDownListDRep" runat="server" class="auto-styledd" AutoPostBack="True">
                        
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style12">Collection Point :</td>
                <td class="contentMenu">
                    <asp:DropDownList ID="DropDownListCollectionPoint" runat="server" class="auto-styledd">
                        
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td></td>
                <td class="auto-style2">
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="button" OnClick="btnUpdate_Click"/>
                </td>
                
            </tr>
            
        </table>
    <asp:Label ID="lblMessage" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Large" Font-Strikeout="False" ForeColor="#006600"></asp:Label>
    </p>
</asp:Content>




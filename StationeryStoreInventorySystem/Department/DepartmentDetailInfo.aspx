<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DepartmentDetailInfo.aspx.cs" Inherits="Department_DepartmentDetailInfo" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            height: 30px;
        }
        .auto-style2 {
            width: 422px;
        }
        .auto-styledd {
            width: 155px;
        }
        .auto-style5 {
            width: 689px;
            height: 380px;
            
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
    <link href="Content/StyleSheet.css" type="text/css" rel="stylesheet"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="Content/JavaScript.js"></script>
   <%--<table class="tableBorder">
       <tr><th>--%>
    <table class="auto-style5">
            <tr >
                <th colspan="2" class="updateDeptHead"> <h2 class="auto-style1">Update Department Info</h2>
                    </th>
            </tr>            
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
                    <asp:Label ID="lblDeptRep" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style12">Collection Point :</td>
                <td class="contentMenu">
                    <asp:Label ID="lblCollectPoint" runat="server"></asp:Label>
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
    
</asp:Content>





﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DepartmentListDHead.aspx.cs" Inherits="DepartmentListDHead" %>
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
            height: 21px;
        }
        .auto-style5 {
            width: 811px;
            height: 444px;
            
        }
        .auto-style6 {
            width: 422px;
            height: 12px;
        }
        .auto-style7 {
            height: 12px;
        }
        .auto-style8 {
            width: 422px;
            height: 23px;
        }
        .auto-style9 {
            height: 23px;
        }
        .auto-style10 {
            height: 21px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="Content/JavaScript.js"></script>
      
    <table class="auto-style5">
            <tr>
                <th colspan="2" class="updateDeptHead"> <h2 class="auto-style1">Update Department Info</h2></th>
            </tr>            
            <tr>
                <td class="auto-style8">Department Name :</td>
                <td class="auto-style9">
                    <asp:Label ID="lblDeptName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Contact Name :</td>
                <td>
                    <asp:Label ID="lblContactName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">Telephone No :</td>
                <td class="auto-style10">
                    <asp:Label ID="lblPhone" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Fax No :</td>
                <td>
                    <asp:Label ID="lblFax" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Head&#39;s Name :</td>
                <td>
                    <asp:Label ID="lblHeadname" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Acting Department Head :</td>
                <td>
                    <asp:DropDownList ID="DropDownListActingDHead" runat="server" class="auto-styledd" AutoPostBack="True" OnSelectedIndexChanged="DropDownListActingDHead_SelectedIndexChanged">
                       
                    </asp:DropDownList>
                    
                </td>
               
            </tr>  
            <tr>
                <td class="auto-style2">Assignment Start Date :</td>
                <td>
                    <asp:TextBox ID="txtSDate" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSDate" ErrorMessage="Please enter Start Date for Delegate!" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                    
                    
                   

                </td>
            </tr>
            <tr>
                <td class="auto-style2">Assignment End Date :</td>
                <td> 
                    

      
                    <asp:CompareValidator ID="CompareEndTodayValidator" Operator="GreaterThan" type="String" 
                        ControltoValidate="txtSDate" ErrorMessage="The 'Start Date' must be after today" runat="server" ForeColor="#FF3300" /><br />
                    <asp:TextBox ID="txtEDate" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEDate" ErrorMessage="Please enter End Date for Delegate!" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                   
                </td>
            </tr>          
            <tr>
                
                <td class="auto-style6">Representative Name :</td>
                <td class="auto-style7">
                    <asp:CompareValidator ID="cmpStartAndEndDates" runat="server" Display="Dynamic"
    Operator="GreaterThan" ControlToValidate="txtEDate" ControlToCompare="txtSDate"
    ErrorMessage="The end date must be after the start date" ForeColor="#FF3300" />
                    
                    <asp:DropDownList ID="DropDownListDRep" runat="server" class="auto-styledd" AutoPostBack="True" OnSelectedIndexChanged="DropDownListDRep_SelectedIndexChanged">
                        
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Collection Point :</td>
                <td>
                    <asp:DropDownList ID="DropDownListCollectionPoint" runat="server" class="auto-styledd">
                        
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td></td>
                <td class="auto-style2">
                    <asp:Button ID="BtnUpdate" runat="server" Text="Update" CssClass="button" OnClick="btnUpdate_Click"/>
                </td>
                
            </tr>
            
        </table>
    <asp:Label ID="lblMessage" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Large" ForeColor="#006600"></asp:Label>
</asp:Content>


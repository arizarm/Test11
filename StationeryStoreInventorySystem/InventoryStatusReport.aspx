<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="InventoryStatusReport.aspx.cs" Inherits="InventoryStatusReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            height: 40px;
        }
         .headerrow {
           color:white;
           background-color:grey;
           height:35px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
        
    
    <table>
         <tr>      
            <td colspan="6" class="mainPageHeader"><h2>Inventory Status Report</h2></td>
            
            <td>&nbsp;</td>
            
        </tr>
        <tr class="headerrow">      
            <th>Item Code</th>
            <th>Description</th>
            <th>Location</th>
            <th>Unit of measurement</th>
            <th>Quantity on hand </th>
            <th>Reorder level</th>
        </tr>
        <tr>
            <td>R002</td>
            <td>Ruler 12&quot;</td>
            <td>AB4</td>
            <td>Dozen</td>
            <td>60</td>
            <td>50</td>
        </tr>
        <tr>
            <td class="auto-style1">R001</td>
            <td class="auto-style1">Ruler 6&quot;</td>
            <td class="auto-style1">AC4</td>
            <td class="auto-style1">Dozen</td>
            <td class="auto-style1">40</td>
            <td class="auto-style1">50</td>
        </tr>
        <tr>
            <td>P042</td>
            <td>Pencil 2B</td>
            <td>AA3</td>
            <td>Dozen</td>
            <td>80</td>
            <td>100</td>
        </tr>
        <tr>
            <td>E001</td>
            <td>&nbsp;Envelope Envelope Brown (3&quot;x6&quot;)</td>
            <td>BC2</td>
            <td>Each</td>
            <td>800</td>
            <td>600</td>
        </tr>
        <tr>
            <td>E008</td>
            <td>Envelope White (5&quot;x7&quot;) w/ Window</td>
            <td>BB2</td>
            <td>Each</td>
            <td>500</td>
            <td>600</td>
        </tr>
        <tr>
            <td>F020</td>
            <td>File Separator</td>
            <td>BF1</td>
            <td>Set</td>
            <td>70</td>
            <td>100</td>
        </tr>
        <tr>
            <td>F021</td>
            <td>File-Blue Plain</td>
            <td>BF2</td>
            <td>Each</td>
            <td>300</td>
            <td>200</td>
        </tr>
        <tr>
            <td>T025</td>
            <td>Transparency Cover 3M</td>
            <td>AF4</td>
            <td>Box</td>
            <td>700</td>
            <td>500</td>
        </tr>
        <tr>
            <td>P036</td>
            <td>Pen Transparency Permanent</td>
            <td>BF2</td>
            <td>Packet</td>
            <td>50</td>
            <td>100</td>
        </tr>
        <tr>
            <td>S100</td>
            <td>Scissors</td>
            <td>BA3</td>
            <td>Each</td>
            <td>50</td>
            <td>50</td>
        </tr>
        <tr>
            <td>S040</td>
            <td>Scotch Tap</td>
            <td>BA1</td>
            <td>Each</td>
            <td>100</td>
            <td>50</td>
        </tr>
    </table>
    
        
    
    <br /> 
</asp:Content>


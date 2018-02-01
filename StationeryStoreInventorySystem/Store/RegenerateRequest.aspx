<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RegenerateRequest.aspx.cs" Inherits="RegenerateRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">   
     <div class="updateDeptHead">
        <h2 class="mainPageHeader">Regenerate Requisition</h2>
    </div> 
      <br /><br />
    <table style="width: 30%;">        
        <tr>
            <td><b>Date :<br /><br /></b></td>
            <td><asp:Label ID="lblReqDate" runat="server"></asp:Label><br /><br /></td>
        </tr>        
        <tr>
            <td><b>Department :</b><br /><br /></td>
            <td><asp:Label ID="lblDepartment" runat="server"></asp:Label><br /><br /></td>
        </tr>
         <tr>
            <td><b>Requested By :</b><br /><br /></td>
            <td><asp:Label ID="lblReqBy" runat="server" ></asp:Label><br /><br /></td>
        </tr>
    </table>    

    <asp:GridView ID="gvRegenerate" runat="server" AutoGenerateColumns="False" CssClass="mGrid mGrid60percent" >
            <Columns>                
                 <asp:TemplateField >
                    <HeaderTemplate>
                        <asp:CheckBox ID="CheckAll" OnCheckedChanged="CheckAll_CheckedChanged" AutoPostBack="true" runat="server" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                             
                <asp:TemplateField HeaderText="Item Description">                                   
                    <ItemTemplate>
                        <asp:Label ID="lbldescription" runat="server" Text='<%# Bind("description") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Shortfall Quantity">                                   
                    <ItemTemplate>
                        <asp:Label ID="lblshortfallQty" runat="server" Text='<%# Bind("quantity") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Unit of Measure">                                   
                    <ItemTemplate>
                        <asp:Label ID="lbluom" runat="server" Text='<%# Bind("uom") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
             </Columns>
        </asp:GridView>
    <asp:Button ID="btnReGenReq" runat="server" Text="Generate Requisition" CssClass="button" OnClick="btnReGenReq_Click"/>

    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnCancel_Click"/>
</asp:Content>
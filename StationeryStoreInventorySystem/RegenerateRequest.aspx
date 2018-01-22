<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RegenerateRequest.aspx.cs" Inherits="RegenerateRequest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

   
    <style type="text/css">
    .modalBackground
    {
        background-color: Black;
        filter: alpha(opacity=90);
        opacity: 0.8;
    }
    .modalPopup
    {
        background-color: #FFFFFF;
        border-width: 3px;
        border-style: solid;
        border-color: black;
        padding-top: 10px;
        padding-left: 10px;
        width: 300px;
        height: 140px;
    }
</style>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
 
<!-- ModalPopupExtender -->
<cc1:ModalPopupExtender     
    ID="ModalPopupExtender1" runat="server" TargetControlID="btnReGenReq"
    PopupControlID="Panel1" BackgroundCssClass="modalBackground" DropShadow="true"
    CancelControlID="btnOkay" >
</cc1:ModalPopupExtender>

<asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center"  style = "display:none">
    Regeneration of Requisition Successful!<br />
    <asp:Button ID="btnOkay" Width="60" runat="server" Text="Okay" OnClick="btnOkay_Click" UseSubmitBehavior="false" />
</asp:Panel>
    
     <h2 class="mainPageHeader">Regenerate Requisition  </h2>
   
    <table style="width: 30%;">
        <tr>
            <td colspan ="2"> <h3>Shortfall Item List</h3></td>
        </tr>
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

    <asp:GridView ID="gvRegenerate" runat="server" AutoGenerateColumns="False" CssClass="mGrid" >
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
</asp:Content>


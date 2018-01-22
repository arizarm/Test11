<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DisbursementListDetail.aspx.cs" Inherits="DisbursementListDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script>function ConfirmApproval(objMsg) {
    if (confirm(objMsg)) {
        alert("execute code.");
        return true;
    }
    else
        return false;
}</script>
    <h2 class="mainPageHeader">Details Disbursement List</h2>
    
    <table style="width: 30%;">
        <tr>
            <td><b>Date :<br /><br /></b></td>
            <td><asp:Label ID="lblDate" runat="server"></asp:Label><br /><br /></td>
        </tr>
        <tr>
            <td><b>Time :</b><br /><br /></td>
            <td><asp:Label ID="lblTime" runat="server" ></asp:Label><br /><br /></td>
        </tr>
        <tr>
            <td><b>Department :</b><br /><br /></td>
            <td><asp:Label ID="lblDepartment" runat="server"></asp:Label><br /><br /></td>
        </tr>
         <tr>
            <td><b>Collection Point :</b><br /><br /></td>
            <td><asp:Label ID="lblColPoint" runat="server" ></asp:Label><br /><br /></td>
        </tr>
    </table>        
                 

    <asp:GridView ID="gvDisbDetail" runat="server" CssClass="mGrid" AutoGenerateColumns ="false">
        <Columns>                
                <asp:TemplateField HeaderText="Item Description">
                    <ItemTemplate>
                       <asp:Label ID="lblitemDesc" runat="server" Text='<%# Bind("itemDesc") %>'></asp:Label>
                        <asp:HiddenField ID="hdnflditemCode" runat="server" Value ='<%# Bind("itemCode") %>'/> 
                    </ItemTemplate>
                </asp:TemplateField>                
                <asp:TemplateField HeaderText="Total Requested Quantity">                                   
                    <ItemTemplate>
                        <asp:Label ID="lblreqQty" runat="server" Text='<%# Bind("reqQty") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Actual Quantity">                                   
                    <ItemTemplate>
                        <asp:TextBox ID="txtactualQty" runat="server" Text='<%# Bind("actualQty") %>'></asp:TextBox> <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="txtactualQty" style="color:red">Quantity cannot be empty</asp:RequiredFieldValidator>
                        <%--<asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Actual Quantity Cannot be more than requested" ControlToValidate="txtactualQty" MaximumValue="<% Convert.ToInt32((gvDisbDetail.FindControl("lblreqQty") as Label).Text); %>""></asp:RangeValidator>--%>
                    </ItemTemplate>
                </asp:TemplateField>
             <asp:TemplateField HeaderText="Remarks" SortExpression="Remarks">                                   
                    <ItemTemplate>
                        <asp:TextBox Width ="100%" ID="txtremarks" runat="server" Text='<%# Bind("remarks") %>'></asp:TextBox> <br />
                    </ItemTemplate>
                </asp:TemplateField>              
             </Columns>
    </asp:GridView>

    
    <br /><br />
    <asp:Label ID="Label1" runat="server" Text="Enter Access Code : "></asp:Label>
    <asp:TextBox ID="txtAccessCode" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="txtAccessCode" style="color:red">Access Code cannot be empty</asp:RequiredFieldValidator>
    <br /> <br /> <br />
    <asp:Button ID="btnAck" runat="server" Text="Acknowledge" CssClass="button" OnClick="btnAck_Click"/>
</asp:Content>


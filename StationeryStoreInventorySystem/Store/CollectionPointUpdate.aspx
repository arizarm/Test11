<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CollectionPointUpdate.aspx.cs" Inherits="CollectionPointUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script src="Content/JavaScript.js"></script>
    <script type="text/javascript">
        $(function () {

            $("#txtSDate").datepicker({
                changeMonth: true,
                changeYear: true,
                yearRange: "-0:+2", // You can set the year range as per as your need
                dateFormat: 'yy-mm-dd'

            }).val()           
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     
      <h1 class="mainPageHeader">Collection Point</h1>
     <p class="mainPageHeader">&nbsp;</p>
     <p class="mainPageHeader">

          <table style="width: 30%;">
 
        <tr>
            <td class="auto-style1"><b>Date:</b></td>
            <td  >
                <asp:TextBox ID="txtSDate" runat="server"  ValidationGroup="1" ClientIDMode="Static" ></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1"  ValidationGroup="1" runat="server" ErrorMessage="Please select the date" ControlToValidate="txtSDate" Style="color: red"></asp:RequiredFieldValidator>
</td>
        </tr>        
       
    </table>

         <asp:GridView ID="gvCollectionPoint" runat="server" AutoGenerateColumns="False">
             <Columns>
                 <asp:TemplateField HeaderText="Collection Point">
                     <EditItemTemplate>
                         <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                     </EditItemTemplate>
                     <ItemTemplate>
                         <asp:Label ID="labCollectionPoint" runat="server" Text='<%# Bind("CollectionPoint") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <%--<asp:TemplateField HeaderText="Date">
                     <EditItemTemplate>
                         <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                     </EditItemTemplate>
                     <ItemTemplate>                         
                    <asp:TextBox ID="txtDate" runat="server" ClientIDMode="Static"></asp:TextBox>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please select the date" ControlToValidate="txtDate" Style="color: red"></asp:RequiredFieldValidator>

                     </ItemTemplate>
                 </asp:TemplateField>--%>


                 <asp:TemplateField HeaderText="Time (HHMM, 24 hour format)">
                     <EditItemTemplate>
                         <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                     </EditItemTemplate>
                     <ItemTemplate>
                       <asp:TextBox ID="time" runat="server" Text='<%# Bind("DefaultCollectionTime") %>'></asp:TextBox>
                     <asp:RegularExpressionValidator
            ID="RegularExpressionValidator1" ControlToValidate="time"
            runat="server" ErrorMessage="you must enter 4 digits."
            ValidationExpression="\d{4}$" ForeColor="Red"></asp:RegularExpressionValidator>
                     </ItemTemplate>
                 </asp:TemplateField>
             </Columns>
         </asp:GridView>
     </p>      
        <asp:Button ID="Submit" runat="server" Text="Submit" CssClass="button" OnClick="Submit_Click" />
</asp:Content>



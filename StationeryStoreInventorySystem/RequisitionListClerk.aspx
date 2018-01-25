<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RequisitionListClerk.aspx.cs" Inherits="ReqisitionListClerk" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
   
  <div>
        <h2 class="mainPageHeader">Requisition List</h2>
      <asp:TextBox ID="SearchBox" ValidationGroup="1" runat="server" Width="311px"></asp:TextBox>
           <asp:button ID="SearchBtn" runat="server" Text="Search"  ValidationGroup="1"  CssClass="button" OnClick="SearchBtn_Click"/>
           <asp:button ID="DisplayBtn" runat="server" Text="Display All" CssClass="button" OnClick="DisplayBtn_Click"/>
       </div>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="1" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="SearchBox" style="color:red"> Please enter search key word!</asp:RequiredFieldValidator>

    <div>
        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="align-right" AutoPostBack="True">
            <asp:ListItem>Select Status</asp:ListItem>
            <asp:ListItem>Approved</asp:ListItem>
            <asp:ListItem>Priority</asp:ListItem>
        </asp:DropDownList>
    </div>

       <div>        
         <asp:GridView ID="gvReq" runat="server" Width="100%" CssClass="mGrid"  AutoGenerateColumns="False" >
            <Columns>
               
                <asp:TemplateField >
                    <HeaderTemplate>
                        <asp:CheckBox ID="CheckAll" OnCheckedChanged="CheckAll_CheckedChanged" AutoPostBack="true" runat="server" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
              
            <asp:TemplateField HeaderText="Date">
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("date") %>' ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Requisition No">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblrequisitionNo" runat="server" Text='<%# Bind("requisitionNo") %>'></asp:Label>
                        </ItemTemplate>
                </asp:TemplateField>

                
                <asp:TemplateField HeaderText="Department">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                   <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("department") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>      
                
                 <asp:TemplateField HeaderText="Status">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                   <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("status") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>       
                
                 <asp:TemplateField HeaderText="Detail">
                    <EditItemTemplate>
                        <asp:Button ID="Button1" runat="server" Text="Detail" />
                    </EditItemTemplate>
                   <ItemTemplate>
                       <asp:Button ID="gvDetailBtn" runat="server" OnClick="gvDetailBtn_Click" Text="Detail" />
                    </ItemTemplate>
                </asp:TemplateField>              

                
            </Columns>
        </asp:GridView>
    </div>
    <asp:Button ID="GenerateBtn" runat="server" Text="Generate Retrieval List" CssClass="button" OnClick="GenerateBtn_Click" Height="60px" Width="273px"/>
    <h2><asp:Label ID="CheckBoxValidation" runat="server" Text=""></asp:Label></h2> 
    </asp:Content>



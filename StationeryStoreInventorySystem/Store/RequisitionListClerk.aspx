<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RequisitionListClerk.aspx.cs" Inherits="ReqisitionListClerk" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>--%>

    <div class="row updateDeptHead">
        <h2 class="mainPageHeader">Requisition List</h2>
    </div>
    <br />
    <br />
    <br />
    <div>
        <asp:TextBox ID="SearchBox" ValidationGroup="1" runat="server" Width="311px"></asp:TextBox>
        <asp:Button ID="SearchBtn" runat="server" Text="Search" ValidationGroup="1" CssClass="button" OnClick="SearchBtn_Click" />
        <asp:Button ID="DisplayBtn" runat="server" Text="Display All" CssClass="button" OnClick="DisplayBtn_Click" />
    </div>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="1" runat="server" ErrorMessage="" ControlToValidate="SearchBox" Style="color: red"> </asp:RequiredFieldValidator>


    <table style="width: 100%;">
        <tr>
            <td>
                <div>
                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="pull-right" AutoPostBack="True" Width="179px">
                        <asp:ListItem>Select Status</asp:ListItem>
                        <asp:ListItem>Approved</asp:ListItem>
                        <asp:ListItem>Priority</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </td>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
        </tr>
    </table>



    <div>
        <asp:GridView ID="gvReq" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvReq_RowDataBound" CssClass="mGrid" RowStyle-Height="50px">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:CheckBox ID="CheckAll" OnCheckedChanged="CheckAll_CheckedChanged" AutoPostBack="true" runat="server" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Date">
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("date") %>'></asp:Label>
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
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("status") %>' Font-Bold="true" Width="140px"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Detail">
                    <EditItemTemplate>
                        <asp:Button ID="Button1" runat="server" Text="Detail"  />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Button ID="gvDetailBtn" runat="server" OnClick="gvDetailBtn_Click" Text="Detail" CssClass="alert-success" />
                    </ItemTemplate>
                </asp:TemplateField>


            </Columns>
        </asp:GridView>
    </div>
    <asp:Button ID="GenerateBtn" runat="server" Text="Generate Retrieval List" CssClass="button" OnClick="GenerateBtn_Click" Height="60px" Width="273px" />
    <asp:Label ID="CheckBoxValidation" runat="server" ForeColor="Red" Font-Size="40px"></asp:Label>

    <%--   </ContentTemplate></asp:UpdatePanel>--%>
</asp:Content>



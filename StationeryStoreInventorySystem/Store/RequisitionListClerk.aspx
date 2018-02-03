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
        <asp:TextBox ID="txtSearchBox" ValidationGroup="1" runat="server" Width="311px"></asp:TextBox>
        <asp:Button ID="btnSearch" runat="server" Text="Search" ValidationGroup="1" CssClass="button" OnClick="BtnSearch_Click" />
        <asp:Button ID="btnDisplay" runat="server" Text="Display All" CssClass="button" OnClick="BtnDisplay_Click" />
    </div>
    <asp:RequiredFieldValidator ID="req" ValidationGroup="1" runat="server" ErrorMessage="" ControlToValidate="txtSearchBox" Style="color: red"> </asp:RequiredFieldValidator>


    <table style="width: 100%;">
        <tr>
            <td>
                <div>
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="pull-right" AutoPostBack="True" Width="179px">
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
        <asp:GridView ID="gvReq" runat="server" AutoGenerateColumns="False" OnRowDataBound="GvReq_RowDataBound" CssClass="mGrid" RowStyle-Height="50px">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:CheckBox ID="cbxCheckAll" OnCheckedChanged="CheckAll_CheckedChanged" AutoPostBack="true" runat="server" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="cbxCheckBox" runat="server" />
                    </ItemTemplate>
                       <ItemStyle Width="20px" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Date">
                    <ItemTemplate>
                        <asp:Label ID="lblDate" runat="server" Text='<%# Bind("date") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="200px" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Requisition No">
                    <ItemTemplate>
                        <asp:Label ID="lblRequisitionNo" runat="server" Text='<%# Bind("requisitionNo") %>'></asp:Label>
                    </ItemTemplate>
                       <ItemStyle Width="20px" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Department">
                    <ItemTemplate>
                        <asp:Label ID="lblDepartment" runat="server" Text='<%# Bind("department") %>'></asp:Label>
                    </ItemTemplate>
                       <ItemStyle Width="250px" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("status") %>' Font-Bold="true" Width="140px"></asp:Label>
                    </ItemTemplate>
                       <ItemStyle Width="100px" />
                
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Detail">
                    <ItemTemplate>
                        <asp:Button ID="btnGvDetail" runat="server" OnClick="BtnGvDetail_Click" Text="Detail" CssClass="alert-success" />
                    </ItemTemplate>
                       <ItemStyle Width="100px" />
                </asp:TemplateField>


            </Columns>

<RowStyle Height="50px"></RowStyle>
        </asp:GridView>
    </div>
    <asp:Button ID="btnGenerate" runat="server" Text="Generate Retrieval List" CssClass="button" OnClick="BtnGenerate_Click" Height="60px" Width="273px" />
    <asp:Label ID="lblCheckBoxValidation" runat="server" ForeColor="Red" Font-Size="40px"></asp:Label>

    <%--   </ContentTemplate></asp:UpdatePanel>--%>
</asp:Content>



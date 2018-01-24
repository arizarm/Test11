<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RetrievalShortfall.aspx.cs" Inherits="RetrievalDecision" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style1 {
            height: 22px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <h2>There are insufficient numbers of quantities for the following items.</h2>
        <h3>Please allocate the items before generating forms</h3>
   
        <asp:GridView ID="gvMain" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField HeaderText="Item Description">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                       <asp:Label ID="itemDescription" runat="server" Text='<%# Bind("Description")%>'></asp:Label>
                        <asp:HiddenField ID="hdfItemCode" runat="server" value='<%# Bind("ItemCode")%>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Available Quantity">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="availableQuantity" runat="server" Text='<%# Bind("Qty")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Breakdown by Department">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                    </EditItemTemplate>

                    <ItemTemplate>
                         <asp:GridView ID="gvSub" runat="server" AutoGenerateColumns="False" CssClass="mGrid" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="RequestDate">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="RequestDate" runat="server" Text='<%# Bind("RequestDate")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="DeptName">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="DeptName" runat="server" Text='<%# Bind("DeptName")%>'></asp:Label>
                                        <asp:HiddenField ID="hdfdeptCode" runat="server" value='<%# Bind("deptCode")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Requested Quantity">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="RequestedQty" runat="server" Text='<%# Bind("RequestedQty")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Actual Quantity">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtActualQuantity" runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </ItemTemplate>
                </asp:TemplateField>


            </Columns>
        </asp:GridView>

    </div>
    <div>
       
       <%-- <asp:Button ID="BtnResetAll" runat="server" Text="Reset All" CssClass="button" align="left" OnClick="BtnResetAll_Click" />--%>
        <table align="right">
            <tr>

                <td>
                   <%-- <asp:Button ID="BtnSave" runat="server" Text="Save" CssClass="button" OnClick="BtnSave_Click" />
                    &nbsp; &nbsp; &nbsp;
         --%>       </td>

                <td>
                    &nbsp;</td>
            </tr>
        </table>
                    <asp:Button ID="BtnGenerateDisbursementList" runat="server" Text="Generate Disbursement List" CssClass="button" OnClick="BtnGenerateDisbursementList_Click" />
    </div>


</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RetrievalShortfall.aspx.cs" Inherits="RetrievalDecision" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <h2> There are insufficient numbers of quantities for the following items.</h2>
        <h3> Please allocate the items before generating forms</h3>
        <table width="100%" border="1">
            <thead>
                <tr>
                    <th>Stationery Description</th>
                    <th>Available Quantity</th>
                    <th>Breakdown by Department</th>
                    <th></th>
                </tr>
            </thead>
            <tr>
                <td>Staplet</td>
                <td>9</td>
                <td>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="mGrid">
                        <Columns>
                            <asp:TemplateField HeaderText="Requisition Date">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label" runat="server" Text='<%# Bind("date")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dept name">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("name")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Requested Quantity">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("needed")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Actual Quantity">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </div>
    <div>
        <br />
        <asp:Button ID="Button2" runat="server" Text="Reset All" align="left"/>
        <table align="right">
            <tr>

                <td>
                    <asp:Button ID="Button3" runat="server" Text="Save" CssClass="button"/>
                     &nbsp; &nbsp; &nbsp;
                </td>
                
                <td>
                    <asp:Button ID="Button4" runat="server" Text="Generate Disbursement List" CssClass="button"/>
                </td>
            </tr>
        </table>
    </div>


</asp:Content>


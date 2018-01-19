<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RetrievalListDetail.aspx.cs" Inherits="RetrievalForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2 class="mainPageHeader"> Retrieval List
    </h2>
    <p class="mainPageHeader"> &nbsp;</p>
    <p class="mainPageHeader"> &nbsp;</p>
    <p class="mainPageHeader"> &nbsp;</p>
    <p class="mainPageHeader"> &nbsp;</p>


    <asp:GridView ID="gvRe" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:TemplateField HeaderText="Bin#">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Item Description">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Total Quantity Requested">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Total Quantity Retrieved">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:TextBox ID="txtRetrieved" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>







    <div>

        <asp:Table ID="Table1" runat="server" BorderStyle="Double" HorizontalAlign="Center">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell BorderStyle="Dashed">Bin #</asp:TableHeaderCell>
                <asp:TableHeaderCell BorderStyle="Dashed">Stationery Description</asp:TableHeaderCell>
                <asp:TableHeaderCell BorderStyle="Dashed">Total Quantity Requested</asp:TableHeaderCell>
                <asp:TableHeaderCell BorderStyle="Dashed">Total Quantity Retrieved</asp:TableHeaderCell>
            </asp:TableHeaderRow>
            <asp:TableRow>
                <asp:TableCell>3</asp:TableCell>
                <asp:TableCell>Staplet</asp:TableCell>
                <asp:TableCell>10</asp:TableCell>
                <asp:TableCell>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="TextBox1" runat="server" Text="9"></asp:TextBox></td>
                            <td>
                              
                        </tr>
                    </table>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>4</asp:TableCell>
                <asp:TableCell>Pencil</asp:TableCell>
                <asp:TableCell>20</asp:TableCell>
                <asp:TableCell>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="TextBox2" runat="server" Text="20"></asp:TextBox></td>
                            <td>
                              
                        </tr>
                    </table>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>20</asp:TableCell>
                <asp:TableCell>Paper</asp:TableCell>
                <asp:TableCell>3</asp:TableCell>
                <asp:TableCell>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="TextBox3" runat="server" Text="3"></asp:TextBox></td>
                            <td>
                              
                        </tr>
                    </table>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow><asp:TableCell></asp:TableCell></asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="4" HorizontalAlign="Right">
                    <asp:Button ID="Button3" runat="server" Text="Save" CssClass="button"/>
                &nbsp;&nbsp;&nbsp; 
                
                    <asp:Button ID="Button4" runat="server" Text="Generate Disbursment List" CssClass="button"/>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>

    </div>

    <%--        <table  align="right">
            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Save All" />
                </td>
                <td>
                    <asp:Button ID="Button2" runat="server" Text="Generate Disbursment Form" />
                </td>
            </tr>
        </table>--%>
</asp:Content>


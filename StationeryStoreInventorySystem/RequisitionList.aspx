<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RequisitionList.aspx.cs" Inherits="RequisitionList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <h2 class="mainPageHeader">Requisition List</h2>
        <asp:TextBox ID="SearchBox" runat="server" Width="311px"></asp:TextBox>
        <asp:Button ID="SearchBtn" runat="server" Text="Search" CssClass="buttonformid"  />
        <asp:Button ID="Display" runat="server" Text="Display All" CssClass="buttonformid" />
    </div>
    <div>
        <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="mGrid" AutoGenerateColumns="False">
            <Columns>

                <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                    <EditItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Date">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("RequestDate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Requisition No">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton2" runat="server" Text='<%# Bind("RequisitionID") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Department">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("RequestedBy") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Status">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>


            </Columns>
        </asp:GridView>
    </div>
    <asp:Button ID="Button2" runat="server" Text="Generate Retrieval List" CssClass="button" />

</asp:Content>


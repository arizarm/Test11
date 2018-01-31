<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RetrievalList.aspx.cs" Inherits="RetrievalList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <h2 class="mainPageHeader">Pending Retrieval List</h2>
        <asp:TextBox ID="SearchBox" ValidationGroup="1" runat="server" Width="311px"></asp:TextBox>
        <asp:Button ID="SearchBtn" runat="server" Text="Search" ValidationGroup="1" CssClass="button" OnClick="SearchBtn_Click" />
        <asp:Button ID="DisplayBtn" runat="server" Text="Display All" CssClass="button" OnClick="DisplayBtn_Click" />
    </div>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="1" runat="server" ErrorMessage="" ControlToValidate="SearchBox" Style="color: red"> </asp:RequiredFieldValidator>
    <div>
        <asp:GridView ID="gvReq" runat="server" Width="100%" CssClass="mGrid" AutoGenerateColumns="False" OnRowDataBound="gvReq_RowDataBound">
            <Columns>

                <asp:TemplateField HeaderText="Date">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("Key.RetrievedDate","{0:dddd, dd MMMM yyyy}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Retrieval No">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="LabelRetrievalID" runat="server" Text='<%# Bind("Key.RetrievalID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Retrieved By">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("Value") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Status">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Key.RetrievalStatus") %>' Font-Bold="true" Width="140px"></asp:Label>
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
</asp:Content>



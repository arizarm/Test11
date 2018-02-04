<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RetrievalList.aspx.cs" Inherits="RetrievalList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div class="row updateDeptHead">
        <h2 class="mainPageHeader">Pending Retrieval List</h2>
    </div>
    <br />
    <br />
    <br />
    <div>
        <asp:TextBox ID="txtSearchBox" ValidationGroup="1" runat="server" Width="311px" ></asp:TextBox>
        <asp:Button ID="btnSearch" runat="server" Text="Search" ValidationGroup="1" CssClass="button" OnClick="BtnSearch_Click" />
        <asp:Button ID="btnDisplay" runat="server" Text="Display All" CssClass="button" OnClick="BtnDisplay_Click" />
    </div>
    <asp:RequiredFieldValidator ID="req" ValidationGroup="1" Display="Dynamic" runat="server" ErrorMessage="" ControlToValidate="txtSearchBox" Style="color: red"> </asp:RequiredFieldValidator>
    <div>
        <asp:Label ID="lblCheckRetrievalListValidation" runat="server" Text="" ForeColor="Red" Font-Size="40px"></asp:Label>
        <asp:GridView ID="gvReq" runat="server" Width="100%" CssClass="mGrid" AutoGenerateColumns="False" OnRowDataBound="GvReq_RowDataBound" EmptyDataText="No Retrieval is found">
            <Columns>

                <asp:TemplateField HeaderText="Date">
                    <ItemTemplate>
                        <asp:Label ID="lblDate" runat="server" Text='<%# Bind("Key.RetrievedDate","{0:dddd, dd MMMM yyyy}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Retrieval No">
                    <ItemTemplate>
                        <asp:Label ID="lblRetrievalID" runat="server" Text='<%# Bind("Key.RetrievalID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Retrieved By">
                    <ItemTemplate>
                        <asp:Label ID="lblRetrievedBy" runat="server" Text='<%# Bind("Value") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Key.RetrievalStatus") %>' Font-Bold="true" Width="140px"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Detail">
                    <ItemTemplate>
                        <asp:Button ID="btnGvDetail" runat="server" OnClick="BtnGvDetail_Click" Text="Detail" CssClass="alert-success" />
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
    </div>
</asp:Content>



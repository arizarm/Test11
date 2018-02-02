<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CollectionListDepRep.aspx.cs" Inherits="ReqisitionListEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSearch">
        <div class="updateDeptHead">
            <h2 class="mainPageHeader">Collection List</h2>
        </div>

        <div>
            <asp:TextBox ID="txtSearch" runat="server" Width="311px"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClick="BtnSearch_Click" />
            <asp:Button ID="btnDisplayAll" runat="server" Text="Display All" CssClass="button" OnClick="BtnDisplayAll_Click" />
        </div>
        <div>
            <h3>All Collection list</h3>
            <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Size="XX-Large" ForeColor="#999999" Text="Label" Visible="False"></asp:Label>
            <asp:GridView ID="gvCollectionList" runat="server" AutoGenerateColumns="False"
                DataKeyNames="RequisitionNo" CssClass="mGrid mGrid60percent" AllowPaging="true" PageSize="15" OnPageIndexChanging="GVCollectionList_PageIndexChanging" OnRowDataBound="GVCollectionList_RowDataBound" RowStyle-Height="50px">
                <Columns>
                    <asp:TemplateField HeaderText="Request Date">
                        <ItemTemplate>
                            <asp:Label ID="lblDate" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Requested By">
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" Text='<%# Bind("EmployeeName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:HyperLinkField HeaderText="Detail" DataNavigateUrlFields="RequisitionNo"
                        DataNavigateUrlFormatString="ApproveRequisition.aspx?id={0}" Text="Detail" />
                </Columns>
            </asp:GridView>
            <hr />
        </div>
    </asp:Panel>
</asp:Content>



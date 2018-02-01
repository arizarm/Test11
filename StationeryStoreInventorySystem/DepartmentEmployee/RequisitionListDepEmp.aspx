<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RequisitionListDepEmp.aspx.cs" Inherits="ReqisitionListEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="updateDeptHead">
        <h2 class="mainPageHeader">Requisition List</h2>
    </div>
    <div>
        <asp:TextBox ID="SearchBox" runat="server" Width="311px"></asp:TextBox>
        <asp:Button ID="SearchBtn" runat="server" Text="Search" CssClass="button" OnClick="SearchBtn_Click" />
        <asp:Button ID="DisplayBtn" runat="server" Text="Display All" CssClass="button" OnClick="DisplayBtn_Click" />
    </div>
    <div>
        <asp:Label ID="Label2" runat="server" Text="To View Requisition by Status : "></asp:Label>
        <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
            <asp:ListItem Selected="True">Select Status</asp:ListItem>
            <asp:ListItem Value="Pending">Pending</asp:ListItem>
            <asp:ListItem Value="Approved">Approved</asp:ListItem>
            <asp:ListItem Value="InProgress">In Progress</asp:ListItem>
            <asp:ListItem Value="Rejected">Rejected</asp:ListItem>
            <asp:ListItem Value="Closed">Closed</asp:ListItem>
        </asp:DropDownList>
    </div>
    <h3>Your Requested Requisition list</h3>

    <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Size="XX-Large" ForeColor="#999999" Text="Label" Visible="False"></asp:Label>
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
            DataKeyNames="RequisitionNo" CssClass="mGrid mGrid60percent" AllowPaging="true" PageSize="15" OnPageIndexChanging="GridView1_PageIndexChanging" RowStyle-Height="50px" OnRowDataBound="GridView1_RowDataBound">
            <PagerStyle BackColor="#424242" ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle Height="50px" Font-Size="Large" />
            <Columns>

                <asp:TemplateField HeaderText="Request Date">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Date") %>'></asp:Label>

                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:HyperLinkField HeaderText="Detail" DataNavigateUrlFields="RequisitionNo"
                    DataNavigateUrlFormatString="RequisitionDetailsEmployee.aspx?requisitionNo={0}" Text="Detail" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>



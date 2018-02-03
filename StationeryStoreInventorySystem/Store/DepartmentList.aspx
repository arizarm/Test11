<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DepartmentList.aspx.cs" Inherits="DepartmentList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="updateDeptHead">
        <h2 class="mainPageHeader">Department Information</h2>
    </div>

    <asp:GridView ID="GridViewDept" runat="server" CssClass="mGrid" RowStyle-Height="50px" AutoGenerateColumns="False">

        <Columns>
            <asp:BoundField DataField="DeptCode" HeaderText="DeptCode" />
            <asp:BoundField DataField="DeptName" HeaderText="DeptName" />
            <asp:BoundField DataField="EmpName" HeaderText="DeptHead" />
            <asp:BoundField DataField="CollectionPoint1" HeaderText="CollectionPoint" />
            <asp:BoundField DataField="DeptContactName" HeaderText="ContactName" />
            <asp:BoundField DataField="DeptTelephone" HeaderText="Phone" />
            <asp:BoundField DataField="DeptFax" HeaderText="Fax" />        
        </Columns>

    </asp:GridView>
</asp:Content>
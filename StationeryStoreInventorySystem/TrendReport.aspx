<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TrendReport.aspx.cs" Inherits="RequisitionTrend" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2 class="mainPageHeader">Requisition Trend Report</h2>
    <br />
    <br />
    <table >
        <%--Row1--%>
        <tr><td>
            <asp:Label runat="server" Text="Select Duration:"></asp:Label><br />
            </td>
            <td class="auto-style1"></td>


            <td>
                <asp:Label ID="SelectCategoryLabel" runat="server" Text="Select Category:"/><br />
            </td>
            <td class="auto-style1"></td>
            <td>
                <asp:Label ID="DepartmentLabel" runat="server" Text="Select Department:"></asp:Label>                 
            </td>
        </tr>
<%--        Row 2--%>
        <tr>
            <td>
                                            <asp:RadioButtonList ID="DurationRadioButtonList" runat="server" OnSelectedIndexChanged="DurationRadioButtonList_SelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem Text ="Past 3 Months" Selected="True"></asp:ListItem>
                <asp:ListItem Text ="Range" />
                <asp:ListItem Text ="Custom" />
            </asp:RadioButtonList>
            </td>
                        <td class="auto-style1"></td>
                        <td>
                                <asp:RadioButtonList ID="CategoryRadioButtonList" runat="server" OnSelectedIndexChanged="CategoryRadioButtonList_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Text ="All" Selected="True"/>
                    <asp:ListItem Text ="Custom" />
                </asp:RadioButtonList>            
            </td>
                        <td class="auto-style1"></td>
            <td>
                                <asp:RadioButtonList ID="DepartmentRadioButtonList" runat="server" OnSelectedIndexChanged="DepartmentRadioButtonList_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Text ="All" Selected="True"/>
                    <asp:ListItem Text ="Custom" />
                </asp:RadioButtonList>
            </td>
        </tr>

        <%--Row 3--%>
        <tr>
            <td>
    &nbsp;<asp:DropDownList ID="DurationDropDownList" runat="server" Visible="false">
    </asp:DropDownList>
    
    <asp:Button ID="DurationAddButton" runat="server" Text="Add" Visible="false"/>
                
                <asp:Label ID="FromLabel" runat="server" Text="From:" Visible="false"/>
                <asp:DropDownList ID="FromDropDownList" runat="server" Visible="false"/>
                <asp:Label ID="ToLabel" runat="server" Text="To:" Visible="false"/>
                <asp:DropDownList ID="ToDropDownList" runat="server" Visible="false"/>
            </td>
                        <td class="auto-style1"></td>
            <td>
    <asp:DropDownList ID="CategoryDropDownList" runat="server" Visible="false">
    </asp:DropDownList>
    
    <asp:Button ID="CategoryAddButton" runat="server" Text="Add" Visible="false" OnClick="CategoryAddButton_Click"/>
            </td>
                                    <td class="auto-style1"></td>
            <td>
                    <asp:DropDownList ID="DepartmentDropDownList" runat="server" Visible="false">
    </asp:DropDownList>
    
    <asp:Button ID="DepartmentAddButton" runat="server" Text="Add" Visible="false" OnClick="DepartmentAddButton_Click"/>
            </td>

        </tr>

        <%--Row 4--%>
        <tr>
            <td>
                  <asp:GridView ID="GridView1" runat="server"></asp:GridView>
            </td>
            <td class="auto-style1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
            <td>
                <asp:GridView ID="CategoryGridView" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:TemplateField HeaderText="Category">
                            <ItemTemplate>
                                <asp:Label ID="CategoryItemLabel" runat="server" Text="<%#Container.DataItem %>"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="RemoveCategoryBtn" runat="server" Text="Remove" OnClick="RemoveCategoryBtn_Click"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
              <td class="auto-style1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
            <td>
                <asp:GridView ID="DepartmentGridView" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:TemplateField HeaderText="Department">
                            <ItemTemplate>
                                <asp:Label ID="DepartmentItemLabel" runat="server" Text="<%#Container.DataItem %>"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="RemoveDepartmentBtn" runat="server" Text="Remove" OnClick="RemoveDepartmentBtn_Click"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    
    <br />
    <br />
  
    Split report by: <br />
    <asp:RadioButtonList ID="SplitReportRadioButtonList" runat="server">
                <asp:ListItem Text="Department" Selected="True"/>
        <asp:ListItem Text="Category" />
    </asp:RadioButtonList>
  <br />
    <br />
    <asp:Button ID="Button2" runat="server" CssClass="button" Text="Generate Report" />
    <br />
    <br />
</asp:Content>


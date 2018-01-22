﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TrendReport.aspx.cs" Inherits="RequisitionTrend" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2 class="mainPageHeader">
        <asp:Label ID="HeaderLabel" runat="server" Text="Please select a valid Trend Report on the Menu"></asp:Label></h2>
    <br />
    <br />

    <table id ="MainTable" aria-hidden="true" runat="server">
        <%--Row1--%>
        <tr><td>
            <asp:Label runat="server" Text="Select Duration:" ID="DurationLabel" Visible="false"></asp:Label><br />
            </td>
            <td class="auto-style1"></td>


            <td>
                <asp:Label ID="SelectCategoryLabel" runat="server" Text="Select Category:" Visible="false"/><br />
            </td>
            <td class="auto-style1"></td>
            <td>
                <asp:Label ID="DepartmentLabel" runat="server" Text="Select Department:" Visible="false" />   <asp:Label ID="SupplierLabel" runat="server" Text="Select Supplier:" Visible="false" />       
            </td>
            </tr>

<%--        Row 2--%>
        <tr>
            <td>
                                            <asp:RadioButtonList ID="DurationRadioButtonList" runat="server" OnSelectedIndexChanged="DurationRadioButtonList_SelectedIndexChanged" AutoPostBack="true" Visible="false">
                <asp:ListItem Text ="Past 3 Months" Selected="True"></asp:ListItem>
                <asp:ListItem Text ="Range (3 Months)" />
                <asp:ListItem Text ="Custom (Select 3 Months)" />
            </asp:RadioButtonList>
            </td>
                        <td class="auto-style1"></td>
                        <td>
                                <asp:RadioButtonList ID="CategoryRadioButtonList" runat="server" OnSelectedIndexChanged="CategoryRadioButtonList_SelectedIndexChanged" AutoPostBack="true" Visible="false">
                    <asp:ListItem Text ="All" Selected="True"/>
                    <asp:ListItem Text ="Custom" />
                </asp:RadioButtonList>            
            </td>
                        <td class="auto-style1"></td>
            <td>
                                <asp:RadioButtonList ID="DepartmentRadioButtonList" runat="server" OnSelectedIndexChanged="DepartmentRadioButtonList_SelectedIndexChanged" AutoPostBack="true" Visible="false">
                    <asp:ListItem Text ="All" Selected="True"/>
                    <asp:ListItem Text ="Custom" />
                </asp:RadioButtonList>
                <asp:RadioButtonList ID="SupplierRadioButtonList" runat="server" OnSelectedIndexChanged="SupplierRadioButtonList_SelectedIndexChanged" AutoPostBack="true" Visible="false">
                    <asp:ListItem Text ="All" Selected="True"/>
                    <asp:ListItem Text ="Custom" />
                </asp:RadioButtonList>
            </td>
        </tr>

                <tr><td><br /><br /><br /></td><td class="auto-style1"/><td><br /><br /><br /></td><td class="auto-style1"/><td><br /><br /><br /></td></tr>

        <%--Row 3--%>
        <tr>
            <td>
    &nbsp;<asp:DropDownList ID="DurationDropDownList" runat="server" Visible="false">
    </asp:DropDownList>
    
    <asp:Button ID="DurationAddButton" runat="server" Text="Add" Visible="false" OnClick="DurationAddButton_Click"/>
                
                <asp:Label ID="FromLabel" runat="server" Text="From:" Visible="false"/>
                <asp:DropDownList ID="FromDropDownList" runat="server" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="FromDropDownList_SelectedIndexChanged"/>
            </td>
                        <td class="auto-style1"></td>
            <td>
    <asp:DropDownList ID="CategoryDropDownList" runat="server" Visible="false">
    </asp:DropDownList>
    
    <asp:Button ID="CategoryAddButton" runat="server" Text="Add" Visible="false" OnClick="CategoryAddButton_Click"/>
            </td>
                                    <td class="auto-style1"></td>
            <td>
                    <asp:DropDownList ID="SharedDropDownList" runat="server" Visible="false">
    </asp:DropDownList>
    
    <asp:Button ID="SharedAddButton" runat="server" Text="Add" Visible="false" OnClick="SharedAddButton_Click"/>
            </td>

        </tr>

                        <tr><td><br /><br /><br /></td><td class="auto-style1"/><td><br /><br /><br /></td><td class="auto-style1"/><td><br /><br /><br /></td></tr>

        <%--Row 4--%>
        <tr>
            <td>
                  <asp:GridView ID="DurationGridView" runat="server" AutoGenerateColumns="False" Visible="false">
                      <Columns>
                                                  <asp:TemplateField HeaderText="Month">
                            <ItemTemplate>
                                <asp:Label ID="DurationItemLabel" runat="server" Text="<%#Container.DataItem %>"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="RemoveDurationBtn" runat="server" Text="Remove" OnClick="RemoveDurationBtn_Click"/>
                            </ItemTemplate>
                        </asp:TemplateField>

                      </Columns>
                  </asp:GridView>
            </td>
            <td class="auto-style1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
            <td>
                <asp:GridView ID="CategoryGridView" runat="server" AutoGenerateColumns="False" Visible="false">
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
                <asp:GridView ID="SharedGridView" runat="server" AutoGenerateColumns="False" Visible="false">
                    <Columns>
                        <asp:TemplateField HeaderText="Added">
                            <ItemTemplate>
                                <asp:Label ID="DepartmentItemLabel" runat="server" Text="<%#Container.DataItem %>"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="RemoveSharedBtn" runat="server" Text="Remove" OnClick="RemoveSharedBtn_Click"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    
    <br />
    <br />
  
    <asp:Label ID="SplitLabel" runat="server" Text="Split report by: " Visible="false"/><br />
    <asp:RadioButtonList ID="SplitReportRadioButtonList" runat="server" Visible="false">
                <asp:ListItem Text="Department" Selected="True"/>
        <asp:ListItem Text="Category" />
    </asp:RadioButtonList>
    <asp:RadioButtonList ID="SplitRORReportRadioButtonList" runat="server" Visible="false">
                <asp:ListItem Text="Supplier" Selected="True"/>
        <asp:ListItem Text="Category" />
    </asp:RadioButtonList>
  <br />
    <br />
    <asp:Button ID="GenerateButton" runat="server" CssClass="button" Text="Generate Report" OnClick="GenerateButton_Click" Visible="false"/>
    <br />
    <br />
</asp:Content>


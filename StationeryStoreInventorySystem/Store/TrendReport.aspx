<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TrendReport.aspx.cs" Inherits="RequisitionTrend" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <%--AUTHOR : ARIZ ARMAND BIN ABDUL RAHMAN--%>
    <div class="updateDeptHead"><h2 class="mainPageHeader">
        <asp:Label ID="LblHeader" runat="server" Text="Please select a valid Trend Report on the Menu"></asp:Label></h2></div>
    <br />
    <br />

    <table id ="TblMain" aria-hidden="true" runat="server" width="98%">
        <%--Row1--%>
        <tr style="color:white;background-color:#424242;height:30px"><td style="width:25%">
            <asp:Label runat="server" Text="Select Duration:" ID="LblDuration" Visible="false"></asp:Label><br />
            </td>
          


            <td style="width:20%">
                <asp:Label ID="LblSelectCategory" runat="server" Text="Select Category:" Visible="false"/><br />
            </td>
           
            <td style="width:40%">
                <asp:Label ID="LblDepartment" runat="server" Text="Select Department:" Visible="false" />   <asp:Label ID="SupplierLabel" runat="server" Text="Select Supplier:" Visible="false" />       
            </td>
              
            <th style="background-color:black;text-align:center;border:1px solid black">
                 <asp:Label ID="LblSplit" runat="server" Text="Split report by: " Visible="false"/><br />
            </th>
            </tr>

<%--        Row 2--%>
        <tr>
            <td>
                                            <asp:RadioButtonList ID="RBtnLDuration" runat="server" OnSelectedIndexChanged="RBtnLDuration_SelectedIndexChanged" AutoPostBack="true" Visible="false">
                <asp:ListItem Text ="Past 3 Months" Selected="True"></asp:ListItem>
                <asp:ListItem Text ="Range (3 Months)" />
                <asp:ListItem Text ="Custom (Select 3 Months)" />
            </asp:RadioButtonList>
            </td>
                       
                        <td>
                                <asp:RadioButtonList ID="RBtnLCategory" runat="server" OnSelectedIndexChanged="RBtnLCategory_SelectedIndexChanged" AutoPostBack="true" Visible="false">
                    <asp:ListItem Text ="All" Selected="True"/>
                    <asp:ListItem Text ="Custom" />
                </asp:RadioButtonList>            
            </td>
                
            <td>
                                <asp:RadioButtonList ID="RBtnLDepartment" runat="server" OnSelectedIndexChanged="RBtnLDepartment_SelectedIndexChanged" AutoPostBack="true" Visible="false">
                    <asp:ListItem Text ="All" Selected="True"/>
                    <asp:ListItem Text ="Custom" />
                </asp:RadioButtonList>
                <asp:RadioButtonList ID="RBtnLSupplier" runat="server" OnSelectedIndexChanged="RBtnLSupplier_SelectedIndexChanged" AutoPostBack="true" Visible="false">
                    <asp:ListItem Text ="All" Selected="True"/>
                    <asp:ListItem Text ="Custom" />
                </asp:RadioButtonList>
            </td>
             
            <th style="border:1px solid black">
                 <asp:RadioButtonList ID="RBtnLSplit" runat="server" Visible="false">
                <asp:ListItem Text="Department" Selected="True"/>
        <asp:ListItem Text="Category" />
    </asp:RadioButtonList>
    <asp:RadioButtonList ID="RBtnLSplitROR" runat="server" Visible="false">
                <asp:ListItem Text="Supplier" Selected="True"/>
        <asp:ListItem Text="Category" />
    </asp:RadioButtonList>
            </th>
        </tr>

                <tr><td><br /><br /><br /></td><td class="auto-style1"/><td><br /><br /><br /></td><td class="auto-style1"/><td><br /><br /><br /></td></tr>

        <%--Row 3--%>
        <tr>
            <td>
    &nbsp;<asp:DropDownList ID="DDLDuration" runat="server" Visible="false">
    </asp:DropDownList>
    
    <asp:Button ID="BtnDurationAdd" runat="server" Text="Add" Visible="false" OnClick="BtnDurationAdd_Click" CssClass="alert-success"/>
                                <br /><asp:Label ID="LblDurAlert" runat="server" Text="Please Click Add after Selecting!" ForeColor="Red" Visible="false"/>
                <asp:Label ID="LblFrom" runat="server" Text="From:" Visible="false"/>
                <asp:DropDownList ID="DDLFrom" runat="server" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="DDLFrom_SelectedIndexChanged"/>
            </td>
                      
            <td>
    <asp:DropDownList ID="DDLCategory" runat="server" Visible="false">
    </asp:DropDownList>
    
    <asp:Button ID="BtnCategoryAdd" runat="server" Text="Add" Visible="false" OnClick="BtnCategoryAdd_Click" CssClass="alert-success"/>
                <br /><asp:Label ID="LblCatAlert" runat="server" Text="Please Click Add after Selecting!" ForeColor="Red" Visible="false"/>
            </td>
                                   
            <td>
                    <asp:DropDownList ID="DDLShared" runat="server" Visible="false">
    </asp:DropDownList>
    
    <asp:Button ID="BtnSharedAdd" runat="server" Text="Add" Visible="false" OnClick="BtnSharedAdd_Click" CssClass="alert-success"/>
                <br /><asp:Label ID="LblSharedAlert" runat="server" Text="Please Click Add after Selecting!" ForeColor="Red" Visible="false"/>
            </td>

        </tr>

                        <tr><td><br /><br /><br /></td><td class="auto-style1"/><td><br /><br /><br /></td><td class="auto-style1"/><td><br /><br /><br /></td></tr>

        <%--Row 4--%>
        <tr>
            <td>
                  <asp:GridView ID="GVDuration" runat="server" AutoGenerateColumns="False" Visible="false"  HeaderStyle-BackColor="#464646" HeaderStyle-ForeColor="white">
                      <Columns>
                                                  <asp:TemplateField HeaderText="Month">
                            <ItemTemplate>
                                <asp:Label ID="LblDurationItem" runat="server" Text="<%#Container.DataItem %>"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="BtnRemoveDuration" runat="server" Text="Remove" OnClick="BtnRemoveDuration_Click" CssClass="alert-warning"/>
                            </ItemTemplate>
                        </asp:TemplateField>

                      </Columns>
                  </asp:GridView>
            </td>
           <td>
                <asp:GridView ID="GVCategory" runat="server" AutoGenerateColumns="False" Visible="false"  HeaderStyle-BackColor="#464646" HeaderStyle-ForeColor="white">
                    <Columns>
                        <asp:TemplateField HeaderText="Category">
                            <ItemTemplate>
                                <asp:Label ID="LblCategoryItem" runat="server" Text="<%#Container.DataItem %>"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="BtnRemoveCategory" runat="server" Text="Remove" OnClick="BtnRemoveCategory_Click" CssClass="alert-warning"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
              <td>
                <asp:GridView ID="GVShared" runat="server" AutoGenerateColumns="False" Visible="false" HeaderStyle-BackColor="#464646" HeaderStyle-ForeColor="white">
                    <Columns>
                        <asp:TemplateField HeaderText="Added">
                            <ItemTemplate>
                                <asp:Label ID="LblDepartmentItem" runat="server" Text="<%#Container.DataItem %>"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField >
                            <ItemTemplate>
                                <asp:Button ID="BtnRemoveShared" runat="server" Text="Remove" OnClick="BtnRemoveShared_Click" CssClass="alert-warning"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    
    <br />
    <br />
  
   
   
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="BtnGenerate" runat="server" CssClass="button" Text="Generate Report" OnClick="BtnGenerate_Click" Visible="false"/>
  <br />
    <br />
    <br />
    <br />
</asp:Content>


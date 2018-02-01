<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RetrievalShortfall.aspx.cs" Inherits="RetrievalDecision" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style1 {
            height: 22px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <div class="row updateDeptHead">
        <h2 class="mainPageHeader">Retrieval Shortfall</h2>
    </div>
    <br />
    <br />
    <br />

    <div>
        <%--<h2>There are insufficient numbers of quantities for the following items.</h2>--%>
        <%--<h3>Please allocate the items to departments</h3>--%>

        <asp:GridView ID="gvMain" runat="server" AutoGenerateColumns="False" CssClass="mGrid " RowStyle-Height="50px">
            <Columns>
                <asp:TemplateField HeaderText="Item Description">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="itemDescription" runat="server" Text='<%# Bind("Description")%>'></asp:Label>
                        <asp:HiddenField ID="hdfItemCode" runat="server" Value='<%# Bind("ItemCode")%>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Retrieved Quantity">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="availableQuantity" runat="server" Text='<%# Bind("Qty")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Breakdown by Department">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                    </EditItemTemplate>

                    <ItemTemplate>
                         <asp:Label ID="totalActualQuantityValidator" runat="server" ForeColor="Red" ></asp:Label>
                        <asp:GridView ID="gvSub" runat="server" AutoGenerateColumns="False" CssClass="mGrid" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="Requested Date">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="RequestDate" runat="server" Text='<%# Bind("RequestDate")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Department Name">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="DeptName" runat="server" Text='<%# Bind("DeptName")%>'></asp:Label>
                                        <asp:HiddenField ID="hdfdeptCode" runat="server" Value='<%# Bind("deptCode")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Requested Quantity">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="requestedQty" runat="server" Text='<%# Bind("RequestedQty")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Allocated Actual Quantity">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtActualQuantity" runat="server"></asp:TextBox>
                                       
                                        <asp:RangeValidator ID="RangeValidator1" runat="server" display="Dynamic" ErrorMessage="Invalid Quantity!" ControlToValidate="txtActualQuantity"  MinimumValue="0" Style="color: red"></asp:RangeValidator>
                                        <br />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" display="Dynamic" ErrorMessage="Please enter the Qty" ControlToValidate="txtActualQuantity" Style="color: red"></asp:RequiredFieldValidator>

                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </ItemTemplate>
                </asp:TemplateField>


            </Columns>
        </asp:GridView>

    </div>
    <div>
        <asp:Button ID="BtnGenerateDisbursementList" runat="server" Text="Finalize Disbursement List" CssClass="button" OnClick="BtnGenerateDisbursementList_Click" />
    </div>


</asp:Content>



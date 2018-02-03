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
        <asp:GridView ID="gvMain" runat="server" AutoGenerateColumns="False" CssClass="mGrid " RowStyle-Height="50px">
            <Columns>
                <asp:TemplateField HeaderText="Item Description">
                    <ItemTemplate>
                        <asp:Label ID="lblItemDescription" runat="server" Text='<%# Bind("Description")%>'></asp:Label>
                        <asp:HiddenField ID="hdfItemCode" runat="server" Value='<%# Bind("ItemCode")%>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Retrieved Quantity">
                    <ItemTemplate>
                        <asp:Label ID="lblRetrievedQuantity" runat="server" Text='<%# Bind("Qty")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Breakdown by Department">
                    <ItemTemplate>
                         <asp:Label ID="lblTotalActualQuantityValidator" runat="server" ForeColor="Red" ></asp:Label>

                        <asp:GridView ID="gvSub" runat="server" AutoGenerateColumns="False" CssClass="mGrid" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="Requested Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRequestDate" runat="server" Text='<%# Bind("RequestDate","{0:dddd, dd MMMM yyyy}")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="200px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Department Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDeptName" runat="server" Text='<%# Bind("DeptName")%>'></asp:Label>
                                        <asp:HiddenField ID="hdfdeptCode" runat="server" Value='<%# Bind("deptCode")%>' />
                                    </ItemTemplate>
                                       <ItemStyle Width="220px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Requested Quantity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRequestedQty" runat="server" Text='<%# Bind("RequestedQty")%>'></asp:Label>
                                    </ItemTemplate>
                                       <ItemStyle Width="140px" />

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Allocated Actual Quantity">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtActualQuantity" runat="server"></asp:TextBox>
                                       
                                        <asp:RangeValidator ID="rng" runat="server" display="Dynamic" ErrorMessage="Invalid Quantity!" ControlToValidate="txtActualQuantity"  MinimumValue="0" Style="color: red"></asp:RangeValidator>
                                        <br />
                                        <asp:RequiredFieldValidator ID="req" runat="server" display="Dynamic" ErrorMessage="Please enter the Qty" ControlToValidate="txtActualQuantity" Style="color: red"></asp:RequiredFieldValidator>
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
        <asp:Button ID="btnFinalizeDisbursementList" runat="server" Text="Finalize Disbursement List" CssClass="button" OnClick="BtnFinalizeDisbursementList_Click" />
    </div>


</asp:Content>



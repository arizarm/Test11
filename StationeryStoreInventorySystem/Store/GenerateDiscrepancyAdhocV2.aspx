<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="GenerateDiscrepancyAdhocV2.aspx.cs" Inherits="GenerateDiscrepancyAdhocV2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <div class="updateDeptHead"><h2 class="mainPageHeader">Discrepancies Summary</h2></div>
        <br />
        <asp:GridView ID="gvDiscrepancies" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
            <Columns>
                <asp:TemplateField HeaderText="Item Code">
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("Key.Key.ItemCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Item Description">
                    <ItemTemplate>
                        <asp:Label ID="lblDesc" runat="server" Text='<%# Bind("Key.Key.Description") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>               
                <asp:TemplateField HeaderText="Unit of Measure">
                    <ItemTemplate>
                        <asp:Label ID="lblUom" runat="server" Text='<%# Bind("Key.Key.UnitOfMeasure") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Quantity in Stock" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblStock" runat="server" Text='<%# Bind("Key.Key.BalanceQty") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Actual Quantity" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblActual" runat="server" Text='<%# Bind("Key.Value") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Adjustment" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <% %>
                        <asp:Label ID="lblAdj" runat="server" Text='<%# Bind("Value") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Remarks (Max 100 characters)" ValidateRequestMode="Enabled">
                    <ItemTemplate>
                        <asp:TextBox ID="txtRemarks" runat="server" Width="300" MaxLength="100"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Label ID="lblErrorCharLimit" runat="server" ForeColor="Red" Text=""></asp:Label>
        <br />
        <asp:Label ID="lblRequired" runat="server" ForeColor="Red" Text=""></asp:Label>
        <br />
        <asp:Button ID="btnSubmit" runat="server" Text="Submit Discrepancy List"  CssClass="button" OnClick="BtnSubmit_Click"/>     
    </div>
</asp:Content>


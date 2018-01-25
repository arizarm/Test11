<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReorderReport.aspx.cs" Inherits="ReorderReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Content/JavaScript.js"></script>
    <h2 class="mainPageHeader">Reorder Report

    </h2>
    Start Date:
                <asp:TextBox ID="startDate" runat="server" ClientIDMode="Static" ></asp:TextBox>
    <asp:RequiredFieldValidator runat="server" ControlToValidate="startDate" Display="Dynamic" ErrorMessage="Please enter the Start Date" ForeColor="Red" ValidationGroup="DateValGrp"></asp:RequiredFieldValidator>
    <br />
    <br />
    End Date:&nbsp;
    <asp:TextBox ID="endDate"  runat="server" ClientIDMode="Static" ></asp:TextBox>
    <asp:RequiredFieldValidator runat="server" ControlToValidate="endDate" Display="Dynamic" ErrorMessage="Please enter the End Date" ForeColor="Red" ValidationGroup="DateValGrp"></asp:RequiredFieldValidator>
    <br />
    <br />
    <br />
    <asp:Button ID="GenerateBtn" runat="server" Text="Generate Report" CssClass="button" CausesValidation="true" OnClick="GenerateBtn_Click" ValidationGroup="DateValGrp" />
    <asp:CustomValidator runat="server" Display="Dynamic" ErrorMessage="Start Date cannot be lesser than End Date" ValidationGroup="DateValGrp" ForeColor="Red" OnServerValidate="CompareDateValidator"></asp:CustomValidator>
    <br />
    <h4>Reorder Report between
        <asp:Label ID="sDate" runat="server" Text=""></asp:Label>
        -<asp:Label ID="eDate" runat="server" Text=""></asp:Label>
    </h4>
    <br />

    <asp:Label runat="server" Text="" ForeColor="Red" ID="txtLbl"></asp:Label>
    <br />
    <br />
    <asp:GridView runat="server" AutoGenerateColumns="False" ID="gvPurchasedreoderItem" EmptyDataText="No records founds within the dates !!" EmptyDataRowStyle-BackColor="Window">
        <Columns>
            <asp:BoundField HeaderText="ItemCode" DataField="ItemCode">
                    <ControlStyle Font-Size="10pt" Height="65px"/>
                    <HeaderStyle Font-Size="11pt" HorizontalAlign="Center" Wrap="False"  Height="45px"  />
                    <ItemStyle Font-Size="Smaller" Height="45px" HorizontalAlign="Center" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField HeaderText="Description" DataField="Description">
                    <ControlStyle Font-Size="10pt" Width="135px" Height="65px"/>
                    <HeaderStyle Font-Size="11pt" HorizontalAlign="Center" Wrap="False" Height="45px" />
                     <ItemStyle Font-Size="Smaller" Height="45px" HorizontalAlign="Center" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField HeaderText="QuantityOnHand" DataField="Balance">
                 <ControlStyle Font-Size="10pt" Height="65px"/>
                <HeaderStyle Font-Size="11pt" HorizontalAlign="Center" Wrap="False" Height="45px"/>
                   <ItemStyle Font-Size="Smaller" Height="45px" HorizontalAlign="Center" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField HeaderText="ReorderLevel" DataField="ReorderLevel">
                 <ControlStyle Font-Size="10pt" Height="65px"/>
                <HeaderStyle Font-Size="11pt" HorizontalAlign="Center" Wrap="False" Height="45px"/>
                     <ItemStyle Font-Size="Smaller" Height="45px" HorizontalAlign="Center" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField HeaderText="ReorderQuantity" DataField="ReorderQuantity">
                 <ControlStyle Font-Size="10pt" Height="65px"/>
                <HeaderStyle Font-Size="11pt" HorizontalAlign="Center" Wrap="False" Height="45px"/>
                   <ItemStyle Font-Size="Smaller" Height="45px" HorizontalAlign="Center" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField HeaderText="PurchaseOrderNo" DataField="PurchaseOrderNo">
                 <ControlStyle Font-Size="10pt" Height="65px"/>
                <HeaderStyle Font-Size="11pt" HorizontalAlign="Center" Wrap="False" Height="45px"/>
                     <ItemStyle Font-Size="Smaller" Height="45px" HorizontalAlign="Center" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField HeaderText="ExpectedDate" DataField="FormattedExpectedDate">
                 <ControlStyle Font-Size="10pt" Height="65px"/>
                <HeaderStyle Font-Size="11pt" HorizontalAlign="Center" Wrap="False" Height="45px"/>
                   <ItemStyle Font-Size="Smaller" Height="45px" HorizontalAlign="Center" Wrap="False" />
            </asp:BoundField>
        </Columns>

        <EmptyDataRowStyle BackColor="Window"></EmptyDataRowStyle>

    </asp:GridView>

    <br />
    <br />
    <br />
    <br />
    <asp:Label runat="server" Text="" ForeColor="Red" ID="txtLbl2"></asp:Label>
    <br />
    <br />
    <asp:GridView runat="server" AutoGenerateColumns="False" ID="gvShortfallItems" EmptyDataText="No records founds within the dates !!" EmptyDataRowStyle-BackColor="Window">
        <Columns>
            <asp:BoundField HeaderText="ItemCode" DataField="ItemCode">
               <ControlStyle Font-Size="10pt" Height="65px"/>
                    <HeaderStyle Font-Size="11pt" HorizontalAlign="Center" Wrap="False"  Height="45px"  />
                    <ItemStyle Font-Size="Smaller" Height="45px" HorizontalAlign="Center" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField HeaderText="Description" DataField="Description">
                <ControlStyle Font-Size="10pt" Width="135px" Height="65px"/>
                    <HeaderStyle Font-Size="11pt" HorizontalAlign="Center" Wrap="False" Height="45px" />
                     <ItemStyle Font-Size="Smaller" Height="45px" HorizontalAlign="Center" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField HeaderText="QuantityOnHand" DataField="Balance">
                 <ControlStyle Font-Size="10pt" Height="65px"/>
                <HeaderStyle Font-Size="11pt" HorizontalAlign="Center" Wrap="False" Height="45px"/>
                   <ItemStyle Font-Size="Smaller" Height="45px" HorizontalAlign="Center" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField HeaderText="ReorderLevel" DataField="ReorderLevel">
              <ControlStyle Font-Size="10pt" Height="65px"/>
                <HeaderStyle Font-Size="11pt" HorizontalAlign="Center" Wrap="False" Height="45px"/>
                     <ItemStyle Font-Size="Smaller" Height="45px" HorizontalAlign="Center" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField HeaderText="ReorderQuantity" DataField="ReorderQuantity">
                <ControlStyle Font-Size="10pt" Height="65px"/>
                <HeaderStyle Font-Size="11pt" HorizontalAlign="Center" Wrap="False" Height="45px"/>
                   <ItemStyle Font-Size="Smaller" Height="45px" HorizontalAlign="Center" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField HeaderText="PurchaseOrderNo" DataField="NullablePurchaseOrderNo">
                <ControlStyle Font-Size="10pt" Height="65px"/>
                <HeaderStyle Font-Size="11pt" HorizontalAlign="Center" Wrap="False" Height="45px"/>
                     <ItemStyle Font-Size="Smaller" Height="45px" HorizontalAlign="Center" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField HeaderText="ExpectedDate" DataField="FormattedExpectedDate">
                 <ControlStyle Font-Size="10pt" Height="65px"/>
                <HeaderStyle Font-Size="11pt" HorizontalAlign="Center" Wrap="False" Height="45px"/>
                   <ItemStyle Font-Size="Smaller" Height="45px" HorizontalAlign="Center" Wrap="False" />
            </asp:BoundField>
        </Columns>

<EmptyDataRowStyle BackColor="Window"></EmptyDataRowStyle>

    </asp:GridView>

</asp:Content>


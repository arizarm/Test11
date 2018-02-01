﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReorderReport.aspx.cs" Inherits="ReorderReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
     <style>
          .gvHeaderColumn{
            /*padding-left:20px;
            margin-left:40px;*/
            text-align:center;
        }
     </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Content/JavaScript.js"></script>
   
    <div class="row updateDeptHead">
        <h2 class="mainPageHeader">Reorder Report</h2>
    </div>
     <br />
    <br />
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
    <asp:CustomValidator runat="server" Display="Dynamic"  ValidationGroup="DateValGrp" ForeColor="Red" OnServerValidate="CompareDateValidator"></asp:CustomValidator>
    <asp:Label Id="errorTxt" runat="server" Text=""></asp:Label>
    <Button type="button" id="printreport" class="btn btn-default pull-right" style="margin-right:140px"  onclick="printDiv()">Print Reorder Report</Button>
    
    <br />
    <h4>Reorder Report between
        <asp:Label ID="sdate" runat="server" Text=""></asp:Label>
        -<asp:Label ID="edate" runat="server" Text=""></asp:Label>
    </h4>
    <br />

    <asp:Label runat="server" Text="" ID="txtLbl" Font-Bold="true"></asp:Label>
    <br />
    <br />
    <div id="printable">:
    <asp:GridView runat="server" AutoGenerateColumns="False" ID="gvPurchasedreoderItem"  EmptyDataRowStyle-BackColor="Window" CssClass="mGrid" RowStyle-Height="50px" >
        <Columns>
            <asp:BoundField HeaderText="ItemCode" DataField="ItemCode" HeaderStyle-CssClass="gvHeaderColumn">
                    <ControlStyle Font-Size="10pt"/>
                    <HeaderStyle Font-Size="11pt" HorizontalAlign="Center" Wrap="False"    Width="100px"/>
                    <ItemStyle Font-Size="Smaller" HorizontalAlign="Center" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField HeaderText="Description" DataField="Description" HeaderStyle-CssClass="gvHeaderColumn">
                    <ControlStyle Font-Size="10pt" Width="135px" />
                    <HeaderStyle Font-Size="11pt" HorizontalAlign="Center" Wrap="False" />
                     <ItemStyle Font-Size="Smaller"  HorizontalAlign="Center" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField HeaderText="Availabe Quantity" DataField="Balance" HeaderStyle-CssClass="gvHeaderColumn">
                 <ControlStyle Font-Size="10pt" />
                <HeaderStyle Font-Size="11pt" HorizontalAlign="Center" Wrap="False"  Width="150px"/>
                   <ItemStyle Font-Size="Smaller" HorizontalAlign="Center" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField HeaderText="Reorder Level" DataField="ReorderLevel" HeaderStyle-CssClass="gvHeaderColumn">
                 <ControlStyle Font-Size="10pt" />
                <HeaderStyle Font-Size="11pt" HorizontalAlign="Center" Wrap="False"  Width="150px"/>
                     <ItemStyle Font-Size="Smaller"  HorizontalAlign="Center" Wrap="False" />
            </asp:BoundField> 
            <asp:BoundField HeaderText="ReorderQuantity" DataField="ReorderQuantity" HeaderStyle-CssClass="gvHeaderColumn">
                 <ControlStyle Font-Size="10pt" />
                <HeaderStyle Font-Size="11pt" HorizontalAlign="Center" Wrap="False"  Width="150px"/>
                   <ItemStyle Font-Size="Smaller"  HorizontalAlign="Center" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField HeaderText="PurchaseOrderNo" DataField="PurchaseOrderNo" HeaderStyle-CssClass="gvHeaderColumn">
                 <ControlStyle Font-Size="10pt" />
               <HeaderStyle Font-Size="11pt" HorizontalAlign="Center" Wrap="False"  Width="170px"/>
                <ItemStyle Font-Size="Smaller"  HorizontalAlign="Center" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField HeaderText="ExpectedDate" DataField="FormattedExpectedDate" HeaderStyle-CssClass="gvHeaderColumn">
                 <ControlStyle Font-Size="10pt" />
                <HeaderStyle Font-Size="11pt" HorizontalAlign="Center" Wrap="False"  Width="150px"/>
                   <ItemStyle Font-Size="Smaller"  HorizontalAlign="Center" Wrap="False" />
            </asp:BoundField>
        </Columns>

        <EmptyDataRowStyle BackColor="Window"></EmptyDataRowStyle>
        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
            <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#242121" />
    </asp:GridView>

    <br />
    <br />
    <br />
    <br />
    <asp:Label runat="server" Text=""  ID="txtLbl2" Font-Bold="true"></asp:Label>
    <br />
    <br />
    <asp:GridView runat="server" AutoGenerateColumns="False" ID="gvShortfallItems"  EmptyDataRowStyle-BackColor="Window">
        <Columns>
            <asp:BoundField HeaderText="ItemCode" DataField="ItemCode" HeaderStyle-CssClass="gvHeaderColumn">
               <ControlStyle Font-Size="10pt" />
                    <HeaderStyle Font-Size="11pt" HorizontalAlign="Center" Wrap="False"   Width="100px"  />
                    <ItemStyle Font-Size="Smaller"  HorizontalAlign="Center" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField HeaderText="Description" DataField="Description" HeaderStyle-CssClass="gvHeaderColumn">
                <ControlStyle Font-Size="10pt" Width="135px" />
                    <HeaderStyle Font-Size="11pt" HorizontalAlign="Center" Wrap="False"  />
                     <ItemStyle Font-Size="Smaller"  HorizontalAlign="Center" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField HeaderText="QuantityOnHand" DataField="Balance" HeaderStyle-CssClass="gvHeaderColumn">
                 <ControlStyle Font-Size="10pt" />
                <HeaderStyle Font-Size="11pt" HorizontalAlign="Center" Wrap="False"   Width="150px"/>
                   <ItemStyle Font-Size="Smaller"  HorizontalAlign="Center" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField HeaderText="ReorderLevel" DataField="ReorderLevel" HeaderStyle-CssClass="gvHeaderColumn">
              <ControlStyle Font-Size="10pt" />
                <HeaderStyle Font-Size="11pt" HorizontalAlign="Center" Wrap="False"   Width="150px"/>
                     <ItemStyle Font-Size="Smaller"  HorizontalAlign="Center" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField HeaderText="ReorderQuantity" DataField="ReorderQuantity" HeaderStyle-CssClass="gvHeaderColumn">
                <ControlStyle Font-Size="10pt" />
                <HeaderStyle Font-Size="11pt" HorizontalAlign="Center" Wrap="False"   Width="150px"/>
                   <ItemStyle Font-Size="Smaller"  HorizontalAlign="Center" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField HeaderText="PurchaseOrderNo" DataField="NullablePurchaseOrderNo" HeaderStyle-CssClass="gvHeaderColumn">
                <ControlStyle Font-Size="10pt" />
                <HeaderStyle Font-Size="11pt" HorizontalAlign="Center" Wrap="False"   Width="170px"/>
                     <ItemStyle Font-Size="Smaller"  HorizontalAlign="Center" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField HeaderText="ExpectedDate" DataField="FormattedExpectedDate" HeaderStyle-CssClass="gvHeaderColumn">
                 <ControlStyle Font-Size="10pt" />
                <HeaderStyle Font-Size="11pt" HorizontalAlign="Center" Wrap="False"   Width="150px"/>
                   <ItemStyle Font-Size="Smaller"  HorizontalAlign="Center" Wrap="False" />
            </asp:BoundField>
        </Columns>

<EmptyDataRowStyle BackColor="Window"></EmptyDataRowStyle>
         <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
            <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#242121" />
    </asp:GridView>
  </div>

</asp:Content>


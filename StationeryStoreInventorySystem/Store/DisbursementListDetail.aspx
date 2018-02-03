<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DisbursementListDetail.aspx.cs" Inherits="DisbursementListDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Content/JavaScript.js"></script>
    <style type="text/css">
        .auto-style1 {
            width: 97%;
        }

        .auto-style2 {
            width: 158px;
        }

        .auto-style3 {
            width: 832px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div class="updateDeptHead">
        <h2 class="mainPageHeader">Details Disbursement List</h2>
    </div>
    <br />
    <br />
    <table style="width: 98%">
        <tr>
            <td>
                <button type="button" id="btnForPrint" runat="server" class="btn btn-default pull-right" onclick="printDiv()">Print Disbursement</button>
            </td>
        </tr>
    </table>

    <div id="printable">

        <br />
        <table class="auto-style1">
            <tr>
                <td class="auto-style2"><b>Date :<br />
                    <br />
                </b></td>
                <td class="auto-style3">
                    <asp:Label ID="lblDate" runat="server"></asp:Label><br />
                    <br />
                </td>
            </tr>
            <tr>
                <td class="auto-style2"><b>Time :</b><br />
                    <br />
                </td>
                <td class="auto-style3">
                    <asp:Label ID="lblTime" runat="server"></asp:Label><br />
                    <br />
                </td>
            </tr>
            <tr>
                <td class="auto-style2"><b>Department :</b><br />
                    <br />
                </td>
                <td class="auto-style3">
                    <asp:Label ID="lblDepartment" runat="server"></asp:Label><br />
                    <br />
                </td>
            </tr>
            <tr>
                <td class="auto-style2"><b>Collection Point :</b><br />
                    <br />
                </td>
                <td class="auto-style3">
                    <asp:Label ID="lblColPoint" runat="server"></asp:Label><br />
                    <br />
                </td>
            </tr>
        </table>

        <asp:GridView ID="gvDisbDetail" runat="server" CssClass="mGrid" AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="Item Description">
                    <ItemTemplate>
                        <asp:Label ID="lblitemDesc" runat="server" Text='<%# Bind("itemDesc") %>'></asp:Label>
                        <asp:HiddenField ID="hdnflditemCode" runat="server" Value='<%# Bind("itemCode") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total Requested Quantity">
                    <ItemTemplate>
                        <asp:Label ID="lblreqQty" runat="server" Text='<%# Bind("reqQty") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Actual Quantity">
                    <ItemTemplate>
                        <asp:TextBox ID="txtactualQty" runat="server" Text='<%# Bind("actualQty") %>'></asp:TextBox>
                        <asp:Label ID="lblActualError" runat="server" ForeColor="Red"></asp:Label>
                        <asp:RangeValidator Display="Dynamic" ID="RangeValidator1" ValidationGroup="1" runat="server" ErrorMessage="Invalid Quantity!" ControlToValidate="txtactualQty" MaximumValue='<%# Eval("reqQty") %>' MinimumValue="0" Style="color: red"></asp:RangeValidator>
                        <br />
                        <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator1" ValidationGroup="1" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="txtactualQty" Style="color: red" EnableViewState="True">Quantity cannot be empty!</asp:RequiredFieldValidator>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Remarks" SortExpression="Remarks">
                    <ItemTemplate>
                        <asp:TextBox Width="100%" ID="txtremarks" runat="server" Text='<%# Bind("remarks") %>'></asp:TextBox>
                        <br />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>    

    <br />
    <br />
    <asp:Label ID="Label1" runat="server" Text="Enter Access Code : "></asp:Label>
    <asp:TextBox ID="txtAccessCode" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ValidationGroup="1" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="txtAccessCode" Style="color: red">Access Code cannot be empty!</asp:RequiredFieldValidator>
    <br />
    <br />
    <br />
        </div>
    <asp:Button ID="btnReset" runat="server" Text="Reset Data" CssClass="button" OnClick="btnReset_Click" />
    <asp:Button ID="btnAck" runat="server" Text="Acknowledge" ValidationGroup="1" CssClass="button" OnClick="btnAck_Click" />
</asp:Content>


<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/StyleSheet.css" rel="stylesheet" />
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            $("[id$=txtDate]").datepicker();
        });
    </script>

</head>
<body>
    <form id="form1" runat="server" action="Login.aspx">
        <div class="wrapper">
            <div class="header row">
                <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/Main.aspx">
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/images/logo.jpeg" CssClass="logo" />
                </asp:HyperLink>
            </div>


            <h3 style="color: rgba(118,180,50,1);">Stationery Deparment</h3>
            <div class="col-md-6">
                <div class="form-group">
                    <asp:Label ID="Label1" runat="server" Text="Enter User ID" for="TextBox1"></asp:Label>&nbsp&nbsp
    <asp:TextBox ID="TextBox1" runat="server" class="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="Label2" runat="server" Text="Enter Password" for="Password1"></asp:Label>&nbsp&nbsp
    <input id="Password1" type="password" runat="server" class="form-control" />
                </div>
                <asp:Label ID="Label3" runat="server"></asp:Label>
                <asp:Button ID="Button1" runat="server" Text="Sign In" CssClass="button" OnClick="Button1_Click" />&nbsp
                <%--<asp:Button ID="Button2" runat="server" Text="Send mail" OnClick="Button2_Click" />--%>
                <asp:Label ID="Label4" runat="server"></asp:Label>
            </div>
        </div>
        <div>
            Remember me?
          <asp:CheckBox ID="Persist" runat="server" />
        </div>
    </form>

</body>
</html>

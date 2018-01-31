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
    <div style="margin:0% 0% 0% 30%;">
        <form id="form1" runat="server" action="Login.aspx">
            <div class="header row">
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/images/logo.jpeg" CssClass="logo"/>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <asp:Label ID="Label1" runat="server" Text="Enter User ID : " for="TextBox1"></asp:Label>&nbsp&nbsp
                    <asp:TextBox ID="TextBox1" runat="server" class="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="TextBox1" ForeColor="Red"
                        ErrorMessage="RequiredFieldValidator">User ID is required</asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <asp:Label ID="Label2" runat="server" Text="Enter Password : " for="Password1"></asp:Label>&nbsp&nbsp
                    <input id="Password1" type="password" runat="server" class="form-control" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                        ControlToValidate="Password1" ForeColor="Red"
                        ErrorMessage="RequiredFieldValidator">Password is required</asp:RequiredFieldValidator>
                </div>
                <asp:Label ID="Label3" runat="server"></asp:Label>
                <asp:Button ID="Button1" runat="server" Text="Sign In" CssClass="button" OnClick="Button1_Click" />&nbsp
               <asp:CheckBox ID="Persist" runat="server" /> Remember me?
                <br />
                <asp:Label ID="Label4" runat="server" ForeColor="Red"></asp:Label>
            </div>       
    </form>
    </div>
    
</body>
</html>

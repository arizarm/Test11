<%@ Application Language="C#" %>

<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        System.Web.UI.ScriptManager.ScriptResourceMapping.AddDefinition
                ("jquery",
                 new System.Web.UI.ScriptResourceDefinition
                 {
                     Path = "~/scripts/jquery-1.12.4.min.js",
                     DebugPath = "~/scripts/jquery-1.12.4.js",
                     CdnPath = "http://ajax.microsoft.com/ajax/jQuery/jquery-1.12.4.min.js",
                     CdnDebugPath = "http://ajax.microsoft.com/ajax/jQuery/jquery-1.12.4.js"
                 }
                );

    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {
        Session["userType"] = "All";
        //Session["userType"] = "Clerk";
        //Session["userType"] = "Supervisor";
        //Session["userType"] = "Head";
        //Session["userType"] = "Employee";
        //Session["userType"] = "Representative";
    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

    //AUTHOR : YIMON SOE
    protected void Application_AuthenticateRequest(Object sender, EventArgs e)
    {
HttpApplication context = (HttpApplication)sender;
        if (context.Request.Url.ToString().Contains(".svc"))
        {
            context.Response.SuppressFormsAuthenticationRedirect = true;
        }

        if (HttpContext.Current.User != null)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if (HttpContext.Current.User.Identity is FormsIdentity)
                {
                    FormsIdentity id =
                    (FormsIdentity)HttpContext.Current.User.Identity;
                    FormsAuthenticationTicket ticket = id.Ticket;

                    // Get the stored user-data, in this case, our roles

                    string userData = ticket.UserData;
                    string[] roles = userData.Split(',');
                    HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(id, roles);
                }
            }
        }
    }

</script>

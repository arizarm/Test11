<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        System.Web.UI.ScriptManager.ScriptResourceMapping.AddDefinition
                ("jquery",
                 new System.Web.UI.ScriptResourceDefinition {
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

</script>

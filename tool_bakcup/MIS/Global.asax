<%@ Application Language="C#" %>

<script runat="server">
          
    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup
        Application["spath"] = Server.MapPath("");
    
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs
        //Session["error"] = Server.GetLastError().InnerException.Message ;

        //HttpContext.Current.Response.Redirect("CustomError.aspx?error=" + Server.GetLastError().Message, true);  

    }

    void Session_Start(object sender, EventArgs e) 
    {
        //Session["conStringIB"] = ConfigurationSettings.AppSettings["conStrIB"].ToString();
        //Session["conStringSQL"] = ConfigurationSettings.AppSettings["conStrSQL"].ToString();        
        // Code that runs when a new session is started
        Session.Timeout = 100000;  

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
        Session.Abandon();
          

    }

    protected void Application_BeginRequest(Object sender, EventArgs e)
    {
        System.Globalization.CultureInfo newCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
        newCulture.DateTimeFormat.ShortDatePattern = "MM/dd/yyyy";
        newCulture.DateTimeFormat.DateSeparator = "/";
        System.Threading.Thread.CurrentThread.CurrentCulture = newCulture;
    }
       
</script>

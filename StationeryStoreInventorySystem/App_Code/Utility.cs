using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.IO;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Web.UI;

/// <summary>
/// Summary description for Utility
/// </summary>
public static class Utility 
{
    //AUTHOR : YIMON SOE
    public static void sendMail(string mailReceiver,string mailSubject,string mailBody)
    {
        MailMessage m = new MailMessage();
        SmtpClient sc = new SmtpClient();
        try
        {
            m.From = new MailAddress("iss.team11.stationery@gmail.com", "Display name");
            m.To.Add(new MailAddress(mailReceiver, "Display name To"));

            m.Subject = mailSubject;
            m.Body = mailBody;

            sc.Host = "smtp.gmail.com";
            sc.Port = 587;
            sc.Credentials = new System.Net.NetworkCredential("iss.team11.stationery@gmail.com", "123!@#iss");
            sc.EnableSsl = true; // runtime encrypt the SMTP communications using SSL
            sc.Send(m);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    //AUTHOR : YIMON SOE
    public static bool checkIsTempDepHead(Employee e)
    {
        DateTime today = DateTime.Now.Date;
        if (e.IsTempHead == "Y")
        {
            if(today >= e.StartDate && today <= e.EndDate )
            {
                return true;
            }
           else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    //AUTHOR : TAN WEN SONG
    public static string FirstUpperCase(string s)
    {
        return s.First().ToString().ToUpper() + s.Substring(1).ToLower();
    }

    //AUTHOR : YIMON SOE
    public static void logout()
    {
        FormsAuthentication.SignOut();
        HttpContext.Current.Session.Remove("emp");
        HttpContext.Current.Session.Remove("empRole");
        HttpContext.Current.Session.Remove("empID");
        HttpContext.Current.Session.Remove("itemlist");
        HttpContext.Current.Session.Remove("itemError");
        HttpContext.Current.Session.Remove("discrepancyDisplay");
        HttpContext.Current.Session.Remove("discrepancyList");
        HttpContext.Current.Session.Remove("monthly");
        HttpContext.Current.Session.Remove("ItemToUpdate");
        HttpContext.Current.Session.Remove("discrepancySummary");
        HttpContext.Current.Session.Remove("RetrievalShortfallItemList");
        HttpContext.Current.Session.Remove("RetrievalID");
        HttpContext.Current.Session.Remove("RequisitionNo");
        HttpContext.Current.Session.Remove("SelectedDisb");
        HttpContext.Current.Session.Remove("disbItemsList");
        HttpContext.Current.Session.Remove("RegenerateDate");
        HttpContext.Current.Session.Remove("RegenerateDep");
        HttpContext.Current.Session.Remove("RequestedByName");
        HttpContext.Current.Session.Remove("RegenerateItems");

        HttpContext.Current.Response.Redirect(LoginController.LoginURI);
    }

    //AUTHOR : ARIZ ARMAND BIN ABDUL RAHMAN
    public static void DisplayAlertMessage(string message)
    {
        Page page = HttpContext.Current.CurrentHandler as Page;
        string script = string.Format("alert('{0}');", message);
        if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
        {
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), "alert", script, true);
        }
    }

    //AUTHOR : ARIZ ARMAND BIN ABDUL RAHMAN
    public static void AlertMessageThenRedirect(string message, string redirectAddress)
    {
        var page = HttpContext.Current.CurrentHandler as Page;
        ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "alert('" + message + "');window.location ='" + redirectAddress + "';", true);
    }
}
﻿using System;
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
    public static bool checkIsTempDepHead(Employee e)
    {   
        if (e.IsTempHead == "Y")
        {
            if(e.StartDate != null && e.EndDate !=null)
            {
                DateTime endDate = (DateTime)e.EndDate;
                DateTime today = DateTime.Now;
                if (today >= e.StartDate && today <= endDate.AddDays(1))
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
        else
        {
            return false;
        }
    }
    public static bool ValidateNewItem(CustomValidator control, string itemCode)
    {
        Item item = EFBroker_Item.GetItembyItemCode(itemCode.ToUpper());
        if (item == null)
        {
            return true;
        }
        else
        {
            if (item.ActiveStatus == "Y")
            {
                control.ErrorMessage = "ItemCode is in use by existing item";
            }
            else
            {
                control.ErrorMessage = "ItemCode is used for archived item";
            }
            return false;
        }
    }
    public static string FirstUpperCase(string s)
    {
        return s.First().ToString().ToUpper() + s.Substring(1).ToLower();
    }
    public static void logout()
    {
        FormsAuthentication.SignOut();
        HttpContext.Current.Session.Remove("emp");
        HttpContext.Current.Session.Remove("empRole");
        HttpContext.Current.Session.Remove("empID");
        HttpContext.Current.Session.Remove("itemlist");
        HttpContext.Current.Response.Redirect(LoginController.LoginURI);
    }

    public static void DisplayAlertMessage(string message)
    {
        Page page = HttpContext.Current.CurrentHandler as Page;
        string script = string.Format("alert('{0}');", message);
        if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
        {
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), "alert", script, true);
        }
    }

    public static void AlertMessageThenRedirect(string message, string redirectAddress)
    {
        var page = HttpContext.Current.CurrentHandler as Page;
        ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "alert('" + message + "');window.location ='" + redirectAddress + "';", true);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.IO;

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
            Console.ReadLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.ReadLine();
        }
    }
    public static bool checkIsTempDepHead(Employee e)
    {
        DateTime today = DateTime.Now;
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
 }


using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DelegateEvent_Leo_WeiChung.Class
{
    class CMail
    {
        string serviceAccount = ConfigurationManager.AppSettings["serviceAccount"]; // 抓取服務信箱
        string servicePassword = ConfigurationManager.AppSettings["servicePassword"]; // 抓取服務密碼
        public void SendMail(string from, List<string> toList, string title,string body)
        {
            System.Net.Mail.SmtpClient cMail = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
            cMail.Credentials = new System.Net.NetworkCredential(serviceAccount, servicePassword);
            cMail.EnableSsl = true;
            
            foreach (var to in toList)
                cMail.Send(from, to, title, body);
            cMail.Dispose();
            
        }
        public void SendMail2(string from, List<string> toList, string title, string body)
        {
            MailMessage mm = new MailMessage();
            mm.From = new MailAddress(serviceAccount, from);
            foreach (var to in toList)
                mm.To.Add(to);
            mm.Subject = title;
            mm.Body = $"<h1>{body}<h1>";
            mm.IsBodyHtml = true;

            SmtpClient sc = new SmtpClient("smtp.gmail.com", 587);
            sc.Credentials = new System.Net.NetworkCredential(serviceAccount, servicePassword);
            sc.EnableSsl = true;

            sc.Send(mm);
            sc.Dispose();
            mm.Dispose();
        }
    }
}

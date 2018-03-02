using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace Moy.BookShop.UI.utility
{

    public static class MailHelper
    {
        private readonly static string SmtpServer = "smtp.wedn.net";
        private readonly static int SmtpServerPort = 25;
        private readonly static bool SmtpEnableSsl = false;
        private readonly static string SmtpUsername = "t2@wedn.net";
        private readonly static string SmtpDisplayName = "jason";
        private readonly static string SmtpPassword = "2014@itcast";

        public static bool Send(string to, string subject, string mailBody, params string[] copyTos)
        {
            return Send(new[] { to }, subject, mailBody, copyTos, new string[] { }, MailPriority.Normal);
        }

        public static bool Send(string[] tos, string subject, string mailBody, string[] ccs, string[] bccs, MailPriority priority, params Attachment[] attachments)
        {
            if (attachments == null) throw new ArgumentNullException("attachments");
            if (tos.Length == 0) return false;

            var message = new System.Net.Mail.MailMessage();
            message.From = new MailAddress(SmtpUsername, SmtpDisplayName);
            message.Subject = subject;
            message.Body = mailBody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            message.Priority = priority;

            foreach (var attachment in attachments)
            {
                message.Attachments.Add(attachment);
            }

            foreach (var to in tos.Where(c => !string.IsNullOrEmpty(c)))
            {
                message.To.Add(new MailAddress(to));
            }
            foreach (var cc in ccs.Where(c => !string.IsNullOrEmpty(c)))
            {
                message.CC.Add(new MailAddress(cc));
            }
            foreach (var bcc in bccs.Where(c => !string.IsNullOrEmpty(c)))
            {
                message.CC.Add(new MailAddress(bcc));
            }

            var client = new SmtpClient
            {
                Host = SmtpServer,
                Credentials = new System.Net.NetworkCredential(SmtpUsername, SmtpPassword),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = SmtpEnableSsl,
                Port = SmtpServerPort
            };

            client.Send(message);

            return true;
        }
    }
}
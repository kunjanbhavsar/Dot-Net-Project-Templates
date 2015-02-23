using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace SimformWebApplication.Core
{
    public class EmailHelper
    {
        public void SendEmailAsync(MailParam objMailParam)
        {
            System.Threading.ParameterizedThreadStart deligateThreadStart = new System.Threading.ParameterizedThreadStart(SendEmail);
            System.Threading.Thread threadMail = new System.Threading.Thread(deligateThreadStart);
            threadMail.Start(objMailParam);
        }

        private void SendEmail(object objMailParam)
        {
            try
            {
                SmtpClient smpclient = new SmtpClient();

                smpclient.Host = "Host";
                smpclient.Port = 25;//"Port";
                MailAddress address = new MailAddress("From Address", "Display Name");
                MailMessage message = new MailMessage();
                message.IsBodyHtml = true;
                smpclient.DeliveryMethod = SmtpDeliveryMethod.Network;
                message.From = address;
                message.Subject = ((MailParam)objMailParam).subject;
                message.Body = ((MailParam)objMailParam).body;

                if (!string.IsNullOrEmpty(((MailParam)objMailParam).toAddress))
                    message.To.Add((((MailParam)objMailParam).toAddress));

                var ccEmails = ((MailParam)objMailParam).ccAddress;
                if (ccEmails != null && ccEmails.Count > 0)
                    ccEmails.ForEach(e => message.Bcc.Add(e));

                var bccEmails = ((MailParam)objMailParam).bccAddress;
                if (bccEmails != null && bccEmails.Count > 0)
                    bccEmails.ForEach(e => message.Bcc.Add(e));

                var attFiles = ((MailParam)objMailParam).attFilesPath;
                if (attFiles != null && attFiles.Count > 0)
                {
                    attFiles.ForEach(p =>
                    {
                        System.Net.Mail.Attachment Matt = new System.Net.Mail.Attachment(p);
                        message.Attachments.Add(Matt);
                    });
                }

                smpclient.EnableSsl = true;
                smpclient.UseDefaultCredentials = false;
                smpclient.Credentials = new System.Net.NetworkCredential("userName", "password");
                // ServicePointManager.ServerCertificateValidationCallback = (obj, certificate, chain, errors) => true;
                smpclient.Send(message);
            }
            catch (Exception ex)
            {
                //throw ex
            }
        }

        public class MailParam
        {
            public string toAddress { get; set; }
            public List<string> ccAddress { get; set; }
            public List<string> bccAddress { get; set; }
            public string subject { get; set; }
            public string from { get; set; }
            public string body { get; set; }
            public List<string> attFilesPath { get; set; }
        }
    }
}
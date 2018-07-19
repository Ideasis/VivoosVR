using Core.Log;
using Core.Mail.Configuration;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace Core.Mail
{
    public static class Mailer
    {
        public static bool IsValidEmail(this string email)
        {
            return Regex.IsMatch(email, @"([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)");
        }

        public static void SendMail(
                    BaseMailUser from,
                    string recipients,
                    string subject,
                    string body
                    )
        {
            MailMessage mailMessage = new MailMessage();
            System.Net.Mail.SmtpClient client = null;

            try
            {
                //Debug.Assert(from != null, "from parameter is null");
                if (from == null)
                {
                    throw new ArgumentException("Invalid from argument. It cannot be null");
                }

                mailMessage.From = new MailAddress(from.Email);
                mailMessage.To.Add(recipients);
                mailMessage.Subject = subject;
                mailMessage.Body = body;

                //mailMessage.BodyEncoding = Encoding.Unicode;
                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.High;
                //mailMessage.SubjectEncoding = Encoding.Unicode;

                client = new SmtpClient(from.Server, from.Port);
                client.Credentials = from.UserCredential;

                client.Send(mailMessage);

            }
            catch
            {
                throw;
            }
            finally
            {
                if (client != null)
                {
                    client.Dispose();
                }
                mailMessage.Dispose();
            }
        }
    }
}

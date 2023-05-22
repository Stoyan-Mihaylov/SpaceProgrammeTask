using System;
using System.Net;
using System.Net.Mail;

namespace SpaceProgramme.Helpers
{
    public class EmailSenderHelper
    {
        ///<summary>
        /// Тhis method gets user data(FileName, email sender, password, emailReciever, subject, body, attackment)
        /// and sends an email to the specified email address.
        ///</summary>
        public static bool TrySendEmail(string fromEmail, string fromPassword, string toEmail, string subject, string body, string attachmentPath)
        {

            try
            {
                using MailMessage mail = new MailMessage();

                mail.From = new MailAddress(fromEmail);
                mail.To.Add(toEmail);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                mail.Attachments.Add(new Attachment(attachmentPath));

                using SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);

                smtp.Credentials = new NetworkCredential(fromEmail, fromPassword);
                smtp.EnableSsl = true;

                smtp.Send(mail);
            }
            catch
            {
                return false;
            }
            return true;
        }

    }
}

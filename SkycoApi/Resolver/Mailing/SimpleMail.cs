using Resolver.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Resolver.Mailing
{
    public class SimpleMail : IMail
    {
        public void SendMail(IStateMail modelData)
        {
            String account = MailConfiguration.GetInstance().AppSettings["mail_account"];
            String password = Base64Encryption.GetInstance().Decrypt(MailConfiguration.GetInstance().AppSettings["mail_password"]);

            MailMessage mail = new MailMessage();

            modelData.To().ToList().ForEach(mailAdress => mail.To.Add(mailAdress));

            mail.Body = modelData.Body();
            mail.BodyEncoding = System.Text.Encoding.UTF8;

            mail.Subject = modelData.Subject();
            mail.SubjectEncoding = System.Text.Encoding.UTF8;

            mail.From = new MailAddress(account);
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = MailConfiguration.GetInstance().AppSettings["mail_host"];
            smtp.Port = Convert.ToInt32(MailConfiguration.GetInstance().AppSettings["mail_port"]);
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential(account, password);// Enter seders User name and password

            smtp.Send(mail);
        }
    }
}

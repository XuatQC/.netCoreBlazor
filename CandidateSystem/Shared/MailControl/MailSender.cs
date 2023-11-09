using System.Net.Mail;
using MimeKit;

namespace CandidateSystem.Shared
{
    public class MailSender : IMailSender
    {
        private MailConfig _mailConfig;
        public MailSender(MailConfig mailConfig)
        {
            _mailConfig = mailConfig;
        }
        public MimeMessage CreateMailMessage(Email email)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_mailConfig.From,_mailConfig.UserName));
            emailMessage.To.Add(new MailboxAddress("Candidate",email.EmailAddress));
            emailMessage.Subject = email.EmailSubject;
            emailMessage.Body = new  TextPart(MimeKit.Text.TextFormat.Text) { Text = email.EmailContent };
            return emailMessage;
        }
        public bool SendMail(MimeMessage message)
        {
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    client.Connect(_mailConfig.SmtpServer, _mailConfig.Post, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_mailConfig.UserName , _mailConfig.AppPassword);
                    client.Send(message);
                    return true;
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

        public MimeMessage CreateMailMessageWithAttrachment(List<Attachment> attachments,Email email)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_mailConfig.From, _mailConfig.UserName));
            emailMessage.To.Add(new MailboxAddress("Candidate", email.EmailAddress));
            emailMessage.Subject = email.EmailSubject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = email.EmailContent };
            var builder = new BodyBuilder();
            builder.TextBody = email.EmailContent;
            foreach (var i in attachments)
            {
                builder.Attachments.Add(i.Name,i.ContentStream);
            }
            emailMessage.Body = builder.ToMessageBody();
            return emailMessage;
        }
    }
}

using MimeKit;
using System.Net.Mail;

namespace CandidateSystem.Shared
{
    public interface IMailSender
    {
        /// <summary>
        /// create Mail by Entity.Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public MimeMessage CreateMailMessage(Email email);

        /// <summary>
        /// Send Mail 
        /// </summary>
        /// <param name="message"></param>
        public bool SendMail(MimeMessage message);

        /// <summary>
        /// create mail with attrachment below
        /// </summary>
        /// <param name="attachments"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public MimeMessage CreateMailMessageWithAttrachment(List<Attachment> attachments, Email email);


    }
}

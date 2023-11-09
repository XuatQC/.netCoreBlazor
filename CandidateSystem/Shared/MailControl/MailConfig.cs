namespace CandidateSystem.Shared
{
    public class MailConfig
    {
        public string From { get; set; }
        public string SmtpServer { get; set; } = "smtp.gmail.com";
        public int Post { get; set; }
        public string UserName { get; set; }
        public string AppPassword { get; set; }

    }
}

namespace CandidateSystem.Shared
{
    public static class EmailTemplate
    {
        public static string TakeATestSubject = "Saishunkan System VietNam_Take A Test Invitation \n";
        public static string InterviewSubject = "Saishunkan System VietNam_Interview Invitation \n";
        public static string InterviewSubject2 = "Saishunkan System VietNam_Interview Invitation 2 \n";
        public static string DenyMailSubject = "Your application to Saishunkan System VietNam\n";
        public static string OfferSubject = "Job Offer- {0} {1}";

        public static string Greeting = "Dear {0} \n";

        public static string DenyLetterBody = "Thanks for giving me the opportunity to learn more about your background and what you're looking for next. We've narrowed our search and have decided not to move forward with your application right now.\n";

        /// <summary>
        /// job title - 
        /// </summary>
        public static string OfferLetterBody = "We are delighted to offer you the position of {0} at Saishunkan System VietNam. After careful consideration of your application and interview, we are confident that you possess the skills,your starting date will be describe in the file below \n To accept this offer, please sign and return a copy ,If you have any questions or concerns, please contact us.";

        public static string Introduction = "We are a recuitment Deparment of Saishunkai System VietNam. \n" +
            "Thanks for Take an attention in {0} {1} position of our company \n";

        public static string EchoPlanTest = "We have a plan to test {0} in the time and location: \n";
        public static string EchoPlanInterview = "We have a plan to interview {0} in the time and location:\n";
        public static string RequirdSendConfirmMail = "Please reply this email to confirm.\n";

        public static string ThankYou = "Thanks!";
        
        public static string interviewerInvitation = "We would like to invite you to joining our Interview as interviewer\n";

    }
}

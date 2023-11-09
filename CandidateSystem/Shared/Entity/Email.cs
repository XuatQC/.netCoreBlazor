using System.ComponentModel.DataAnnotations;

namespace CandidateSystem
{
    public class Email
    {
        /// <summary>
        /// Email ID
        /// </summary>
        public int EmailID { get; set; }

        /// <summary>
        /// Email's Subject
        /// </summary>
        public string EmailSubject { get; set; } = "";

        /// <summary>
        /// Content Body
        /// </summary>
        [Required]
        public string EmailContent { get; set; } = "";

        /// <summary>
        /// delFlag
        /// </summary>
        public int DelFlag { get; set; } = 1;

        /// <summary>
        /// CandidateID
        /// </summary>
        public int CandidateID { get; set; }

        /// <summary>
        /// Email File's Locate Directory
        /// </summary>
        [Required]
        public string EmailFile { get; set; }

        /// <summary>
        /// InvitedPlace to interview
        /// </summary>
        public string InvitedPlace { get; set; } = "";

        /// <summary>
        /// InvitedDate to interview
        /// </summary>
        public DateTime InvitedDate { get; set; }

        /// <summary>
        /// Email Address
        /// </summary>
        public string EmailAddress { get; set; } = "";
        
        /// <summary>
        /// Receive Person
        /// </summary>
        public string ReceivePerson { get; set; } = "";

        /// <summary>
        /// CandidateStatusID
        /// </summary>
        public int? CandidateStatusID { get; set; }

    }   
}

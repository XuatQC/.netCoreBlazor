using System.ComponentModel.DataAnnotations;

namespace CandidateSystem.Shared
{
    public class Interviewer{
        /// <summary>
        /// ID
        /// </summary>
        public int InterviewerID { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string InterviewerName{ get; set;}

        /// <summary>
        /// Department 
        /// </summary>
        public string InterviewerDepartment{ get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [Required]
        public string InterviewerEmail { get; set; }

        /// <summary>
        /// Password to login 
        /// </summary>
        [Required]
        public string InterviewerPassword { get; set; }

        /// <summary>
        /// Login to authorilize
        /// </summary>

        public string Token { get; set; } = "";
  }
}

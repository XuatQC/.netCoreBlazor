namespace CandidateSystem.Shared
{
    public class Assess
    {
        /// <summary>
        /// ID 
        /// </summary>
        public int AssessID { get; set; }

        /// <summary>
        /// is Candidate pass
        /// </summary>
        public int isPass { get; set; }

        /// <summary>
        /// Why do we deny you ? 
        /// </summary>
        public string DenyReason { get; set; }

        /// <summary>
        /// Note, comment, judge
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// interviewerID who writes this assessment
        /// </summary>
        public int? InterviewerID { get; set; }

        /// <summary>
        /// InterviewMeetingDetailID (foreigh key)
        /// </summary>
        public int InterviewMeetingDetailID { get; set;}

        /// <summary>
        /// interviewmeeting's assessment
        /// </summary>
        public int? InterviewMeetingID { get; set; }
    }
}

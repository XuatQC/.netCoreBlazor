namespace CandidateSystem.Shared
{
    public class InterviewMeetingInfoExtends
    {
        public InterviewMeeting InterviewMeeting { get; set; }
        public List<Interviewer> Interviewers { get; set; } = new List<Interviewer>();
    }
}

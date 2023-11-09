using CandidateSystem.Shared;


namespace CandidateSystem.DataLayer.InterviewMeetingRepository
{
    public interface IInterviewMeetingRes
    {
        public int Insert(InterviewMeeting interviewMeeting);
        public List<InterviewMeeting> Show();
        List<InterviewMeeting> ShowByDate(DateOnly selectedDate);
        public bool Update(InterviewMeeting value, int InterviewMeetingID);
        public InterviewMeetingInfoExtends GetById(int InterviewID);
        public bool UpdateIsHaveAssess(int isHaveAssess, int interviewMeetingID);

    }
}

using CandidateSystem.Shared;


namespace CandidateSystem.BussinessLogic
{
    public interface IInterviewMeetingService
    {
        public Task<bool> Insert(InterviewMeeting interviewMeeting, List<int> listOfInterviewer);
        public Task<List<InterviewMeeting>> Show();
        public Task<List<InterviewMeeting>> ShowByDate(DateOnly selectedDate);
        public Task<bool> Update(InterviewMeetingInfoExtends value, int InterviewMeetingID);
        public Task<InterviewMeetingInfoExtends> GetById(int InterviewID);
        public Task<bool> UpdateIsHaveAssess(int isHaveAssess, int interviewMeetingID, bool isUpdate);

    }
}

using CandidateSystem.Shared;


namespace CandidateSystem.BussinessLogic
{
    public interface IInterviewMeetingDetailSer
    {
        public Task<bool> MassInsert(List<InterviewMeetingDetail> listOfInterviewMeetingDetail);
        public Task<bool> MassDeleteByInterviewMeetingID(int InterviewMeetingID);
        public Task<int> GetNewestInterviewMeetingIDByCandidateID(int candidateID);
        public Task<int> GetInterviewMeetingDetailIDByIDs(int interviewerID, int interviewMeetingID);


    }
}

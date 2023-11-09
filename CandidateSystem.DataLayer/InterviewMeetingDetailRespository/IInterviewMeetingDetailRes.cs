using CandidateSystem.Shared;

namespace CandidateSystem.DataLayer
{
    public interface IInterviewMeetingDetailRes
    {
        public bool MassInsert(List<InterviewMeetingDetail> interviewMeetingDetail);
        public bool MassDeleteByInterviewMeetingID(int InterviewMeetingID);
        public int GetNewestInterviewMeetingIDByCandidateID(int candidateID);
        public int GetInterviewMeetingDetailIDByIDs(int interviewerID, int interviewMeetingID);




    }
}

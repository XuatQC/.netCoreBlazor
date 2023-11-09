using CandidateSystem.Shared;


namespace CandidateSystem.DataLayer
{
    public interface IAssessRes
    {
        public bool Insert(Assess value);
        public Assess? GetByInterviewMeetingIDAndInterviewerID(int interviewerID, int interviewMeetingID);
        public bool Update(Assess value, int AssessID);
        public bool IsAllInterviewerWriteTheirAssessment(int interviewMeetingID);
        public List<int> getAllIsPassResult(int interviewMeetingID);
        public int GetByInterviewMeetingDetailID(int interviewMeetingDetailID);
    }
}

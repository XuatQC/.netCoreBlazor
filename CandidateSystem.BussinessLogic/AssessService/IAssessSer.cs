using CandidateSystem.Shared;

namespace CandidateSystem.BussinessLogic.AssessService
{
    public interface IAssessSer
    {
        public Task<bool> Insert(Assess value);
        public Task<Assess?> GetByInterviewMeetingIDAndInterviewerID(int interviewerID, int interviewMeetingID);
        public Task<bool> Update(Assess value, int AssessID);
        public Task<bool> IsAllInterviewerWriteTheirAssessment(int interviewMeetingID);


    }
}

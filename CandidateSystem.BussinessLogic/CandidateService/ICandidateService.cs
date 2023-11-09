using CandidateSystem.Shared;


namespace CandidateSystem.BussinessLogic.CandidateService 
{ 
    public interface ICandidateService
    {
        public Task<bool> ChangeDelFlag(int candidateID, int delFlag);
        public Task<bool> ChangeStatus(int candidateID, int status);
        public Task<List<Candidate>> GetAll();
        public Task<List<Candidate>> GetByCandidateStatuses(List<int> statusIDs);
        public Task<string> GetDenyReson(string email, string phone);
        public Task<Candidate> GetByID(int candidateid);
        public Task<List<Candidate>> GetByNotInStatus(List<int> status);
        public Task<List<Candidate>> GetCandidatesByIDs(List<int> candidateIDs);
        public Task<bool> Update(Candidate candidate, int candidateid);
        public Task<List<Candidate>> GetByCondition(int status, int delFlag);
        public Task<bool> Insert(Candidate value);
        public Task<bool> MassChangeDelFlag(List<int> candidateIDs, int delFlag);
        public Task<bool> MassChangeStatus(List<int> listOfCandidateID, int statusid);
        public Task<bool> massChangeHasEmailFlag(List<int> listOfCandidateID, int hasEmailFlag);
        public Task<bool> MassIncreaseByOneCandidateStatus(List<int> candidateIDs);
        public Task<List<Tuple<Candidate, InterviewMeeting>>> GetCandidatesByNewestInterviewMeeting(string afterDay, int interviewerid);

    }
}

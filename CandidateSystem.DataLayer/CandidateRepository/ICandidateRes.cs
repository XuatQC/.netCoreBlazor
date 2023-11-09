using CandidateSystem.Shared;

namespace CandidateSystem.DataLayer
{
    public interface ICandidateRes
    {
        public List<Candidate> GetAll();

        public Candidate GetByID(int candidateid);
        public string GetDenyReson(string email, string phone);

        public bool Insert(Candidate value);
        public bool Update(Candidate candidate, int candidateid);
        public List<Candidate> GetByCondition(int status, int delFlag);
        public List<Candidate> GetCandidatesByIDs(List<int> candidateIDs);
        public List<Candidate> GetByCandidateStatuses(List<int> statusIDs, bool isStatusInList);
        public bool ChangeDelFlagAndStatus(int candidateID, int delFlag, int status);
        public bool MassChange(List<int> listOfCandidateID, int delFlag, int status, int hasEmailFlag);
        public bool MassIncreaseByOneCandidateStatus(List<int> candidateIDs);
        public List<Tuple<Candidate, InterviewMeeting>> GetCandidatesByNewestInterviewMeeting(string afterDay,int interviewerid);
    }

}

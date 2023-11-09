namespace CandidateSystem.DataLayer.EmailRepository
{
    public interface IEmailRes
    {
        public List<Email> GetAll();
        public bool MultiInsert(List<Email> emails);
        public int Insert(Email value, int CandidateStatusID);
        public Email GetByCandidateID(int CandidateID);
        public bool Update(Email value, int emailID);
        public Email GetByID(int emailID);
        public bool MassDeleteByCandidateID(List<int> candidateIDs);
        public Tuple<Email, Candidate> GetNearestEmailAndCandidateByCandidateId(int CandidateID);
    }
}

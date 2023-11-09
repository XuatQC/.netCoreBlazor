

namespace CandidateSystem.BussinessLogic
{
    public interface IEmailService
    {
        public Task<List<Email>> GetAll();
        public Task<bool> MassCreateMail(List<int> candidateIDs);
        public Task<Email> GetByCandidateID(int CandidateID);
        public Task<bool> Update(Email value, int emailID);
        public Task<Email> GetByID(int emailID);

        public Task<int> Insert(Email email);

        public Task<bool> MassDeleteByCandidateID(List<int> candidateIDs);
        public Task<Tuple<Email, Candidate>> GetNearestEmailAndCandidateByCandidateId(int CandidateID);
        public Task<List<Email>> CreateRejectEmails(List<int> candidateIDs);
    }
}

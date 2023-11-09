using CandidateSystem.Shared;

namespace CandidateSystem.DataLayer
{
    public interface IInterviewerRes
    {
        public Interviewer GetByID(int id);
        public List<Interviewer> Get();
        public Interviewer? GetByEmailAndPassword(string email, string password);
        public List<Interviewer> GetByInterviewMeetingID(int interviewMeetingID);
  }
}

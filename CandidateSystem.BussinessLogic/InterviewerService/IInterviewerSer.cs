using CandidateSystem.Shared;


namespace CandidateSystem.BussinessLogic
{
  public interface IInterviewerSer  
  {
    public Task<List<Interviewer>> Get();

    public Task<Interviewer?> GetByEmailAndPassword(string email, string password);

    public Task<List<Interviewer>> GetByInterviewMeetingID(int interviewMeetingID);

  }
}
using CandidateSystem.DataLayer;
using CandidateSystem.Shared;


namespace CandidateSystem.BussinessLogic
{
      public class InterviewerSer : IInterviewerSer  
      {
            #region Properties

            private readonly IInterviewerRes _interviewerRes;

            #endregion

            #region Constructor
            public InterviewerSer(IInterviewerRes interviewerRes)
            {
                _interviewerRes = interviewerRes;
            }

            #endregion

            #region methods
            
            /// <summary>
            /// Get All interviewers
            /// </summary>
            /// <returns></returns>
            public async Task<List<Interviewer>> Get()
            {
                return _interviewerRes.Get();
            }

        public async Task<Interviewer?> GetByEmailAndPassword(string email, string password)
        {
            return _interviewerRes.GetByEmailAndPassword(email, password);
        }

        /// <summary>
        /// Get interviewer By ID
        /// </summary>
        /// <param name="interviewMeetingID"></param>
        /// <returns></returns>
        public async Task<List<Interviewer>> GetByInterviewMeetingID(int interviewMeetingID)
            {
                return _interviewerRes.GetByInterviewMeetingID(interviewMeetingID);
            } 
            #endregion
    }
}
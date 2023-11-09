using CandidateSystem.DataLayer;
using CandidateSystem.Shared;


namespace CandidateSystem.BussinessLogic
{
    public class InterviewMeetingDetailSer : IInterviewMeetingDetailSer
    {
        #region Properties

        private readonly IInterviewMeetingDetailRes _interviewMeetingDetailRes;

        #endregion
        #region Constructor

        public InterviewMeetingDetailSer(IInterviewMeetingDetailRes interviewMeetingDetailRes)
        {
            _interviewMeetingDetailRes = interviewMeetingDetailRes;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Insert InterviewMeetingDetails
        /// </summary>
        /// <param name="listOfInterviewMeetingDetail"></param>
        /// <returns></returns>
        public async Task<bool> MassInsert(List<InterviewMeetingDetail> listOfInterviewMeetingDetail)
        {
            return _interviewMeetingDetailRes.MassInsert(listOfInterviewMeetingDetail);
        }

        /// <summary>
        /// Delete by InterviewMeetingID
        /// </summary>
        /// <param name="InterviewMeetingID"></param>
        /// <returns></returns>
        public async Task<bool> MassDeleteByInterviewMeetingID(int InterviewMeetingID)
        {
            return _interviewMeetingDetailRes.MassDeleteByInterviewMeetingID(InterviewMeetingID);
        }

        /// <summary>
        /// Get newest InterviewmeetingID by CandidateId 
        /// </summary>
        /// <param name="candidateID"></param>
        /// <returns></returns>
        public async Task<int> GetNewestInterviewMeetingIDByCandidateID(int candidateID)
        {
            return _interviewMeetingDetailRes.GetNewestInterviewMeetingIDByCandidateID(candidateID);
        }

        /// <summary>
        /// Get InterviewMeetingDetailID by interviewerID and InterviewMeetingID
        /// </summary>
        /// <param name="interviewerID"></param>
        /// <param name="interviewMeetingID"></param>
        /// <returns></returns>
        public async Task<int> GetInterviewMeetingDetailIDByIDs(int interviewerID, int interviewMeetingID)
        {
            return _interviewMeetingDetailRes.GetInterviewMeetingDetailIDByIDs(interviewerID, interviewMeetingID);
        }


        #endregion

    }
}

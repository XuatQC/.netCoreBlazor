
using CandidateSystem.DataLayer;
using CandidateSystem.Shared;

namespace CandidateSystem.BussinessLogic.CandidateService
{
    public class CandidateService :ICandidateService
    {
        #region Properties
        private ICandidateRes _candidateRes;

        #endregion

        #region Constructor
        public CandidateService(ICandidateRes candidateRes)
        {
            _candidateRes = candidateRes;
        } 
        #endregion

        #region Methods

        /// <summary>
        /// change del flag by CandidateId
        /// </summary>
        /// <param name="candidateID"></param>
        /// <param name="delFlag"></param>
        /// <returns></returns>
        public async Task<bool> ChangeDelFlag(int candidateID, int delFlag)
        {
            return _candidateRes.ChangeDelFlagAndStatus(candidateID, delFlag,-1);
        }

        /// <summary>
        /// chagne status by id
        /// </summary>
        /// <param name="candidateID"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<bool> ChangeStatus(int candidateID, int status)
        {
            return _candidateRes.ChangeDelFlagAndStatus(candidateID,-1, status);
        }

        /// <summary>
        /// get all candidate
        /// </summary>
        /// <returns></returns>
        public async Task<List<Candidate>> GetAll()
        {
           return _candidateRes.GetByCondition(-1,-1);
        }

        /// <summary>
        /// Get Candidate by ID
        /// </summary>
        /// <param name="candidateid"></param>
        /// <returns></returns>
        public async Task<Candidate> GetByID(int candidateid)
        {
            return _candidateRes.GetByID(candidateid);
        }

        /// <summary>
        /// Get Candidate by ID
        /// </summary>
        /// <param name="candidateid"></param>
        /// <returns></returns>
        public async Task<string> GetDenyReson(string email, string phone)
        {
            return _candidateRes.GetDenyReson(email,phone);
        }

        /// <summary>
        /// Get candidate that is not be it statuses
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<List<Candidate>> GetByNotInStatus(List<int> status)
        {
            return _candidateRes.GetByCandidateStatuses(status,false);
        }

        /// <summary>
        /// Get Candidates by ids
        /// </summary>
        /// <param name="candidateIDs"></param>
        /// <returns></returns>
        public async Task<List<Candidate>> GetCandidatesByIDs(List<int> candidateIDs)
        {
            return _candidateRes.GetCandidatesByIDs(candidateIDs);
        }

        /// <summary>
        /// GUpdate candidate
        /// </summary>
        /// <param name="candidate"></param>
        /// <param name="candidateid"></param>
        /// <returns></returns>
        public async Task<bool> Update(Candidate candidate, int candidateid)
        {
           
            return _candidateRes.Update(candidate, candidateid);    
        }

        /// <summary>
        /// get by DelFlag
        /// </summary>
        /// <param name="status"></param>
        /// <param name="delFlag"></param>
        /// <returns></returns>
        public async Task<List<Candidate>> GetByCondition(int status, int delFlag)
        {
            return _candidateRes.GetByCondition(status, delFlag);    
        }

        /// <summary>
        /// Insert candidate
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<bool> Insert(Candidate value)
        {
            return _candidateRes.Insert(value);
        }

        /// <summary>
        /// change del flag for candidates
        /// </summary>
        /// <param name="candidteIDs"></param>
        /// <param name="delFlag"></param>
        /// <returns></returns>
        public async Task<bool> MassChangeDelFlag(List<int> candidteIDs,int delFlag)
        {
            return _candidateRes.MassChange(candidteIDs, delFlag,-1,-1);
        }

        /// <summary>
        /// chagne status for candidates;
        /// </summary>
        /// <param name="listOfCandidateID"></param>
        /// <param name="statusid"></param>
        /// <returns></returns>
        public async Task<bool> MassChangeStatus(List<int> listOfCandidateID, int statusid)
        {
            return _candidateRes.MassChange(listOfCandidateID, -1, statusid, -1);
        }

        /// <summary>
        /// change has Email flag for candidates
        /// </summary>
        /// <param name="listOfCandidateID"></param>
        /// <param name="hasEmailFlag"></param>
        /// <returns></returns>
        public async Task<bool> massChangeHasEmailFlag(List<int> listOfCandidateID, int hasEmailFlag)
        {
            return _candidateRes.MassChange(listOfCandidateID, -1, -1,hasEmailFlag);
        }

        /// <summary>
        /// increase by one to candidateStatuses
        /// </summary>
        /// <param name="candidateIDs"></param>
        /// <returns></returns>
        public async Task<bool> MassIncreaseByOneCandidateStatus(List<int> candidateIDs)
        {
            return _candidateRes.MassIncreaseByOneCandidateStatus(candidateIDs);
        }

        /// <summary>
        /// get candidates by statuses
        /// </summary>
        /// <param name="statusIDs"></param>
        /// <returns></returns>
        public async Task<List<Candidate>> GetByCandidateStatuses(List<int> statusIDs)
        {
            return _candidateRes.GetByCandidateStatuses(statusIDs,true);
        }

        /// <summary>
        /// get the the newest round that candidate took place
        /// </summary>
        /// <param name="afterDay">current day</param>
        /// <returns></returns>
        public async Task<List<Tuple<Candidate, InterviewMeeting>>> GetCandidatesByNewestInterviewMeeting(string afterDay,int interviewerid)
        {
            return _candidateRes.GetCandidatesByNewestInterviewMeeting(afterDay, interviewerid);
        }
        #endregion


    }
}

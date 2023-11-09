using CandidateSystem.BussinessLogic.CandidateService;
using CandidateSystem.DataLayer;
using CandidateSystem.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static System.Formats.Asn1.AsnWriter;

namespace CandidateSystem.BussinessLogic.AssessService
{
    public class AssessSer : IAssessSer
    {
        #region Properties

        private readonly IAssessRes _assessRes;
        private readonly IInterviewMeetingDetailSer _interviewMeetingDetailSer;
        private readonly IInterviewMeetingService _interviewMeetingSer;
        private readonly ICandidateService _canddiateSer;

        #endregion

        #region Constructor
        public AssessSer(IAssessRes assessRes, IInterviewMeetingDetailSer interviewMeetingDetailSer, IInterviewMeetingService inteviewmeetingSer, ICandidateService canddiateSer)
        {
            _assessRes = assessRes;
            _interviewMeetingDetailSer = interviewMeetingDetailSer;
            _interviewMeetingSer = inteviewmeetingSer;
            _canddiateSer = canddiateSer;

        } 
        #endregion

        #region Methods
        /// <summary>
        /// Insert an assess
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<bool> Insert(Assess value)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    // get interviewmeetingdetailid by interviewmeetingid and interviewerid
                    bool result = false;
                    value.InterviewMeetingDetailID = await _interviewMeetingDetailSer.GetInterviewMeetingDetailIDByIDs((int)value.InterviewerID, (int)value.InterviewMeetingID);
                    if (await GetByInterviewMeetingDetailID(value.InterviewMeetingDetailID) != -1)
                    {
                        result = await Update(value,value.AssessID);
                    }
                    else
                    {
                        result = _assessRes.Insert(value);
                        if (_assessRes.IsAllInterviewerWriteTheirAssessment((int)value.InterviewMeetingID))
                        {
                            List<int> resultList = _assessRes.getAllIsPassResult((int)value.InterviewMeetingID);
                            //var resultTest = IsPassOrFail(resultList);
                            _interviewMeetingSer.UpdateIsHaveAssess(IsPassOrFail(resultList), (int)value.InterviewMeetingID,false);
                        }
                    }
                    scope.Complete();
                    return result;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public async Task<int> GetByInterviewMeetingDetailID(int interviewMeeingDetailID)
        {
            return _assessRes.GetByInterviewMeetingDetailID(interviewMeeingDetailID);
        }

        /// <summary>
        /// Get Assess By interviewmeetingID and InterviewerID though InterviewmeetingDetail table
        /// </summary>
        /// <param name="interviewerID"></param>
        /// <param name="interviewMeetingID"></param>
        /// <returns></returns>
        public async Task<Assess?> GetByInterviewMeetingIDAndInterviewerID(int interviewerID, int interviewMeetingID)
        {
            return _assessRes.GetByInterviewMeetingIDAndInterviewerID(interviewerID, interviewMeetingID);
        }

        /// <summary>
        /// Update assess by AssessID
        /// </summary>
        /// <param name="value"></param>
        /// <param name="AssessID"></param>
        /// <returns></returns>
        public async Task<bool> Update(Assess value, int AssessID)
        {
            using (TransactionScope scope = new TransactionScope())
            {

                value.InterviewMeetingDetailID = await _interviewMeetingDetailSer.GetInterviewMeetingDetailIDByIDs((int)value.InterviewerID, (int)value.InterviewMeetingID);
                value.AssessID = 0;
                var result = _assessRes.Update(value,AssessID);
                if (_assessRes.IsAllInterviewerWriteTheirAssessment((int)value.InterviewMeetingID))
                {
                    List<int> resultList = _assessRes.getAllIsPassResult((int)value.InterviewMeetingID);
                    //var resultTest = IsPassOrFail(resultList);
                    //Console.WriteLine(IsPassOrFail(resultList));
                    _interviewMeetingSer.UpdateIsHaveAssess(IsPassOrFail(resultList), (int)value.InterviewMeetingID,true);
                }
                scope.Complete();
               return result;
            }
        }

        /// <summary>
        /// Check if All of interviewer has been interviewed that meeting already wrote their Assessment
        /// </summary>
        /// <param name="interviewMeetingID"></param>
        /// <returns></returns>
        public async Task<bool> IsAllInterviewerWriteTheirAssessment(int interviewMeetingID)
        {
            return _assessRes.IsAllInterviewerWriteTheirAssessment(interviewMeetingID);
        }

        /// <summary>
        /// Pass Or Fail Checker // 1: Pass 2: fail
        /// </summary>
        /// <param name="resultList"></param>
        /// <returns></returns>
        protected int IsPassOrFail(List<int> resultList)
        {
            if (resultList.Count == 1) return resultList[0] == 0 ? 2 : 1;
            for (int i = 0; i < resultList.Count - 1; i++)
            {
                if (resultList[i] != resultList[i + 1])
                    return ISHAVEASSESS.FAIL;
            }
            return resultList[0] == 0 ? ISHAVEASSESS.FAIL  : resultList[0];
        }
        #endregion
    }
}

using System.Transactions;
using CandidateSystem.BussinessLogic.CandidateService;
using CandidateSystem.DataLayer;
using CandidateSystem.DataLayer.InterviewMeetingRepository;
using CandidateSystem.Shared;

namespace CandidateSystem.BussinessLogic
{
    public class InterviewMeetingService : IInterviewMeetingService
    {

        #region Properties
        private readonly IInterviewMeetingRes _interviewMeetingRes;
        private readonly IInterviewMeetingDetailSer _interviewMeetingDetailRes;
        private readonly IInterviewerRes _interviewerRes;
        private readonly IMailSender _mailSender;
        private readonly ICandidateService _candidateSer;
        #endregion


        #region Constructor
        public InterviewMeetingService(IInterviewMeetingRes interviewMeetingRes, IInterviewMeetingDetailSer interviewMeetingDetailRes, IInterviewerRes interviewerRes, IMailSender mailSender, ICandidateService candidateSer)
        {
            _interviewMeetingRes = interviewMeetingRes;
            _interviewMeetingDetailRes = interviewMeetingDetailRes;
            _interviewerRes = interviewerRes;
            _mailSender = mailSender;
            _candidateSer  = candidateSer;
        }
        #endregion


        #region Methods

        /// <summary>
        /// Insert also insert into InterMeetinDetail corresponding
        /// </summary>
        /// <param name="interviewMeeting"></param>
        /// <param name="listOfInterviewer"></param>
        /// <returns></returns>
        public async Task<bool> Insert(InterviewMeeting interviewMeeting, List<int> listOfInterviewer)
        {
            int id = _interviewMeetingRes.Insert(interviewMeeting);
            //return false;
            List<InterviewMeetingDetail> listOfInterviewMeetingDetail = new List<InterviewMeetingDetail>();
            List<Interviewer> listOfInterviewerObject = _interviewerRes.Get();
            bool isSendingMailSuccesfully = true;
            foreach (var i in listOfInterviewer)
            {
                listOfInterviewMeetingDetail.Add(
                    new InterviewMeetingDetail()
                    {
                        InterviewMeetingDetailID = 0, //auto-increment
                        InterviewerID = i,
                        InterviewMeetingID = id
                    }
                );
                Interviewer currentInterviewer = null;
                foreach (var j in listOfInterviewerObject)
                {
                    if (j.InterviewerID == i)
                    {
                        currentInterviewer = j;
                        break;
                    }
                }
                //[TODO: send mail]
                Email email = new Email()
                {
                    EmailSubject = interviewMeeting.InterviewMeetingHeader,
                    EmailContent = EmailTemplate.interviewerInvitation + $"Date:{interviewMeeting.InterviewMeetingDate}\nLocation:{interviewMeeting.InterviewMeetingPlace}\n",
                    //CandidateID = interviewMeeting
                    EmailAddress = currentInterviewer.InterviewerEmail
                };
                isSendingMailSuccesfully = _mailSender.SendMail(_mailSender.CreateMailMessage(email));
                if (!isSendingMailSuccesfully)
                    break;
            }
            var isInsertSuccessfully = _interviewMeetingDetailRes.MassInsert(listOfInterviewMeetingDetail);
            var isCompleteSuccessfully = isSendingMailSuccesfully && await isInsertSuccessfully;
            return isCompleteSuccessfully;
        }

        /// <summary>
        /// Show All InterviewMeeting
        /// </summary>
        /// <returns></returns>
        public async Task<List<InterviewMeeting>> Show()
        {
            return _interviewMeetingRes.Show();
        }

        /// <summary>
        /// Show interview Meeting by Date
        /// </summary>
        /// <returns></returns>
        public async Task<List<InterviewMeeting>> ShowByDate(DateOnly selectedDate)
        {
            return _interviewMeetingRes.ShowByDate(selectedDate);
        }

        /// <summary>
        /// Update interview meeting (include inviewers)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="InterviewMeetingID"></param>
        /// <returns></returns>
        public async Task<bool> Update(InterviewMeetingInfoExtends value, int InterviewMeetingID)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                var listOfInterviewer = value.Interviewers;
                List<InterviewMeetingDetail> listInterviewDetail = new List<InterviewMeetingDetail>();
                foreach (var interviewItem in listOfInterviewer)
                {
                    listInterviewDetail.Add(
                    new InterviewMeetingDetail()
                        {
                            InterviewMeetingDetailID = 0, //auto-increment 
                            InterviewerID = interviewItem.InterviewerID,
                            InterviewMeetingID = InterviewMeetingID
                        }
                    );
                }
                //Update interviewmeeting Information
                _interviewMeetingRes.Update(value.InterviewMeeting,InterviewMeetingID);
                //Delete All interviewMeetingDetail with these InterviewmeetingID 
                _interviewMeetingDetailRes.MassDeleteByInterviewMeetingID(InterviewMeetingID);
                //Insert brand new interviewMeetingDetail with InterviewmeetingID and InterviewerIDs
                _interviewMeetingDetailRes.MassInsert(listInterviewDetail);
                scope.Complete();
                return await Task.FromResult(true);
            }
        }

        /// <summary>
        /// Get InterviewMeeting By its ID
        /// </summary>
        /// <param name="InterviewID"></param>
        /// <returns></returns>
        public async Task<InterviewMeetingInfoExtends> GetById(int InterviewID)
        {
            return _interviewMeetingRes.GetById(InterviewID);
        }

        /// <summary>
        /// update isHaveAssess field
        /// </summary>
        /// <param name="isHaveAssess"></param>
        /// <param name="interviewMeetingID"></param>
        /// <returns></returns>
        public async Task<bool> UpdateIsHaveAssess(int isHaveAssess, int interviewMeetingID,bool isUpdate)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                InterviewMeeting currentInterviewMeeting = _interviewMeetingRes.GetById(interviewMeetingID).InterviewMeeting;
                if (!isUpdate)
                {
                    _candidateSer.MassIncreaseByOneCandidateStatus(new List<int>(){ currentInterviewMeeting.CandidateID});
                }
                var result =  _interviewMeetingRes.UpdateIsHaveAssess(isHaveAssess, interviewMeetingID);
                scope.Complete();
                return result;
            }
        }

        #endregion
    }
}

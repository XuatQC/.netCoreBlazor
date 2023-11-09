using CandidateSystem.DataLayer;
using CandidateSystem.DataLayer.EmailRepository;
using CandidateSystem.Shared;
using System.Transactions;

namespace CandidateSystem.BussinessLogic
{
    public class EmailService : IEmailService
    {

        #region Properties
        private IEmailRes _emailRes;
        private ICandidateRes _candidateRes;
        #endregion


        #region Constructor
        public EmailService(IEmailRes emailRes, ICandidateRes candidateRes)
        {
            _candidateRes = candidateRes;
            _emailRes = emailRes;
        }
        #endregion


        #region Methods
        /// <summary>
        /// Get all email
        /// </summary>
        /// <returns></returns>
        public async Task<List<Email>> GetAll()
        {
            return _emailRes.GetAll();
        }

        /// <summary>
        /// Get by id
        /// </summary>
        /// <returns></returns>
        public async Task<Email> GetByID(int emailID)
        {
            return _emailRes.GetByID(emailID);
        }

        /// <summary>
        /// Get By candidateID
        /// </summary>
        /// <param name="candidateID"></param>
        /// <returns></returns>
        public async Task<Email> GetByCandidateID(int candidateID)
        {
            return _emailRes.GetByCandidateID(candidateID);
        }

        /// <summary>
        /// Get By candidateID
        /// </summary>
        /// <param name="candidateID"></param>
        /// <returns></returns>
        public async Task<int> Insert(Email email)
        {
            return _emailRes.Insert(email,email.EmailID);
        }

        /// <summary>
        /// Create Mails
        /// </summary>
        /// <param name="candidateIDs"></param>
        /// <returns></returns>
        public async Task<bool> MassCreateMail(List<int> candidateIDs)
        {
            try { 
                List<Candidate> candidateBeCreateMail = _candidateRes.GetCandidatesByIDs(candidateIDs);
                List<Email> email = new List<Email>();
                foreach (Candidate candidate in candidateBeCreateMail)
                {
                    //if(candidate.CandidateStatusID == (int)CandidateStatusCode.AcceptCV)
                    //{

                    //}
                    // EmailContent
                    string Subject = "";
                    string EchoPlan = "";
                    string Greeting = String.Format(EmailTemplate.Greeting, candidate.CandidateName);
                    string Introduction = String.Format(EmailTemplate.Introduction, candidate.JobTitle, candidate.JobPosition);
                    string When = $"-Date:{DateTime.Now}\n";
                    string Location = "Saishunkan System Vietnam \n 14th Floor, Thai Binh Tower, 106 Hoang Quoc Viet, Nghia Do, Cau Giay, Ha Noi";
                    string Where = $"-Location: {Location}\n";
                    if (candidate.CandidateStatusID == (int)CandidateStatusCode.AcceptCV)
                    {
                        Subject = EmailTemplate.TakeATestSubject;
                        EchoPlan = EmailTemplate.EchoPlanTest;
                    }
                    else if (candidate.CandidateStatusID == (int)CandidateStatusCode.TestOK)
                    {
                        Subject = EmailTemplate.InterviewSubject;
                        EchoPlan = EmailTemplate.EchoPlanInterview;
                    }
                    else if (candidate.CandidateStatusID == (int)CandidateStatusCode.PassedInterviewRoundOne)
                    {
                        Subject = EmailTemplate.InterviewSubject2;
                        EchoPlan = EmailTemplate.EchoPlanInterview;
                    }
                    EchoPlan = String.Format(EchoPlan, candidate.JobTitle, candidate.JobPosition);
                    string EmailContent = Greeting + Introduction + EchoPlan + When + Where + EmailTemplate.ThankYou;
                    email.Add(
                        new Email()
                        {
                            EmailSubject = Subject,
                            EmailContent = EmailContent,
                            EmailFile = "",
                            CandidateID = candidate.CandidateID,
                            InvitedDate = DateTime.Now,
                            InvitedPlace = Location,
                            CandidateStatusID = candidate.CandidateStatusID,
                            EmailAddress = candidate.CandidateEmail
                        }
                    );
                }
                using (TransactionScope scope = new TransactionScope())
                {
                    _candidateRes.MassChange(candidateIDs, -1, -1, 1);
                    var result = _emailRes.MultiInsert(email);
                    scope.Complete();
                    return result;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Mass Delete by candidateID
        /// </summary>
        /// <param name="candidateIDs"></param>
        /// <returns></returns>
        public async Task<bool> MassDeleteByCandidateID(List<int> candidateIDs)
        {
            return _emailRes.MassDeleteByCandidateID(candidateIDs);
        }

        /// <summary>
        /// Update Email 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="emailID"></param>
        /// <returns></returns>
        public async Task<bool> Update(Email value, int emailID)
        {
            _emailRes.Update(value, emailID);
            return true;
        }

        /// <summary>
        /// get nearest Email and Candidate By candidateid (depend on CandidateStatus)
        /// </summary>
        /// <param name="CandidateID"></param>
        /// <returns></returns>
        public async Task<Tuple<Email, Candidate>> GetNearestEmailAndCandidateByCandidateId(int CandidateID)
        {
            return _emailRes.GetNearestEmailAndCandidateByCandidateId(CandidateID);
        }

        public async Task<List<Email>> CreateRejectEmails(List<int> candidateIDs)
        {
            List<Email> listOfMail = new List<Email>();
            foreach(var i in candidateIDs) {
                var candidate = _candidateRes.GetByID(i);
                var greeting = string.Format(EmailTemplate.Greeting, candidate.CandidateName);
                var rejectEmail = new Email()
                {
                    EmailSubject = EmailTemplate.DenyMailSubject,
                    EmailContent = greeting + EmailTemplate.DenyLetterBody + EmailTemplate.ThankYou,
                    EmailAddress = candidate.CandidateEmail
                };
                listOfMail.Add(rejectEmail);
            }
            return listOfMail;
        }
        #endregion


    }
}

namespace CandidateSystem
{
    public enum CandidateStatusCode
    {
        /// <summary>
        /// Python
        /// </summary>
        Python = 1,

        /// <summary>
        /// NodeJS
        /// </summary>
        NodeJS = 2,

        /// <summary>
        /// C#
        /// </summary>
        CSharp = 6,

        /// <summary>
        /// Receive Cv
        /// </summary>
        Intern = 3,

        /// <summary>
        /// Receive Cv
        /// </summary>
        Fresher = 4,

        /// <summary>
        /// Receive Cv
        /// </summary>
        Staff = 5,

        /// <summary>
        /// Receive Cv
        /// </summary>
        ReceiveCV = 7,

        /// <summary>
        /// Deny Candidate CV
        /// </summary>
        DenyCV = 8,

        /// <summary>
        /// Accept Candidate CV
        /// </summary>
        AcceptCV = 9,

        /// <summary>
        /// Invited do the Test
        /// </summary>
        InvitedTest = 10,

        /// <summary>
        /// Pass The Test
        /// </summary>
        TestOK = 11,

        /// <summary>
        /// Invite to go to interview 1 
        /// Intern only need 1 round
        /// </summary>
        InvitedInterviewRoundOne = 12,

        /// <summary>
        /// Pass the interview 1
        /// Intern got the offer here : Status 16(SentOffer)
        /// </summary>
        PassedInterviewRoundOne = 13,

        /// <summary>
        /// Invite to go to interview 2
        /// </summary>
        InvitedInterviewRoundTwo = 14,

        /// <summary>
        /// Pass The interview 2
        /// </summary>
        PassedInterviewRoundTwo = 15,

        /// <summary>
        /// Sent offert to the candidate
        /// </summary>
        SentOffer = 16,

        /// <summary>
        /// candidate accept offer
        /// </summary>
        AcceptOffer = 17,

        /// <summary>
        /// candidate deny offer
        /// </summary>
        DenyOffer = 18,

        /// <summary>
        /// candidate sent form information
        /// </summary>
        SentForm  = 19,

        /// <summary>
        /// completed recuited processing
        /// </summary>
        Completed = 20,


        ///[Can Contact value]
        /// <summary>
        /// not going to contact to candidate
        /// </summary>
        NotContactYet = 21,

        ///[Can Contact value]
        /// <summary>
        /// can contact to candidate
        /// </summary>
        CanContact = 22,

        ///[Can Contact value
        /// <summary>
        /// can not contact to candidate
        /// </summary>
        CanNotContact = 23,
    }
}

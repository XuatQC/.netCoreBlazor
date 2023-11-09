using CandidateSystem.BussinessLogic;
using CandidateSystem.BussinessLogic.CandidateService;
using CandidateSystem.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CandidateSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class EmailController : ControllerBase
    {

        #region Properties
        private IEmailService _emailService;
        private IMailSender _mailSender;
        private ICandidateService _candidateService;

        #endregion

        #region Constructor
        public EmailController(IEmailService emailService, IMailSender mailSender, ICandidateService candidateService)
        {
            _emailService = emailService;
            _mailSender = mailSender;
            _candidateService = candidateService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Get all Email
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Email> GetAll()
        {
            return _emailService.GetAll().Result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="candidateID"></param>
        /// <returns></returns>
        [HttpPost("create-single-mail")]
        public IActionResult Insert([FromBody] Email email)
        {
            var addingItem = _emailService.Insert(email).Result;
            if (addingItem != -1)
            {
                return Ok(addingItem);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.NotModified);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="candidateID"></param>
        /// <returns></returns>
        [HttpPost("createmail")]
        public IActionResult MassCreateMail([FromBody] List<int> candidateID)
        {
            bool isComplete = _emailService.MassCreateMail(candidateID).Result;
            if (isComplete)
            {
                return Ok("Complete");
            }
            else
            {
                return StatusCode((int)HttpStatusCode.NotModified);
            }
        }

        /// <summary>
        /// Get Email By CandidateID
        /// </summary>
        /// <param name="candidateID"></param>
        /// <returns></returns>
        [HttpGet("{candidateID}")]
        public IActionResult GetEmailByCandidateID(int candidateID)
        {
            try
            {
                Email result = _emailService.GetByCandidateID(candidateID).Result;
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// update Email
        /// </summary>
        /// <param name="value"></param>
        /// <param name="emailID"></param>
        /// <returns></returns>
        [HttpPut("{emailID}")]
        public IActionResult Update(Email value, int emailID)
        {
            try
            {
                _emailService.Update(value, emailID);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Sending mail
        /// </summary>
        /// <param name="CandidateID"></param>
        /// <returns></returns>
        [HttpPost("sending-mails")]
        public IActionResult SendingInvitedMail(List<int> CandidateID)
        {
            try
            {
                foreach (var iter in CandidateID)
                {
                    Email email = _emailService.GetByCandidateID(iter).Result;
                    _mailSender.SendMail(_mailSender.CreateMailMessage(email));
                }
                //change has Email Flag to "No"
                _candidateService.massChangeHasEmailFlag(CandidateID, 0);

                //Delete Email --> do it later  
                //_emailService.MassDeleteByCandidateID(CandidateID);

                //chagne CandidateStatusID to ++;
                _candidateService.MassIncreaseByOneCandidateStatus(CandidateID);
                return Ok("Completed");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// get email that has just added 
        /// </summary>
        /// <param name="CandidateID"></param>
        /// <returns></returns>
        [HttpGet("get-nearest-email-candidate/{CandidateID}")]
        public IActionResult GetNearestEmailAndCandidateByCandidateId(int CandidateID)
        {
            try
            {
                return Ok(_emailService.GetNearestEmailAndCandidateByCandidateId(CandidateID).Result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("send-reject-email")]
        public IActionResult SendRejectMail([FromBody] List<int> candidateIDs)
        {
            try {
                List<Email> ListOfEmail = _emailService.CreateRejectEmails(candidateIDs).Result;
                foreach(var i in candidateIDs)
                {
                    _candidateService.ChangeDelFlag(i,0);
                }
                var isSuccess = true;
                foreach (var iter in ListOfEmail)
                {
                    isSuccess = _mailSender.SendMail(_mailSender.CreateMailMessage(iter));
                    if (!isSuccess) break;
                }
                _candidateService.MassChangeDelFlag(candidateIDs,DELFLAG.NO);    
                if (!isSuccess) return BadRequest();
                return Ok("Completed");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);   
            }
        }

        // [TODO] Send Accept Mail
        //[HttpPost("send-congratulation-email")]
        //public IActionResult SendAcceptMail([FromBody] Candidate candidate)
        //{
        //    try
        //    {

        //        var isSuccess = true;
        //        foreach (var iter in ListOfEmail)
        //        {
        //            isSuccess = _mailSender.SendMail(_mailSender.CreateMailMessage(iter));
        //            if (!isSuccess) break;
        //        }
        //        _candidateService.MassChangeDelFlag(candidateIDs, DELFLAG.NO);
        //        if (!isSuccess) return BadRequest();
        //        return Ok("Completed");
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //}


        #endregion
    }
}

using CandidateSystem.BussinessLogic;
using CandidateSystem.BussinessLogic.CandidateService;
using CandidateSystem.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CandidateSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class FileController : ControllerBase
    {

        private readonly IMailSender _mailSender;
        private readonly IEmailService _emailService;
        private readonly ICandidateService _candidateSer;
        public FileController(IMailSender mailSender,IEmailService emailService, ICandidateService candidateService)
        {
            _mailSender = mailSender;
            _emailService = emailService;
            _candidateSer = candidateService;
        }
        /// <summary>
        ///  API: post a file to server side
        /// </summary>
        /// <param name="files">the file is going to being fetch</param>
        /// <param name="filename">file name</param>
        /// <returns></returns>
        [HttpPost("{filename}")]
        public async Task<string> PostFileWithName([FromForm] IFormFile files, string filename)
        {
            var path = link.SEVER_RESOURCE_CONTAINS_CV + filename + ".pdf";
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            await using FileStream fs = System.IO.File.Create(path);
            files.CopyTo(fs);
            return "Done";
        }

        /// <summary>
        ///  API: post a file to server side
        /// </summary>
        /// <param name="files">the file is going to being fetch</param>
        /// <param name="filename">file name</param>
        /// <returns></returns>
        [HttpPost("multi/{EmailID}/{candidateID}")]
        public async Task<string> PostMultilFile([FromForm] IEnumerable<IFormFile> files, int EmailID,int candidateID)
        {
            Candidate currentCandidate = await _candidateSer.GetByID(candidateID);
            List<Attachment> listOfAttractToMail = new List<Attachment>();
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        Attachment att = new Attachment(new MemoryStream(fileBytes), file.FileName);
                        listOfAttractToMail.Add(att); 
                    }
                }
            }
            Email toEmail = await _emailService.GetByID(EmailID);
            bool complete = _mailSender.SendMail(_mailSender.CreateMailMessageWithAttrachment(listOfAttractToMail, toEmail));
            if(currentCandidate.JobPositionID == (int)CandidateStatusCode.Intern)
            {
                _candidateSer.ChangeStatus(candidateID,(int)CandidateStatusCode.SentOffer);
            }
            else
            {
                _candidateSer.MassIncreaseByOneCandidateStatus(new List<int>()
                {
                    candidateID
                });
            }

            return complete + "";
        }
    }
}

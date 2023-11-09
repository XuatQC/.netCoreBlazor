using CandidateSystem.Shared;
using Microsoft.AspNetCore.Mvc;
using CandidateSystem.BussinessLogic;
using Microsoft.AspNetCore.Authorization;

namespace CandidateSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class InterviewMeetingDetailController : ControllerBase
    {
        #region Properties

        private readonly IInterviewMeetingDetailSer _interviewMeetingDetailSer;

        #endregion
        #region Constructor

        public InterviewMeetingDetailController(IInterviewMeetingDetailSer interviewMeetingDetailSer)
        {
            _interviewMeetingDetailSer = interviewMeetingDetailSer;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Mass Insert 
        /// </summary>
        /// <param name="interviewMeetingDetails"></param>
        [HttpPost("mash-insert")]
        public void InsertMass([FromBody] List<InterviewMeetingDetail> interviewMeetingDetails)
        {
            _interviewMeetingDetailSer.MassInsert(interviewMeetingDetails);
        }

        /// <summary>
        /// GetNewestInterviewMeetingDetailIDByCandidateID
        /// get newest interviewmeetingDetailID by candidateid
        /// </summary>
        /// <param name="interviewMeetingDetails"></param>
        [HttpGet("interview-meeting-detail-by-candidate/{candizdateID}")]
        public IActionResult GetInterviewMeetingDetailIDByCandidateID([FromRoute] int candidateID)
        {
            try
            {
                return Ok(_interviewMeetingDetailSer.GetNewestInterviewMeetingIDByCandidateID(candidateID).Result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        } 
        #endregion


    }
}

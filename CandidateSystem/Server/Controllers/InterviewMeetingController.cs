using CandidateSystem.BussinessLogic;
using CandidateSystem.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CandidateSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class InterviewMeetingController : ControllerBase
    {
        #region Properties

        private readonly IInterviewMeetingService _interviewMeetingSerice;

        #endregion

        #region Constructor
        public InterviewMeetingController(IInterviewMeetingService interviewMeetingSerice)
        {
            _interviewMeetingSerice = interviewMeetingSerice;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Insert interviewMeeting (includes Interviewers)
        /// </summary>
        /// <param name="interviewMeetingInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Insert([FromBody] InterviewMeetingInfo interviewMeetingInfo)
        {
            bool isComplete = _interviewMeetingSerice.Insert(interviewMeetingInfo.InterviewMeeting, interviewMeetingInfo.InterviewerIDs).Result;
            if (isComplete)
            {
                return Ok();
            }
            else
            {
                return BadRequest();   
            }
        }

        /// <summary>
        /// Show All InterviewMeetings
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Show()
        {
            List<InterviewMeeting> list = _interviewMeetingSerice.Show().Result;
            if (list != null)
            {
                return Ok(list);
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Get InterviewMeeting By Date
        /// </summary>
        /// <param name="selectedDate"></param>
        /// <returns></returns>
        [HttpPost("get")]
        public IActionResult ShowByDate([FromBody] string selectedDate)
        {
            List<InterviewMeeting> list = _interviewMeetingSerice.ShowByDate(DateOnly.Parse(selectedDate)).Result;
            if (list != null)
            {
                return Ok(list);
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Get interviewMeeting By ID
        /// </summary>
        /// <param name="interviewMeetingID"></param>
        /// <returns></returns>
        [HttpGet("{interviewMeetingID}")]
        public IActionResult GetById(int interviewMeetingID)
        {

            InterviewMeetingInfoExtends value = _interviewMeetingSerice.GetById(interviewMeetingID).Result;
            return value == null ? BadRequest() : Ok(value);
        }

        /// <summary>
        /// Update By ID
        /// </summary>
        /// <param name="value"></param>
        /// <param name="interviewMeetingID"></param>
        /// <returns></returns>
        [HttpPut("{interviewMeetingID}")]
        public IActionResult Update([FromBody] InterviewMeetingInfoExtends value, int interviewMeetingID)
        {
            var result = _interviewMeetingSerice.Update(value, interviewMeetingID).Result;
            return result ? Ok() : BadRequest();
        }

        #endregion
    }

    
}

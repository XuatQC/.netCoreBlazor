using CandidateSystem.BussinessLogic.AssessService;
using CandidateSystem.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CandidateSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class AssessController : ControllerBase
    {
        #region Properties

        private readonly IAssessSer _assessSer;

        #endregion

        #region Constructor
        public AssessController(IAssessSer assessSer)
        {
            _assessSer = assessSer;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Insert an assess
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Insert([FromBody] Assess value)
        {
            try
            {
                Newtonsoft.Json.JsonConvert.SerializeObject(value);
                var result = _assessSer.Insert(value).Result;
                if (result)
                    return Ok("Completed");
                else
                    return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get Assess By interviewerID and InterviewMeetingID
        /// </summary>
        /// <param name="interviewerID"></param>
        /// <param name="interviewMeetingID"></param>
        /// <returns></returns>
        [HttpGet("get-by-ids/{interviewerID}/{interviewMeetingID}")]
        public IActionResult GetByIDs(int interviewerID, int interviewMeetingID)
        {
            try
            {
                var result = _assessSer.GetByInterviewMeetingIDAndInterviewerID(interviewerID, interviewMeetingID).Result;
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Update an assess by AssessID
        /// </summary>
        /// <param name="value"></param>
        /// <param name="assessID"></param>
        /// <returns></returns>
        [HttpPut("{assessID}")]
        public IActionResult Update([FromBody] Assess value, int assessID)
        {
            try
            {
                var result = _assessSer.Update(value, assessID).Result;
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// check if is all interviewer already wrote their assessment
        /// </summary>
        /// <param name="inteviewMeetingID"></param>
        /// <returns></returns>
        [HttpGet("is-pass/{inteviewMeetingID}")]
        public IActionResult IsAllInterviewerWriteTheirAssessment(int inteviewMeetingID)
        {
            try
            {
                var result = _assessSer.IsAllInterviewerWriteTheirAssessment(inteviewMeetingID).Result;
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        #endregion
    }
}

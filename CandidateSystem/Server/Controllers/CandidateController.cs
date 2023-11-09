using Microsoft.AspNetCore.Mvc;
using CandidateSystem.Shared;
using CandidateSystem.BussinessLogic.CandidateService;
using Microsoft.AspNetCore.Authorization;

namespace CandidateSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class CandidateController : ControllerBase
    {
        #region Properties

        private readonly IConfiguration _configuration;
        private ICandidateService _candidateService;

        #endregion

        #region Constructor
        public CandidateController(IConfiguration configuration, ICandidateService candidateService)
        {
            _configuration = configuration;
            _candidateService = candidateService;
        }
        #endregion

        #region Methods

        /// <summary>
        /// [Get(api/candidate)] 
        /// get all candidates
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {

                return Ok(_candidateService.GetAll().Result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString()); 
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// [Get(api/candidate)] 
        /// get all candidates by status is not in list of notStatus
        /// </summary>
        /// <returns></returns>
        [HttpPost("notinstatus")]
        public IActionResult GetByNotInStatus([FromBody] List<int> notStatus)
        {
            try
            {
                return Ok(_candidateService.GetByNotInStatus(notStatus).Result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// [Get(api/candidate)] 
        /// get all candidates by status is not in list of notStatus
        /// </summary>
        /// <returns></returns>
        [HttpPost("status-ids")]
        public IActionResult GetByStatusIDs([FromBody] List<int> statusIDs)
        {
            try
            {
                return Ok(_candidateService.GetByCandidateStatuses(statusIDs).Result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// [Get(api/candidate)] 
        /// get all candidates by IDs
        /// </summary>
        /// <returns></returns>
        [HttpPost("massgetbyids")]
        public IActionResult GetCandidatesByIDs([FromBody] List<int> ids)
        {
            try
            {
                return Ok(_candidateService.GetCandidatesByIDs(ids).Result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// [Get(api/candidate)] 
        /// get all candidates by IDs
        /// </summary>
        /// <returns></returns>
        [HttpPost("deny-reason/{email}")]
        public IActionResult GetDenyReason([FromBody]string phone, string email)
        {
            try
            {
                return Ok(_candidateService.GetDenyReson(email,phone).Result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// [Post(api/candidate/)] 
        /// insert a new component
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody] Candidate value)
        {
            try
            {
                return Ok(_candidateService.Insert(value).Result);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// [Get(api/candidate/store/{statuscode}/{del flag})] 
        /// get candidates by delFlag and statuscode
        /// </summary>
        /// <returns></returns>
        [HttpGet("store/{status}/{delFlag}")]
        public IActionResult GetByCondition(int status,int delFlag)
        {
            try
            {
               return Ok(_candidateService.GetByCondition(status, delFlag).Result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// get candidate by its id
        /// </summary>
        /// <param name="candidateIDs"></param>
        /// <param name="delFlag"></param>
        /// <returns></returns>
        [HttpGet("{candidateid}")]
        public IActionResult GetById(int candidateid)
        {
            try
            {
                return Ok(_candidateService.GetByID(candidateid).Result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// update by its candidate
        /// </summary>
        /// <param name="candidateIDs"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Update([FromBody] Candidate candidate)
        {
            try
            {
                return Ok(_candidateService.Update(candidate,candidate.CandidateID).Result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// [Put(api/candidate/status/{candidateid}/{status})] 
        /// change status of candidate
        /// </summary>
        /// <returns></returns>
        [HttpDelete("status/{candidateID}/{status}")]
        public IActionResult ChangeStatus(int candidateID, int status)
        {
            try
            {
                return Ok(_candidateService.ChangeStatus(candidateID, status).Result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// [Delete(api/candidate/changedelflag/{candidateid}/{delFlag})] 
        /// soft delete or [restore] candidate
        /// </summary>
        /// <returns></returns>
        [HttpDelete("changedelflag/{candidateID}/{delFlag}")]
        public IActionResult ChangeDelFlag(int candidateID,int delFlag)
        {
            try
            {
               _candidateService.ChangeDelFlag(candidateID, delFlag);
                return Ok("Updated");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// mass change flag with a list of candidateid
        /// </summary>
        /// <param name="candidateIDs"></param>
        /// <param name="delFlag"></param>
        /// <returns></returns>
        [HttpPost("masschangeflag/{delFlag}")]
        public IActionResult MassChangeDelFlag([FromBody]List<int> candidateIDs,int delFlag)
        {
            try
            {
                _candidateService.MassChangeDelFlag(candidateIDs, delFlag);
                return Ok("done");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// change candidateStatusid with a list of candidateid
        /// </summary>
        /// <param name="candidateIDs"></param>
        /// <param name="statusid"></param>
        /// <returns></returns>
        [HttpPost("masschangestatus/{statusid}")]
        public IActionResult MassChangeStatus([FromBody] List<int> candidateIDs, int statusid)
        {
            try
            {
                _candidateService.MassChangeStatus(candidateIDs, statusid);
                return Ok("done");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// change candidateStatusid with a list of candidateid
        /// </summary>
        /// <param name="candidateIDs"></param>
        /// <param name="statusid"></param>
        /// <returns></returns>
        [HttpPost("masschangehasemailflag")]
        public IActionResult MassChangeHasEmailHasEmailFlag([FromBody] List<int> candidateIDs, int statusid)
        {
            try
            {
                _candidateService.MassChangeStatus(candidateIDs, statusid);
                return Ok("done");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// [test]
        /// </summary>
        /// <param name="candidateIDs"></param>
        /// <param name="statusid"></param>
        /// <returns></returns>
        [HttpPost("mass-increase-candidatestatus-by-one")]
        public IActionResult MassIncreaseCandidateStatusByOne([FromBody] List<int> candidateIDs)
        {
            try
            {
                _candidateService.MassIncreaseByOneCandidateStatus(candidateIDs);
                return Ok("done");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// [test]
        /// </summary>
        /// <param name="candidateIDs"></param>
        /// <param name="statusid"></param>
        /// <returns></returns>
        [HttpPost("get-candidates-by-newest-interview/{interviewerid}")]
        public IActionResult GetCandidatesByNewestInterview([FromBody] string afterDay, int interviewerid)
        {
            try
            {
                List<Tuple<Candidate,InterviewMeeting>> list = _candidateService.GetCandidatesByNewestInterviewMeeting(afterDay,interviewerid).Result;
                return Ok(list);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        #endregion
    }
}

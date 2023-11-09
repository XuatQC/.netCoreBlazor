using CandidateSystem.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using CandidateSystem.DataLayer;
using Microsoft.IdentityModel.Tokens;
using CandidateSystem.BussinessLogic;
using System.Text;
using System.Runtime.InteropServices;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using CandidateSystem.DataLayer.JwtRepository;
using Microsoft.AspNetCore.Authorization;
using CandidateSystem.DataLayer.UserServiceRepository;
using CandidateSystem.Shared.Entity;

namespace CandidateSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InterviewerController : ControllerBase
    {
        #region Properties

        private readonly IInterviewerSer _interviewSer;
        private readonly IJwtRes _jwtRes;
        private readonly IUserAuthenticateRes _userAuthenticateRes;
        #endregion

        #region Constructor
        public InterviewerController(IInterviewerSer interviewerSer,IJwtRes jwtRes,IUserAuthenticateRes userAuthenticate)
        {
            _interviewSer = interviewerSer;
            _jwtRes = jwtRes;
            _userAuthenticateRes = userAuthenticate;
        }
        #endregion

        #region Methods

        //[Authorize]
        [HttpGet("get-all")]
        public IActionResult Get()
        {
            try
            {
                var response = _interviewSer.Get();
                return Ok(response.Result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status501NotImplemented);
            }

        }

        [HttpGet("get-interviewer-by-interview-meeting/{interviewMeetingID}")]
        public IActionResult GetByInterviewMeetingID(int interviewMeetingID)
        {
            try
            {
                var response = _interviewSer.GetByInterviewMeetingID(interviewMeetingID).Result;
                
                return Ok(response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status501NotImplemented);
            }
        }
        //[HttpPost]
        //public IActionResult GetByEmailAndPassword([FromBody] Interviewer value)
        //{
        //    try
        //    {
        //        var response = _interviewSer.GetByEmailAndPassword(value.InterviewerEmail,value.InterviewerPassword).Result;
        //        if (response != null)
        //        {
        //            response.Token = GenerateToken(value);
        //            //TODO: generate token
        //        }
        //        return Ok(response);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        return StatusCode(StatusCodes.Status501NotImplemented);
        //    }
        //}
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(Interviewer interviewer)
        {
            var validUser = await _userAuthenticateRes.IsValidUserAsync(interviewer);
            if (!validUser)
            {
                return Unauthorized("Incorrect Password or Username");
            }
            var token = _jwtRes.GenerateToken(interviewer.InterviewerEmail);
            // saving refresh token to the db
            Shared.Entity.UserRefreshToken obj = new Shared.Entity.UserRefreshToken
            {
                RefreshToken = token.RefreshTokens,
                UserName = interviewer.InterviewerEmail
            };
            _userAuthenticateRes.AddUserRefreshTokens(obj);
            var currentUser = await _interviewSer.GetByEmailAndPassword(interviewer.InterviewerEmail,interviewer.InterviewerPassword);
             return Ok(new {token, currentUser });

        }
        [AllowAnonymous]
        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh(Token token)
        {
            var principal = _jwtRes.GetPrincipalFromExpiredToken(token.Tokens);
            var username = principal.Identity?.Name;

            //retrieve the saved refresh token from database
            var savedRefreshToken = _userAuthenticateRes.GetSavedRefreshTokens(username, token.RefreshTokens);

            if (savedRefreshToken.RefreshToken != token.RefreshTokens)
            {
                return Unauthorized("Invalid attempt!");
            }

            var newJwtToken = _jwtRes.GenerateRefreshToken(username);

            if (newJwtToken == null)
            {
                return Unauthorized("Invalid attempt!");
            }

            // saving refresh token to the db
            UserRefreshToken obj = new UserRefreshToken
            {
                RefreshToken= newJwtToken.RefreshTokens,
                UserName = username
            };
                _userAuthenticateRes.DeleteUserRefreshTokens(username, token.RefreshTokens);
                _userAuthenticateRes.AddUserRefreshTokens(obj);
            return Ok(newJwtToken);
        }
        #endregion
    }
}

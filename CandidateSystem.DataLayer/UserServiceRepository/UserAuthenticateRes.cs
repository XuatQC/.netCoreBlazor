using CandidateSystem.DataLayer.UserRefreshTokenRes;
using CandidateSystem.Shared;
using CandidateSystem.Shared.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateSystem.DataLayer.UserServiceRepository
{
    public class UserAuthenticateRes : IUserAuthenticateRes
    {
        private readonly IUserRefreshToken _userRefresh;
        private readonly IInterviewerRes _interviewerRes;
        public UserAuthenticateRes(IUserRefreshToken userRes, IInterviewerRes interviewres)
        {
            _userRefresh = userRes;
            _interviewerRes = interviewres;
        }

        public Shared.Entity.UserRefreshToken AddUserRefreshTokens(Shared.Entity.UserRefreshToken user)
        {
            _userRefresh.Insert(user);
            return user;
        }

        public void DeleteUserRefreshTokens(string username, string refreshToken)
        {
            _userRefresh.Delete(username, refreshToken);
        }

        public Shared.Entity.UserRefreshToken GetSavedRefreshTokens(string username, string refreshtoken)
        {
            return _userRefresh.Get(username, refreshtoken);
        }

        public async Task<bool> IsValidUserAsync(Interviewer users)
        {
            var result = _interviewerRes.GetByEmailAndPassword(users.InterviewerEmail, users.InterviewerPassword);
            if(result == null)
            {
                return false;
            }
            return true;
        }


        public int SaveCommit()
        {
            throw new NotImplementedException();
        }
    }
}

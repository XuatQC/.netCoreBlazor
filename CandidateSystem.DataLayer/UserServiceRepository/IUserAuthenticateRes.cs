using CandidateSystem.Shared;
using CandidateSystem.Shared.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateSystem.DataLayer.UserServiceRepository
{
    public interface IUserAuthenticateRes
    {
        Task<bool> IsValidUserAsync(Interviewer users);
        Shared.Entity.UserRefreshToken AddUserRefreshTokens(Shared.Entity.UserRefreshToken user);

        Shared.Entity.UserRefreshToken GetSavedRefreshTokens(string username, string refreshtoken);

        void DeleteUserRefreshTokens(string username, string refreshToken);

        int SaveCommit();
    }
}

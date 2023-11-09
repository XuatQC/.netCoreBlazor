using CandidateSystem.Shared;
using CandidateSystem.Shared.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CandidateSystem.DataLayer.JwtRepository
{
    public interface IJwtRes
    {
        public Token GenerateToken(string str);

        public string GenerateRefreshToken();
        public Token GenerateRefreshToken(string str);

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

    }
}

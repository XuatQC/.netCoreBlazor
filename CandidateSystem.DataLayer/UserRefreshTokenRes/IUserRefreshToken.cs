using CandidateSystem.Shared.Entity;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateSystem.DataLayer.UserRefreshTokenRes
{
    public interface IUserRefreshToken
    {
        public bool Insert(Shared.Entity.UserRefreshToken user);
        public bool Delete(string userName, string refreshToken);
        public Shared.Entity.UserRefreshToken Get(string userName, string refreshToken);

    }
}

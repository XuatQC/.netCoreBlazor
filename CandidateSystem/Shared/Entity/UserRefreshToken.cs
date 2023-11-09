using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateSystem.Shared.Entity
{
    public class UserRefreshToken
    {
        public int id { get; set; }
        public string UserName { get; set; }
        public string RefreshToken { get; set; }

    }
}

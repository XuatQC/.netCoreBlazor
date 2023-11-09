using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CandidateSystem.Server
{
    public class TokenConfiguration
    {
        private readonly IConfiguration Configuration;

        public TokenConfiguration(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void TokenServiceConfiguration(IServiceCollection services)
        {
            
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace CandidateSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class CandidatePropertyController : ControllerBase
    {
        #region Properties

        private readonly IConfiguration _configuration;

        #endregion

        #region Constructor

        public CandidatePropertyController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #endregion

        #region Methods

        /// <summary>
        /// [get] list of jobtitle 
        /// </summary>
        /// <returns></returns>
        [HttpGet("{candidatetype}")]
        public IActionResult GetCandidateProperty(string candidateType)
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    List<CandidateProperty> list = new List<CandidateProperty>();
                    MySqlCommand cmd = new MySqlCommand($"select * from candidateproperty where propertyType = '{candidateType}';", conn);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new CandidateProperty()
                        {
                            PropertyID = Convert.ToInt32(reader["propertyID"]),
                            PropertyName = reader["propertyName"].ToString(),
                        }
                        );
                    }
                    return Ok(list);
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

       

        #endregion
    }
}

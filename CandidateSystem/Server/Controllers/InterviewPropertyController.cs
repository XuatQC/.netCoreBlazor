using CandidateSystem.DataLayer;
using CandidateSystem.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
namespace CandidateSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class InterviewPropertyController : ControllerBase
    {
        #region Properties

        private readonly IDatabaseConnector _dataconnector;

        #endregion
        #region Constructor

        public InterviewPropertyController(IDatabaseConnector databaseConnection)
        {
            _dataconnector = databaseConnection;
        }
        #endregion

        #region Methods
        /// <summary>
        /// [get] list of jobtitle 
        /// </summary>
        /// <returns></returns>
        [HttpGet("{propertyType}")]
        public IActionResult GetInterview(string propertyType)
        {
            MySqlConnection conn = _dataconnector.getConnection();
            try
            {
                _dataconnector.OpenConnection(conn);
                List<InterviewProperty> list = new List<InterviewProperty>();
                MySqlCommand cmd = new MySqlCommand($"select * from InterviewProperty where propertytype ='{propertyType}';", conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new InterviewProperty()
                    {
                        PropertyID = Convert.ToInt16(reader["PropertyID"].ToString()),
                        PropertyName = reader["PropertyName"].ToString(),
                        PropertyDetail = reader["PropertyDetail"].ToString(),
                        PropertyType = reader["PropertyType"].ToString()
                    }
                    );
                }
                return Ok(list);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            finally
            {
                _dataconnector.CloseConnection(conn);
            }
        } 
        #endregion
    }
}

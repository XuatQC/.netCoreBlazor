using CandidateSystem.Shared;
using MySqlConnector;

namespace CandidateSystem.DataLayer
{
    public class InterviewerRes : IInterviewerRes
    {
        #region Properties

        private readonly IDatabaseConnector _databaseConnector;

        #endregion

        #region Constructor
        public InterviewerRes(IDatabaseConnector databaseConnector)
        {
            _databaseConnector = databaseConnector;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Get Interviewer By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Interviewer GetByID(int id)
        {
            MySqlConnection conn = null;
            try
            {
                List<Interviewer> list = new List<Interviewer>();
                conn = _databaseConnector.getConnection();
                _databaseConnector.OpenConnection(conn);
                string command = $"select * from interviewer where interviewerid = @interviewerID;";
                MySqlCommand cmd = new MySqlCommand(command, conn);
                cmd.Parameters.AddWithValue("@interviewerID", id);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(
                      new Interviewer()
                      {
                          InterviewerID = reader["InterviewerID"] is DBNull ? -1 : Convert.ToInt32(reader["InterviewerID"].ToString()),
                          InterviewerName = reader["InterviewerName"] is DBNull ? "" : reader["InterviewerName"].ToString(),
                          InterviewerDepartment = reader["InterviewerDepartment"] is DBNull ? "" : reader["InterviewerDepartment"].ToString(),
                          InterviewerEmail = reader["InterviewerEmail"] is DBNull ? "" : reader["InterviewerEmail"].ToString(),
                          InterviewerPassword = reader["InterviewerPassword"] is DBNull ? "" : reader["InterviewerPassword"].ToString(),
                          Token = reader["Token"] is DBNull ? "" : reader["Token"].ToString()

                      }
                    );
                }
                return list[0];
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {
                _databaseConnector.CloseConnection(conn);
            }
        }

        /// <summary>
        /// Get Interviewer By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Interviewer? GetByEmailAndPassword(string email,string password)
        {
            MySqlConnection conn = null;
            try
            {
                List<Interviewer> list = new List<Interviewer>();
                conn = _databaseConnector.getConnection();
                _databaseConnector.OpenConnection(conn);
                string command = $"select * from interviewer where InterviewerEmail = @interviewerEmail and InterviewerPassword = @interviewPassword;";
                MySqlCommand cmd = new MySqlCommand(command, conn);
                cmd.Parameters.AddWithValue("@interviewerEmail", email);
                cmd.Parameters.AddWithValue("@interviewPassword", password);
                MySqlDataReader reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    list.Add(
                      new Interviewer()
                      {
                          InterviewerID = reader["interviewerID"] is DBNull ? -1 : Convert.ToInt32(reader["interviewerID"].ToString()),
                          InterviewerName = reader["InterviewerName"] is DBNull ? "" : reader["InterviewerName"].ToString(),
                          InterviewerDepartment = reader["InterviewerDepartment"] is DBNull ? "" : reader["InterviewerDepartment"].ToString(),
                          InterviewerEmail = reader["InterviewerEmail"] is DBNull ? "" : reader["InterviewerEmail"].ToString(),
                          InterviewerPassword = reader["InterviewerPassword"] is DBNull ? "" : reader["InterviewerPassword"].ToString(),
                          Token = reader["Token"] is DBNull ? "" : reader["Token"].ToString()
                      }
                    );
                }
                if (list.Count == 0)
                {
                    return null;
                }
                return list[0];
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {
                _databaseConnector.CloseConnection(conn);
            }
        }

        /// <summary>
        /// Get All Interviewer 
        /// </summary>
        /// <returns></returns>
        public List<Interviewer> Get()
        {
            MySqlConnection conn = null;
            try
            {
                List<Interviewer> list = new List<Interviewer>();
                conn = _databaseConnector.getConnection();
                _databaseConnector.OpenConnection(conn);
                string command = $"select * from interviewer;";
                MySqlCommand cmd = new MySqlCommand(command, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(
                      new Interviewer()
                      {
                          InterviewerID = reader["InterviewerID"] is DBNull ? -1 : Convert.ToInt32(reader["InterviewerID"].ToString()),
                          InterviewerName = reader["InterviewerName"] is DBNull ? "" : reader["InterviewerName"].ToString(),
                          InterviewerDepartment = reader["InterviewerDepartment"] is DBNull ? "" : reader["InterviewerDepartment"].ToString(),
                          InterviewerEmail = reader["InterviewerEmail"] is DBNull ? "" : reader["InterviewerEmail"].ToString(),
                          InterviewerPassword = reader["InterviewerPassword"] is DBNull ? "" : reader["InterviewerPassword"].ToString(),
                          Token = reader["Token"] is DBNull ? "" : reader["Token"].ToString()

                      }
                    );
                }
                return list;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {
                _databaseConnector.CloseConnection(conn);
            }
        }

        /// <summary>
        /// Get Interviewer By InterviewMeetingID
        /// </summary>
        /// <param name="interviewMeetingID"></param>
        /// <returns></returns>
        public List<Interviewer> GetByInterviewMeetingID(int interviewMeetingID)
        {
            MySqlConnection conn = null;
            try
            {
                List<Interviewer> list = new List<Interviewer>();
                conn = _databaseConnector.getConnection();
                _databaseConnector.OpenConnection(conn);
                string command = $"select er.* from interviewer er, interviewmeetingdetail d where er.interviewerid = d.interviewerid and d.interviewmeetingid = @interviewMeetingID;";
                MySqlCommand cmd = new MySqlCommand(command, conn);
                cmd.Parameters.AddWithValue("@interviewMeetingID", interviewMeetingID);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(
                      new Interviewer()
                      {
                          InterviewerID = reader["InterviewerID"] is DBNull ? -1 : Convert.ToInt32(reader["InterviewerID"].ToString()),
                          InterviewerName = reader["InterviewerName"] is DBNull ? "" : reader["InterviewerName"].ToString(),
                          InterviewerDepartment = reader["InterviewerDepartment"] is DBNull ? "" : reader["InterviewerDepartment"].ToString(),
                          InterviewerEmail = reader["InterviewerEmail"] is DBNull ? "" : reader["InterviewerEmail"].ToString(),
                          InterviewerPassword = reader["InterviewerPassword"] is DBNull ? "" : reader["InterviewerPassword"].ToString(),
                          Token = reader["Token"] is DBNull ? "" : reader["Token"].ToString()

                      }
                    );
                }
                return list;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {
                _databaseConnector.CloseConnection(conn);
            }
        } 
        #endregion
    }
}
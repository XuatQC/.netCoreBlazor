using CandidateSystem.Shared;
using MySqlConnector;


namespace CandidateSystem.DataLayer.InterviewMeetingRepository
{
    public class InterviewMeetingRes : IInterviewMeetingRes
    {
        #region Properties

        private readonly IDatabaseConnector _databaseConnector;

        #endregion
        #region Constructor

        public InterviewMeetingRes(IDatabaseConnector databaseConnector)
        {
            _databaseConnector = databaseConnector;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Insert InterviewMeeting 
        /// </summary>
        /// <param name="interviewMeeting"></param>
        /// <returns></returns>
        public int Insert(InterviewMeeting interviewMeeting)
        {
            var conn = _databaseConnector.getConnection();
            try
            {
                _databaseConnector.OpenConnection(conn);
                string query = $"insert into interviewmeeting values(@InterviewMeetingID,@InterviewMeetingHeader,@InterviewMeetingPlace,@InterviewMeetingDate,@InterviewMeetingRoomID,@CandidateID,@isHaveAssess);select last_insert_id() as 'additemid';";
                MySqlCommand command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@InterviewMeetingID", interviewMeeting.InterviewMeetingID);
                command.Parameters.AddWithValue("@InterviewMeetingHeader", interviewMeeting.InterviewMeetingHeader);
                command.Parameters.AddWithValue("@InterviewMeetingPlace", interviewMeeting.InterviewMeetingPlace);
                command.Parameters.AddWithValue("@InterviewMeetingDate", interviewMeeting.InterviewMeetingDate);
                command.Parameters.AddWithValue("@InterviewMeetingRoomID", interviewMeeting.InterviewMeetingRoomID);
                command.Parameters.AddWithValue("@CandidateID", interviewMeeting.CandidateID);
                command.Parameters.AddWithValue("@isHaveAssess", interviewMeeting.isHaveAssess);
                MySqlDataReader reader = command.ExecuteReader();
                int addItemID = 0;
                while (reader.Read())
                {
                    addItemID = reader["additemid"] is DBNull ? -1 : Convert.ToInt16(reader["additemid"].ToString());
                }
                return addItemID;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return -1;
            }
            finally
            {
                _databaseConnector.CloseConnection(conn);
            }
        }

        /// <summary>
        /// Insert InterviewMeeting 
        /// </summary>
        /// <param name="interviewMeeting"></param>
        /// <returns></returns>
        public List<InterviewMeeting> Show()
        {
            var conn = _databaseConnector.getConnection();
            try
            {
                _databaseConnector.OpenConnection(conn);
                string query = $"select * from InterviewMeeting;";
                List<InterviewMeeting> list = new List<InterviewMeeting>();
                MySqlCommand command = new MySqlCommand(query, conn);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(
                        new InterviewMeeting()
                        {
                            InterviewMeetingID = reader["InterviewMeetingID"] is DBNull ? -1 : Convert.ToInt16(reader["InterviewMeetingID"].ToString()),
                            InterviewMeetingHeader = reader["InterviewMeetingHeader"] is DBNull ? "" : reader["InterviewMeetingHeader"].ToString(),
                            InterviewMeetingPlace = reader["InterviewMeetingPlace"] is DBNull ? "" : reader["InterviewMeetingPlace"].ToString(),
                            InterviewMeetingDate = reader["InterviewMeetingDate"] is DBNull ? new DateTime() : DateTime.Parse(reader["InterviewMeetingDate"].ToString()),
                            InterviewMeetingRoomID = reader["InterviewMeetingRoomID"] is DBNull ? -1 : Convert.ToInt32(reader["InterviewMeetingRoomID"].ToString())
                        });

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
        /// Insert InterviewMeeting 
        /// </summary>
        /// <param name="interviewMeeting"></param>
        /// <returns></returns>
        public List<InterviewMeeting> ShowByDate(DateOnly selectedDate)
        {
            var conn = _databaseConnector.getConnection();
            try
            {
                _databaseConnector.OpenConnection(conn);
                string query = $"select * from InterviewMeeting where date(interviewMeetingDate) = str_to_date('{selectedDate}','%m/%d/%Y');";
                List<InterviewMeeting> list = new List<InterviewMeeting>();
                MySqlCommand command = new MySqlCommand(query, conn);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(
                        new InterviewMeeting()
                        {
                            InterviewMeetingID = reader["InterviewMeetingID"] is DBNull ? -1 : Convert.ToInt16(reader["InterviewMeetingID"].ToString()),
                            InterviewMeetingHeader = reader["InterviewMeetingHeader"] is DBNull ? "" : reader["InterviewMeetingHeader"].ToString(),
                            InterviewMeetingPlace = reader["InterviewMeetingPlace"] is DBNull ? "" : reader["InterviewMeetingPlace"].ToString(),
                            InterviewMeetingDate = reader["InterviewMeetingDate"] is DBNull ? new DateTime() : DateTime.Parse(reader["InterviewMeetingDate"].ToString()),
                            InterviewMeetingRoomID = reader["InterviewMeetingRoomID"] is DBNull ? -1 : Convert.ToInt32(reader["InterviewMeetingRoomID"].ToString())
                        });
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
        /// Insert InterviewMeeting 
        /// </summary>
        /// <param name="interviewMeeting"></param>
        /// <returns></returns>
        public bool Update(InterviewMeeting value, int InterviewMeetingID)
        {
            var conn = _databaseConnector.getConnection();
            try
            {
                _databaseConnector.OpenConnection(conn);
                string query = $"update interviewmeeting set interviewMeetingHeader= @InterviewMeetingHeader,interviewmeetingplace = @InterviewMeetingPlace,interviewmeetingdate=@InterviewMeetingDate,interviewmeetingroomid = @InterviewMeetingRoomID,isHaveAssess = @isHaveAssess where interviewmeetingid=@InterviewMeetingID;";
                MySqlCommand command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@InterviewMeetingHeader", value.InterviewMeetingHeader);
                command.Parameters.AddWithValue("@InterviewMeetingPlace", value.InterviewMeetingPlace);
                command.Parameters.AddWithValue("@InterviewMeetingDate", value.InterviewMeetingDate);
                command.Parameters.AddWithValue("@InterviewMeetingRoomID", value.InterviewMeetingRoomID);
                command.Parameters.AddWithValue("@isHaveAssess", value.isHaveAssess);
                command.Parameters.AddWithValue("@InterviewMeetingID", InterviewMeetingID);
                int effort = command.ExecuteNonQuery();
                return effort == -1 ? false : true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                _databaseConnector.CloseConnection(conn);
            }
        }

        /// <summary>
        /// Insert InterviewMeeting 
        /// </summary>
        /// <param name="interviewMeeting"></param>
        /// <returns></returns>
        public bool UpdateIsHaveAssess(int isHaveAssess, int interviewMeetingID)
        {
            var conn = _databaseConnector.getConnection();
            try
            {
                _databaseConnector.OpenConnection(conn);
                string query = $"update interviewmeeting set isHaveAssess= @isHaveAssess where interviewmeetingid = @InterviewMeetingID;";
                MySqlCommand command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("isHaveAssess", isHaveAssess);
                command.Parameters.AddWithValue("@InterviewMeetingID", interviewMeetingID);
                int effort = command.ExecuteNonQuery();
                return effort == -1 ? false : true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                _databaseConnector.CloseConnection(conn);
            }
        }

        /// <summary>
        /// Insert InterviewMeeting 
        /// </summary>
        /// <param name="interviewMeeting"></param>
        /// <returns></returns>
        public InterviewMeetingInfoExtends GetById(int InterviewID)
        {
            var conn = _databaseConnector.getConnection();
            try
            {
                _databaseConnector.OpenConnection(conn);
                string query = $"select i.*,p.* from InterviewMeeting i, Interviewer p, InterviewMeetingDetail pp where i.InterviewMeetingID = pp.InterviewMeetingID and p.interviewerid = pp.interviewerid and i.interviewmeetingid = {InterviewID};";
                List<InterviewMeeting> list = new List<InterviewMeeting>();
                List<Interviewer> listInterviewer = new List<Interviewer>();
                MySqlCommand command = new MySqlCommand(query, conn);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(
                        new InterviewMeeting()
                        {
                            InterviewMeetingID = reader["InterviewMeetingID"] is DBNull ? -1 : Convert.ToInt16(reader["InterviewMeetingID"].ToString()),
                            InterviewMeetingHeader = reader["InterviewMeetingHeader"] is DBNull ? "" : reader["InterviewMeetingHeader"].ToString(),
                            InterviewMeetingPlace = reader["InterviewMeetingPlace"] is DBNull ? "" : reader["InterviewMeetingPlace"].ToString(),
                            InterviewMeetingDate = reader["InterviewMeetingDate"] is DBNull ? new DateTime() : DateTime.Parse(reader["InterviewMeetingDate"].ToString()),
                            InterviewMeetingRoomID = reader["InterviewMeetingRoomID"] is DBNull ? -1 : Convert.ToInt32(reader["InterviewMeetingRoomID"].ToString()),
                            CandidateID = reader["CandidateID"] is DBNull ? -1 : Convert.ToInt32(reader["CandidateID"].ToString())
                        });
                    listInterviewer.Add(
                        new Interviewer()
                            {
                                InterviewerID = reader["InterviewerID"] is DBNull ? -1 : Convert.ToInt32(reader["InterviewerID"].ToString()),
                                InterviewerName = reader["InterviewerName"] is DBNull ? "" : reader["InterviewerName"].ToString(),
                                InterviewerDepartment = reader["InterviewerDepartment"] is DBNull ? "" : reader["InterviewerDepartment"].ToString(),
                                InterviewerEmail = reader["InterviewerEmail"] is DBNull ? "" : reader["InterviewerEmail"].ToString()
                            }
                        );
                }
                return new InterviewMeetingInfoExtends()
                {
                    Interviewers = listInterviewer,
                    InterviewMeeting = list[0]
                };
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

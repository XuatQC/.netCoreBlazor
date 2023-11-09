using CandidateSystem.Shared;
using MySqlConnector;

namespace CandidateSystem.DataLayer
{
    public class InterviewMeetingDetailRes : IInterviewMeetingDetailRes
    {
        #region Properties

        private readonly IDatabaseConnector _databaseConnector;

        #endregion
        #region Constructor

        public InterviewMeetingDetailRes(IDatabaseConnector databaseConnector)
        {
            _databaseConnector = databaseConnector;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Insert interviewMeetingDetails 
        /// </summary>
        /// <param name="interviewMeetingDetail"></param>
        /// <returns></returns>
        public bool MassInsert(List<InterviewMeetingDetail> interviewMeetingDetail)
        {
            var conn = _databaseConnector.getConnection();
            try
            {
                _databaseConnector.OpenConnection(conn);
                var listOfInterviewMeetingDetailAsString = "";
                for (int i = 0; i < interviewMeetingDetail.Count; i++)
                {
                    listOfInterviewMeetingDetailAsString += $"({interviewMeetingDetail[i].InterviewMeetingDetailID},{interviewMeetingDetail[i].InterviewMeetingID},{interviewMeetingDetail[i].InterviewerID})";
                    if (i != interviewMeetingDetail.Count - 1)
                    {
                        listOfInterviewMeetingDetailAsString += ",";
                    }
                }
                string query = $"insert into interviewMeetingDetail values {listOfInterviewMeetingDetailAsString};";
                MySqlCommand command = new MySqlCommand(query, conn);
                command.ExecuteNonQuery();
                return true;
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
        /// delete interviewMeetingDetails 
        /// </summary>
        /// <param name="interviewMeetingDetail"></param>
        /// <returns></returns>
        public bool MassDeleteByInterviewMeetingID(int InterviewMeetingID)
        {
            var conn = _databaseConnector.getConnection();
            try
            {
                _databaseConnector.OpenConnection(conn);
                string query = $"delete from interviewmeetingdetail where InterviewMeetingId = {InterviewMeetingID};";
                MySqlCommand command = new MySqlCommand(query, conn);
                command.ExecuteNonQuery();
                return true;
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
        /// Get interviewMeetingid by candidateID
        /// </summary>
        /// <param name="interviewMeetingDetail"></param>
        /// <returns></returns>
        public int GetNewestInterviewMeetingIDByCandidateID(int candidateID)
        {
            var conn = _databaseConnector.getConnection();
            try
            {
                _databaseConnector.OpenConnection(conn);
                string query = $"select d.interviewmeetingdetailid from interviewmeetingdetail d, interviewmeeting i where i.interviewmeetingid = d.interviewmeetingid and i.candidateid = {candidateID} order by (d.interviewmeetingdetailid) desc limit 1;";
                MySqlCommand command = new MySqlCommand(query, conn);
                MySqlDataReader reader = command.ExecuteReader();
                int InterviewMeetingDetailID = -1;
                while (reader.Read())
                {
                    InterviewMeetingDetailID = reader["InterviewMeetingDetailID"] is DBNull ? -1 : Convert.ToInt16(reader["interviewMeetingDetailID"].ToString());
                }
                return InterviewMeetingDetailID;
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
        /// get interviewmeetingDetailID by interviewerID,InterviewmeetingID
        /// </summary>
        /// <returns></returns>
        public int GetInterviewMeetingDetailIDByIDs(int interviewerID, int interviewMeetingID)
        {
            var conn = _databaseConnector.getConnection();
            try
            {
                _databaseConnector.OpenConnection(conn);
                string query = $"select interviewMeetingDetailID from interviewMeetingDetail where interviewMeetingID = @interviewMeetingID and interviewerID = @interviewerID;";
                MySqlCommand command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@interviewMeetingID", interviewMeetingID);
                command.Parameters.AddWithValue("@interviewerID", interviewerID);
                MySqlDataReader reader = command.ExecuteReader();
                int InterviewMeetingDetailID = -1;
                while (reader.Read())
                {
                    InterviewMeetingDetailID = reader["InterviewMeetingDetailID"] is DBNull ? -1 : Convert.ToInt16(reader["interviewMeetingDetailID"].ToString());
                }
                return InterviewMeetingDetailID;
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
        #endregion

    }
}

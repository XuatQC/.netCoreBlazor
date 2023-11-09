using CandidateSystem.Shared;
using MySqlConnector;

namespace CandidateSystem.DataLayer
{
    public class AssessRes : IAssessRes
    {
        #region Properties

        private readonly IDatabaseConnector _databaseConnector;

        #endregion

        #region Constructor
        public AssessRes(IDatabaseConnector databaseConnector)
        {
            _databaseConnector = databaseConnector;
        }
        #endregion

        #region Methods
        /// <summary>
        /// insert an assess
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Insert(Assess value)
        {
            MySqlConnection conn = null;
            try
            {
                conn = _databaseConnector.getConnection();
                _databaseConnector.OpenConnection(conn);
                string command = $"insert into Assess values(@AssessID,@isPass,@DenyReason,@Comment,@InterviewMeetingDetailID);";
                MySqlCommand cmd = new MySqlCommand(command, conn);
                cmd.Parameters.AddWithValue("@AssessID", value.AssessID);
                cmd.Parameters.AddWithValue("@isPass", value.isPass);
                cmd.Parameters.AddWithValue("@DenyReason", value.DenyReason);
                cmd.Parameters.AddWithValue("@Comment", value.Comment);
                cmd.Parameters.AddWithValue("@InterviewMeetingDetailID", value.InterviewMeetingDetailID);
                int i = cmd.ExecuteNonQuery();
                if (i != -1)
                    return true;
                else
                    return false;
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
        public int GetByInterviewMeetingDetailID(int interviewMeetingDetailID)
        {
            var conn = _databaseConnector.getConnection();
            try
            {
                _databaseConnector.OpenConnection(conn);
                string query = $"select * from  assess where interviewMeetingDetailId = @interviewmeetingdetail;";
                MySqlCommand command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@interviewmeetingdetail", interviewMeetingDetailID);
                MySqlDataReader reader = command.ExecuteReader();
                Assess CurrentAssess = new Assess()
                {
                    InterviewMeetingDetailID = -1
                };
                while (reader.Read())
                {
                    CurrentAssess = new Assess()
                    {
                        AssessID = reader["AssessID"] is DBNull ? -1 : Convert.ToInt16(reader["AssessID"].ToString()),
                        InterviewMeetingDetailID = reader["InterviewMeetingDetailID"] is DBNull ? -1 : Convert.ToInt16(reader["interviewMeetingDetailID"].ToString()),
                        DenyReason = reader["DenyReason"] is DBNull ? "" : reader["Denyreason"].ToString(),
                        Comment = reader["comment"] is DBNull ? "" : reader["comment"].ToString(),
                        isPass = reader["isPass"] is DBNull ? -1 : Convert.ToInt16(reader["isPass"].ToString())
                    };
                }
                return CurrentAssess.InterviewMeetingDetailID;

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
        /// 
        /// </summary>
        /// <param name="interviewerID"></param>
        /// <param name="interviewMeetingID"></param>
        /// <returns></returns>
        public Assess? GetByInterviewMeetingIDAndInterviewerID(int interviewerID, int interviewMeetingID)
        {
            MySqlConnection conn = null;
            try
            {
                conn = _databaseConnector.getConnection();
                _databaseConnector.OpenConnection(conn);
                string command = $"select s.*,d.interviewmeetingID from assess s, interviewMeetingDetail d where s.interviewMeetingDetailid = d.interviewmeetingdetailId and d.interviewmeetingid = @interviewMeetingID and d.interviewerid = @interviewerID and s.interviewmeetingdetailid = d.interviewmeetingdetailid;";
                List<Assess> list = new List<Assess>();
                MySqlCommand cmd = new MySqlCommand(command, conn);
                cmd.Parameters.AddWithValue("@interviewMeetingID", interviewMeetingID);
                cmd.Parameters.AddWithValue("@interviewerID", interviewerID);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(
                        new Assess()
                        {
                            AssessID = reader["assessID"] is DBNull ? -1 : Convert.ToInt16(reader["assessID"].ToString()),
                            isPass = reader["isPass"] is DBNull ? -1 : Convert.ToInt16(reader["isPass"].ToString()),
                            DenyReason = reader["isPass"] is DBNull ? "" : reader["DenyReason"].ToString(),
                            Comment = reader["isPass"] is DBNull ? "" : reader["Comment"].ToString(),
                            InterviewMeetingDetailID = reader["InterviewMeetingDetailID"] is DBNull ? -1 : Convert.ToInt16(reader["InterviewMeetingDetailID"].ToString()),
                            InterviewMeetingID = reader["InterviewMeetingID"] is DBNull ? -1 : Convert.ToInt16(reader["InterviewMeetingID"].ToString())
                        }
                    );
                }
                if (list.Count > 0)
                    return list[0];
                else
                    return null;
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
        /// check if all interviewers has created their assessment
        /// </summary>
        /// <returns></returns>
        public List<int> getAllIsPassResult(int interviewMeetingID)
        {
            MySqlConnection conn = null;
            try
            {
                conn = _databaseConnector.getConnection();
                _databaseConnector.OpenConnection(conn);
                List<int> list = new List<int>();
                string command = $"select s.ispass from assess s, interviewmeetingdetail d where s.interviewmeetingdetailid = d.interviewmeetingdetailid and d.interviewmeetingid = @interviewMeetingID;";
                MySqlCommand cmd = new MySqlCommand(command, conn);
                cmd.Parameters.AddWithValue("@interviewMeetingID", interviewMeetingID);
                MySqlDataReader reader = cmd.ExecuteReader();
                int isEqual = 0;
                while (reader.Read())
                {
                    list.Add(
                        reader["isPass"] is DBNull ? -1 : Convert.ToUInt16(reader["ispass"].ToString())
                        );
                }
                _databaseConnector.CloseConnection(conn);
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
        /// Update Assess
        /// </summary>
        /// <returns></returns>
        public bool Update(Assess value, int AssessID)
        {
            MySqlConnection conn = null;
            try
            {
                conn = _databaseConnector.getConnection();
                _databaseConnector.OpenConnection(conn);
                MySqlCommand cmd = new MySqlCommand($"update Assess set isPass = @isPass,DenyReason = @DenyReason,Comment = @Comment where assessid = @AssessID;", conn);
                cmd.Parameters.AddWithValue("@isPass", value.isPass);
                cmd.Parameters.AddWithValue("@DenyReason", value.DenyReason);
                cmd.Parameters.AddWithValue("@Comment", value.Comment);
                cmd.Parameters.AddWithValue("@AssessID", AssessID);
                int effectedRow = cmd.ExecuteNonQuery();
                _databaseConnector.CloseConnection(conn);
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
        /// check if all interviewers has created their assessment
        /// </summary>
        /// <returns></returns>
        public bool IsAllInterviewerWriteTheirAssessment(int interviewMeetingID)
        {
            MySqlConnection conn = null;
            try
            {
                conn = _databaseConnector.getConnection();
                _databaseConnector.OpenConnection(conn);
                string command = $"select (select count(s.assessid) from assess s, interviewmeetingdetail d where d.interviewmeetingdetailid = s.interviewmeetingdetailid and d.interviewmeetingid = @interviewMeetingID) = (select count(interviewmeetingdetailid) from interviewmeetingdetail where interviewmeetingid = @interviewMeetingID) as 'isEqual';";
                MySqlCommand cmd = new MySqlCommand(command, conn);
                cmd.Parameters.AddWithValue("interviewMeetingID", interviewMeetingID);
                MySqlDataReader reader = cmd.ExecuteReader();
                int isEqual = 0;
                while (reader.Read())
                {
                    isEqual = reader["isEqual"] is DBNull ? -1 : Convert.ToUInt16(reader["isEqual"].ToString());
                }
                _databaseConnector.CloseConnection(conn);
                if (isEqual == 0)
                    return false;
                else
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
        #endregion


    }
}

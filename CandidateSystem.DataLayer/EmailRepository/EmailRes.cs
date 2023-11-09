using CandidateSystem.DataLayer.EmailRepository;
using MySqlConnector;

namespace CandidateSystem.DataLayer
{
    public class EmailRes : IEmailRes
    {
        private IDatabaseConnector _databaseConnector;
        public EmailRes(IDatabaseConnector databaseConnector)
        {
            _databaseConnector = databaseConnector;
        }
        
        /// <summary>
        /// Get Al Email
        /// </summary>
        /// <returns></returns>
        public List<Email> GetAll()
        {
            MySqlConnection conn = null;
            try
            {
                conn = _databaseConnector.getConnection();
                List<Email> list = new List<Email>();
                _databaseConnector.OpenConnection(conn);
                MySqlCommand command = new MySqlCommand("Select * from Email", conn);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(
                        new Email()
                        {
                            EmailID = reader["EmailID"] is DBNull ? -1 : Convert.ToInt32(reader["EmailID"].ToString()),
                            EmailSubject = reader["EmailID"] is DBNull ? "" : reader["EmailSubject"].ToString(),
                            EmailContent = reader["EmailID"] is DBNull ? "" : reader["EmailContent"].ToString(),
                            DelFlag = reader["EmailID"] is DBNull ? -1 : Convert.ToInt16(reader["DelFlag"].ToString()),
                            EmailFile = reader["EmailID"] is DBNull ? "" : reader["EmailFile"].ToString(),
                            CandidateID = reader["EmailID"] is DBNull ? -1 : Convert.ToInt16(reader["CandidateID"].ToString()),
                            InvitedDate = reader["EmailID"] is DBNull ? new DateTime() : DateTime.Parse(reader["InvitedDate"].ToString()),
                            InvitedPlace = reader["EmailID"] is DBNull ? "" : reader["InvitedPlace"].ToString(),
                            CandidateStatusID = reader["EmailID"] is DBNull ? -1 : Convert.ToInt16(reader["CandidateStatusID"].ToString()),
                            EmailAddress = reader["EmailID"] is DBNull ? "" : reader["EmailAddress"].ToString()
                        }
                );
                }
                return list;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<Email>()
                {

                };
            }
            finally
            {
                _databaseConnector.CloseConnection(conn);
            }
        }


        /// <summary>
        /// Insert Email by candidateStatusID
        /// </summary>
        /// <param name="value"></param>
        /// <param name="CandidateStatusID"></param>
        /// <returns></returns>
        public int Insert(Email value,int CandidateStatusID )
        {
            MySqlConnection conn = null;
            try
            {
                conn = _databaseConnector.getConnection();
                _databaseConnector.OpenConnection(conn);
                string command = $"insert into email values(" +
                    $"@EmailID," +
                    $"@EmailSubject," +
                    $"@EmailContent," +
                    $"@delFlag," +
                    $"@EmailFile," +
                    $"@CandidateID," +
                    $"@InvitedPlace," +
                    $"@InvitedDate," +
                    $"@CandidateStatusID," +
                    $"@EmailAddress); select last_insert_id() as 'additemid';";
                MySqlCommand cmd = new MySqlCommand(command, conn);
                cmd.Parameters.AddWithValue("@EmailID",value.EmailID);
                cmd.Parameters.AddWithValue("@EmailSubject", value.EmailSubject);
                cmd.Parameters.AddWithValue("@EmailContent", value.EmailContent);
                cmd.Parameters.AddWithValue("@EmailFile", value.EmailFile);
                cmd.Parameters.AddWithValue("@CandidateID", value.CandidateID);
                cmd.Parameters.AddWithValue("@InvitedPlace", value.InvitedPlace);
                cmd.Parameters.AddWithValue("@InvitedDate", value.InvitedDate);
                cmd.Parameters.AddWithValue("@CandidateStatusID", value.CandidateStatusID);
                cmd.Parameters.AddWithValue("@EmailAddress", value.EmailAddress);
                cmd.Parameters.AddWithValue("@delFlag", value.DelFlag);
                MySqlDataReader reader = cmd.ExecuteReader();
                int addItemID = 0;
                while (reader.Read())
                {
                    addItemID = reader["additemid"] is DBNull ? -1 : Convert.ToInt16(reader["additemid"].ToString());
                }
                return addItemID;

            }
            catch(Exception e)
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
        /// Gety newest By CandidateID
        /// </summary>
        /// <param name="CandidateID"></param>
        /// <returns></returns>
        public Email GetByCandidateID(int CandidateID)
        {
            MySqlConnection conn = null;
            try
            {
                List<Email> list = new List<Email>(); 
                conn = _databaseConnector.getConnection();
                _databaseConnector.OpenConnection(conn);
                string command = $"select e.*, c.candidateemail as 'emailaddress',e.candidatestatusid, c.candidatename as 'receiveperson' from email e, candidate c where c.candidateid = e.candidateid and e.candidateID = @candidateid order by (e.emailid) desc";
                MySqlCommand cmd = new MySqlCommand(command, conn);
                cmd.Parameters.AddWithValue("@candidateid", CandidateID);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(
                        new Email()
                        {
                            EmailID = reader["EmailID"] is DBNull ? -1 : Convert.ToInt32(reader["EmailID"].ToString()),
                            EmailSubject = reader["EmailSubject"] is DBNull ? "" : reader["EmailSubject"].ToString(),
                            EmailContent = reader["EmailContent"] is DBNull ? "" : reader["EmailContent"].ToString(),
                            DelFlag = reader["DelFlag"] is DBNull ? -1 : Convert.ToInt16(reader["DelFlag"].ToString()),
                            EmailFile = reader["EmailFile"] is DBNull ? "" : reader["EmailFile"].ToString(),
                            CandidateID = reader["CandidateID"] is DBNull ? -1 : Convert.ToInt16(reader["CandidateID"].ToString()),
                            InvitedDate = reader["InvitedDate"] is DBNull ? new DateTime() : DateTime.Parse(reader["InvitedDate"].ToString()),
                            InvitedPlace = reader["InvitedPlace"] is DBNull ? "" : reader["InvitedPlace"].ToString(),
                            CandidateStatusID = reader["CandidateStatusID"] is DBNull ? -1 : Convert.ToInt16(reader["CandidateStatusID"].ToString()),
                            ReceivePerson = reader["ReceivePerson"] is DBNull ? ""  : reader["receivePerson"].ToString(),
                            EmailAddress = reader["EmailAddress"] is DBNull ? "" : reader["EmailAddress"].ToString()
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
        /// Gety By CandidateID
        /// </summary>
        /// <param name="CandidateID"></param>
        /// <returns></returns>
        public Email GetByID(int emailID)
        {
            MySqlConnection conn = null;
            try
            {
                List<Email> list = new List<Email>();
                conn = _databaseConnector.getConnection();
                _databaseConnector.OpenConnection(conn);
                string command = $"select * from email where emailid = {emailID};";
                MySqlCommand cmd = new MySqlCommand(command, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(
                        new Email()
                        {
                            EmailID = reader["EmailID"] is DBNull ? -1 : Convert.ToInt32(reader["EmailID"].ToString()),
                            EmailSubject = reader["EmailSubject"] is DBNull ? "" : reader["EmailSubject"].ToString(),
                            EmailContent = reader["EmailContent"] is DBNull ? "" : reader["EmailContent"].ToString(),
                            DelFlag = reader["DelFlag"] is DBNull ? -1 : Convert.ToInt16(reader["DelFlag"].ToString()),
                            EmailFile = reader["EmailFile"] is DBNull ? "" : reader["EmailFile"].ToString(),
                            CandidateID = reader["CandidateID"] is DBNull ? -1 : Convert.ToInt16(reader["CandidateID"].ToString()),
                            InvitedDate = reader["InvitedDate"] is DBNull ? new DateTime() : DateTime.Parse(reader["InvitedDate"].ToString()),
                            InvitedPlace = reader["InvitedPlace"] is DBNull ? "" : reader["InvitedPlace"].ToString(),
                            CandidateStatusID = reader["CandidateStatusID"] is DBNull ? -1 : Convert.ToInt16(reader["CandidateStatusID"].ToString()),
                            EmailAddress = reader["EmailAddress"] is DBNull ? "" : reader["EmailAddress"].ToString()
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
        /// Insert emails
        /// </summary>
        /// <param name="emails"></param>
        /// <returns></returns>
        public bool MultiInsert(List<Email> emails)
        {
            MySqlConnection conn = null;
            try
            {
                conn = _databaseConnector.getConnection();
                _databaseConnector.OpenConnection(conn);
                string command = $"insert into email values ";
                for(int i = 0; i < emails.Count; i++)
                {
                    string valuesString = "";
                    valuesString += $"{emails[i].EmailID},'{emails[i].EmailSubject}','{emails[i].EmailContent}',{emails[i].DelFlag},'{emails[i].EmailFile}',{emails[i].CandidateID},'{emails[i].InvitedPlace}',str_to_date('{emails[i].InvitedDate}','%m/%d/%Y %r'),{emails[i].CandidateStatusID},'{emails[i].EmailAddress}'";
                    valuesString = $"({valuesString})";
                    if (i != emails.Count - 1)
                    {
                        valuesString += ",";
                    }
                    command += valuesString;
                }
                command += ";";
                MySqlCommand cmd = new MySqlCommand(command, conn);
                int effortRow = cmd.ExecuteNonQuery();
                if(effortRow == -1)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch(Exception e)
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
        /// Update
        /// </summary>
        /// <param name="value"></param>
        /// <param name="emailID"></param>
        /// <returns></returns>
        public bool Update(Email value, int emailID)
        {
            MySqlConnection conn = null;
            try
            {
                conn = _databaseConnector.getConnection();
                _databaseConnector.OpenConnection(conn);
                string command = $"update email set " +
                    $"EmailSubject = @EmailSubject," +
                    $"EmailContent = @EmailContent," +
                    $"EmailFile = @EmailFile," +
                    $"CandidateID = @CandidateID, " +
                    $"InvitedPlace = @InvitedPlace," +
                    $"InvitedDate = @InvitedDate, " +
                    $"CandidateStatusId = @CandidateStatusID where emailid = @EmailID";
                MySqlCommand cmd = new MySqlCommand(command, conn);
                cmd.Parameters.AddWithValue("@EmailID", emailID);
                cmd.Parameters.AddWithValue("@EmailSubject", value.EmailSubject);
                cmd.Parameters.AddWithValue("@EmailContent", value.EmailContent);
                cmd.Parameters.AddWithValue("@EmailFile", value.EmailFile);
                cmd.Parameters.AddWithValue("@CandidateID", value.CandidateID);
                cmd.Parameters.AddWithValue("@InvitedPlace", value.InvitedPlace);
                cmd.Parameters.AddWithValue("@InvitedDate", value.InvitedDate);
                cmd.Parameters.AddWithValue("@CandidateStatusID", value.CandidateStatusID);
                cmd.Parameters.AddWithValue("@EmailAddress", value.EmailAddress);
                int effectedRow = cmd.ExecuteNonQuery();
                if (effectedRow != -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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
        /// Delete Emails By CandidateIDs
        /// </summary>
        /// <param name="candidateIDs"></param>
        /// <returns></returns>
        public bool MassDeleteByCandidateID(List<int> candidateIDs)
        {
            MySqlConnection conn = null;
            try
            {
                conn = _databaseConnector.getConnection();
                _databaseConnector.OpenConnection(conn);
                string candidateIDsAsString = "";
                for (int i = 0; i < candidateIDs.Count; i++)
                {
                    candidateIDsAsString += candidateIDs[i];
                    if (i != candidateIDs.Count - 1)
                    {
                        candidateIDsAsString += ",";
                    }
                }
                string command = $"Delete from email where candidateid in  ({candidateIDsAsString});";
                MySqlCommand cmd = new MySqlCommand(command, conn);

                int effectedRow = cmd.ExecuteNonQuery();
                if (effectedRow != -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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
        /// Get Nearest Email And Candidate By CandidateID
        /// </summary>
        /// <param name="CandidateID"></param>
        /// <returns></returns>
        public Tuple<Email,Candidate> GetNearestEmailAndCandidateByCandidateId(int CandidateID)
        {
            MySqlConnection conn = null;
            try
            {
                List<Tuple<Email,Candidate>> list = new List<Tuple<Email, Candidate>>();
                conn = _databaseConnector.getConnection();
                _databaseConnector.OpenConnection(conn);
                string command = $"select e.*,c.* from email e, candidate c where e.candidateid = c.candidateid and e.candidatestatusid = c.candidatestatusid-1 and e.candidateid = @candidateID order by(e.emailid) desc;";
                MySqlCommand cmd = new MySqlCommand(command, conn);
                cmd.Parameters.AddWithValue("@candidateID", CandidateID);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(
                        new Tuple<Email, Candidate>(new Email()
                        {
                            EmailID = reader["EmailID"] is DBNull ? -1 : Convert.ToInt32(reader["EmailID"].ToString()),
                            EmailSubject = reader["EmailID"] is DBNull ? "" : reader["EmailSubject"].ToString(),
                            EmailContent = reader["EmailID"] is DBNull ? "" : reader["EmailContent"].ToString(),
                            DelFlag = reader["EmailID"] is DBNull ? -1 : Convert.ToInt16(reader["DelFlag"].ToString()),
                            EmailFile = reader["EmailID"] is DBNull ? "" : reader["EmailFile"].ToString(),
                            CandidateID = reader["EmailID"] is DBNull ? -1 : Convert.ToInt16(reader["CandidateID"].ToString()),
                            InvitedDate = reader["EmailID"] is DBNull ? new DateTime() : DateTime.Parse(reader["InvitedDate"].ToString()),
                            InvitedPlace = reader["EmailID"] is DBNull ? "" : reader["InvitedPlace"].ToString(),
                            CandidateStatusID = reader["EmailID"] is DBNull ? -1 : Convert.ToInt16(reader["CandidateStatusID"].ToString()),
                            EmailAddress = reader["EmailID"] is DBNull ? "" : reader["EmailAddress"].ToString()
                        },
                        new Candidate()
                        {
                            CandidateID = reader["CandidateID"] is DBNull ? -1 : Convert.ToInt32(reader["CandidateID"]),
                            CandidateName = reader["CandidateName"] is DBNull ? "" : reader["CandidateName"].ToString(),
                            CandidateDateOfBirth = reader["CandidateDateOfBirth"] is DBNull ? new DateTime() : DateTime.Parse(reader["CandidateDateOfBirth"].ToString()),
                            CandidateAddress = reader["CandidateAddress"] is DBNull ? "" : reader["CandidateAddress"].ToString(),
                            CandidateNumber = reader["CandidateNumber"] is DBNull ? "" : reader["CandidateNumber"].ToString(),
                            CandidateCV = reader["CandidateCV"] is DBNull ? "" : reader["CandidateCV"].ToString(),
                            CandidateEmail = reader["CandidateEmail"] is DBNull ? "" : reader["CandidateEmail"].ToString(),
                            Resource = reader["Resource"] is DBNull ? "" : reader["Resource"].ToString(),
                            JobTitleID = reader["JobTitleID"] is DBNull ? -1 : Convert.ToInt32(reader["JobTitleID"]),
                            JobPositionID = reader["JobPositionID"] is DBNull ? -1 : Convert.ToInt32(reader["JobPositionID"]),
                            DelFlag = reader["DelFlag"] is DBNull ? -1 : Convert.ToInt16(reader["DelFlag"]),
                            HasApplyBefore = reader["HasApplyBefore"] is DBNull ? -1 : Convert.ToInt16(reader["HasApplyBefore"]),
                            CandidateStatusID = reader["CandidateStatusID"] is DBNull ? -1 : Convert.ToInt16(reader["CandidateStatusID"]),
                             CanContactID = reader["CanContactID"] is DBNull ? -1 :  Convert.ToInt16(reader["CancontactID"]),
                            DenyReason = reader["DenyReason"] is DBNull ? "" : reader["DenyReason"].ToString(),
                            HasEmailFlag = reader["HasEmailFlag"] is DBNull ? -1 : Convert.ToInt16(reader["HasEmailFlag"].ToString()),
                        })
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
    }
}

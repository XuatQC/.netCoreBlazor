using CandidateSystem.Shared;
using Microsoft.Extensions.Configuration;
using MySqlConnector;


namespace CandidateSystem.DataLayer
{
    public class CandidateRes : ICandidateRes
    {   
        #region Properties
        /// <summary>
        /// Configuration to get connectionstring
        /// </summary>
        private readonly IConfiguration _configuration;
        private readonly IDatabaseConnector _databaseConnector;

        #endregion
        #region Constructor
        public CandidateRes(IConfiguration configuration, IDatabaseConnector databaseConnector)
        {
            _databaseConnector = databaseConnector;
            _configuration = configuration;
        }

        #endregion
        #region Methods

        /// <summary>
        /// change delflag by single candidate id 
        /// </summary>
        /// <param name="candidateID"></param>
        /// <param name="delFlag"></param>
        /// <returns></returns>
        public bool ChangeDelFlagAndStatus(int candidateID, int delFlag, int status)
        {
            MySqlConnection conn = null;
            try
            {
                string denyReason = "";
                if(delFlag == 0)
                {
                    Candidate unchangedCandidate = GetByID(candidateID);
                            denyReason = Enum.GetName(typeof(CandidateStatusCode),unchangedCandidate.CandidateStatusID);
                }
                conn = _databaseConnector.getConnection();
                _databaseConnector.OpenConnection(conn);
                string assign = "";
                if (status != -1) {
                    assign += $" CandidateStatusId = @status ";
                }
                
                else if (delFlag != -1)
                {
                    assign += $" delFlag = @delFlag ";
                }
                string query = $"update candidate set {assign},denyreason='{denyReason}' where candidateID = @candidateID ;";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@status",status);
                cmd.Parameters.AddWithValue("@delFlag", delFlag);
                cmd.Parameters.AddWithValue("@candidateID", candidateID);
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
        /// get all candidate with no condition
        /// no need to use 
        /// </summary>
        /// <returns></returns>
        public List<Candidate> GetAll()
        {
            MySqlConnection conn = null;
            try
            {
                conn = _databaseConnector.getConnection();
                _databaseConnector.OpenConnection(conn);
                List<Candidate> list = new List<Candidate>();
                MySqlCommand cmd = new MySqlCommand("select * from candidate;", conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Candidate()
                    {
                        CandidateID = reader["CandidateID"] is DBNull ? -1 : Convert.ToInt32(reader["CandidateID"]),
                        CandidateName = reader["CandidateName"] is DBNull ? "" : reader["CandidateName"].ToString(),
                        CandidateDateOfBirth = reader["CandidateDateOfBirth"] is DBNull ?  new DateTime() :  DateTime.Parse(reader["CandidateDateOfBirth"].ToString()),
                        CandidateAddress = reader["CandidateAddress"] is DBNull ? "" :  reader["CandidateAddress"].ToString(),
                        CandidateNumber = reader["CandidateNumber"] is DBNull ? "" :  reader["CandidateNumber"].ToString(),
                        CandidateCV = reader["CandidateCV"] is DBNull ? "" : reader["CandidateCV"].ToString(),
                        CandidateEmail = reader["CandidateEmail"] is DBNull ? "" :  reader["CandidateEmail"].ToString(),
                        Resource = reader["Resource"] is DBNull ? "" :  reader["Resource"].ToString(),
                        JobTitleID = reader["JobTitleID"] is DBNull ? -1 :  Convert.ToInt32(reader["JobTitleID"]),
                        JobPositionID = reader["JobPositionID"] is DBNull ? -1 : Convert.ToInt32(reader["JobPositionID"]),
                        HasApplyBefore = reader["HasApplyBefore"] is DBNull ? -1 : Convert.ToInt16(reader["HasApplyBefore"]),
                        CandidateStatusID = reader["CandidateStatusID"] is DBNull ? -1 : Convert.ToInt16(reader["CandidateStatusID"])
                    });
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
        /// get all candidate with condition of logic del and status
        /// </summary>
        /// <param name="status"></param>
        /// <param name="delFlag"></param>
        /// <returns></returns>
        public List<Candidate> GetByCondition(int status, int delFlag)
        {
            MySqlConnection conn = null;
            try
            {
                string query = $"" +
                    $" select * ," +
                    $"jobTitleTable.propertyname as 'jobtitle'," +
                    $" jobPositionTable.propertyname as 'jobposition'," +
                    $" status.propertyname as 'CandidateStatus'," +
                    $" cancontactTable.propertyname as 'cancontact'" +
                    $" from candidate c , candidateproperty jobTitleTable ," +
                    $" candidateproperty jobPositionTable, candidateproperty status, candidateproperty cancontactTable" +
                    $" where c.jobtitleid = jobTitleTable.propertyid" +
                    $" and c.jobpositionid = jobPositionTable.propertyid" +
                    $" and c.candidatestatusid = status.propertyid " +
                    $" and c.cancontactid = cancontactTable.propertyid ";
                if (status != -1)
                {
                    query += $" and c.candidatestatusid = @status ";
                }
                if(delFlag != -1)
                {
                    query += $" and c.delFlag = @delFlag ";
                }
                query += ";";
                
                conn = _databaseConnector.getConnection();
                _databaseConnector.OpenConnection(conn);
                List<Candidate> list = new List<Candidate>();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@delFlag", delFlag);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Candidate()
                    {
                        CandidateID = reader["CandidateID"] is DBNull ? -1 : Convert.ToInt32(reader["CandidateID"]),
                        CandidateName = reader["CandidateName"] is DBNull ? "" : reader["CandidateName"].ToString(),
                        CandidateDateOfBirth = reader["CandidateDateOfBirth"] is DBNull ? new DateTime() : DateTime.Parse(reader["CandidateDateOfBirth"].ToString()),
                        CandidateAddress = reader["CandidateAddress"] is DBNull ? "" :  reader["CandidateAddress"].ToString(),
                        CandidateNumber = reader["CandidateNumber"] is DBNull ? "" :  reader["CandidateNumber"].ToString(),
                        CandidateCV = reader["CandidateCV"] is DBNull ? "" :  reader["CandidateCV"].ToString(),
                        CandidateEmail = reader["CandidateEmail"] is DBNull ? "" :  reader["CandidateEmail"].ToString(),
                        Resource = reader["Resource"] is DBNull ? "" : reader["Resource"].ToString(),
                        JobTitleID = reader["JobTitleID"] is DBNull ? -1 :  Convert.ToInt32(reader["JobTitleID"]),
                        JobPositionID = reader["JobPositionID"] is DBNull ? -1 :  Convert.ToInt32(reader["JobPositionID"]),
                        HasApplyBefore = reader["HasApplyBefore"] is DBNull ? -1 :  Convert.ToInt16(reader["HasApplyBefore"]),
                        JobTitle = reader["JobTitle"] is DBNull ? "" :  reader["Jobtitle"].ToString(),
                        JobPosition = reader["JobPosition"] is DBNull ? "" :  reader["Jobposition"].ToString(),
                        DelFlag = reader["DelFlag"] is DBNull ? -1 :  Convert.ToInt16(reader["DelFlag"]),
                        CandidateStatusID = reader["CandidateStatusID"] is DBNull ? -1 : Convert.ToInt16(reader["CandidateStatusID"]),
                        CandidateStatus = reader["CandidateStatus"] is DBNull ? "" : reader["CandidateStatus"].ToString(),
                        CanContactID = reader["CanContactID"] is DBNull ? -1 : Convert.ToInt16(reader["CancontactID"]),
                        CanContact = reader["CanContact"] is DBNull ? "" : reader["CanContact"].ToString(),
                        DenyReason = reader["DenyReason"] is DBNull ? "" : reader["DenyReason"].ToString(),
                        HasEmailFlag = reader["HasEmailFlag"] is DBNull ? -1 : Convert.ToInt16(reader["HasEmailFlag"].ToString())
                    }) ; 
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
        /// Get by candidate statuses
        /// </summary>
        /// <param name="statusIDs"></param>
        /// <returns></returns>
        public List<Candidate> GetByCandidateStatuses(List<int> statusIDs, bool isStatusInList)
        {
            MySqlConnection conn = null;
            try
            {
                conn = _databaseConnector.getConnection();
                _databaseConnector.OpenConnection(conn);
                List<Candidate> list = new List<Candidate>();
                string statusIdsAsString = "";
                for (int i = 0; i < statusIDs.Count; i++)
                {
                    statusIdsAsString += statusIDs[i];
                    if (i != statusIDs.Count - 1)
                    {
                        statusIdsAsString += ",";
                    }
                }
                string condition = "";
                if (isStatusInList)
                    condition = " in ";
                else
                    condition = " not in ";
                string command = $"select c.*,jobtitle.propertyname as 'jobtitle', jobposition.propertyname as 'jobposition', candidatestatus.propertyname as 'candidatestatus' " +
                    $"from candidate c," +
                    $" CandidateProperty jobTitle," +
                    $" CandidateProperty jobPosition," +
                    $" CandidateProperty candidatestatus" +
                    $" where candidatestatusid {condition} ({statusIdsAsString}) " +
                    $" and c.delflag = 1" +
                    $" and c.jobtitleid = jobtitle.propertyid" +
                    $" and c.jobpositionid = jobposition.propertyid" +
                    $" and c.candidateStatusid= candidatestatus.propertyid;";

                MySqlCommand cmd = new MySqlCommand(command, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Candidate()
                    {
                        CandidateID = reader["CandidateID"] is DBNull ? -1 : Convert.ToInt32(reader["CandidateID"]),
                        CandidateName = reader["CandidateName"] is DBNull ? "" :  reader["CandidateName"].ToString(),
                        CandidateDateOfBirth = reader["CandidateDateOfBirth"] is DBNull ? new DateTime() : DateTime.Parse(reader["CandidateDateOfBirth"].ToString()),
                        CandidateAddress = reader["CandidateAddress"] is DBNull ? "" : reader["CandidateAddress"].ToString(),
                        CandidateNumber = reader["CandidateNumber"] is DBNull ? "" : reader["CandidateNumber"].ToString(),
                        CandidateCV = reader["CandidateCV"] is DBNull ? "" : reader["CandidateCV"].ToString(),
                        CandidateEmail = reader["CandidateEmail"] is DBNull ? "" : reader["CandidateEmail"].ToString(),
                        Resource = reader["Resource"] is DBNull ? "" : reader["Resource"].ToString(),
                        JobTitleID = reader["JobTitleID"] is DBNull ? -1 : Convert.ToInt32(reader["JobTitleID"]),
                        JobTitle = reader["JobTitle"] is DBNull ? "" : reader["Jobtitle"].ToString(),
                        JobPosition= reader["JobPosition"] is DBNull ? "" : reader["JobPosition"].ToString(),
                        JobPositionID = reader["JobPositionID"] is DBNull ? -1 : Convert.ToInt32(reader["JobPositionID"]),
                        HasApplyBefore = reader["HasApplyBefore"] is DBNull ? -1 : Convert.ToInt16(reader["HasApplyBefore"]),
                        DelFlag = reader["DelFlag"] is DBNull ? -1 : Convert.ToInt16(reader["DelFlag"]),
                        CandidateStatusID = reader["CandidateStatusID"] is DBNull ? -1 : Convert.ToInt16(reader["CandidateStatusId"]),
                        CanContactID = reader["CanContactID"] is DBNull ? -1 : Convert.ToInt16(reader["CancontactID"]),
                        DenyReason = reader["DenyReason"] is DBNull ? "" : reader["DenyReason"].ToString(),
                        HasEmailFlag = reader["HasEmailFlag"] is DBNull ? -1 : Convert.ToInt16(reader["HasEmailFlag"].ToString()),
                        CandidateStatus = reader["CandidateStatus"] is DBNull ? "" : reader["CandidateStatus"].ToString()
                    });
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
        /// get by candidate id
        /// </summary>
        /// <param name="candidateid"></param>
        /// <returns></returns>
        public Candidate GetByID(int candidateid)
        {
            MySqlConnection conn = null;
            try
            {
                conn = _databaseConnector.getConnection();
                _databaseConnector.OpenConnection(conn);
                List<Candidate> list = new List<Candidate>();
                string command = $"select * from candidate where candidateid = @candidateID ;";
                MySqlCommand cmd = new MySqlCommand(command, conn);
                cmd.Parameters.AddWithValue("@candidateID", candidateid);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Candidate()
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
                        HasApplyBefore = reader["HasApplyBefore"] is DBNull ? -1 : Convert.ToInt16(reader["HasApplyBefore"]),
                        DenyReason = reader["DenyReason"] is DBNull ? "" : reader["DenyReason"].ToString(),
                        DelFlag = reader["DelFlag"] is DBNull ? -1 : Convert.ToInt16(reader["DelFlag"]),
                        HasEmailFlag = reader["HasEmailFlag"] is DBNull ? -1 : Convert.ToInt16(reader["HasEmailFlag"].ToString()),
                        CanContactID = reader["CanContactId"] is DBNull ? -1 : Convert.ToInt16(reader["CanContactID"].ToString()),
                        CandidateStatusID = reader["CandidateStatusID"] is DBNull ? -1 : reader["CandidateStatusID"] is DBNull ? -1 : Convert.ToInt16(reader["CandidateStatusID"])
                    });
                }
                _databaseConnector.CloseConnection(conn);
                if(list.Count == 1)
                {
                    return list.First();
                }
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
        /// get by candidate id
        /// </summary>
        /// <param name="candidateid"></param>
        /// <returns></returns>
        public string GetDenyReson(string email, string phone)
        {
            MySqlConnection conn = null;
            try
            {
                conn = _databaseConnector.getConnection();
                _databaseConnector.OpenConnection(conn);
                List<Candidate> list = new List<Candidate>();
                string command = $"select denyreason from candidate where candidateemail = @candidateEmail or candidatenumber = @phoneNumber";
                MySqlCommand cmd = new MySqlCommand(command, conn);
                cmd.Parameters.AddWithValue("@candidateEmail", email);
                cmd.Parameters.AddWithValue("@phoneNumber", phone);
                MySqlDataReader reader = cmd.ExecuteReader();
                string DenyReason = "";
                int i = 0;
                while (reader.Read())
                {
                    
                    i++;
                    if (string.IsNullOrEmpty(reader["denyreason"].ToString()))
                    {
                        DenyReason += reader["denyreason"].ToString();
                    }
                    else
                    {
                        DenyReason += $"Time {i + 1}:";
                        DenyReason += reader["denyreason"].ToString();
                        DenyReason += ",\n";
                    }
                }
                _databaseConnector.CloseConnection(conn);
                return DenyReason;
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
        /// get all candidates with it IDs
        /// </summary>
        /// <param name="candidateIDs"></param>
        /// <returns></returns>
        public List<Candidate> GetCandidatesByIDs(List<int> candidateIDs)
        {
            MySqlConnection conn = null;
            try
            {
                conn = _databaseConnector.getConnection();
                _databaseConnector.OpenConnection(conn);
                List<Candidate> list = new List<Candidate>();
                string statusListAsString = "";
                for (int i = 0; i < candidateIDs.Count; i++)
                {
                    statusListAsString += candidateIDs[i];
                    if (i != candidateIDs.Count - 1)
                    {
                        statusListAsString += ",";
                    }
                }
                string command = $"" +
                    $"select * ," +
                    $"jobTitleTable.propertyname as 'jobtitle'," +
                    $" jobPositionTable.propertyname as 'jobposition'," +
                    $" status.propertyname as 'CandidateStatus'," +
                    $" cancontactTable.propertyname as 'cancontact'" +
                    $" from candidate c , candidateproperty jobTitleTable ," +
                    $" candidateproperty jobPositionTable, candidateproperty status, candidateproperty cancontactTable" +
                    $" where c.jobtitleid = jobTitleTable.propertyid" +
                    $" and c.jobpositionid = jobPositionTable.propertyid" +
                    $" and c.candidatestatusid = status.propertyid " +
                    $" and c.cancontactid = cancontactTable.propertyid " +
                    $" and c.delFlag = 1" +
                    $" and c.candidateID  in ({statusListAsString});";

                MySqlCommand cmd = new MySqlCommand(command, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Candidate()
                    {
                        CandidateID = reader["CandidateID"] is DBNull ? -1 : Convert.ToInt32(reader["CandidateID"]),
                        CandidateName = reader["CandidateName"] is DBNull ? "" : reader["CandidateName"].ToString(),
                        CandidateDateOfBirth = reader["CandidateDateOfBirth"] is DBNull ? new DateTime() : DateTime.Parse(reader["CandidateDateOfBirth"].ToString()),
                        CandidateAddress = reader["CandidateAddress"] is DBNull ? "" : reader["CandidateAddress"].ToString(),
                        CandidateNumber = reader["CandidateNumber"] is DBNull ? "" : reader["CandidateNumber"].ToString(),
                        CandidateCV = reader["CandidateCV"] is DBNull ? "" : reader["CandidateCV"].ToString(),
                        CandidateEmail = reader["CandidateEmail"] is DBNull ? "" : reader["CandidateEmail"].ToString(),
                        Resource = reader["Resource"] is DBNull ? "" : reader["Resource"].ToString(),
                        JobTitleID = reader["JobTitleID"] is DBNull ? -1 : Convert.ToInt32(reader["JobTitleID"].ToString()),
                        JobTitle = reader["JobTitle"] is DBNull ? "" : reader["JobTitle"].ToString(),
                        JobPosition = reader["JobPosition"] is DBNull ? "" : reader["JobPosition"].ToString(),
                        JobPositionID = reader["JobPositionID"] is DBNull ? -1 : Convert.ToInt32(reader["JobPositionID"]),
                        HasApplyBefore = reader["HasApplyBefore"] is DBNull ? -1 : Convert.ToInt16(reader["HasApplyBefore"]),
                        DenyReason = reader["DenyReason"] is DBNull ? "" : reader["DenyReason"].ToString(),
                        DelFlag = reader["DelFlag"] is DBNull ? -1 : Convert.ToInt16(reader["DelFlag"]),
                        HasEmailFlag = reader["HasEmailFlag"] is DBNull ? -1 : Convert.ToInt16(reader["HasEmailFlag"].ToString()),
                        CanContactID = reader["CanContactId"] is DBNull ? -1 : Convert.ToInt16(reader["CanContactID"].ToString()),
                        CandidateStatusID = reader["CandidateStatusID"] is DBNull ? -1 : reader["CandidateStatusID"] is DBNull ? -1 : Convert.ToInt16(reader["CandidateStatusID"]),
                        CanContact   = reader["CanContact"] is DBNull ? "" : reader["CanContact"].ToString(),
                        CandidateStatus= reader["CandidateStatus"] is DBNull ? "" : reader["CandidateStatus"].ToString(),
                    });
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
        /// update by candidate id
        /// </summary>
        /// <returns></returns>
        public bool Update(Candidate candidate, int candidateid)
        {
            MySqlConnection conn = null;
            try
            {
                if (candidate.CandidateStatusID == (int)CandidateStatusCode.DenyCV)
                {
                    Candidate unchangedCandidate = GetByID(candidate.CandidateID);
                    for(int i = unchangedCandidate.CandidateStatusID; i > 0; i--)
                    {
                        if(i == (int)CandidateStatusCode.AcceptCV ||
                            i == (int)CandidateStatusCode.TestOK ||
                            i == (int)CandidateStatusCode.PassedInterviewRoundOne ||
                            i == (int)CandidateStatusCode.PassedInterviewRoundTwo ||
                            i == (int)CandidateStatusCode.AcceptOffer)
                        {
                            candidate.DenyReason = Enum.GetName(typeof(CandidateStatusCode),i);
                            break;
                        }
                    }
                }
                conn = _databaseConnector.getConnection();
                _databaseConnector.OpenConnection(conn);
                string command = $"update candidate " +
                    $"set candidatename = @candidateName ," +
                    $"candidateDateofbirth = @candidateDateofbirth," +
                    $"candidateAddress = @candidateAddress , "+
                    $"candidatenumber = @candidateNumber ," +
                    $"candidateemail = @candidateEmail ,"+
                    $"resource = @resource ," +
                    $"jobTitleID = @jobTitle , " +
                    $"jobPositionID = @jobPositionID ," +
                    $"hasApplyBefore = @hasApplyBefore ," +
                    $"delFlag = @delFlag ," +
                    $"candidateStatusiD = @candidateStatusID ," +
                    $"CanContactID = @cancontactID ," +
                    $"Denyreason = @DenyReason , " +
                    $"hasEmailFlag = @hasEmailFlag " +
                    $"where candidateid = @candidateID";
                MySqlCommand cmd = new MySqlCommand(command, conn);
                cmd.Parameters.AddWithValue("@candidateName",candidate.CandidateName);
                cmd.Parameters.AddWithValue("@candidateDateofbirth", candidate.CandidateDateOfBirth);
                cmd.Parameters.AddWithValue("@candidateAddress", candidate.CandidateAddress);
                cmd.Parameters.AddWithValue("@candidateNumber", candidate.CandidateNumber);
                cmd.Parameters.AddWithValue("@candidateEmail", candidate.CandidateEmail);
                cmd.Parameters.AddWithValue("@resource", candidate.Resource);
                cmd.Parameters.AddWithValue("@jobTitle", candidate.JobTitleID);
                cmd.Parameters.AddWithValue("@jobPositionID", candidate.JobPositionID);
                cmd.Parameters.AddWithValue("@hasApplyBefore", candidate.HasApplyBefore);
                cmd.Parameters.AddWithValue("@delFlag", candidate.DelFlag);
                cmd.Parameters.AddWithValue("@candidateStatusID", candidate.CandidateStatusID);
                cmd.Parameters.AddWithValue("@cancontactID", candidate.CanContactID);
                cmd.Parameters.AddWithValue("@DenyReason", candidate.DenyReason);
                cmd.Parameters.AddWithValue("@hasEmailFlag", candidate.HasEmailFlag);
                cmd.Parameters.AddWithValue("@candidateID", candidateid);
                int changes = cmd.ExecuteNonQuery();
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
        /// Insert a candidate
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Insert(Candidate value)
        {
            MySqlConnection conn = null;
            try
            {
                conn = _databaseConnector.getConnection();
                _databaseConnector.OpenConnection(conn);
                MySqlCommand find_cmd = new MySqlCommand($"select candidateid from candidate where candidateEmail = @candidateEmail or candidateNumber = @candidateNumber", conn);
                find_cmd.Parameters.AddWithValue("@candidateEmail", value.CandidateEmail);
                find_cmd.Parameters.AddWithValue("@candidateNumber", value.CandidateNumber);
                MySqlDataReader reader = find_cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    value.HasApplyBefore = 1;
                }
                else
                {
                    value.HasApplyBefore = 0;
                }
                _databaseConnector.CloseConnection(conn);
                _databaseConnector.OpenConnection(conn);
                MySqlCommand insert_cmd = new MySqlCommand($"insert into candidate values (@candidateID,@candidateName,@candidateDateOfBirth,@candidateAddress,@candidateNumber,@candidateCV,@candidateEmail,@resource,@jobTitle,@jobPosition,@hasApplyBefore,1,7,@cancontact,@denyReason,0)", conn);
                insert_cmd.Parameters.AddWithValue("@candidateID",value.CandidateID);
                insert_cmd.Parameters.AddWithValue("@candidateName", value.CandidateName);
                insert_cmd.Parameters.AddWithValue("@candidateDateOfBirth", value.CandidateDateOfBirth);
                insert_cmd.Parameters.AddWithValue("@candidateAddress", value.CandidateAddress);
                insert_cmd.Parameters.AddWithValue("@candidateNumber", value.CandidateNumber);
                insert_cmd.Parameters.AddWithValue("@candidateCV", value.CandidateCV);
                insert_cmd.Parameters.AddWithValue("@candidateEmail", value.CandidateEmail);
                insert_cmd.Parameters.AddWithValue("@resource", value.Resource);
                insert_cmd.Parameters.AddWithValue("@jobTitle", value.JobTitleID);
                insert_cmd.Parameters.AddWithValue("@jobPosition", value.JobPositionID);
                insert_cmd.Parameters.AddWithValue("@hasApplyBefore", value.HasApplyBefore);
                insert_cmd.Parameters.AddWithValue("@cancontact", value.CanContactID);
                insert_cmd.Parameters.AddWithValue("@denyReason", value.DenyReason);
                int isChanged = insert_cmd.ExecuteNonQuery();
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
        /// mass Change delFlag with a list of candidateid
        /// </summary>
        /// <param name="listOfCandidateID"></param>
        /// <param name="delFlag"></param>
        /// <returns></returns>
        public bool MassChange(List<int> listOfCandidateID, int delFlag,int status,int hasEmailFlag)
        {
            MySqlConnection conn = null;
            try
            {
                conn = _databaseConnector.getConnection();
                _databaseConnector.OpenConnection(conn);
                string assign = "";
                if(delFlag != -1)
                {
                    assign += $" delFlag = @delFlag ";
                }
                if(status != -1)
                {
                    assign += $" candidatestatusid = @status";
                }
                if(hasEmailFlag != -1)
                {
                    assign += $" hasEmailFlag = @hasEmailFlag";
                }

                string command = $"update candidate set {assign} where ";
                listOfCandidateID.ForEach((i) => command += $" candidateid = {i} or ");
                command += " 1=0 ;";
                MySqlCommand cmd = new MySqlCommand(command, conn);
                cmd.Parameters.AddWithValue("@delFlag",delFlag);
                cmd.Parameters.AddWithValue("@status",status);
                cmd.Parameters.AddWithValue("@hasEmailFlag",hasEmailFlag);
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
        /// mass increase by 1 CandidateStatusId with a list of candidateid
        /// </summary>
        /// <param name="listOfCandidateID"></param>
        /// <param name="delFlag"></param>
        /// <returns></returns>
        public bool MassIncreaseByOneCandidateStatus(List<int> candidateIDs)
        {
            MySqlConnection conn = null;
            try
            {
                conn = _databaseConnector.getConnection();
                _databaseConnector.OpenConnection(conn);
                string statusListAsString = "";
                for (int i = 0; i < candidateIDs.Count; i++)
                {
                    statusListAsString += candidateIDs[i];
                    if (i != candidateIDs.Count - 1)
                    {
                        statusListAsString += ",";
                    }
                }
                string command = $"update candidate set candidatestatusid = candidatestatusid + 1 where candidateid in ({statusListAsString});";
                MySqlCommand cmd = new MySqlCommand(command, conn);
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
        /// get list of candidate by the newest interviewMeeting 
        /// </summary>
        /// <param name="afterDay">get all candidate has interviewmeeting before afterDay </param>
        /// <returns></returns>
        public List<Tuple<Candidate,InterviewMeeting>> GetCandidatesByNewestInterviewMeeting(string afterDay, int interviewerid)
        {
            MySqlConnection conn = null;
            try
            {
                conn = _databaseConnector.getConnection();
                _databaseConnector.OpenConnection(conn);
                List<Tuple<Candidate,InterviewMeeting>> listOfCandidate = new List<Tuple<Candidate, InterviewMeeting>>();
                string command = $"select c.*," +
                    $"i.interviewmeetingid," +
                    $"i.ishaveassess," +
                    $"p.propertyname as 'jobtitle', pp.propertyname as 'jobposition'," +
                    $"ppp.propertyname as 'cancontact', " +
                    $"candidatestatustable.propertyname as 'CandidateStatus' " +
                    $"from interviewmeeting i, candidate c,candidateproperty p, " +
                    $"candidateproperty pp,candidateproperty ppp,candidateproperty candidatestatustable, " +
                    $"interviewmeetingdetail detail " +
                    $"where c.candidateid = i.candidateid " +
                    $"and detail.interviewmeetingid = i.interviewmeetingid " +
                    $"and c.jobtitleid= p.propertyid " +
                    $"and pp.propertyid = c.jobpositionid " +
                    $"and ppp.propertyid = c.cancontactid " +
                    $"and detail.interviewerid = @interviewerid " +
                    $"and c.candidatestatusid = candidatestatustable.propertyid " +
                    $"and i.interviewmeetingdate < @afterDay and c.delFlag = 1 and c.denyreason != 'none' and i.interviewmeetingid in " +
                    $"(select interviewmeetingid from interviewmeeting where interviewmeetingid in " +
                    $"(select max(i.interviewmeetingid) from interviewmeeting i, candidate c where c.candidateid = i.candidateid group by (c.candidateid) order by (interviewmeetingid) desc)); ";
                MySqlCommand cmd = new MySqlCommand(command, conn);
                cmd.Parameters.AddWithValue("@afterDay",afterDay);
                cmd.Parameters.AddWithValue("@interviewerid", interviewerid);
                //cmd.Parameters.AddWithValue("@test",CandidateStatusCode.InvitedTest);
                //cmd.Parameters.AddWithValue("@interviewone", CandidateStatusCode.InvitedInterviewRoundOne);
                //cmd.Parameters.AddWithValue("@interviewtwo", CandidateStatusCode.InvitedInterviewRoundTwo);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listOfCandidate.Add(
                        new Tuple<Candidate,InterviewMeeting>(
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
                        JobTitleID = reader["JobTitleID"] is DBNull ? -1 : Convert.ToInt32(reader["JobTitleID"].ToString()),
                        JobTitle = reader["JobTitle"] is DBNull ? "" : reader["JobTitle"].ToString(),
                        JobPosition = reader["JobPosition"] is DBNull ? "" : reader["JobPosition"].ToString(),
                        JobPositionID = reader["JobPositionID"] is DBNull ? -1 : Convert.ToInt32(reader["JobPositionID"]),
                        HasApplyBefore = reader["HasApplyBefore"] is DBNull ? -1 : Convert.ToInt16(reader["HasApplyBefore"]),
                        DenyReason = reader["DenyReason"] is DBNull ? "" : reader["DenyReason"].ToString(),
                        DelFlag = reader["DelFlag"] is DBNull ? -1 : Convert.ToInt16(reader["DelFlag"]),
                        HasEmailFlag = reader["HasEmailFlag"] is DBNull ? -1 : Convert.ToInt16(reader["HasEmailFlag"].ToString()),
                        CanContactID = reader["CanContactId"] is DBNull ? -1 : Convert.ToInt16(reader["CanContactID"].ToString()),
                        CandidateStatusID = reader["CandidateStatusID"] is DBNull ? -1 : reader["CandidateStatusID"] is DBNull ? -1 : Convert.ToInt16(reader["CandidateStatusID"]),
                        CanContact = reader["CanContact"] is DBNull ? "" : reader["CanContact"].ToString(),
                        CandidateStatus = reader["CandidateStatus"] is DBNull ? "" : reader["CandidateStatus"].ToString()

                    },new InterviewMeeting() 
                    { isHaveAssess = reader["isHaveAssess"] is DBNull ? -1 : Convert.ToInt16(reader["isHaveAssess"].ToString()) ,
                        InterviewMeetingID = reader["InterviewMeetingID"] is DBNull ? -1 : Convert.ToInt16(reader["InterviewMeetingID"].ToString())
                    }));
                }
                _databaseConnector.CloseConnection(conn);
                return listOfCandidate;
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

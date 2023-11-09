using Microsoft.Extensions.Configuration;
using MySqlConnector;


namespace CandidateSystem.DataLayer
{
    public class DatabaseConnector : IDatabaseConnector
    {
        #region Properties
        /// <summary>
        /// configuration to get connection string
        /// </summary>
        private IConfiguration _configuration;
        #endregion

        #region Constructor
        public DatabaseConnector(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Close a connection
        /// </summary>
        /// <param name="conn"></param>
        public void CloseConnection(MySqlConnection conn)
        {
            conn.Close();
        }

        /// <summary>
        /// get a connection
        /// </summary>
        /// <param name="conn"></param>
        public MySqlConnection getConnection()
        {
            return new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        /// <summary>
        /// Open an connection
        /// </summary>
        /// <param name="conn"></param>
        public void OpenConnection(MySqlConnection conn)
        {
            conn.Open();
        }
        #endregion
    }
}

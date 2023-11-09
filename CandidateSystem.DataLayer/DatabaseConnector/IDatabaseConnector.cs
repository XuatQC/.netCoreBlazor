using MySqlConnector;

namespace CandidateSystem.DataLayer
{

    public interface IDatabaseConnector
    {
        public MySqlConnection getConnection();
        public void OpenConnection(MySqlConnection conn);
        public void CloseConnection(MySqlConnection conn);


    }
}

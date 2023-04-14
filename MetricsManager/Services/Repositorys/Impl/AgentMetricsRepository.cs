using Dapper;
using MetricsManager.Models;
using Microsoft.Extensions.Options;
using System.Data.SQLite;

namespace MetricsManager.Services.Repositorys.Impl
{
    public class AgentMetricsRepository : IAgentMetricsManagerRepository
    {
        private readonly IOptions<DatabaseOptions> _databaseOptions;

        public AgentMetricsRepository(IOptions<DatabaseOptions> databaseOptions)
        {
            _databaseOptions = databaseOptions;
        }

        public void Create(string newAgent)
        {
            string str = newAgent.ToString();
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            connection.Open();
            connection.Execute("INSERT INTO AgentInfo(AgentAddress) VALUES(@AgentAddress)", new
            {
                AgentAddress = str
            });
        }

        public IList<AgentInfo> GetAll()
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            return connection.Query<AgentInfo>("SELECT * FROM AgentInfo").ToList();
        }

        public AgentInfo GetById(int id)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            AgentInfo metric = connection.QuerySingle<AgentInfo>("SELECT * FROM AgentInfo WHERE AgentId = @AgentId",
            new { AgentId = id });
            return metric;
        }
    }
}

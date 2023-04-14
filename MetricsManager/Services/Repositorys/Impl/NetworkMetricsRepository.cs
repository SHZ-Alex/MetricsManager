using Dapper;
using MetricsManager.Models;
using Microsoft.Extensions.Options;
using System.Data.SQLite;

namespace MetricsManager.Services.Repositorys.Impl
{
    public class NetworkMetricsRepository : INetworkMetricsManagerRepository
    {
        private readonly IOptions<DatabaseOptions> _databaseOptions;

        public NetworkMetricsRepository(IOptions<DatabaseOptions> databaseOptions)
        {
            _databaseOptions = databaseOptions;
        }

        public IList<NetworkMetric> GetAll()
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            return connection.Query<NetworkMetric>("SELECT * FROM NetworkMetric").ToList();
        }

        public NetworkMetric GetById(int id)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            NetworkMetric metric = connection.QuerySingle<NetworkMetric>("SELECT * FROM NetworkMetric WHERE id = @id",
            new { id = id });
            return metric;
        }

        public IList<NetworkMetric> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            List<NetworkMetric> metrics = connection.Query<NetworkMetric>("SELECT * FROM NetworkMetric where time >= @timeFrom and time <= @timeTo",
                new { timeFrom = timeFrom.TotalSeconds, timeTo = timeTo.TotalSeconds }).ToList();
            return metrics;
        }
    }
}

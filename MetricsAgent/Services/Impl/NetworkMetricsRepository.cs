using Dapper;
using MetricsAgent.Models;
using Microsoft.Extensions.Options;
using System.Data.SQLite;

namespace MetricsAgent.Services.Impl
{
    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        private readonly IOptions<DatabaseOptions> _databaseOptions;

        public NetworkMetricsRepository(IOptions<DatabaseOptions> databaseOptions)
        {
            _databaseOptions = databaseOptions;
        }

        public void Create(NetworkMetric item)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            connection.Open();
            connection.Execute("INSERT INTO networkmetrics(value, time) VALUES(@value, @time)", new
            {
                value = item.Value,
                time = item.Time
            });
        }

        public void Delete(int id)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            connection.Open();
            connection.Execute("DELETE FROM networkmetrics WHERE id=@id",
            new
            {
                id = id
            });
        }

        public void Update(NetworkMetric item)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            connection.Execute("UPDATE networkmetrics SET value = @value, time = @time WHERE id = @id",
            new
            {
                value = item.Value,
                time = item.Time,
                id = item.Id
            });
        }

        public IList<NetworkMetric> GetAll()
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            return connection.Query<NetworkMetric>("SELECT Id, Time, Value FROM networkmetrics").ToList();
        }

        public NetworkMetric GetById(int id)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            NetworkMetric metric = connection.QuerySingle<NetworkMetric>("SELECT Id, Time, Value FROM networkmetrics WHERE id = @id",
            new { id = id });
            return metric;
        }

        public IList<NetworkMetric> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            List<NetworkMetric> metrics = connection.Query<NetworkMetric>("SELECT * FROM networkmetrics where time >= @timeFrom and time <= @timeTo",
                new { timeFrom = timeFrom.TotalSeconds, timeTo = timeTo.TotalSeconds }).ToList();
            return metrics;
        }
    }
}

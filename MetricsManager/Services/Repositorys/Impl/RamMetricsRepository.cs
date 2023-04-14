using Dapper;
using MetricsManager.Models;
using Microsoft.Extensions.Options;
using System.Data.SQLite;

namespace MetricsManager.Services.Repositorys.Impl
{
    public class RamMetricsRepository : IRamMetricsManagerRepository
    {
        private readonly IOptions<DatabaseOptions> _databaseOptions;

        public RamMetricsRepository(IOptions<DatabaseOptions> databaseOptions)
        {
            _databaseOptions = databaseOptions;
        }

        public IList<RamMetric> GetAll()
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            return connection.Query<RamMetric>("SELECT * FROM RamMetric").ToList();
        }

        public RamMetric GetById(int id)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            RamMetric metric = connection.QuerySingle<RamMetric>("SELECT * FROM RamMetric WHERE id = @id",
            new { id = id });
            return metric;
        }

        public IList<RamMetric> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            List<RamMetric> metrics = connection.Query<RamMetric>("SELECT * FROM RamMetric where time >= @timeFrom and time <= @timeTo",
                new { timeFrom = timeFrom.TotalSeconds, timeTo = timeTo.TotalSeconds }).ToList();
            return metrics;
        }
    }
}

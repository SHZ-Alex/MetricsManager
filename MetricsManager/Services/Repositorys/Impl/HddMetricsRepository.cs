using Dapper;
using MetricsManager.Models;
using Microsoft.Extensions.Options;
using System.Data.SQLite;

namespace MetricsManager.Services.Repositorys.Impl
{
    public class HddMetricsRepository : IHddMetricsManagerRepository
    {
        private readonly IOptions<DatabaseOptions> _databaseOptions;

        public HddMetricsRepository(IOptions<DatabaseOptions> databaseOptions)
        {
            _databaseOptions = databaseOptions;
        }

        public IList<HddMetric> GetAll()
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            return connection.Query<HddMetric>("SELECT * FROM HddMetric").ToList();
        }

        public HddMetric GetById(int id)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            HddMetric metric = connection.QuerySingle<HddMetric>("SELECT * FROM HddMetric WHERE id = @id",
            new { id = id });
            return metric;
        }

        public IList<HddMetric> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            List<HddMetric> metrics = connection.Query<HddMetric>("SELECT * FROM HddMetric where time >= @timeFrom and time <= @timeTo",
                new { timeFrom = timeFrom.TotalSeconds, timeTo = timeTo.TotalSeconds }).ToList();
            return metrics;
        }
    }
}

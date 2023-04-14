using Dapper;
using MetricsManager.Models;
using Microsoft.Extensions.Options;
using System.Data.SQLite;

namespace MetricsManager.Services.Repositorys.Impl
{
    public class CpuMetricsRepository : ICpuMetricsManagerRepository
    {
        private readonly IOptions<DatabaseOptions> _databaseOptions;

        public CpuMetricsRepository(IOptions<DatabaseOptions> databaseOptions)
        {
            _databaseOptions = databaseOptions;
        }

        public IList<CpuMetric> GetAll()
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            return connection.Query<CpuMetric>("SELECT * FROM CpuMetric").ToList();
        }

        public CpuMetric GetById(int id)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            CpuMetric metric = connection.QuerySingle<CpuMetric>("SELECT * FROM CpuMetric WHERE id = @id",
            new { id = id });
            return metric;
        }

        public IList<CpuMetric> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            List<CpuMetric> metrics = connection.Query<CpuMetric>("SELECT * FROM CpuMetric where time >= @timeFrom and time <= @timeTo",
                new { timeFrom = timeFrom.TotalSeconds, timeTo = timeTo.TotalSeconds }).ToList();
            return metrics;
        }
    }
}

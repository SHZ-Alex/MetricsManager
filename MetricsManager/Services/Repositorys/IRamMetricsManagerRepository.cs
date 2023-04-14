using MetricsManager.Models;

namespace MetricsManager.Services.Repositorys
{
    public interface IRamMetricsManagerRepository : IManagerRepository<RamMetric>, ITimeEntity<RamMetric>
    {
    }
}

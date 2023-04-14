using MetricsManager.Models;

namespace MetricsManager.Services.Repositorys
{
    public interface IHddMetricsManagerRepository : IManagerRepository<HddMetric>, ITimeEntity<HddMetric>
    {
    }
}

using MetricsManager.Models;

namespace MetricsManager.Services.Repositorys
{
    public interface ICpuMetricsManagerRepository : IManagerRepository<CpuMetric>, ITimeEntity<CpuMetric>
    {
    }
}

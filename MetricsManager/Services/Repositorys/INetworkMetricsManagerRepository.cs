using MetricsManager.Models;

namespace MetricsManager.Services.Repositorys
{
    public interface INetworkMetricsManagerRepository : IManagerRepository<NetworkMetric>, ITimeEntity<NetworkMetric>
    {
    }
}

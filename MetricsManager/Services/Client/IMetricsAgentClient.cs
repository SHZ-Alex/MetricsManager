using MetricsManager.Models.Requests;
using MetricsManager.Models.Requests.Impl.Response;
using MetricsManager.Models.Requests;
using MetricsManager.Models.Requests.Impl.Request;

namespace MetricsManager.Services.Client
{
    public interface IMetricsAgentClient
    {
        public CpuMetricsResponse GetCpuMetrics(CpuMetricsRequest request);
        public HddMetricsResponse GetHddMetrics(HddMetricsRequest request);
        public RamMetricsResponse GetRamMetrics(RamMetricsRequest request);
        public NetworkMetricsResponse GetNetworkMetrics(NetworkMetricsRequest request);

    }
}

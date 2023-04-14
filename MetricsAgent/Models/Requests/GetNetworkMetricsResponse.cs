using MetricsAgent.Models.Dto;

namespace MetricsAgent.Models.Requests
{
    public class GetNetworkMetricsResponse
    {
        public List<NetworkMetricDto> Metrics { get; set; }
    }
}

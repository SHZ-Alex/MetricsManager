using MetricsAgent.Models.Dto;

namespace MetricsAgent.Models.Requests
{
    public class GetCpuMetricsResponse
    {
        public List<CpuMetricDto> Metrics { get; set; }
    }
}

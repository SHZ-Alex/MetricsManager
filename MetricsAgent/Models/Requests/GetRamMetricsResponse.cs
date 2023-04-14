using MetricsAgent.Models.Dto;

namespace MetricsAgent.Models.Requests
{
    public class GetRamMetricsResponse
    {
        public List<RamMetricDto> Metrics { get; set; }
    }
}

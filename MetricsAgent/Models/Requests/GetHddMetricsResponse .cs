using MetricsAgent.Models.Dto;

namespace MetricsAgent.Models.Requests
{
    public class GetHddMetricsResponse
    {
        public List<HddMetricDto> Metrics { get; set; }
    }
}

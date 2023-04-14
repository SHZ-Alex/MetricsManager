using System.Text.Json.Serialization;

namespace MetricsManager.Models.Requests.Impl.Response
{
    public class CpuMetricsResponse
    {
        public int AgentId { get; set; }


        [JsonPropertyName("metrics")]
        public CpuMetric[] Metrics { get; set; }
    }
}

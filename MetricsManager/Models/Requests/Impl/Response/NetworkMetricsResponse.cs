using System.Text.Json.Serialization;

namespace MetricsManager.Models.Requests.Impl.Response
{
    public class NetworkMetricsResponse
    {
        public int AgentId { get; set; }


        [JsonPropertyName("metrics")]
        public NetworkMetric[] Metrics { get; set; }
    }
}

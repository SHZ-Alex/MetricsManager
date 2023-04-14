using System.Text.Json.Serialization;

namespace MetricsManager.Models
{
    public class NetworkMetric
    {
        [JsonPropertyName("time")]
        public long Time { get; set; }

        [JsonPropertyName("value")]
        public int Value { get; set; }
    }
}

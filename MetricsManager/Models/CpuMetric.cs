using System.Text.Json.Serialization;

namespace MetricsManager.Models
{
    public class CpuMetric
    {
        [JsonPropertyName("value")]
        public int Value { get; set; }

        [JsonPropertyName("time")]
        public long Time { get; set; }
    }
}

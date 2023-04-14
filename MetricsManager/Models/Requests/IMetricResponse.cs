using System.Text.Json.Serialization;

namespace MetricsManager.Models.Requests
{
    public interface IMetricResponse<T> where T : class
    {
        public int AgentId { get; set; }

        public T[] Metrics { get; set; }
    }
}

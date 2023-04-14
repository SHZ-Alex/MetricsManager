namespace MetricsManager.Models.Requests.Impl.Request
{
    public class CpuMetricsRequest
    {
        public int AgentId { get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
    }
}

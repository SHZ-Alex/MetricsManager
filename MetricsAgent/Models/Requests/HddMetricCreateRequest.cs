namespace MetricsAgent.Models.Requests
{
    public class HddMetricCreateRequest
    {
        public int Value { get; set; }

        public TimeSpan Time { get; set; }
    }
}

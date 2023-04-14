namespace MetricsAgent.Models.Requests
{
    public class RamMetricCreateRequest
    {
        public int Value { get; set; }

        public TimeSpan Time { get; set; }
    }
}

using MetricsAgent.Models;
using MetricsAgent.Services;
using Quartz;
using System.Diagnostics;

namespace MetricsAgent.Jobs.MetricsJobs
{
    public class NetworkMetricJob : IJob
    {
        private PerformanceCounter _lanCounter;
        private IServiceScopeFactory _serviceScopeFactory;

        public NetworkMetricJob(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;

            PerformanceCounterCategory performanceCounterCategory = new PerformanceCounterCategory("Network Interface");
            string instance = performanceCounterCategory.GetInstanceNames()[0];
            _lanCounter = new PerformanceCounter("Network Interface", "Bytes Sent/sec", instance);
        }

        public Task Execute(IJobExecutionContext context)
        {
            using (IServiceScope serviceScope = _serviceScopeFactory.CreateScope())
            {
                var lanMetricRepository = serviceScope.ServiceProvider.GetService<INetworkMetricsRepository>();

                try
                {
                    var lanBytesSent = _lanCounter.NextValue();
                    var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                    Debug.WriteLine($"{time} => {lanBytesSent}");

                    lanMetricRepository.Create(new NetworkMetric
                    {
                        Value = (int)lanBytesSent,
                        Time = (long)time.TotalSeconds
                    });

                }

                catch (Exception e)
                {

                }

                return Task.CompletedTask;
            }
        }
    }
}

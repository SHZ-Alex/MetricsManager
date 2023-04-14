using MetricsAgent.Models;
using MetricsAgent.Services;
using Quartz;
using System.Diagnostics;

namespace MetricsAgent.Jobs.MetricsJobs
{
    public class RamMetricJob : IJob
    {
        private PerformanceCounter _ramCounter;
        private IServiceScopeFactory _serviceScopeFactory;

        public RamMetricJob(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _ramCounter = new PerformanceCounter("Memory", "Available MBytes", null);
        }

        public Task Execute(IJobExecutionContext context)
        {
            using (IServiceScope serviceScope = _serviceScopeFactory.CreateScope())
            {
                var ramMetricRepository = serviceScope.ServiceProvider.GetService<IRamMetricsRepository>();

                try
                {
                    var freeSpace = _ramCounter.NextValue();
                    var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                    Debug.WriteLine($"{time} => {freeSpace}");

                    ramMetricRepository.Create(new RamMetric
                    {
                        Value = (int)freeSpace,
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

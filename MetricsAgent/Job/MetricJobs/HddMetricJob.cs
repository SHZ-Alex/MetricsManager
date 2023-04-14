using MetricsAgent.Models;
using MetricsAgent.Services;
using Quartz;
using System.Diagnostics;

namespace MetricsAgent.Jobs.MetricsJobs
{
    public class HddMetricJob : IJob
    {
        private PerformanceCounter _hddCounter;
        private IServiceScopeFactory _serviceScopeFactory;

        public HddMetricJob(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _hddCounter = new PerformanceCounter("PhysicalDisk", "Disk Write Bytes/sec", "_Total");

        }

        public Task Execute(IJobExecutionContext context)
        {
            using (IServiceScope serviceScope = _serviceScopeFactory.CreateScope())
            {
                var hddMetricRepository = serviceScope.ServiceProvider.GetService<IHddMetricsRepository>();

                try
                {
                    var avgWriteSpeed = _hddCounter.NextValue();
                    var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                    Debug.WriteLine($"{time} => {avgWriteSpeed}");

                    hddMetricRepository.Create(new HddMetric
                    {
                        Value = (int)avgWriteSpeed,
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

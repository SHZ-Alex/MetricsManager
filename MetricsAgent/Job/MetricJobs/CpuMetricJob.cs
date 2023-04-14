using MetricsAgent.Services;
using MetricsAgent.Services.Impl;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System.Diagnostics;

namespace MetricsAgent.Job.MetricJobs
{
    public class CpuMetricJob : IJob
    {
        private PerformanceCounter _cpuCounter;
        private IServiceScopeFactory _serviceScopeFactory;

        public CpuMetricJob(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        }


        public Task Execute(IJobExecutionContext context)
        {

            using (IServiceScope serviceScope = _serviceScopeFactory.CreateScope())
            {
                var cpuMetricsRepository = serviceScope.ServiceProvider.GetService<ICpuMetricsRepository>();
                try
                {
                    var cpuUsageInPercents = _cpuCounter.NextValue();
                    var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                    Debug.WriteLine($"{time} > {cpuUsageInPercents}");
                    cpuMetricsRepository.Create(new Models.CpuMetric
                    {
                        Value = (int)cpuUsageInPercents,
                        Time = (long)time.TotalSeconds
                    });
                }
                catch (Exception ex)
                {

                }
            }


            return Task.CompletedTask;
        }
    }
}

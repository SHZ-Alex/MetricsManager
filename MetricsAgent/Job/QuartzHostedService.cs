using MetricsAgent.Services;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MetricsAgent.Job
{
    /// <summary>
    /// Сервис, использующий механизм планировщика, для запуска задач
    /// </summary>
    public class QuartzHostedService : IHostedService
    {

        #region Services

        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;
        private readonly IEnumerable<JobSchedule> _jobSchedules;

        #endregion

        #region Constructors

        public QuartzHostedService(
            ISchedulerFactory schedulerFactory,
            IJobFactory jobFactory,
            IEnumerable<JobSchedule> jobSchedules)
        {
            _schedulerFactory = schedulerFactory;
            _jobSchedules = jobSchedules;
            _jobFactory = jobFactory;
        }

        #endregion

        public IScheduler Scheduler { get; set; }

        #region IHostedService Implementation
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            Scheduler.JobFactory = _jobFactory;
            foreach (var jobSchedule in _jobSchedules)
            {
                var job = CreateJobDetail(jobSchedule);
                var trigger = CreateTrigger(jobSchedule);
                await Scheduler.ScheduleJob(job, trigger, cancellationToken);
            }
            await Scheduler.Start(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Scheduler?.Shutdown(cancellationToken);
        }

        #endregion

        private static IJobDetail CreateJobDetail(JobSchedule schedule)
        {
            var jobType = schedule.JobType;
            return JobBuilder
                .Create(jobType)
                .WithIdentity(jobType.FullName)
                .WithDescription(jobType.Name)
                .Build();
        }
        private static ITrigger CreateTrigger(JobSchedule schedule)
        {
            return TriggerBuilder
                .Create()
                .WithIdentity($"{schedule.JobType.FullName}.trigger")
                .WithCronSchedule(schedule.CronExpression)
                .WithDescription(schedule.CronExpression)
                .Build();
        }
    }
}

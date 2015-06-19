using System;
using System.Configuration;
using System.Net;
using Quartz;
using Quartz.Impl;

namespace TwitterBotPing
{
    class Program
    {
        // Entirely based on this tutorial:
        // http://tech.pro/tutorial/1222/put-the-cloud-to-work-pt-1-create-a-background-worker-using-quartznet

        private static readonly IScheduler Scheduler;

        static Program()
        {
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            Scheduler = schedulerFactory.GetScheduler();
        }

        private static IJobDetail _pingUrlJobDetail;

        static void Main()
        {
            Scheduler.Start();

            CreateJob();

            ScheduleJob();
        }

        private static void ScheduleJob()
        {
            int hour;
            int minutes;

            var appHours = ConfigurationManager.AppSettings["HourTimeToPing"];
            var appMinutes = ConfigurationManager.AppSettings["MinuteTimeToPing"];

            int.TryParse(appHours, out hour);
            int.TryParse(appMinutes, out minutes);

            var trigger = TriggerBuilder.Create()
                .WithDescription("Ping URL every day at half past seven")
                .WithDailyTimeIntervalSchedule(x => x
                    .WithIntervalInHours(24)
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(hour, minutes))
                    .InTimeZone(TimeZoneInfo.Utc))
                .Build();

            Scheduler.ScheduleJob(_pingUrlJobDetail, trigger);
        }

        public class PingUrlJob : IJob
        {
            public void Execute(IJobExecutionContext context)
            {
                var url = ConfigurationManager.AppSettings["URLToPing"];

                var request = WebRequest.Create(url);

                request.GetResponse();
            }
        }

        private static void CreateJob()
        {
            _pingUrlJobDetail = JobBuilder.Create<PingUrlJob>()
                .WithIdentity("Ping URL")
                .Build();
        }
    }
}
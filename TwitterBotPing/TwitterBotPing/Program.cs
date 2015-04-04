using System;
using System.Net;
using Quartz;
using Quartz.Impl;

namespace TwitterBotPing
{
    class Program
    {
        // http://tech.pro/tutorial/1222/put-the-cloud-to-work-pt-1-create-a-background-worker-using-quartznet

        private static readonly ISchedulerFactory SchedulerFactory;
        private static readonly IScheduler Scheduler;

        static Program()
        {
            // Create a regular old Quartz scheduler
            SchedulerFactory = new StdSchedulerFactory();
            Scheduler = SchedulerFactory.GetScheduler();
        }

        private static IJobDetail _emailJobDetail;

        static void Main()
        {

            // Now let's start our scheduler; you could perform
            // any processing or bootstrapping code here before
            // you start it but it must be started to schedule
            // any jobs
            Scheduler.Start();

            // Let's generate our email job detail now
            CreateJob();

            // And finally, schedule the job
            ScheduleJob();
        }

        private static void ScheduleJob()
        {

            // Let's create a trigger that fires immediately
            ITrigger trigger = TriggerBuilder.Create()

                // A description helps other people understand what you want
                .WithDescription("Every day at 3AM CST")

                // A daily time schedule gives you a
                // DailyTimeIntervalScheduleBuilder which provides
                // a fluent interface to build a schedule
                .WithDailyTimeIntervalSchedule(x => x

                    // Here we specify the interval
                    .WithIntervalInHours(24)

                    // And how often to repeat it
                    .OnEveryDay()

                    // Specify the time of day the trigger fires, in UTC (9am),
                    // since CST is UTC-0600
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(10, 35))

                    // Specify the timezone
                    //
                    // I like to use UTC dates in my applications to make sure
                    // I stay consistent, especially when you never know what
                    // server you're on!
                    .InTimeZone(TimeZoneInfo.Local))

                // Finally, we take the schedule and build a trigger
                .Build();

            // Ask the scheduler to schedule our EmailJob
            Scheduler.ScheduleJob(_emailJobDetail, trigger);
        }

        public class PingUrlJob : IJob
        {
            public void Execute(IJobExecutionContext context)
            {
                // visit page
                const string url = "http://lentbot.apphb.com/";

                var request = WebRequest.Create(url);

                request.GetResponse();
            }
        }

        private static void CreateJob()
        {
            // The job builder uses a fluent interface to
            // make it easier to build and generate an
            // IJobDetail object
            _emailJobDetail = JobBuilder.Create<PingUrlJob>()
                .WithIdentity("Ping URL")   // Here we can assign a friendly name to our job        
                .Build();                       // And now we build the job detail
        }
    }
}
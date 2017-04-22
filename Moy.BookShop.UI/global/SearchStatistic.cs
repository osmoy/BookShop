using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moy.BookShop.UI.global
{
    public class SearchStatistic
    {
        public static void BeginJob()
        {
            // Grab the Scheduler instance from the Factory 
            IScheduler sched = StdSchedulerFactory.GetDefaultScheduler();
            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<IndexJob>()
                .WithIdentity("job1", "group1").Build();
            // Trigger the job to run now, and then repeat every 10 seconds
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1").StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(10)
                    .RepeatForever()).Build();
            // Tell quartz to schedule the job using our trigger
            sched.ScheduleJob(job, trigger);
            // and start it off
            sched.Start();
            // some sleep to show what's happening.
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(60));
            // and last shut down the scheduler when you are ready to close your program.
            //sched.Shutdown();
        }
    }
}
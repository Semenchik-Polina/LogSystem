using Quartz;
using Quartz.Impl;

namespace LogSystem.BLL.Utils.MailHelpers
{
    public class MailScheduler
    {
        public static async void Start()
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<MailSender>().Build();

            // job is made 00:00:01 every day 
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("uaReportTrigger", "uaReportGroup")
                .StartAt(DateBuilder.TomorrowAt(0, 0, 1))                            
                .WithSimpleSchedule(x => x            
                    .WithIntervalInHours(24)          
                    .RepeatForever())                  
                .Build();                               

            await scheduler.ScheduleJob(job, trigger);        
        }
    }
}

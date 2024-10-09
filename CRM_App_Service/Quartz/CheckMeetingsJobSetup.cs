using Microsoft.Extensions.Options;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_App_Service.Quartz
{
    public class CheckMeetingsJobSetup : IConfigureOptions<QuartzOptions>
    {
        public void Configure(QuartzOptions options)
        {
            JobKey jobKey = JobKey.Create(nameof(CheckMeetingsJob));

            options
                .AddJob<CheckMeetingsJob>(jobBuilder => jobBuilder.WithIdentity(jobKey))
                    .AddTrigger(options =>
                    {
                        options
                        .ForJob(jobKey)
                        .WithSimpleSchedule(schedule => schedule.WithIntervalInHours(24)
                        .RepeatForever());
                    });

        }
    }
}

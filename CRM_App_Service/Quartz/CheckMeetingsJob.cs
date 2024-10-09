using CRM_App_Core.DTOs;
using CRM_App_Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace CRM_App_Service.Quartz
{
    public class CheckMeetingsJob : IJob
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;


        public CheckMeetingsJob(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var meetingService = scope.ServiceProvider.GetRequiredService<IMeetingService>();
                    var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();
                    var upcomingMeetings = await meetingService.GetUpcomingMeetingsAsync();


                    if (upcomingMeetings == null || !upcomingMeetings.Any())
                    {
                        Console.WriteLine("There are no upcoming meetings.");
                    }
                    else
                    {
                        //var todaysMeetings = upcomingMeetings.Where(x => x.MeetingDate.Date == DateTime.UtcNow.Date).ToList();
                        var futureMeetings = await meetingService.GetUpcomingMeetingsAsync();

                        TimeZoneInfo turkeyTimeZone = TimeZoneInfo.FindSystemTimeZoneById("GTB Standard Time");
                        DateTime utcNow = DateTime.UtcNow;
                        DateTime gmt3Time = TimeZoneInfo.ConvertTimeFromUtc(utcNow, turkeyTimeZone);
                        Console.WriteLine("*********************");
                        Console.WriteLine(gmt3Time);
                        Console.WriteLine(utcNow);
                        Console.WriteLine("*********************");
                        var todaysMeetings = upcomingMeetings
    .Where(x => x.MeetingDate.Date == gmt3Time.Date && x.MeetingDate >= gmt3Time)
    .ToList();
                        if (!todaysMeetings.Any())
                        {
                            Console.WriteLine("There are no meetings scheduled for today.");
                        }
                        else
                        {
                            foreach (var meeting in todaysMeetings)
                            {
                                string formattedDate = meeting.MeetingDate.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz");
                                Console.WriteLine($"Formatted Meeting Date: {formattedDate}");
                                // Mesaj gönderme işlemi
                                var token = "f3V9HUACRkqxtHb1qPFV-U:APA91bHZzpj0mwxULDLKLUKq5TAG_q8qTxqSKNA-tj8P6EetEWEr3MlX4iRf57xYOmT1WDwZUD-fFTM36__Luu63jJn81dM5uBwZ2neTefEFlnem7PlssvxkhVtR5fOH45L4kiU9VpPw"; //"cLt-4hoFRheQuwu5MR0zFt:APA91bFsjxilHyj19zE80RFcCzvuVGkaK23bA5JZXy-9onD_kFQ3VpPN4Qq6ejrhrYY8ndS_-UWpdhL20HHhx4bC6krvjYlNtU_YNqkey7XBsxneMBvSjEUzVcCt7q-gHAJx8EXuCytc"; // Bu token'ı gerçek cihaz token'ı ile değiştirin
                                var title = $"Meeting Reminder: {meeting.Title}";
                                var body = $"You have a meeting with client ID: {meeting.ClientId} today.";

                                await notificationService.SendNotification(title, body);
                                Console.WriteLine("==========================================================");
                                Console.WriteLine($"{meeting.ClientId}id'li kullanici ile {meeting.Id} id'li {title} konulu gorusme.");
                                Console.WriteLine("==========================================================");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }

}

using CRM_App_Core.Services;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_App_Service.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IMeetingService _meetingService;

        public NotificationService(IMeetingService meetingService)
        {
            _meetingService = meetingService;
            //FirebaseApp.Create(new AppOptions()
            //{
            //    Credential = GoogleCredential.FromFile("C:\\Users\\betul\\Desktop\\CRM_App\\CRM_App\\CRM_App\\myapplication-53592-firebase-adminsdk-wwpqf-2df7797369.json")
            //});
        }

        public async Task SendNotification(string title, string body)
        {
            var message = new Message()
            {
                Notification = new Notification
                {
                    Title = title,
                    Body = body
                },
                Token = "f3V9HUACRkqxtHb1qPFV-U:APA91bHZzpj0mwxULDLKLUKq5TAG_q8qTxqSKNA-tj8P6EetEWEr3MlX4iRf57xYOmT1WDwZUD-fFTM36__Luu63jJn81dM5uBwZ2neTefEFlnem7PlssvxkhVtR5fOH45L4kiU9VpPw"//"cLt-4hoFRheQuwu5MR0zFt:APA91bFsjxilHyj19zE80RFcCzvuVGkaK23bA5JZXy-9onD_kFQ3VpPN4Qq6ejrhrYY8ndS_-UWpdhL20HHhx4bC6krvjYlNtU_YNqkey7XBsxneMBvSjEUzVcCt7q-gHAJx8EXuCytc"   // Bu token, kullanıcı cihazından elde edilen FCM token'ıdır
            };

            // Bildirimi gönder
            string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
            Console.WriteLine($"Successfully sent message: {response}");


        }

    }
}



//cLt-4hoFRheQuwu5MR0zFt:APA91bFsjxilHyj19zE80RFcCzvuVGkaK23bA5JZXy-9onD_kFQ3VpPN4Qq6ejrhrYY8ndS_-UWpdhL20HHhx4bC6krvjYlNtU_YNqkey7XBsxneMBvSjEUzVcCt7q-gHAJx8EXuCytc

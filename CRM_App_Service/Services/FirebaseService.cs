using Google.Apis.Auth.OAuth2;
using Google.Apis.FirebaseCloudMessaging.v1;
using Google.Apis.FirebaseCloudMessaging.v1.Data;
using Google.Apis.Services;
using System.Threading.Tasks;

namespace CRM_App_Service.Services
{
    public class FirebaseService
    {
        private readonly FirebaseCloudMessagingService _fcmService;

        public FirebaseService()
        {
            // Service Account JSON dosyasını yükle
            var credential = GoogleCredential.FromFile("C:\\Users\\betul\\Desktop\\CRM_App\\CRM_App\\CRM_App\\google-services.json")
                .CreateScoped("https://www.googleapis.com/auth/firebase.messaging");

            _fcmService = new FirebaseCloudMessagingService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential
            });
        }
    }
}

using Microsoft.AspNetCore.Identity;

namespace CRM_App_Core.Models
{
    public class User : IdentityUser
    { 
        public DateTime DateOfBirth { get; set; }
        //public string DeviceToken { get; set; }
    }
}

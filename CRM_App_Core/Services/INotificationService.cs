using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_App_Core.Services
{
    public interface INotificationService
    {
        Task SendNotification( string title, string body);
    }

}


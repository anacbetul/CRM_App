using CRM_App_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_App_Core.Services
{
    public interface IMeetingService
    {
        Task<IEnumerable<Meeting>> GetUpcomingMeetingsAsync();
    }
}


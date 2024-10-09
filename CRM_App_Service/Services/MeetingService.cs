using CRM_App_Core.Models;
using CRM_App_Core.Services;
using CRM_App_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_App_Service.Services
{
    public class MeetingService : IMeetingService
    {
        private readonly AppDbContext _context;

        public MeetingService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Meeting>> GetUpcomingMeetingsAsync()
        {
            var today = DateTime.UtcNow;
            return await Task.FromResult(_context.Meetings.Where(x => x.MeetingDate > today).ToList());
        }
    }
}

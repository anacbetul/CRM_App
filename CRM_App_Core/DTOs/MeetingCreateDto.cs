using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_App_Core.DTOs
{
    public class MeetingCreateDto
    {
        public int ClientId { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime MeetingDate { get; set; }
    }
}

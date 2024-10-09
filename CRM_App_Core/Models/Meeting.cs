using CRM_App_Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_App_Core.Models
{
    public class Meeting
    {
        public int Id { get; set; }
        public int ClientId{ get; set; }
        public string UserId{ get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime MeetingDate { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_App_Core.DTOs
{
    public class ClientCreateDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public List<string> Notes { get; set; }
        public string Address { get; set; }
        public string UserId { get; set; }


    }
}

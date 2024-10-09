﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_App_Core.Configuration
{
    public class Client
    {
        public int Id { get; set; }
        public string Secret { get; set; }
        public List<String> Audiences { get; set; }
    }
}

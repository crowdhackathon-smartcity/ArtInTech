using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.ViewModel
{
    public class ViewDevices
    {
        public Devices devices { get; set; }
        public List<DevLayout> devlayout { get; set; }
    }
}
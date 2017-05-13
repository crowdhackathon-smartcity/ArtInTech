﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.ViewModel
{
    public class ViewLayoutSettings
    {
        public int idlu { get; set; }
        public List<LayoutSettings> LayoutSettingsList { get; set; }
        public List<DeviceIO> DeviceIO { get; set; }
    }
}
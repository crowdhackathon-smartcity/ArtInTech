using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.ViewModel
{
    public class ViewLayout
    {
        public DevLayout Devlayout { get; set; }
        public string result { get; set; }

        public int inputCount { get; set; }
        public int outputCount { get; set; }
        public int virtualCount { get; set; }
    }
}
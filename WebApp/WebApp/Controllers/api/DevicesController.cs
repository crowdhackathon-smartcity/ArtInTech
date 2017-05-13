using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApp.Controllers.api
{
    public class DevicesController : ApiController
    {
        private hacaEntities db = new hacaEntities();
        public IHttpActionResult GetDevices(string devApi, string devApiKey)
        {
            List<Devices> dev = db.Devices.Where(d => d.devApi == devApi && d.devApiKey == devApiKey).ToList();
            if (dev.Count == 1)
            {
                return Ok(dev[0]);
            }
            return Ok();
        }
    }
}

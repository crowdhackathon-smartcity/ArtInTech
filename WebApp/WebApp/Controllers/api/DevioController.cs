using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApp.Controllers.api
{
    public class DevioController : ApiController
    {
        private hacaEntities db = new hacaEntities();
        public IHttpActionResult GetDevio(string devApi, string devApiKey, string dbioId, string ioValue)
        {
            List<Devices> dev = db.Devices.Where(d => d.devApi == devApi && d.devApiKey == devApiKey).ToList();
            if (dev.Count == 1)
            {
                int id = Convert.ToInt32(dbioId);
                DeviceIO devio = db.DeviceIO.Find(id);
                if(devio != null)
                {
                    devio.ioValue = ioValue;
                    db.SaveChanges();
                }
            }
            return Ok();
        }
    }
}

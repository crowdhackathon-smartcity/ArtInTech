using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApp.Controllers.api
{
    public class setiovalController : ApiController
    {
        hacaEntities db = new hacaEntities();
        // GET: GetCredits
        public IHttpActionResult Getsetioval(int id,string val)
        {
            DeviceIO io = db.DeviceIO.Find(id);
            io.ioValue = val;
            db.SaveChanges();
            return Ok();
        }
    }
}

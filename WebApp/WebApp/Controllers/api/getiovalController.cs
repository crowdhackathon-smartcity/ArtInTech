using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApp.Models;

namespace WebApp.Controllers.api
{
    public class getiovalController : ApiController
    {
        hacaEntities db = new hacaEntities();
        // GET: GetCredits
        public string Getgetioval(int id)
        {
            DeviceIO io = db.DeviceIO.Find(id);


            return io.ioValue.ToString();
        }
    }
}

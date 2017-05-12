using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApp;
using WebApp.Models.ViewModel;

namespace WebApp.Controllers
{
    public class DevicesController : Controller
    {
        private hacaEntities db = new hacaEntities();

        // GET: Devices
        public ActionResult Index()
        {
            return View(db.Devices.ToList());
        }

        // GET: Devices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Devices devices = db.Devices.Find(id);
            if (devices == null)
            {
                return HttpNotFound();
            }
            return View(devices);
        }

        // GET: Devices/Create
        public ActionResult Create()
        {
            Devices dev = new Devices();
            dev.devApi = Guid.NewGuid().ToString("d").Substring(1, 7);
            dev.devApiKey = Guid.NewGuid().ToString("d").Substring(1, 7);
            List<DevLayout> devlayouts = db.DevLayout.ToList();
            ViewDevices vdev = new ViewDevices
            {
                devices = dev,
                devlayout = devlayouts
            };
            return View(vdev);
        }

        // POST: Devices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "devAutoId,devCode,devType,devApi,devApiKey,devLocation,devIP,devAdmin,devPass")] Devices devices)
        {
            if (ModelState.IsValid)
            {
                db.Devices.Add(devices);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(devices);
        }

        // GET: Devices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Devices devices = db.Devices.Find(id);
            List<DevLayout> devlayouts = db.DevLayout.ToList();
            if (devices == null)
            {
                return HttpNotFound();
            }

            ViewDevices vdev = new ViewDevices
            {
                devices = devices,
                devlayout = devlayouts
            };

            return View(vdev);
        }

        // POST: Devices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Devices devices)
        {
            if (ModelState.IsValid)
            {
                if(devices.devLayoutId == -1)
                {
                    devices.devLayoutId = null;
                }

                db.Entry(devices).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            List<DevLayout> devlayouts = db.DevLayout.ToList();
            ViewDevices vdev = new ViewDevices
            {
                devices = devices,
                devlayout = devlayouts
            };
            return View(devices);
        }

        // GET: Devices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Devices devices = db.Devices.Find(id);
            if (devices == null)
            {
                return HttpNotFound();
            }
            return View(devices);
        }

        // POST: Devices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Devices devices = db.Devices.Find(id);
            db.Devices.Remove(devices);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
    }
}

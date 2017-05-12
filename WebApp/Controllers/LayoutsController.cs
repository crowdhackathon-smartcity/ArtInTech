using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApp;
using WebApp.Models.ViewModel;

namespace WebApp.Controllers
{
    public class LayoutsController : Controller
    {
        private hacaEntities db = new hacaEntities();

        // GET: Layouts
        public ActionResult Index()
        {
            return View(db.DevLayout.ToList());
        }

        // GET: Layouts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DevLayout devLayout = db.DevLayout.Find(id);
            if (devLayout == null)
            {
                return HttpNotFound();
            }
            return View(devLayout);
        }

        // GET: Layouts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Layouts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViewLayout ViewdevLayout)
        {
            if (ModelState.IsValid)
            {
                ViewdevLayout.Devlayout.dvLtVersion = "1";
                ViewdevLayout.Devlayout.dvLtLastUpdate = DateTime.Now.ToString();
                string[] sett = ViewdevLayout.result.Split('#');
                db.DevLayout.Add(ViewdevLayout.Devlayout);
                db.SaveChanges();
                foreach (string s in sett)
                {
                    if (s != "")
                    {
                        LayoutSettings layouteset = new LayoutSettings();
                        string[] data = s.Split(';');
                        foreach (string v in data)
                        {
                            string[] val = v.Split(':');
                            if (val[0] == "type")
                                layouteset.ltSType = val[1];
                            else if (val[0] == "left")
                                layouteset.ItSPosition += val[0] + ":" + val[1] + ";";
                            else if (val[0] == "top")
                                layouteset.ItSPosition += val[0] + ":" + val[1] + ";";
                            else if (val[0] == "width")
                                layouteset.ItSPosition += val[0] + ":" + val[1] + ";";
                            else if (val[0] == "height")
                                layouteset.ItSPosition += val[0] + ":" + val[1] + ";";
                        }
                        layouteset.ltSDevLayoutId = ViewdevLayout.Devlayout.dvLtAutoId;
                        db.LayoutSettings.Add(layouteset);
                        db.SaveChanges();
                    }
                }

                string subPath = ViewdevLayout.Devlayout.dvLtAutoId.ToString();

                System.IO.Directory.CreateDirectory(Server.MapPath("~/files/" + subPath));

                return RedirectToAction("Index");
            }

            return View(ViewdevLayout);
        }

        public ActionResult Settings(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<LayoutSettings> devLayout = db.LayoutSettings.Where(ls => ls.ltSDevLayoutId == id).ToList();
            ViewLayoutSettings viewlayout = new ViewLayoutSettings
            {
                LayoutSettingsList = devLayout
            };
            if (devLayout == null)
            {
                return HttpNotFound();
            }
            return View(viewlayout);
        }


        [HttpPost]
        public ActionResult Upload(int? id)
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];

                var fileName = Path.GetFileName(file.FileName);

                var path = Path.Combine(Server.MapPath("~/files/" + id + "/"), fileName);
                file.SaveAs(path);
            }
            return Json(new { success = true, responseText = "Your message successfuly sent!" }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Settings(ViewLayoutSettings LayoutSetting)
        {
            return RedirectToAction("Index");
        }

        // GET: Layouts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DevLayout devLayout = db.DevLayout.Find(id);
            if (devLayout == null)
            {
                return HttpNotFound();
            }
            return View(devLayout);
        }

        // POST: Layouts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "dvLtAutoId,dvLtName,dvLtVersion,dvLtDescription,dvLtLastUpdate")] DevLayout devLayout)
        {
            if (ModelState.IsValid)
            {
                devLayout.dvLtLastUpdate = DateTime.Now.ToString();
                devLayout.dvLtVersion = (Convert.ToInt32(devLayout.dvLtVersion) + 1).ToString();

                db.Entry(devLayout).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(devLayout);
        }

        // GET: Layouts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DevLayout devLayout = db.DevLayout.Find(id);
            if (devLayout == null)
            {
                return HttpNotFound();
            }
            return View(devLayout);
        }

        // POST: Layouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DevLayout devLayout = db.DevLayout.Find(id);
            db.DevLayout.Remove(devLayout);
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
    }
}

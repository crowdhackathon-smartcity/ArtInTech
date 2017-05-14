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
            ViewLayout ViewdevLayout = new ViewLayout();
            ViewdevLayout.inputCount = 0;
            ViewdevLayout.outputCount = 0;
            ViewdevLayout.virtualCount = 0;
            ViewdevLayout.result = "";
            return View(ViewdevLayout);
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
                if (ViewdevLayout.Devlayout.dvLtType == 1)
                {
                    
                    db.DevLayout.Add(ViewdevLayout.Devlayout);
                    db.SaveChanges();
                    if (ViewdevLayout.result != null)
                    {
                        string[] sett = ViewdevLayout.result.Split('#');
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
                    }

                    DeviceIO addnew;
                    for (int i = 0; i < ViewdevLayout.inputCount; i++)
                    {
                        addnew = new DeviceIO
                        {
                            ioType = "1",
                            ioDeviceId = ViewdevLayout.Devlayout.dvLtAutoId,
                            ioValType = "bit"
                        };
                        db.DeviceIO.Add(addnew);
                    }
                    for (int i = 0; i < ViewdevLayout.outputCount; i++)
                    {
                        addnew = new DeviceIO
                        {
                            ioType = "2",
                            ioDeviceId = ViewdevLayout.Devlayout.dvLtAutoId,
                            ioValType = "bit"
                        };
                        db.DeviceIO.Add(addnew);
                    }
                    for (int i = 0; i < ViewdevLayout.virtualCount; i++)
                    {
                        addnew = new DeviceIO
                        {
                            ioType = "3",
                            ioDeviceId = ViewdevLayout.Devlayout.dvLtAutoId,
                            ioValType = "string"
                        };
                        db.DeviceIO.Add(addnew);
                    }
                    db.SaveChanges();
                    return RedirectToAction("Settings", new { id = ViewdevLayout.Devlayout.dvLtAutoId });
                }
                else if (ViewdevLayout.Devlayout.dvLtType == 2)
                {
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
                    return RedirectToAction("Settings", new { id = ViewdevLayout.Devlayout.dvLtAutoId });
                }
                else if (ViewdevLayout.Devlayout.dvLtType == 3)
                {
                    db.DevLayout.Add(ViewdevLayout.Devlayout);
                    db.SaveChanges();
                    DeviceIO addnew;
                    for (int i = 0; i < ViewdevLayout.inputCount; i++)
                    {
                        addnew = new DeviceIO
                        {
                            ioType = "1",
                            ioDeviceId = ViewdevLayout.Devlayout.dvLtAutoId,
                            ioValType = "bit"
                        };
                        db.DeviceIO.Add(addnew);
                    }
                    for (int i = 0; i < ViewdevLayout.outputCount; i++)
                    {
                        addnew = new DeviceIO
                        {
                            ioType = "2",
                            ioDeviceId = ViewdevLayout.Devlayout.dvLtAutoId,
                            ioValType = "bit"
                        };
                        db.DeviceIO.Add(addnew);
                    }
                    for (int i = 0; i < ViewdevLayout.virtualCount; i++)
                    {
                        addnew = new DeviceIO
                        {
                            ioType = "3",
                            ioDeviceId = ViewdevLayout.Devlayout.dvLtAutoId,
                            ioValType = "string"
                        };
                        db.DeviceIO.Add(addnew);
                    }
                    db.SaveChanges();
                    return RedirectToAction("Settings", new { id = ViewdevLayout.Devlayout.dvLtAutoId });
                }
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
            List<DeviceIO> devLayoutIO = db.DeviceIO.Where(ls => ls.ioDeviceId == id).ToList();
            ViewLayoutSettings viewlayout = new ViewLayoutSettings
            {
                idlu = id.Value,
                LayoutSettingsList = devLayout,
                DeviceIO = devLayoutIO
            };
            if (devLayout == null)
            {
                return HttpNotFound();
            }
            return View(viewlayout);
        }

        public ActionResult SettingsIO(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<DeviceIO> devLayoutIO = db.DeviceIO.Where(ls => ls.ioDeviceId == id).ToList();
            ViewDeviceIO viewlayoutIO = new ViewDeviceIO
            {
                DeviceIO = devLayoutIO
            };
            if (devLayoutIO == null)
            {
                return HttpNotFound();
            }
            return View(viewlayoutIO);
        }

        public ActionResult monitoring(int? id)
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult SettingsIO(ViewDeviceIO viewlayoutIO)
        {
            for(int i=0;i<viewlayoutIO.DeviceIO.Count;i++)
            {
                DeviceIO data = db.DeviceIO.Find(viewlayoutIO.DeviceIO[i].ioId);
                data.ioName = viewlayoutIO.DeviceIO[i].ioName;
                data.ioValType = viewlayoutIO.DeviceIO[i].ioValType;
            }
            return RedirectToAction("SettingsIO");
        }


        [HttpPost]
        public ActionResult Upload(int? id)
        {
            string fileName = "error";
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];

                fileName = Path.GetFileName(file.FileName);
                string path2 = Server.MapPath("~");
                var path = Path.Combine(Server.MapPath("~/files/" + id + "/"), fileName);
                
                
                file.SaveAs(path);
            }
            return Json(new { success = true, responseText = fileName }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Settings(ViewLayoutSettings LayoutSetting)
        {
            if (LayoutSetting.LayoutSettingsList != null)
            {
                for (int i = 0; i < LayoutSetting.LayoutSettingsList.Count; i++)
                {
                    LayoutSettings data = db.LayoutSettings.Find(LayoutSetting.LayoutSettingsList[i].ltSId);
                    data.ltSContent = LayoutSetting.LayoutSettingsList[i].ltSContent;
                    db.SaveChanges();
                }
            }
            if (LayoutSetting.DeviceIO != null)
            {
                for (int i = 0; i < LayoutSetting.DeviceIO.Count; i++)
                {
                    DeviceIO data = db.DeviceIO.Find(LayoutSetting.DeviceIO[i].ioId);
                    data.ioName = LayoutSetting.DeviceIO[i].ioName;
                    data.ioPortName = LayoutSetting.DeviceIO[i].ioPortName;
                    data.ioValType = LayoutSetting.DeviceIO[i].ioValType;
                    db.SaveChanges();
                }
            }

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

            List<LayoutSettings> set = db.LayoutSettings.Where(sl => sl.ltSDevLayoutId == devLayout.dvLtAutoId).ToList();

            string res = "";
            if (set.Count > 0)
            {                
                for (int i = 0; i < set.Count; i++)
                {
                    res += "id:" + (i+1) + ";type:" + set[i].ltSType + ";" + set[i].ItSPosition;
                    res = res.Remove(res.Length - 1, 1);
                    res += "#";
                }
            }
            ViewLayout veilout = new ViewLayout {
                Devlayout = devLayout,
                result = res
            };
            if (devLayout == null)
            {
                return HttpNotFound();
            }
            return View(veilout);
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

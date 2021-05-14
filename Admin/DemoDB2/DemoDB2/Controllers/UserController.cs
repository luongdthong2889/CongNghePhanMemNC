using DemoDB2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoDB2.Controllers
{
    public class UserController : Controller
    {
        DBRaoVatEntities db = new DBRaoVatEntities();
        // GET: User
        //public ActionResult Index()
        //{
        //    return View(db.USERs.ToList());
        //}
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(USER x)
        {
            try
            {
                db.USERs.Add(x);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Error Create New");
            }
        }
        public ActionResult Details(int id)
        {
            return View(db.USERs.Where(s => s.MANGUOIDUNG == id).FirstOrDefault());
        }
        public ActionResult Edit(int id)
        {
            return View(db.USERs.Where(s => s.MANGUOIDUNG == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(int id, USER x)
        {
            db.Entry(x).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            return View(db.USERs.Where(s => s.MANGUOIDUNG == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(int id, USER x)
        {
            try
            {
                x = db.USERs.Where(s => s.MANGUOIDUNG == id).FirstOrDefault();
                db.USERs.Remove(x);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("This data is using in other table, Error Delete!");
            }
        }
        public ActionResult Index(string _name)
        {
            if (_name == null)
                return View(db.USERs.ToList());
            else
                return View(db.USERs.Where(s => s.TENDANGNHAP.Contains(_name)).ToList());
        }
    }
}
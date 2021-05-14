using DemoDB2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoDB2.Controllers
{
    public class RaoVatController : Controller
    {
        DBRaoVatEntities db = new DBRaoVatEntities();
        // GET: RaoVat
        //public ActionResult Index()
        //{
        //    return View(db.RAOVATs.ToList());
        //}
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(RAOVAT x)
        {
            try
            {
                db.RAOVATs.Add(x);
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
            return View(db.RAOVATs.Where(s => s.MATIN == id).FirstOrDefault());
        }
        public ActionResult Edit(int id)
        {
            return View(db.RAOVATs.Where(s => s.MATIN == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(int id, RAOVAT x)
        {
            db.Entry(x).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            return View(db.RAOVATs.Where(s => s.MATIN == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(int id, RAOVAT x)
        {
            try
            {
                x = db.RAOVATs.Where(s => s.MATIN == id).FirstOrDefault();
                db.RAOVATs.Remove(x);
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
                return View(db.RAOVATs.ToList());
            else
                return View(db.RAOVATs.Where(s => s.TIEUDE.Contains(_name)).ToList());
        }
        public ActionResult SelectCate()
        {
            CATEGORY se_cate = new CATEGORY();
            se_cate.ListCate = db.CATEGORies.ToList<CATEGORY>();
            return PartialView(se_cate);
        }
        public ActionResult SelectTinhTrang()
        {
            TINHTRANG se_tinhtrang = new TINHTRANG();
            se_tinhtrang.ListTinhTrang = db.TINHTRANGs.ToList<TINHTRANG>();
            return PartialView(se_tinhtrang);
        }
        public ActionResult SelectLoaiTin()
        {
            LOAITIN se_loaitin = new LOAITIN();
            se_loaitin.ListLoaiTin = db.LOAITINs.ToList<LOAITIN>();
            return PartialView(se_loaitin);
        }
        public ActionResult SelectHinhThuc()
        {
            HINHTHUC se_hinhthuc = new HINHTHUC();
            se_hinhthuc.ListHinhThuc = db.HINHTHUCs.ToList<HINHTHUC>();
            return PartialView(se_hinhthuc);
        }
    }
}
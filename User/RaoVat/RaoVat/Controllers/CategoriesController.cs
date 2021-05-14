using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RaoVat.Models;
using PagedList;

namespace RaoVat.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: Categories
        DBRaoVatEntities db = new DBRaoVatEntities();
        public ActionResult Detail(int? id)
        {
           if(id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            return View(db.RAOVATs.Where(x => x.MATIN == id).FirstOrDefault());
        }
        public ActionResult Index(string category)
        {
            if (category == null)
            {
                var list = db.RAOVATs.OrderByDescending(x => x.TIEUDE).Where(s => s.MATRANGTHAI == 1);
                return View(list);
            }
            else
            {
                var list = db.RAOVATs.OrderByDescending(x => x.TIEUDE).Where(x => x.CATEGORY.TENLOAI == category && x.MATRANGTHAI == 1);
                return View(list);
            }
        }
        public ActionResult Search(string keyword)
        {
            var search = db.RAOVATs.Where(x => x.TIEUDE.Contains(keyword)).ToList();
            return View(search);
        }
        public PartialViewResult CategoryPartial()
        {
            var catelist = db.CATEGORies.ToList();
            return PartialView(catelist);
        }
    }
}
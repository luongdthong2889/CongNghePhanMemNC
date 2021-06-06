using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RaoVat.Models;
using PagedList;
using PagedList.Mvc;

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
        public ActionResult Index(string category, int? page)
        {
            int pageSize = 4;
            int pageNum = (page ?? 1);
            if (category == null)
            {
                var list = db.RAOVATs.OrderByDescending(x => x.NGAYGIODANG).Where(s => s.MATRANGTHAI == 1);
                return View(list.ToPagedList(pageNum,pageSize));
            }
            else
            {
                var list = db.RAOVATs.OrderByDescending(x => x.NGAYGIODANG).Where(x => x.CATEGORY.TENLOAI == category && x.MATRANGTHAI == 1);
                return View(list.ToPagedList(pageNum, pageSize));
            }
        }
        public ActionResult Search(string keyword, int? page)
        {
            int pageSize = 4;
            int pageNum = (page ?? 1);
            var search = db.RAOVATs.Where(x => x.TIEUDE.Contains(keyword) && x.MATRANGTHAI == 1).ToList();
            return View(search.ToPagedList(pageNum, pageSize));
        }
        public PartialViewResult CategoryPartial()
        {
            var catelist = db.CATEGORies.ToList();
            return PartialView(catelist);
        }
    }
}
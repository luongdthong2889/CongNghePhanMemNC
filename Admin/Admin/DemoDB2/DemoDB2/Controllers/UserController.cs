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
            if (Session["TENDANGNHAP"] == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Create(FormCollection frc)
        {
            var _usernname = frc["username"];
            var _phone = frc["phone"];
            var _pass = frc["pass"];
            if (ModelState.IsValid)
            {
                var check_id = db.USERs.Where(s => s.TENDANGNHAP == _usernname).FirstOrDefault();
                if (check_id == null)//chưa có id
                {
                    USER _user = new USER();
                    _user.TENDANGNHAP = _usernname;
                    _user.SODIENTHOAI = _phone;
                    _user.MATKHAU = _pass;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.USERs.Add(_user);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorRegister = "Tên đăng nhập đã tồn tại";
                    return View();
                }

            }
            return View();
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
            var userTenDangNhap = Session["TENDANGNHAP"];
            var userCurrent = db.USERs.Where(s => s.TENDANGNHAP == userTenDangNhap.ToString()).FirstOrDefault();
            x.MANGUOIDUNG = Convert.ToInt32(userCurrent.MANGUOIDUNG);
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
            if (Session["TENDANGNHAP"] == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                if (_name == null)
                    return View(db.USERs.ToList());
                else
                    return View(db.USERs.Where(s => s.TENDANGNHAP.Contains(_name)).ToList());
            }
        }
    }
}
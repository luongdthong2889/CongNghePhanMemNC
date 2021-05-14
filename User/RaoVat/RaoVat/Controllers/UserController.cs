using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RaoVat.Models;

namespace RaoVat.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        DBRaoVatEntities db = new DBRaoVatEntities();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginAcc(USER _user)
        {
            var check = db.USERs.Where(s => s.TENDANGNHAP == _user.TENDANGNHAP && s.MATKHAU == _user.MATKHAU).FirstOrDefault();
            if (check == null)
            {
                ViewBag.ErrorInfo = "Sai thông tin";
                return View("Index");
            }
            else
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                Session["TENDANGNHAP"] = _user.TENDANGNHAP;
                Session["MATKHAU"] = _user.MATKHAU;
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult RegisterUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterUser(USER _user)
        {
            if (ModelState.IsValid)
            {
                var check_id = db.USERs.Where(s => s.TENDANGNHAP == _user.TENDANGNHAP).FirstOrDefault();
                if (check_id == null)//chưa có id
                {
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
        public ActionResult LogOutUser()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}
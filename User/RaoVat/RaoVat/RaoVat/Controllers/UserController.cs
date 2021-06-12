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
        public ActionResult LoginAcc(FormCollection frc)
        {
            var _username = frc["username"];
            var _pass = frc["pass"];
            var check = db.USERs.Where(s => s.TENDANGNHAP == _username && s.MATKHAU == _pass).FirstOrDefault();
            if (check == null)
            {
                ViewBag.ErrorInfo = "Sai thông tin";
                return View("Index");
            }
            else
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                Session["TENDANGNHAP"] = _username;
                Session["MATKHAU"] = _pass;
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult RegisterUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterUser(FormCollection frc)
        {
            var _usernname = frc["username"];
            var _name = frc["name"];
            var _phone = frc["phone"];
            var _email = frc["email"];
            var _pass = frc["pass"];
            var _repass = frc["repeat-pass"];
            if (ModelState.IsValid)
            {
                var check_id = db.USERs.Where(s => s.TENDANGNHAP == _usernname).FirstOrDefault();
                if (check_id == null)//chưa có id
                {
                    USER _user = new USER();
                    _user.TENDANGNHAP = _usernname;
                    _user.HOTEN = _name;
                    _user.SODIENTHOAI = _phone;
                    _user.EMAIL = _email;
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
        public ActionResult LogOutUser()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}
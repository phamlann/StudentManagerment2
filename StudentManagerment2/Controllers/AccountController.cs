using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using StudentManagerment2.Models;


namespace StudentManagerment2.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        //public ActionResult Index()
        //{
        //    return View();
        //}
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Register()
        {
            return View();

        }
        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                user.Password = Crypto.HashPassword(user.Password);
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(user);
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                var u = db.Users.FirstOrDefault(a => a.Username.Equals(user.Username));
                if (u != null && BCrypt.Net.BCrypt.Verify(user.Password, u.Password))
                {
                    Session["UserID"] = u.Id;
                    Session["Username"] = u.Username.ToString();
                    return RedirectToAction("Index", "StudentManagerment2");
                }
                else
                {
                    ModelState.AddModelError("", "Username or Password is wrong.");
                }
            }
            return View(user); 
        }


        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login");
        }
        public ActionResult Create()
        {
            ViewBag.Role = new SelectList(db.Roles.ToList(), "Id", "RoleName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user, string ConfirmPassword)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra mật khẩu và xác nhận mật khẩu có khớp nhau không
                if (user.Password != ConfirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "Mật khẩu và xác nhận mật khẩu không khớp.");
                    return View(user);
                }
                // Kiểm tra Email đã tồn tại chưa
                if (db.Users.Any(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "Email đã được sử dụng.");
                    return View(user);
                }
                // Mã hóa mật khẩu
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Role = new SelectList(db.Roles.ToList(), "Id", "RoleName");
            return View(user);
        }
        public ActionResult Index()
        {
            var users = db.Users.ToList();

            return View(users);
        }
        public ActionResult Edit()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.Role = new SelectList(db.Roles.ToList(), "Id", "RoleName", user.RoleId);
            return View(user);
        }
        public ActionResult Delete(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
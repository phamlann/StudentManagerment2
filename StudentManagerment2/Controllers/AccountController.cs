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
                user.Password = Crypto.HashPassword(user.Password);// Mã hóa mật khẩu
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
        public ActionResult Login(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                // Tìm người dùng theo tên đăng nhập
                var u = db.Users.FirstOrDefault(a => a.Username.Equals(user.Username));

                // Kiểm tra xem người dùng có tồn tại và mật khẩu có khớp không
                if (u != null && BCrypt.Net.BCrypt.Verify(user.Password, u.Password))
                {
                    // Thiết lập session cho người dùng
                    Session.Clear();// Xóa hết các session cũ
                    Session["UserID"] = u.Id;// Lưu Id của người dùng
                    Session["Username"] = u.Username.ToString();// Lưu tên đăng nhập của người dùng
                    Session["Role"] = u.Role.RoleName;// Lưu quyền của người dùng
                    return RedirectToAction("Index", "StudentManagerment2");
                }
                else
                {
                    // Thêm lỗi vào ModelState nếu tên đăng nhập hoặc mật khẩu không đúng
                    ModelState.AddModelError("", "Username or Password is wrong.");
                    //TempData["LoginError"] = "Tên đăng nhập hoặc mật khẩu không đúng.";
                }
            }
            return View(user); // Chuyển về trang login nếu có lỗi
        }


        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login");
        }
        public ActionResult Create()
        {
            ViewBag.Role = new SelectList(db.Roles.ToList(), "Id", "RoleName");// Lấy danh sách quyền
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
                    ModelState.AddModelError("Email", "Email đã được sử dụng.");// Thêm lỗi vào ModelState
                    ViewBag.Role = new SelectList(db.Roles.ToList(), "Id", "RoleName");// Lấy danh sách quyền
                    return View(user);
                }
                // Mã hóa mật khẩu
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);// Mã hóa mật khẩu
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index", "Account");
            }
            ViewBag.Role = new SelectList(db.Roles.ToList(), "Id", "RoleName");// Lấy danh sách quyền
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
            ViewBag.Role = new SelectList(db.Roles.ToList(), "Id", "RoleName", user.RoleId);// Lấy danh sách quyền
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
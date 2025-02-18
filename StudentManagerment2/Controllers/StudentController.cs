using System.Linq;
using System.Web.Mvc;
using StudentManagerment2.Models;

namespace StudentManagerment2.Controllers
{

    public class StudentController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();//kết nối với database


        public ActionResult Index()
        {
            var students = db.Students.Include("Class").ToList();//lấy ra danh sách sinh viên
            return View(students);
        }



        [AuthorizeByRole("Admin")]
        public ActionResult Create()
        {
            ViewBag.ClassId = new SelectList(db.Classes, "Id", "ClassName");//tạo ra danh sách lớp
            return View();
        }


        [HttpPost]
        [AuthorizeByRole("Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassId = new SelectList(db.Classes, "Id", "ClassName", student.ClassId);//tạo ra danh sách lớp
            return View(student);
        }


        [AuthorizeByRole("Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);//trả về lỗi 400
            }
            var student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassId = new SelectList(db.Classes, "Id", "ClassName", student.ClassId);//tạo ra danh sách lớp
            return View(student);
        }


        [HttpPost]
        [AuthorizeByRole("Admin")]
        [ValidateAntiForgeryToken]// chống tấn công CSRF
        public ActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = System.Data.Entity.EntityState.Modified;//cập nhật dữ liệu
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassId = new SelectList(db.Classes, "Id", "ClassName", student.ClassId);//tạo ra danh sách lớp
            return View(student);
        }


        [AuthorizeByRole("Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }


        [HttpPost, ActionName("Delete")]
        [AuthorizeByRole("Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var student = db.Students.Find(id);
            if (student != null)
            {
                db.Students.Remove(student);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}


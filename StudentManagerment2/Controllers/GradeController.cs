using System.Linq;
using System.Web.Mvc;
using StudentManagerment2.Models;

namespace StudentManagerment2.Controllers
{

    public class GradeController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        
        public ActionResult Index()
        {
            var grades = db.Grades.Include("Student").Include("Subject").ToList();//lấy dữ liệu từ bảng Student và Subject
            return View(grades);
        }

        
        [AuthorizeByRole("Admin")]
        public ActionResult Create()
        {
            ViewBag.StudentId = new SelectList(db.Students, "Id", "FullName");//tạo ra danh sách sinh viên
            ViewBag.SubjectID = new SelectList(db.Subjects, "Id", "SubjectName");//tạo ra danh sách môn học
            return View();
        }

        
        [HttpPost]
        [AuthorizeByRole("Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Grade grade)
        {
            if (ModelState.IsValid)
            {
                db.Grades.Add(grade);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StudentId = new SelectList(db.Students, "Id", "FullName", grade.StudentId);//tạo ra danh sách sinh viên
            ViewBag.SubjectID = new SelectList(db.Subjects, "Id", "SubjectName", grade.SubjectID);//tạo ra danh sách môn học
            return View(grade);
        }

        
        [AuthorizeByRole("Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var grade = db.Grades.Find(id);
            if (grade == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentId = new SelectList(db.Students, "Id", "FullName", grade.StudentId);
            ViewBag.SubjectID = new SelectList(db.Subjects, "Id", "SubjectName", grade.SubjectID);
            return View(grade);
        }

        
        [HttpPost]
        [AuthorizeByRole("Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Grade grade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(grade).State = System.Data.Entity.EntityState.Modified;//cập nhật dữ liệu
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentId = new SelectList(db.Students, "Id", "FullName", grade.StudentId);
            ViewBag.SubjectID = new SelectList(db.Subjects, "Id", "SubjectName", grade.SubjectID);
            return View(grade);
        }

        
        [AuthorizeByRole("Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var grade = db.Grades.Find(id);
            if (grade == null)
            {
                return HttpNotFound();
            }
            return View(grade);
        }

        
        [HttpPost, ActionName("Delete")]
        [AuthorizeByRole("Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var grade = db.Grades.Find(id);
            if (grade != null)
            {
                db.Grades.Remove(grade);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}

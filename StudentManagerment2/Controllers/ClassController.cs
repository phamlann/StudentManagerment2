using System.Linq;
using System.Web.Mvc;
using StudentManagerment2.Models;

namespace StudentManagerment2.Controllers
{


    public class ClassController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult Index()
        {
            var classes = db.Classes.Include("Teacher").Include("Subject").ToList();//lấy dữ liệu từ bảng Class
            return View(classes);
        }


        [AuthorizeByRole("Admin")]
        public ActionResult Create()
        {
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "FullName");//tạo ra danh sách giáo viên
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "SubjectName");//tạo ra danh sách môn học
            return View();
        }


        [HttpPost]
        [AuthorizeByRole("Admin")]
        [ValidateAntiForgeryToken]

        public ActionResult Create(Class @class)
        {
            if (ModelState.IsValid)
            {
                db.Classes.Add(@class);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "FullName", @class.TeacherId);//tạo ra danh sách giáo viên
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "SubjectName", @class.SubjectId);//tạo ra danh sách môn học
            return View(@class);
        }


        [AuthorizeByRole("Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var @class = db.Classes.Find(id);
            if (@class == null)
            {
                return HttpNotFound();
            }
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "FullName", @class.TeacherId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "SubjectName", @class.SubjectId);
            return View(@class);
        }


        [HttpPost]
        [AuthorizeByRole("Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Class @class)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@class).State = System.Data.Entity.EntityState.Modified;//cập nhật dữ liệu
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "FullName", @class.TeacherId);//tạo ra danh sách giáo viên
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "SubjectName", @class.SubjectId);//tạo ra danh sách môn học
            return View(@class);
        }


        [AuthorizeByRole("Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var @class = db.Classes.Find(id);
            if (@class == null)
            {
                return HttpNotFound();
            }
            return View(@class);//trả về view
        }


        [HttpPost, ActionName("Delete")]
        [AuthorizeByRole("Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var @class = db.Classes.Find(id);
            if (@class != null)
            {
                db.Classes.Remove(@class);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}

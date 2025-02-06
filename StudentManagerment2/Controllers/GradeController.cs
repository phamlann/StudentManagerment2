using System.Linq;
using System.Web.Mvc;
using StudentManagerment2.Models;

namespace StudentManagerment2.Controllers
{
   
    public class GradeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Grade
        public ActionResult Index()
        {
            var grades = db.Grades.Include("Student").Include("Subject").ToList();
            return View(grades);
        }

        // GET: Grade/Create
        [AuthorizeByRole("Admin")]
        public ActionResult Create()
        {
            ViewBag.StudentId = new SelectList(db.Students, "Id", "FullName");
            ViewBag.SubjectID = new SelectList(db.Subjects, "Id", "SubjectName");
            return View();
        }

        // POST: Grade/Create
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

            ViewBag.StudentId = new SelectList(db.Students, "Id", "FullName", grade.StudentId);
            ViewBag.SubjectID = new SelectList(db.Subjects, "Id", "SubjectName", grade.SubjectID);
            return View(grade);
        }

        // GET: Grade/Edit/5
        [AuthorizeByRole("Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Grade grade = db.Grades.Find(id);
            if (grade == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentId = new SelectList(db.Students, "Id", "FullName", grade.StudentId);
            ViewBag.SubjectID = new SelectList(db.Subjects, "Id", "SubjectName", grade.SubjectID);
            return View(grade);
        }

        // POST: Grade/Edit/5
        [HttpPost]
        [AuthorizeByRole("Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Grade grade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(grade).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentId = new SelectList(db.Students, "Id", "FullName", grade.StudentId);
            ViewBag.SubjectID = new SelectList(db.Subjects, "Id", "SubjectName", grade.SubjectID);
            return View(grade);
        }

        // GET: Grade/Delete/5
        [AuthorizeByRole("Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Grade grade = db.Grades.Find(id);
            if (grade == null)
            {
                return HttpNotFound();
            }
            return View(grade);
        }

        // POST: Grade/Delete/5
        [HttpPost, ActionName("Delete")]
        [AuthorizeByRole("Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Grade grade = db.Grades.Find(id);
            db.Grades.Remove(grade);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

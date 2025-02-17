using System.Linq;
using System.Web.Mvc;
using StudentManagerment2.Models;

namespace StudentManagerment2.Controllers
{

    public class SubjectController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();//khai báo db context

        
        public ActionResult Index()
        {
            var subjects = db.Subjects.ToList();
            return View(subjects);
        }

        
        [AuthorizeByRole("Admin")]
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [AuthorizeByRole("Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Subject subject)
        {
            if (ModelState.IsValid)
            {
                db.Subjects.Add(subject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subject);
        }

        
        [AuthorizeByRole("Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var subject = db.Subjects.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        
        [HttpPost]
        [AuthorizeByRole("Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Subject subject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subject).State = System.Data.Entity.EntityState.Modified;//cập nhật dữ liệu
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subject);
        }

        
        [AuthorizeByRole("Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var subject = db.Subjects.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        
        [HttpPost, ActionName("Delete")]
        [AuthorizeByRole("Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var subject = db.Subjects.Find(id);
            if (subject != null)
            {
                db.Subjects.Remove(subject);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}


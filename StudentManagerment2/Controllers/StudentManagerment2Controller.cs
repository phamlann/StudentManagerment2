using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentManagerment2.Controllers
{
    [Authorize]
    public class StudentManagerment2Controller : Controller
    {
                
        
        // GET: StudentManagerment2
        //private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            //var students = db.Students.Include("Class").ToList();

            return View();
        }
    }
}
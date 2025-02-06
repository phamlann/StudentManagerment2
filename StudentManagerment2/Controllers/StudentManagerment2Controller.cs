using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentManagerment2.Controllers
{
    //[Authorize]
    public class StudentManagerment2Controller : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

    }
}
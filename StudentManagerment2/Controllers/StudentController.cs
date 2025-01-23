﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentManagerment2.Models;

namespace StudentManagerment2.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        // GET: Student
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var students = db.Students.Include("Class").ToList();
            return View(students);
        }
        public ActionResult Create()
        {
            ViewBag.ClassId = new SelectList(db.Classes, "Id", "ClassName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassId = new SelectList(db.Classes, "Id", "ClassName", student.ClassId);
            return View(student);
        }
        public ActionResult Delete(int id)
        {
            var student = db.Students.Find(id);
            return View(student);
        }
        [HttpPost]
        public ActionResult Delete(int id, Student student)
        {
            student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassId = new SelectList(db.Classes, "Id", "ClassName", student.ClassId);
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassId = new SelectList(db.Classes, "Id", "ClassName", student.ClassId);
            return View(student);
        }

    }
}
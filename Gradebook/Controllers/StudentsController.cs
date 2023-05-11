using Gradebook.Models.Gradebook;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gradebook.Controllers
{
    public class StudentsController : Controller
    {
        // GET: StudentsController
        private readonly GradebookDbContext _context;

        public StudentsController(GradebookDbContext context)
    {
        _context = context;
    }
        public ActionResult Index()
        {
            return View(_context.Students.ToList());
        }

        public ActionResult StudentsAndTheirGrades()
        {
            var gradebookDbContext = _context.Grades.ToList();
            return View(_context.Students.ToList());
        }

        // GET: StudentsController/Details/5
        public ActionResult Details(int id)
        {
            return View(_context.Students.Find(id));
        }

        // GET: StudentsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {
            try
            {
                _context.Students.Add(student);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentsController/Edit/5
        public ActionResult Edit(int id)
        {
            Student student = _context.Students.Find(id);
            return View(student);
        }

        // POST: StudentsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student student)
        {
            try
            {
                _context.Students.Update(student);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public IActionResult SearchBy2()
        {
            var students = _context.Students.ToList();
            ViewBag.Email = students.Select(m => m.Email).Distinct().ToList();
            return View(students);

        }
        [HttpPost]
        public IActionResult SearchBy2(string email, string firstname)
        {

            var students = _context.Students.ToList();
            ViewBag.Email = students.Select(m => m.Email).ToList();
            ViewBag.Firstname = firstname;

            if (!string.IsNullOrEmpty(email) && email != "All")
            {
                students = students.Where(m => m.Email == email).ToList();
            }

            if (!string.IsNullOrEmpty(firstname))
            {
                students = students.Where(m => m.Firstname.Contains(firstname)).ToList();
            }
            if (email == "ALL")
            {
                students = _context.Students.ToList();
            }
            if (email == "ALL" && !string.IsNullOrEmpty(firstname))
            {
                students = students.Where(m => m.Firstname.Contains(firstname)).ToList();
            }
            return View("SearchBy2", students);
        }

        // GET: StudentsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_context.Students.Find(id));
        }

        // POST: StudentsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection, Student student)
        {
            try
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

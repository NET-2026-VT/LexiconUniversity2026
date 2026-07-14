using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using LexiconUniversity2026.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LexiconUniversity2026.Web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly LexiconUniversityContext _context;
        private static Faker _faker; 

        public StudentsController(LexiconUniversityContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var model = _context.Students.AsNoTracking()
                .OrderByDescending(s=>s.Id)
                .Select(s => new StudentIndexViewModel
                {
                    Id = s.Id,
                    Avatar = s.Avatar,
                    FullName = s.Name.FullName,
                    City = s.Address.City,
                    CourseInfos = s.Enrollments.Select(e => new CourseInfo
                    {
                        CourseName = e.Course.Title,
                        Grade = e.Grade
                    })
                })
                .Take(5);
           
            return View(await model.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var student = await _context.Students
                 .Where(s => s.Id == id)
                 .Select(s => new StudentDetailsViewModel
                 {
                     Id = s.Id,
                     Avatar = s.Avatar,
                     FirstName = s.Name.FirstName,
                     LastName = s.Name.LastName,
                     Email = s.Email,
                     Street = s.Address.Street,
                     ZipCode = s.Address.ZipCode,
                     City = s.Address.City,
                     Attending = s.Enrollments.Count,
                     Courses = s.Enrollments
                     .Select(e => e.Course)
                     .ToList()
                 }).FirstOrDefaultAsync();

            if(student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ModelStateIsValid]
        public async Task<IActionResult> Create(StudentCreateViewModel viewModel)
        {
            _faker = new Faker("sv");
            Random rnd = new Random(); 

            //if (ModelState.IsValid)
            //{
                Student student = new Student()
                {
                    Avatar = _faker.Internet.Avatar(),
                    Name = new Name() { FirstName = viewModel.FirstName, LastName = viewModel.LastName },
                    Email = viewModel.Email,
                    Address = new Address { Street = viewModel.Street, ZipCode = viewModel.ZipCode, City = viewModel.City }
                };

                foreach (int courseId in viewModel.SelectedCourses)
                {
                    student.Enrollments.Add(new Enrollment
                    {
                        CourseId = courseId,
                        Grade = rnd.Next(1, 5)
                    });
                }

                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            //return View(viewModel);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Avatar,FirstName,LastName,Email")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Delete/5
        [RequiredModel]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
          

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}

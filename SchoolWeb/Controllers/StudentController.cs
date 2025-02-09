using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolWeb.Data;
using SchoolWeb.DTOs;
using SchoolWeb.Models;

namespace SchoolWeb.Controllers
{
    public class StudentController : Controller
    {
        private readonly DataContext _dataContext;

        public StudentController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //[HttpGet]
        public IActionResult Index()
        {
            var students = _dataContext.Students.Include(s => s.StudentSubjects).ThenInclude(ss => ss.Subject).ToList();
            return View(students);
        }

        [HttpGet("{id}")]
        public IActionResult GetStudentById(int id)
        {
            var student = _dataContext.Students
                .FirstOrDefault(x => x.Id == id);

            return Ok(student);
        }

        [HttpGet("subject")]
        public IActionResult GetStudentsBySubjectId([FromQuery] int subjectId)
        {
            var students = _dataContext.StudentSubjects
                .Where(s => s.SubjectId == subjectId)
                .Select(student => student.Student)
                .ToList();

            return Ok(students);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var student = new Student();
            return View(student);
        }

        [HttpPost, ActionName("Create")]
        public IActionResult AddStudent(CreateStudentModel model)
        {
            var student = _dataContext.Students
            .FirstOrDefault(s => s.Phone == model.Phone);

            if (student != null)
            {
                return Content("Student already exists");
            }

            var createStudent = new Student()
            {
                Name = model.Name,
                Surname = model.Surname,
                Patronymic = model.Patronymic,
                Class = model.Class,
                Phone = model.Phone
            };

            if (!ModelState.IsValid) 
            {
                return View(createStudent);
            }

            _dataContext.Students.Add(createStudent);
            _dataContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id) 
        { 
            var student = _dataContext.Students.FirstOrDefault(s => s.Id == id);
            return View(student);
        }

        [HttpPost]
        public IActionResult Update(Student model, int id)
        {
            var updateStudent = _dataContext.Students
                .FirstOrDefault(s => s.Id == id);

            if (updateStudent == null)
            {
                return Content("Student was not found");
            }

            updateStudent.Surname = model.Surname;
            updateStudent.Name = model.Name;
            updateStudent.Patronymic = model.Patronymic;
            updateStudent.Class = model.Class;
            updateStudent.Phone = model.Phone;

            _dataContext.SaveChangesAsync();

            return View();
        }

        public IActionResult Delete(int id) 
        {
            var studentToDelete = _dataContext.Students
               .FirstOrDefault(s => s.Id == id);

            if (studentToDelete == null)
            {
                return View("Student was not found");
            }

            return View(studentToDelete);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteStudent(int id)
        {
            var studentToDelete = _dataContext.Students
                .FirstOrDefault(s => s.Id == id);

            if (studentToDelete == null)
            {
                return View("Student was not found");
            }

            _dataContext.Remove(studentToDelete);
            _dataContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}

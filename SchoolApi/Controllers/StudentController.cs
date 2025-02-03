using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient.Server;
using SchoolApi.Data;
using SchoolApi.Entities;
using SchoolApi.Models;

namespace SchoolApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly DataContext _dataContext;
        public StudentController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            var students = _dataContext.Students.ToList();

            if (!students.Any())
            {

                return NotFound();
            }

            var studentModels = students.Select(s => new ReadStudentModel()
            {
                Id = s.Id,
                Name = s.Name,
                Surname = s.Surname,
                Patronymic = s.Patronymic,
                Class = s.Class,
                Phone = s.Phone
            });

            return Ok(studentModels);
        }

        [HttpGet("{id}")]
        public IActionResult GetStudentById(int id) 
        {
            var student = _dataContext.Students
                .FirstOrDefault(x => x.Id == id);

            if (student == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }

            var studentModel = new ReadStudentModel()
            {
                Id = student.Id,
                Name = student.Name,
                Surname = student.Surname,
                Patronymic = student.Patronymic,
                Class = student.Class,
                Phone = student.Phone
            };

            return Ok(studentModel);
        }

        [HttpGet("subject")]
        public IActionResult GetStudentsBySubjectId([FromQuery] int subjectId)
        {
            var students = _dataContext.StudentSubjects
                .Where(s => s.SubjectId == subjectId)
                .Select(student => student.Student)
                .ToList();

            if (!students.Any())
            {
                return NotFound();
            }

            var studentModels = students.Select(s => new ReadStudentModel() 
            {
                Id = s.Id,
                Name = s.Name,
                Surname = s.Surname,
                Patronymic = s.Patronymic,
                Class = s.Class,
                Phone = s.Phone
            });

            return Ok(studentModels);
        }

        [HttpPost]
        public IActionResult AddStudent(CreateStudentModel model)
        {
            var student = _dataContext.Students
                .FirstOrDefault(s => s.Phone == model.Phone);

            if(student != null)
            {
                return BadRequest("Student already exists");
            }

            var createStudent = new Student()
            {
                Name = model.Name,
                Surname = model.Surname,
                Patronymic = model.Patronymic,
                Class = model.Class,
                Phone = model.Phone
            };

            _dataContext.Students.Add(createStudent);
            _dataContext.SaveChanges();

            return Ok("Successfully created");
        }

        [HttpPut]
        public IActionResult UpdateStudent(CreateStudentModel model, int id)
        {
            var updateStudent = _dataContext.Students
                .FirstOrDefault(s => s.Id == id);

            if(updateStudent == null)
            {
                return NotFound("Student was not found");
            }
          
            updateStudent.Surname = model.Surname;
            updateStudent.Name = model.Name;
            updateStudent.Patronymic = model.Patronymic;
            updateStudent.Class = model.Class;
            updateStudent.Phone = model.Phone;
           
            _dataContext.SaveChangesAsync();

            return Ok("Successfully updated");
        }

        [HttpDelete]
        public IActionResult DeleteStudent(int id)
        {
            var studentToDelete = _dataContext.Students
                .FirstOrDefault(s => s.Id == id);

            if (studentToDelete == null)
            {
                return NotFound("Student was not found");
            }

            _dataContext.Remove(studentToDelete);
            _dataContext.SaveChangesAsync();

            return Ok("Successfully deleted");
        }
    }
}

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
            if(students == null)
                return NotFound();

            return Ok(students);
        }

        [HttpGet("{id}")]
        public IActionResult GetStudentById(int id) 
        {
            if(id <= 0) 
                return BadRequest("Invalid student ID.");

            var student = _dataContext.Students.FirstOrDefault(x => x.Id == id);
            if(student == null)
                return NotFound($"Student with ID {id} not found.");

            return Ok(student);
        }

        [HttpGet("subject")]
        public IActionResult GetStudentsBySubjectId([FromQuery] int subjectId)
        {
            if (subjectId <= 0)
                return BadRequest("Invalid subject ID.");

            var students = _dataContext.StudentSubjects.Where(s => s.SubjectId == subjectId).Select(student => student.Student).ToList();

            if (students == null)
                return NotFound();

            return Ok(students);
        }

        [HttpPost]
        public IActionResult AddStudent(StudentModel model)
        {
            if (model == null)
                return BadRequest("Student model is empty");

            var student = _dataContext.Students.Where(s => s.Phone == model.Phone).FirstOrDefault();

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
        public IActionResult UpdateStudent(StudentModel model, int id)
        {
            if (model == null)
                return BadRequest("Student model is empty");

            if (id <= 0)
                return BadRequest("Invalid student ID.");

            var updateStudent = _dataContext.Students.Where(s => s.Id == id).FirstOrDefault();
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
            if (id <= 0)
                return BadRequest("Invalid student ID.");

            var studentToDelete = _dataContext.Students.Where(s => s.Id == id).FirstOrDefault();
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

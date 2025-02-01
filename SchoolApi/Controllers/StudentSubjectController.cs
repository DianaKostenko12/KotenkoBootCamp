using Microsoft.AspNetCore.Mvc;
using SchoolApi.Data;
using SchoolApi.Entities;
using SchoolApi.Models;

namespace SchoolApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentSubjectController : Controller
    {
        private readonly DataContext _dataContext;
        public StudentSubjectController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost]
        public IActionResult AddStudentSubject(int studentId, int subjectId)
        {
            if (studentId <= 0 || subjectId <= 0)
                return BadRequest("Invalid ID.");

            var createStudentSubject = new StudentSubject()
            {
                StudentId = studentId,
                SubjectId = subjectId
            };

            _dataContext.StudentSubjects.Add(createStudentSubject);
            _dataContext.SaveChanges();
            return Ok("Successfully created");
        }

        [HttpDelete]
        public IActionResult DeleteStudentSubject(int studentId, int subjectId)
        {
            if (studentId <= 0 || subjectId <= 0)
                return BadRequest("Invalid ID.");

            var studentSubjectToDelete = _dataContext.
                StudentSubjects.Where(s => s.StudentId == studentId 
                && s.SubjectId == subjectId).
                FirstOrDefault();

            if (studentSubjectToDelete == null)
            {
                return NotFound("This object was not found");
            }

            _dataContext.Remove(studentSubjectToDelete);
            _dataContext.SaveChangesAsync();
            return Ok("Successfully deleted");
        }
    }
}

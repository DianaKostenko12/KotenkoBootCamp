using Microsoft.AspNetCore.Mvc;
using SchoolWeb.Data;
using SchoolWeb.Models;

namespace SchoolWeb.Controllers
{
    public class StudentSubjectController : Controller
    {
        private readonly DataContext _dataContext;
        public IActionResult Index()
        {
            return View();
        }

        public StudentSubjectController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost]
        public IActionResult AddStudentSubject(int studentId, int subjectId)
        {
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
            var studentSubjectToDelete = _dataContext.StudentSubjects
                .FirstOrDefault(s => s.StudentId == studentId && s.SubjectId == subjectId);

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

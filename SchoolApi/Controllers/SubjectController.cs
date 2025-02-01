using Microsoft.AspNetCore.Mvc;
using SchoolApi.Data;
using SchoolApi.Entities;
using SchoolApi.Models;

namespace SchoolApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : Controller
    {
        private readonly DataContext _dataContext;
        public SubjectController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult GetSubjects()
        {
            var subjects = _dataContext.Subjects.ToList();
            if (subjects == null)
                return NotFound();

            return Ok(subjects);
        }

        [HttpGet("{id}")]
        public IActionResult GetSubjectById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid subject ID.");

            var subject = _dataContext.Subjects.FirstOrDefault(x => x.Id == id);
            if (subject == null)
                return NotFound($"Subject with ID {id} not found.");

            return Ok(subject);
        }

        [HttpGet("student")]
        public IActionResult GetSubjectsByStudentId([FromQuery] int studentId)
        {
            if (studentId <= 0)
                return BadRequest("Invalid subject ID.");

            var subjects = _dataContext.StudentSubjects.Where(s => s.StudentId == studentId).Select(subject => subject.Subject).ToList();

            if (subjects == null)
                return NotFound();

            return Ok(subjects);
        }

        [HttpPost]
        public IActionResult AddSubject(SubjectModel model)
        {
            if (model == null)
                return BadRequest("Subject model is empty");

            var subject = _dataContext.Subjects.Where(s => s.SubjectName == model.SubjectName).FirstOrDefault();

            if (subject != null)
            {
                return BadRequest("Subject already exists");
            }

            var createSubject = new Subject()
            {
                SubjectName = model.SubjectName,
                Description = model.Description
            };

            _dataContext.Subjects.Add(createSubject);
            _dataContext.SaveChanges();
            return Ok("Successfully created");
        }

        [HttpPut]
        public IActionResult UpdateSubject(SubjectModel model, int id)
        {
            if (model == null)
                return BadRequest("Subject model is empty");

            if (id <= 0)
                return BadRequest("Invalid subject ID.");

            var updateSubject = _dataContext.Subjects.Where(s => s.Id == id).FirstOrDefault();
            if (updateSubject == null)
            {
                return NotFound("Subject was not found");
            }

            updateSubject.SubjectName = model.SubjectName;
            updateSubject.Description = model.Description;
            
            _dataContext.SaveChangesAsync();
            return Ok("Successfully updated");
        }

        [HttpDelete]
        public IActionResult DeleteSubject(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid subject ID.");

            var subjectToDelete = _dataContext.Subjects.Where(s => s.Id == id).FirstOrDefault();
            if (subjectToDelete == null)
            {
                return NotFound("Subject was not found");
            }

            _dataContext.Remove(subjectToDelete);
            _dataContext.SaveChangesAsync();
            return Ok("Successfully deleted");
        }
    }
}

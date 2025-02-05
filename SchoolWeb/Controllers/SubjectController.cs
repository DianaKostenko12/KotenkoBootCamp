using Microsoft.AspNetCore.Mvc;
using SchoolWeb.Data;
using SchoolWeb.DTOs;
using SchoolWeb.Models;

namespace SchoolWeb.Controllers
{
    public class SubjectController : Controller
    {
        private readonly DataContext _dataContext;

        public IActionResult Index()
        {
            return View();
        }

        public SubjectController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult GetSubjects()
        {
            var subjects = _dataContext.Subjects.ToList();

            if (!subjects.Any())
            {
                return NotFound();
            }

            var subjectModels = subjects.Select(s => new ReadSubjectModel()
            {
                Id = s.Id,
                SubjectName = s.SubjectName,
                Description = s.Description,
            });

            return Ok(subjectModels);
        }

        [HttpGet("{id}")]
        public IActionResult GetSubjectById(int id)
        {
            var subject = _dataContext.Subjects
                .FirstOrDefault(x => x.Id == id);

            if (subject == null)
            {
                return NotFound($"Subject with ID {id} not found.");
            }

            var subjectModel = new ReadSubjectModel()
            {
                Id = subject.Id,
                SubjectName = subject.SubjectName,
                Description = subject.Description,
            };

            return Ok(subjectModel);
        }

        [HttpGet("student")]
        public IActionResult GetSubjectsByStudentId([FromQuery] int studentId)
        {
            var subjects = _dataContext.StudentSubjects
                .Where(s => s.StudentId == studentId)
                .Select(subject => subject.Subject)
                .ToList();

            if (!subjects.Any())
            {
                return NotFound();
            }

            var subjectModels = subjects.Select(s => new ReadSubjectModel()
            {
                Id = s.Id,
                SubjectName = s.SubjectName,
                Description = s.Description,
            });

            return Ok(subjectModels);
        }

        [HttpPost]
        public IActionResult AddSubject(CreateSubjectModel model)
        {
            var subject = _dataContext.Subjects
                .FirstOrDefault(s => s.SubjectName == model.SubjectName);

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
        public IActionResult UpdateSubject(CreateSubjectModel model, int id)
        {
            var updateSubject = _dataContext.Subjects
                .FirstOrDefault(s => s.Id == id);

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
            var subjectToDelete = _dataContext.Subjects
                .FirstOrDefault(s => s.Id == id);

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

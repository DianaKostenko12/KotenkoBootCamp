using System.ComponentModel.DataAnnotations;

namespace SchoolWeb.Models
{
    public class Subject
    {
        public int Id { get; set; }
        [Required]
        public string SubjectName { get; set; }
        public string Description { get; set; }
        public ICollection<StudentSubject> StudentSubjects { get; set; }
    }
}

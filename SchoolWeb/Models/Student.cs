using System.ComponentModel.DataAnnotations;

namespace SchoolWeb.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Patronymic { get; set; }
        [Required]
        public string Class { get; set; }
        [Phone]
        public string Phone { get; set; }
        public ICollection<StudentSubject> StudentSubjects { get; set; }
    }
}

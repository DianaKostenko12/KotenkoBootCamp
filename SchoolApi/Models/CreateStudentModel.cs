using System.ComponentModel.DataAnnotations;

namespace SchoolApi.Models
{
    public class CreateStudentModel
    {
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Patronymic { get; set; }
        [Required]
        public string Class { get; set; }
        [Required, Phone]
        public string Phone { get; set; }
    }
}

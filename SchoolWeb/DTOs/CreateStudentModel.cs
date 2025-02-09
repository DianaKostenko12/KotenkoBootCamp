using System.ComponentModel.DataAnnotations;

namespace SchoolWeb.DTOs
{
    public class CreateStudentModel
    {
        [Required(ErrorMessage = "Surname is required.")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Patronymic number is required.")]
        public string Patronymic { get; set; }
        [Required(ErrorMessage = "Class is required.")]
        public string Class { get; set; }
        [Required(ErrorMessage = "Phone is required."), Phone]
        public string Phone { get; set; }
    }
}

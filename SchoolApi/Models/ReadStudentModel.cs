using System.ComponentModel.DataAnnotations;

namespace SchoolApi.Models
{
    public class ReadStudentModel
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Class { get; set; }
        public string Phone { get; set; }
    }
}

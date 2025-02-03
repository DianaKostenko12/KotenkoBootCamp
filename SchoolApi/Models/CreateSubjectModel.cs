using System.ComponentModel.DataAnnotations;

namespace SchoolApi.Models
{
    public class CreateSubjectModel
    {
        [Required]
        public string SubjectName { get; set; }
        public string Description { get; set; }
    }
}

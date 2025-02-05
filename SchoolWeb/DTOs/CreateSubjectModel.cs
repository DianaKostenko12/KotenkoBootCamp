using System.ComponentModel.DataAnnotations;

namespace SchoolWeb.DTOs
{
    public class CreateSubjectModel
    {
        [Required]
        public string SubjectName { get; set; }
        public string Description { get; set; }
    }
}

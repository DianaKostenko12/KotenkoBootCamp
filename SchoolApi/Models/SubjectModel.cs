using System.ComponentModel.DataAnnotations;

namespace SchoolApi.Models
{
    public class SubjectModel
    {
        [Required]
        public string SubjectName { get; set; }
        public string Description { get; set; }
    }
}

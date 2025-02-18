using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagerment2.Models
{
    [Table("Subjects")]
    public class Subject
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên môn học không được để trống")]
        [StringLength(100, ErrorMessage = "Tên môn học không được vượt quá 100 ký tự")]
        public string SubjectName { get; set; }

        [StringLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự")]
        public string Description { get; set; }

        // Liên kết với Class (1 Subject -> N Class)
        public ICollection<Class> Classes { get; set; } = new List<Class>();

        // Liên kết với Teacher (Nhiều Teacher dạy 1 Subject)
        public ICollection<TeacherSubject> TeacherSubjects { get; set; } = new List<TeacherSubject>();
    }
}

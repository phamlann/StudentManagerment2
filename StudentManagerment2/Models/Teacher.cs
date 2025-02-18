using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagerment2.Models
{
    [Table("Teachers")]
    public class Teacher
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Họ và tên không được để trống")]
        [StringLength(100, ErrorMessage = "Họ và tên không được vượt quá 100 ký tự")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Khoa không được để trống")]
        [StringLength(100, ErrorMessage = "Khoa không được vượt quá 100 ký tự")]
        public string Department { get; set; } = string.Empty;

        // Liên kết với Class (1 Teacher -> nhiều Class)
        public ICollection<Class> Classes { get; set; } = new List<Class>();

        // Liên kết với Subject (Nhiều Teacher -> N Subject)
        public ICollection<TeacherSubject> TeacherSubjects { get; set; } = new List<TeacherSubject>();
    }
}

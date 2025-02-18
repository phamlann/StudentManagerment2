using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagerment2.Models
{
    [Table("Classes")]
    public class Class
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên lớp không được để trống")]
        [StringLength(100, ErrorMessage = "Tên lớp không được vượt quá 100 ký tự")]
        public string ClassName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mô tả không được để trống")]
        [StringLength(255, ErrorMessage = "Mô tả không được vượt quá 255 ký tự")]
        public string Description { get; set; }

        // Liên kết với Subject (1 Class -> 1 Subject)
        [Required(ErrorMessage = "Môn học không được để trống")]
        public int SubjectId { get; set; }

        [ForeignKey("SubjectId")]
        public Subject Subject { get; set; }

        // Liên kết với Teacher (1 Class -> 1 Teacher)
        [Required(ErrorMessage = "Giáo viên không được để trống")]
        public int TeacherId { get; set; }

        [ForeignKey("TeacherId")]
        public Teacher Teacher { get; set; }

        // Liên kết với Student (Nhiều Student trong 1 Class)
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StudentManagerment2.Models;

namespace StudentManagerment2.Models
{
    [Table("Classes")]
    public class Class
    {
        [Key]
        [Required]
        public int Id { get; set; } // Khóa chính
        [Required]
        public string ClassName { get; set; } = string.Empty; // Tên lớp học
        [Required]
        public string Description { get; set; } // Mô tả lớp học

        // Liên kết với Subject (1 Class -> 1 Subject)
        public int SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        public Subject Subject { get; set; } 

        // Liên kết với Teacher (1 Class -> 1 Teacher)
        public int TeacherId { get; set; }
        [ForeignKey("TeacherId")]
        public Teacher Teacher { get; set; } 

        // Liên kết với Student (Nhiều Student trong 1 Class)
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }

}

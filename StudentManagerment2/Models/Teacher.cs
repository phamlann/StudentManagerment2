using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StudentManagerment2.Models;

namespace StudentManagerment2.Models
{
    [Table("Teachers")]
    public class Teacher
    {
        [Key]
        public int Id { get; set; } // Khóa chính
        public string FullName { get; set; } = string.Empty; // Họ và tên
        public string Department { get; set; } = string.Empty; // Khoa giảng dạy

        // Liên kết với Class (1 Teacher -> N Class)
        public ICollection<Class> Classes { get; set; } = new List<Class>();

        // Liên kết với Subject (Nhiều Teacher -> N Subject)
        public ICollection<TeacherSubject> TeacherSubjects { get; set; } = new List<TeacherSubject>();
    }

}

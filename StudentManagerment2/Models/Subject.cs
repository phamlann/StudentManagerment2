using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StudentManagerment2.Models;

namespace StudentManagerment2.Models
{
    [Table("Subjects")]
    public class Subject
    {
        [Key]
        public int Id { get; set; } // Khóa chính
        public string SubjectName { get; set; } // Tên môn học
        public string Description { get; set; } // Mô tả môn học

        // Liên kết với Class (1 Subject -> N Class)
        public ICollection<Class> Classes { get; set; } = new List<Class>();

        // Liên kết với Teacher (Nhiều Teacher dạy 1 Subject)
        public ICollection<TeacherSubject> TeacherSubjects { get; set; } = new List<TeacherSubject>();
    }

}

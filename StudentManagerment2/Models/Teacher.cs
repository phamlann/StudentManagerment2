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
        public int Id { get; set; } 
        public string FullName { get; set; } = string.Empty; 
        public string Department { get; set; } = string.Empty; 

        // Liên kết với Class (1 Teacher -> nhiều Class)
        public ICollection<Class> Classes { get; set; } = new List<Class>();

        // Liên kết với Subject (Nhiều Teacher -> N Subject)
        public ICollection<TeacherSubject> TeacherSubjects { get; set; } = new List<TeacherSubject>();
    }

}

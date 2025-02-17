using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StudentManagerment2.Models;

namespace StudentManagerment2.Models
{
    [Table("Grades")]
    public class Grade
    {
        [Key]
        public int Id { get; set; } 
        public int SubjectID { get; set; } 
        [ForeignKey("SubjectID")]
        public Subject Subject { get; set; } 
        [Required]
        public float Score { get; set; } 

        // Liên kết với Student
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public Student Student { get; set; } 
    }

}

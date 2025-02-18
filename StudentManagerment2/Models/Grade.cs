using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagerment2.Models
{
    [Table("Grades")]
    public class Grade
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Môn học không được để trống")]
        public int SubjectID { get; set; }

        [ForeignKey("SubjectID")]
        public Subject Subject { get; set; }

        [Required(ErrorMessage = "Điểm số không được để trống")]
        [Range(0, 10, ErrorMessage = "Điểm số phải từ 0 đến 10")]
        public float Score { get; set; }

        [Required(ErrorMessage = "Sinh viên không được để trống")]
        public int StudentId { get; set; }

        [ForeignKey("StudentId")]
        public Student Student { get; set; }
    }
}

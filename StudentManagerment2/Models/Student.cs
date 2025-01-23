using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace StudentManagerment2.Models
{
    [Table("Students")]
    public class Student
    {
        [Key]
        public int Id { get; set; } // Khóa chính
        public string FullName { get; set; } = string.Empty; // Họ và tên
        public DateTime DateOfBirth { get; set; } // Ngày sinh
        public string Email { get; set; } = string.Empty; // Email liên hệ

        // Liên kết với Class
        public int ClassId { get; set; }
        [ForeignKey("ClassId")]
        public Class Class { get; set; } 

        // Liên kết với Grade
        public ICollection<Grade> Grades { get; set; } = new List<Grade>();
    }

}

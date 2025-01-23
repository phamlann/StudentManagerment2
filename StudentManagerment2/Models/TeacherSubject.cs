﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StudentManagerment2.Models;

namespace StudentManagerment2.Models
{
    [Table("TeacherSubjects")]
    public class TeacherSubject
    {
        [Key]
        public int Id { get; set; }
        public int TeacherId { get; set; } // Khóa ngoại

        public Teacher Teacher { get; set; } 

        public int SubjectId { get; set; } // Khóa ngoại

        public Subject Subject { get; set; } 
    }
}
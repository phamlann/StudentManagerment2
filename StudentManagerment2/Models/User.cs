using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudentManagerment2.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(15)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        [NotMapped]
        [Required]
        [StringLength(255)]
        public string ConfirmPassword { get; set; }
        
        public int? RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

    }
}

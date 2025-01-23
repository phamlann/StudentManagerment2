using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagerment2.Models
{
    [Table("Roles")]
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string RoleName { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}

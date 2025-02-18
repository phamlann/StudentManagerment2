using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagerment2.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Tự động tăng ID
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên đăng nhập không được để trống.")]
        [StringLength(50, ErrorMessage = "Tên đăng nhập không được vượt quá 50 ký tự.")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Tên đăng nhập chỉ được chứa chữ cái và số, không dấu và không khoảng trắng.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email không được để trống.")]
        [StringLength(100, ErrorMessage = "Email không được vượt quá 100 ký tự.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống.")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Số điện thoại phải từ 10 đến 15 ký tự.")]
        [RegularExpression(@"^\d{10,15}$", ErrorMessage = "Số điện thoại chỉ được chứa chữ số.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        [StringLength(255, ErrorMessage = "Mật khẩu không được vượt quá 255 ký tự.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Xác nhận mật khẩu không được để trống.")]
        //[StringLength(255, ErrorMessage = "Xác nhận mật khẩu không được vượt quá 255 ký tự.")]
        //[Compare("Password", ErrorMessage = "Mật khẩu xác nhận không khớp.")]
        //[DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public int? RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

    }
}

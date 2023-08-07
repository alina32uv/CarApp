using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarApp.Entities
{
    [Table("User")]
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [StringLength(255), Column(TypeName = "varchar(255)")]
        public string Username { get; set; }

        [StringLength(255), Column(TypeName = "varchar(255)")]
        public string Email { get; set; }

        [StringLength(20), Column(TypeName = "varchar(20)")]
        public string Phone { get; set; }

        [StringLength(20), Column(TypeName = "varchar(20)")]
        public string Password { get; set; }

        public virtual  ICollection<Role> Role { get; set; }
    }
}

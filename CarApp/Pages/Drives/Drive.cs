using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarApp.Pages.Drives
{
    [Table("Drive")]
    public class Drive
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DriveId { get; set; }

        [StringLength(255), Column(TypeName = "varchar(255)")]
        public string Name { get; set; }

        // public ICollection<Car> Car { get; set; }
    }
}

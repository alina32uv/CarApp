using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarApp.Pages.Vehicle
{
    [Table("VehicleType")]
    public class VehicleType
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VehicleId { get; set; }

        [StringLength(255), Column(TypeName = "varchar(255)")]
        public string Name { get; set; }
        //public bool IsSelected { get; set; }
        //public ICollection<Car> Car { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarApp.Pages.Fuels
{

    [Table("Fuel")]
    public class Fuel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FuelId { get; set; }

        [StringLength(255), Column(TypeName = "varchar(255)")]
        public string Name { get; set; }

        // public virtual Car Car { get; set; }
    }
}

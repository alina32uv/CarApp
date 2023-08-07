using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarApp.Pages.Transmissions
{

    [Table("Transmission")]
    public class Transmission
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransmissionId { get; set; }


        [StringLength(255), Column(TypeName = "varchar(255)")]

        public string Type { get; set; }

        // public ICollection<Car> Cars { get; set; }

        //  public virtual Brand Brands { get; set; }
    }
}

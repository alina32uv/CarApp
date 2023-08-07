using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarApp.Pages.Brands
{
    [Table("Brand")]
    public class Brand
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BrandId { get; set; }


        [StringLength(255), Column(TypeName = "varchar(255)")]

        public string Name { get; set; }


        // public ICollection<Car> Cars { get; set; }

        //  public virtual Brand Brands { get; set; }
    }
}

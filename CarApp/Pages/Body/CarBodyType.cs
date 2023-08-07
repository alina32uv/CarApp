using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarApp.Entities
{
    [Table("CarBodyType")]

    public class CarBodyType
    {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int CarBodyTypeId { get; set; }

            [StringLength(255), Column(TypeName = "varchar(255)")]
            public string Name { get; set; }

            // public ICollection<Car> Car { get; set; }
            // public virtual CarBodyType CarBodyTypes { get; set; }
    }
}

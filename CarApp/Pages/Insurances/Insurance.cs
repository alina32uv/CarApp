using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarApp.Pages.Insurances
{
    [Table("Insurance")]
    public class Insurance
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InsuranceId { get; set; }

        [StringLength(255), Column(TypeName = "varchar(255)")]
        public string Name { get; set; }

        // public ICollection<RentInfo> RentInfo { get; set; }
    }
}

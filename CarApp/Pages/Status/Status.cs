using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarApp.Pages.Status
{
    [Table("Status")]
    public class Status
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StatusId { get; set; }

        [StringLength(255), Column(TypeName = "varchar(255)")]
        public string Name { get; set; }

        //  public ICollection<RentInfo> RentInfo { get; set; }

    }
}

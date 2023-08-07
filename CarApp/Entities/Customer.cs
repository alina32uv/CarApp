using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CarApp.Pages.RentInfo;

namespace CarApp.Entities
{
    [Table("Customer")]
    public class Customer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }

        [StringLength(255), Column(TypeName = "varchar(255)")]
        public string Username { get; set; }


        [StringLength(255), Column(TypeName = "varchar(255)")]
        public string Email { get; set; }

        [StringLength(20), Column(TypeName = "varchar(20)")]
        public string Phone { get; set; }

        [StringLength(20), Column(TypeName = "varchar(20)")]
        public string Password { get; set; }


        public ICollection<RentInfo> RentInfo { get; set; }


        //public virtual Customer Customers { get; set; }
    }
}
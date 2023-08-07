using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CarApp.Pages.Status;
using CarApp.Pages.Car;

namespace CarApp.Pages.RentInfo
{

    [Table("RentInfo")]
    public class RentInfo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RentInfoId { get; set; }
        public string UserId { get; set; }


        [ForeignKey(nameof(Car))]
        public int CarId { get; set; }
        public virtual Pages.Car.Car Car { get; set; }


        [ForeignKey(nameof(Status))]
        public int StatusId { get; set; }
        public virtual Pages.Status.Status Status { get; set; }


        public DateTime DateBring { get; set; }
        public DateTime DateReturn { get; set; }



        public string DateBringDate => DateBring.ToString("MM/dd/yyyy");
        public string DateBringTime => DateBring.ToString("hh:mm tt");
        public string DateReturnDate => DateReturn.ToString("MM/dd/yyyy");
        public string DateReturnTime => DateReturn.ToString("hh:mm tt");

    }
}

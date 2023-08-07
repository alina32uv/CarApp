using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Drawing2D;
using CarApp.Pages.Fuels;
using CarApp.Pages.Body;
using CarApp.Pages.Brands;
using CarApp.Pages.Drives;
using CarApp.Pages.Transmissions;
using CarApp.Pages.Vehicle;
using CarApp.Entities;

namespace CarApp.Pages.Car
{
    [Table("Car")]
    public class Car
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CarId { get; set; }

        [StringLength(255), Required, Column(TypeName = "varchar(255)")]
        public string Name { get; set; }



        [ForeignKey(nameof(Transmission))]
        public int TransmissionId { get; set; }
        public virtual Transmission Transmission { get; set; }


        [MaxLength(20)]
        [Column(TypeName = "int")]
        public int Seats { get; set; }




        [ForeignKey(nameof(Fuel))]
        public int FuelId { get; set; }
        public virtual Fuel Fuel { get; set; }
        //public IEnumerable<Fuel> Fuel { get; set; }





        [ForeignKey(nameof(Drive))]
        public int DriveId { get; set; }
        public virtual Drive Drive { get; set; }




        [Column(TypeName = "decimal(18, 2)")]
        public decimal DailyPrice { get; set; }

        [StringLength(10), Column(TypeName = "varchar(10)")]
        public string Year { get; set; }



        [StringLength(255), Column(TypeName = "varchar(255)")]
        public string ModelName { get; set; }




        [StringLength(255), Column(TypeName = "varchar(255)")]
        public string Image { get; set; }




        [ForeignKey(nameof(CarBodyType))]
        public int CarBodyTypeId { get; set; }

        public virtual CarBodyType CarBodyType { get; set; }



        [ForeignKey(nameof(VehicleType))]
        public int VehicleTypeId { get; set; }
        [Required]
        public virtual VehicleType VehicleType { get; set; }



        [ForeignKey(nameof(Brand))]
        public int BrandId { get; set; }
        [Required]
        public virtual Brand Brand { get; set; }



        public bool IsSelected { get; set; }

        public bool IsRented { get; set; }



        /* [ForeignKey(nameof(Insurance))]
            public int InsuranceId { get; set; }
           // [Required]
            public virtual Insurance Insurance { get; set; }
        */
    }
}

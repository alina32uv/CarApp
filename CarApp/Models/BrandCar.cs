using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarApp.Models
{
    public class BrandCar
    {
        public int BrandId { get; set; }

        public string Name { get; set; } 
    }
}

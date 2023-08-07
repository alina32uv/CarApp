using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarApp.Dto
{
    public class BrandForCreationDto
    {
        public int BrandId { get; set; }

        public string Name { get; set; }
    }
}

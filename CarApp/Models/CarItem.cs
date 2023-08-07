namespace CarApp.Models
{
    public class CarItem
    {

      
        public int CarId { get; set; }
        public string Name { get; set; }
        public string TransmissionType { get; set; }
        public int Seats { get; set; }
        public string FuelName { get; set; }
        public string DriveName { get; set; }
        public decimal DailyPrice { get; set; }
        public string Year { get; set; }
        public string ModelName { get; set; }

        public IFormFile DescriptionImage { get; set; }

        public string CarBodyTypeName  { get; set; }
     
        public string VehicleTypeName{ get; set; }
        public string Brand { get; set; }

    }
}

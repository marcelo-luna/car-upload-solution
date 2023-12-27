namespace FineData.Domain
{
    public class Car
    {
        public Guid Id { get; set; }
        public Person Person { get; set; }
        public string CarBrand { get; set; }
        public string CarModel { get; set; }
        public string Plate { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
    }
}

﻿namespace FineData.Domain
{
    public class Person
    {
        public Guid Id { get; set; }
        public string CodiceFiscale { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public IList<Car> Cars { get; set; }
    }
}

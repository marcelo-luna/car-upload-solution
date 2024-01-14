using CsvHelper;
using FineData.Models;
using System.Globalization;

namespace FineData.Services
{
    public class LoadFromCSVService : ILoadService
    {
        private readonly IApiService _apiService;

        public LoadFromCSVService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public IList<Car> ImportListOfCars(string filePath)
        {
            var cars = ReadCsv<Car>(filePath);
            _apiService.ImportCars(cars);
            return cars;
        }

        public IList<Person> ImportListOfPerson(string filePath)
        {
            var persons = ReadCsv<Person>(filePath);
            _apiService.ImportPeople(persons);

            return persons;
        }

        private List<T> ReadCsv<T>(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<T>().ToList();
            }
        }
    }
}

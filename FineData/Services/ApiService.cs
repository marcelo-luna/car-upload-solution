using FineData.Models;
using static System.Net.Mime.MediaTypeNames;
using System.Text;
using System.Text.Json;
using Azure;

namespace FineData.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri("http://localhost:5139/");
        }

        public async Task<IEnumerable<PersonCar>> GetPersonCars()
        {
            string carsJson = null!;
            string peopleJson = null!;
            var personCars = new List<PersonCar>();

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                // You can add other options as needed
            };

            using var httpResponseMessageCars =
                await _httpClient.GetAsync("/api/Car");

            using var httpResponseMessagePeople =
                await _httpClient.GetAsync("/api/Person");

            if (httpResponseMessageCars.IsSuccessStatusCode)
                carsJson = await httpResponseMessageCars.Content.ReadAsStringAsync();

            if (httpResponseMessagePeople.IsSuccessStatusCode)
                peopleJson = await httpResponseMessagePeople.Content.ReadAsStringAsync();

           var cars = System.Text.Json.JsonSerializer.Deserialize<IList<Car>>(carsJson, options);
           var people = System.Text.Json.JsonSerializer.Deserialize<IList<Person>>(peopleJson, options);

            foreach (var car in cars!)
            {
                var pc = new PersonCar
                {
                    Brand = car.CarBrand,
                    Model = car.CarMode,
                    Plate = car.Plate,
                    Person = people.FirstOrDefault(x => x.CodiceFiscale == car.CodiceFiscale).Name
                };

                personCars.Add(pc);
            }

            return personCars;
        }

        public async Task ImportCars(IEnumerable<Car> cars)
        {
            var carJson = new StringContent(
                JsonSerializer.Serialize(cars),
                Encoding.UTF8,
                Application.Json);

            using var httpResponseMessage =
                await _httpClient.PostAsync($"/api/Car", carJson);

            httpResponseMessage.EnsureSuccessStatusCode();
        }

        public async Task ImportPeople(IEnumerable<Person> people)
        {
            var peopleJson = new StringContent(
                JsonSerializer.Serialize(people),
                Encoding.UTF8,
                Application.Json);

            using var httpResponseMessage =
                await _httpClient.PostAsync($"/api/Person", peopleJson);

            httpResponseMessage.EnsureSuccessStatusCode();
        }
    }
}

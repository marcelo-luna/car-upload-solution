using FineData.Models;

namespace FineData.Services
{
    public interface IApiService
    {
        Task ImportPeople(IEnumerable<Person> people);
        Task ImportCars(IEnumerable<Car> cars);
        Task<IEnumerable<PersonCar>> GetPersonCars(); 
    }
}

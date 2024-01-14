using FineData.Models;

namespace FineData.Services
{
    public interface ILoadService
    {
        public IList<Person> ImportListOfPerson(string filePath);
        public IList<Car> ImportListOfCars(string filePath);
    }
}

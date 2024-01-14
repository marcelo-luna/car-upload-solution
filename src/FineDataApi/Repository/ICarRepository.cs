
using FineDataApi.Model;

namespace FineDataApi.Repository
{
    public interface ICarRepository
    {
        public Task ImportBulkCar(IList<Car> car);
        public Task<IEnumerable<Car>> GetAllCars();
    }
}

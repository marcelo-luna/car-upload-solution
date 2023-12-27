using FineData.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FineDataApi.Repository
{
    public class CarRepository : ICarRepository
    {
        private readonly FineDataContext _fineDataContext;

        public CarRepository(FineDataContext fineDataContext)
        {
            _fineDataContext = fineDataContext;
        }

        public async Task ImportBulkCar(IList<Model.Car> cars)
        {
            var carsToAdd = cars.Select(c => new FineData.Domain.Car
            {
                Year = c.Year,
                CarBrand = c.CarBrand,
                CarModel = c.CarMode,
                Color = c.Color,
                Plate = c.Plate,
                Person = _fineDataContext.People.FirstOrDefault(p => p.CodiceFiscale == c.CodiceFiscale)!
            });

            var carsWithoutOwner = carsToAdd.Where(c => c.Person == null);

            if (carsWithoutOwner.Any()) 
            {
                throw new Exception();
            }

            _fineDataContext.Cars.AddRange(carsToAdd);
            await _fineDataContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Model.Car>> GetAllCars()
        {
            var cars = await _fineDataContext.Cars.Include(c => c.Person).ToListAsync();

            return cars.Select(c => new Model.Car
            {
                CarBrand = c.CarBrand,
                CarMode = c.CarModel,
                Color = c.Color,
                Plate = c.Plate,
                Year = c.Year,
                CodiceFiscale = _fineDataContext.People.First(p => p.Id == c.Person.Id).CodiceFiscale
            });
        }
    }
}

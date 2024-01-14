using FineData.Domain;
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
            var carsToUpdate = new List<FineData.Domain.Car>();

            foreach (var car in cars)
            {
                var carDb = _fineDataContext.Cars.FirstOrDefault(c => c.Plate == car.Plate);

                if (carDb != null)
                {
                    carDb.CarBrand = car.CarBrand;
                    carDb.Year = car.Year;
                    carDb.Color = car.Color;
                    carDb.CarModel = car.CarMode;
                    carDb.Person = _fineDataContext.People.FirstOrDefault(p => p.CodiceFiscale == car.CodiceFiscale)!;

                    carsToUpdate.Add(carDb);
                }
                else
                {
                    var carToAdd = new Car
                    {
                        Year = car.Year,
                        CarBrand = car.CarBrand,
                        CarModel = car.CarMode,
                        Color = car.Color,
                        Plate = car.Plate,
                        Person = _fineDataContext.People.FirstOrDefault(p => p.CodiceFiscale == car.CodiceFiscale)!
                    };

                    carsToUpdate.Add(carToAdd);
                }
            }

            var carsWithoutOwner = carsToUpdate.Where(c => c.Person == null);

            if (carsWithoutOwner.Any()) 
            {
                throw new Exception("Car(s) Without Owner Found");
            }
            
            _fineDataContext.Cars.UpdateRange(carsToUpdate);
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

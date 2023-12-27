using FineDataApi.Model;
using FineDataApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FineDataApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarRepository _carRepository;

        public CarController(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        [HttpPost(Name = "PostCar")]
        public async Task<IActionResult> Post(IList<Car> cars)
        {
            await _carRepository.ImportBulkCar(cars);

            return Ok();
        }

        [HttpGet(Name = "GetCar")]
        public async Task<IEnumerable<Car>> Get()
        {
            var cars = await _carRepository.GetAllCars();

            return cars;
        }
    }
}

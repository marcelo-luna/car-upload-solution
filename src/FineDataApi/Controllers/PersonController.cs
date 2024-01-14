using FineDataApi.Model;
using FineDataApi.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace FineDataApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;

        public PersonController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        [HttpPost(Name = "PostPeople")]
        public async Task<IActionResult> Post(IList<Person> people)
        {
            await _personRepository.ImportBulkPersonAsync(people);

            return Ok();
        }

        [HttpGet(Name = "GetPeople")]
        public async Task<IEnumerable<Person>> Get()
        {
            var people = await _personRepository.GetAllPeopleAsync();

            return people;
        }
    }
}

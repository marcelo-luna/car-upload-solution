using FineData.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FineDataApi.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly FineDataContext _fineDataContext;

        public PersonRepository(FineDataContext fineDataContext)
        {
            _fineDataContext = fineDataContext;
        }

        public async Task<IEnumerable<Model.Person>> GetAllPeopleAsync()
        {
            var people = await _fineDataContext.People.ToListAsync();

            return people.Select(p => new Model.Person
            {
                Address = p.Address,
                Birthday = p.Birthday,
                City = p.City,
                CodiceFiscale = p.CodiceFiscale,
                Country = p.Country,
                Name = p.Name,
                Phone = p.Phone,
                Province = p.Province
            });
        }

        public async Task ImportBulkPersonAsync(IList<Model.Person> people)
        {
            var personToAdd = people.Select(p => new FineData.Domain.Person
            {
                Address = p.Address,
                Birthday = p.Birthday,
                City = p.City,
                CodiceFiscale = p.CodiceFiscale,
                Country = p.Country,
                Name = p.Name,
                Phone = p.Phone,
                Province = p.Province,
            });

            _fineDataContext.People.AddRange(personToAdd);
            await _fineDataContext.SaveChangesAsync();
        }
    }
}

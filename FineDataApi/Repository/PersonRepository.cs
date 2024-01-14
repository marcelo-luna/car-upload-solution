using FineData.Persistence;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
            var toUpdate = new List<FineData.Domain.Person>();

            foreach (var person in people)
            {
                var personDb = _fineDataContext?.People.FirstOrDefault(p => p.CodiceFiscale == person.CodiceFiscale);

                if (personDb != null)
                {
                    personDb.Province = person.Province;
                    personDb.Country = person.Country;
                    personDb.Address = person.Address;
                    personDb.Birthday = person.Birthday;
                    personDb.City = person.City;
                    personDb.Phone = person.Phone;
                    personDb.Name = person.Name;

                    toUpdate.Add(personDb);
                }
                else
                {
                    var personToAdd = new FineData.Domain.Person
                    {
                        CodiceFiscale = person.CodiceFiscale,
                        Name = person.Name,
                        Address = person.Address,
                        City = person.City,
                        Province = person.Province,
                        Country = person.Country,
                        Birthday = person.Birthday,
                        Phone = person.Phone,
                    };

                    toUpdate.Add(personToAdd);
                }
            }

            //var domainPeople = people.Distinct().Select(p => new FineData.Domain.Person
            //{
            //    Address = p.Address,
            //    Birthday = p.Birthday,
            //    City = p.City,
            //    CodiceFiscale = p.CodiceFiscale,
            //    Country = p.Country,
            //    Name = p.Name,
            //    Phone = p.Phone,
            //    Province = p.Province
            //});


            ////if (_fineDataContext?.People?.Any() == true)
            ////{
            //toUpdate = _fineDataContext
            //.People
            //?.ToList()
            //.Where(p => people.Any(pm => pm?.CodiceFiscale == p?.CodiceFiscale))
            //.ToList();

            //toUpdate = people!.Select(p => new FineData.Domain.Person
            //{
            //    Address = p.Address,
            //    Birthday = p.Birthday,
            //    City = p.City,
            //    CodiceFiscale = p.CodiceFiscale,
            //    Country = p.Country,
            //    Name = p.Name,
            //    Phone = p.Phone,
            //    Province = p.Province,
            //}).ToList();

            _fineDataContext.People?.UpdateRange(toUpdate);

            //}

            //var toInsert = domainPeople.Where(tu => !toUpdate.Any(p => tu.CodiceFiscale == p.CodiceFiscale));

            //if (toInsert.Any() == true)
            //    _fineDataContext.People?.AddRange(toInsert);

            await _fineDataContext.SaveChangesAsync();
        }
    }
}

using FineDataApi.Model;

namespace FineDataApi.Repository
{
    public interface IPersonRepository
    {
        public Task ImportBulkPersonAsync(IList<Person> person);
        public Task<IEnumerable<Person>> GetAllPeopleAsync();
    }
}

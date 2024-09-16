using SkepERP.Models;

namespace SkepERP.Interfaces
{
    public interface IPersonRepository
    {
        public ICollection<Person> GetPersons();
    }
}

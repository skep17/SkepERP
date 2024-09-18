using SkepERP.Dto;
using SkepERP.Models;

namespace SkepERP.Interfaces
{
    public interface IPersonRepository
    {
        public ICollection<PersonDto> GetPersons();
        public PersonDto? GetPersonById(int id);
        public PersonDto? CreatePerson(CreatePersonDto person);
    }
}

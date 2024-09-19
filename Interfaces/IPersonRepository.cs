using SkepERP.Dto;
using SkepERP.Models;

namespace SkepERP.Interfaces
{
    public interface IPersonRepository
    {
        public ICollection<PersonDto> GetPersons();
        public PersonDto? GetPersonById(int id);
        public PersonDto? CreatePerson(CreatePersonDto person);
        public PersonDto? UpdatePerson(int id, UpdatePersonDto person);
        public PersonDto? AddPhones(int id, UpdatePhoneDto phones);
        public PersonDto? RemovePhones(int id, UpdatePhoneDto phones);
        public PersonDto? AddRelations(int id, UpdateRelationsDto relations);
        public PersonDto? RemoveRelations(int id, UpdateRelationsDto relations);
        public void DeletePerson(int id);
    }
}

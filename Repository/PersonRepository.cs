using SkepERP.Data;
using SkepERP.Interfaces;
using SkepERP.Models;

namespace SkepERP.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly DataContext _context;

        public PersonRepository(DataContext context) 
        {
            _context = context;
        }

        public ICollection<Person> GetPersons()
        {
            var persons = _context.Person.ToList();
            /*
            foreach (Person p in persons)
            {
                p.Phones = _context.Phone.Where(ph => ph.PersonId == p.Id).ToList();
                p.PersonalRelations = _context.PersonalRelations.Where(rel => rel.PersonId == p.Id).ToList();
            }
            */
            return persons;
        }
    }
}

using SkepERP.Data;
using SkepERP.Dto;
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

        public ICollection<PersonDto> GetPersons()
        {
            var persons = _context.Person
                                  .Select(p => new PersonDto
                                  {
                                      Id = p.Id,
                                      FirstName = p.FirstName,
                                      LastName = p.LastName,
                                      Gender = p.Gender.ToString(),
                                      IdNum = p.IdNum,
                                      DateOfBirth = p.DateOfBirth,
                                      Phones = _context.Phone
                                                       .Where(ph => ph.PersonId == p.Id)
                                                       .Select(ph => new PhoneDto
                                                       {
                                                            PhoneType = ph.Type.ToString(),
                                                            PhoneNumber = ph.Number,
                                                       })
                                                       .ToList(),
                                      PersonalRelations = _context.PersonalRelations
                                                .Where(rel => rel.PersonId == p.Id)
                                                .Select(rel => new RelationDto
                                                {
                                                    RelationType = rel.Type.ToString(),
                                                    PersonId = rel.RelatedPersonId,
                                                })
                                                .ToList(),
                                  })
                                  .ToList();

            return persons;
        }

        public PersonDto? GetPersonById(int id)
        {
            PersonDto? ret = null;

            var person = _context.Person.Find(id);

            if (person == null)
            {
                // TODO : log the error for invalid id
            }
            else
            {
                ret = new PersonDto
                {
                    Id = person.Id,
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    Gender = person.Gender.ToString(),
                    IdNum = person.IdNum,
                    DateOfBirth = person.DateOfBirth,
                    Phones = _context.Phone
                                        .Where(ph => ph.PersonId == person.Id)
                                        .Select(ph => new PhoneDto
                                        {
                                            PhoneType = ph.Type.ToString(),
                                            PhoneNumber = ph.Number,
                                        })
                                        .ToList(),
                    PersonalRelations = _context.PersonalRelations
                                                .Where(rel => rel.PersonId == person.Id)
                                                .Select(rel => new RelationDto
                                                {
                                                    RelationType = rel.Type.ToString(),
                                                    PersonId = rel.RelatedPersonId,
                                                })
                                                .ToList(),
                };
            }

            return ret;
        }

        public PersonDto? CreatePerson(CreatePersonDto person)
        {
            PersonDto? ret = null;

            var np = new Person
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                Gender = (Gender)person.Gender,
                IdNum = person.IdNum,
                DateOfBirth = person.DateOfBirth,
                Phones = new List<Phone>(),
            };

            string msg = np.Validate();

            if (!string.IsNullOrEmpty(msg)) throw new ArgumentException(msg);

            var phones = person.Phones
                                .Select(ph => new Phone
                                {
                                    Type = (PhoneType)ph.PhoneType,
                                    Number = ph.PhoneNumber,
                                }).ToList();

            foreach ( var ph in phones )
            {
                msg = np.Validate();
                if (!string.IsNullOrEmpty(msg)) throw new ArgumentException(msg);
                np.Phones.Add(ph);
            }

            _context.Person.Add(np);
            _context.SaveChanges();

            ret = GetPersonById(np.Id);

            return ret;
        }
    }
}

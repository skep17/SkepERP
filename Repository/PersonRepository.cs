using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SkepERP.Data;
using SkepERP.Dto;
using SkepERP.Helpers;
using SkepERP.Interfaces;
using SkepERP.Models;
using System;
using System.Reflection;

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

            if (person != null)
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

            if (person.Phones != null)
            {
                var phones = person.Phones
                                .Select(ph => new Phone
                                {
                                    Type = (PhoneType)ph.PhoneType,
                                    Number = ph.PhoneNumber,
                                }).ToList();

                foreach (var ph in phones)
                {
                    msg = ph.Validate();
                    if (!string.IsNullOrEmpty(msg)) throw new ArgumentException(msg);
                    np.Phones.Add(ph);
                }
            }

            _context.Person.Add(np);
            _context.SaveChanges();

            ret = GetPersonById(np.Id);

            return ret;
        }

        public PersonDto? UpdatePerson(int id, UpdatePersonDto person)
        {
            PersonDto? ret = null;

            if (person == null) throw new ArgumentNullException(nameof(person));

            var oldPerson = _context.Person
                                    .Include(p => p.Phones)
                                    .First(p => p.Id == id);

            if (oldPerson == null) throw new ArgumentException($"Person with id {id} not found.");

            if (person.FirstName != null) oldPerson.FirstName = person.FirstName;
            if (person.LastName != null) oldPerson.LastName = person.LastName;
            if (person.IdNum != null) oldPerson.IdNum = person.IdNum;
            if (person.Gender != null) oldPerson.Gender = (Gender)person.Gender;
            if (person.DateOfBirth != null) oldPerson.DateOfBirth = (DateOnly)person.DateOfBirth;

            string msg = oldPerson.Validate();

            if (!string.IsNullOrEmpty(msg)) throw new ArgumentException(msg);

            if (person.Phones != null)
            {
                if (!oldPerson.Phones.IsNullOrEmpty()) _context.Phone.RemoveRange(oldPerson.Phones);

                oldPerson.Phones = person.Phones.Select(ph => new Phone
                {
                    Type = (PhoneType)ph.PhoneType,
                    Number = ph.PhoneNumber
                }).ToList();

                foreach (var ph in oldPerson.Phones)
                {
                    msg = ph.Validate();
                    if (!string.IsNullOrEmpty(msg)) throw new ArgumentException(msg);
                }
            }

            _context.Person.Update(oldPerson);
            _context.SaveChanges();

            ret = GetPersonById(oldPerson.Id);

            return ret;
        }

        public PersonDto? AddPhones(int id, UpdatePhoneDto phones)
        {
            PersonDto? ret = null;

            if (phones == null || phones.Phones == null) throw new ArgumentNullException(nameof(phones));
            var oldPerson = _context.Person
                                    .Include(p => p.Phones)
                                    .First(p => p.Id == id);

            if (oldPerson == null) throw new ArgumentException($"Person with id {id} not found.");

            if (oldPerson.Phones == null) oldPerson.Phones = new List<Phone>();

            string msg = string.Empty;

            foreach (var ph in phones.Phones)
            {
                Phone cur = new Phone
                {
                    Type = (PhoneType)ph.PhoneType,
                    Number = ph.PhoneNumber,
                };
                msg = cur.Validate();
                if (!string.IsNullOrEmpty(msg)) throw new ArgumentException(msg);
                oldPerson.Phones.Add(cur);
            }

            _context.Person.Update(oldPerson);
            _context.SaveChanges();

            ret = GetPersonById(oldPerson.Id);

            return ret;
        }

        public PersonDto? RemovePhones(int id, UpdatePhoneDto phones)
        {
            PersonDto? ret = null;

            var oldPerson = _context.Person
                                    .Include(p => p.Phones)
                                    .First(p => p.Id == id);

            if (oldPerson == null) throw new ArgumentException($"Person with id {id} not found.");

            if (oldPerson.Phones.IsNullOrEmpty())
            {
                throw new ArgumentException($"Person with id {id} doesn't have any phones.");
            }
            else
            {
                if (phones == null || phones.Phones.IsNullOrEmpty())
                {
                    _context.Phone.RemoveRange(oldPerson.Phones);
                }
                else
                {
                    foreach(var ph in phones.Phones)
                    {
                        var cur = _context.Phone.Where(p => p.Type == (PhoneType)ph.PhoneType && p.Number == ph.PhoneNumber).ToList();
                        if (cur.IsNullOrEmpty())
                        {
                            throw new ArgumentException($"Person with id {id} doesn't have a phone with type {ph.PhoneType} and number {ph.PhoneNumber}.");
                        }
                        else
                        {
                            _context.Phone.RemoveRange(cur);
                        }
                    }
                }
            }

            _context.SaveChanges();

            ret = GetPersonById(oldPerson.Id);

            return ret;
        }

        public PersonDto? AddRelations(int id, UpdateRelationsDto relations)
        {
            PersonDto? ret = null;

            var oldPerson = _context.Person
                                    .Include(p => p.PersonalRelations)
                                    .First(p => p.Id == id);

            if (oldPerson == null) throw new ArgumentException($"Person with id {id} not found.");

            if (relations == null || relations.Relations.IsNullOrEmpty()) throw new ArgumentNullException(nameof(relations));

            if (oldPerson.PersonalRelations == null) oldPerson.PersonalRelations = new List<PersonalRelation>();

            string msg = string.Empty;

            foreach (var rel in relations.Relations)
            {
                PersonalRelation cur = new PersonalRelation
                {
                    Type = (RelationType)rel.RelationType,
                    PersonId = id,
                    RelatedPersonId = rel.PersonId,
                };
                msg = cur.Validate();
                if (!string.IsNullOrEmpty(msg)) throw new ArgumentException(msg);

                var relatedPerson = _context.Person
                                            .Include(p => p.PersonalRelations)
                                            .First(p => p.Id == rel.PersonId);

                if (relatedPerson == null) throw new ArgumentException($"Person with id {rel.PersonId} not found to add in relations for person with {id}.");

                if (relatedPerson.PersonalRelations == null) relatedPerson.PersonalRelations = new List<PersonalRelation>();
                relatedPerson.PersonalRelations.Add(new PersonalRelation
                {
                    Type = cur.Type,
                    PersonId = relatedPerson.Id,
                    RelatedPersonId = oldPerson.Id,
                });

                oldPerson.PersonalRelations.Add(cur);
                _context.Person.Update(relatedPerson);
            }

            _context.Person.Update(oldPerson);
            _context.SaveChanges();

            ret = GetPersonById(oldPerson.Id);

            return ret;
        }

        public PersonDto? RemoveRelations(int id, UpdateRelationsDto relations)
        {
            PersonDto? ret = null;

            var oldPerson = _context.Person
                                    .Include(p => p.PersonalRelations)
                                    .First(p => p.Id == id);

            if (oldPerson == null) throw new ArgumentException($"Person with id {id} not found.");

            if (oldPerson.PersonalRelations.IsNullOrEmpty())
            {
                throw new ArgumentException($"Person with id {id} doesn't have any personal relations.");
            }
            else
            {
                if (relations == null || relations.Relations.IsNullOrEmpty())
                {
                    _context.PersonalRelations.RemoveRange(oldPerson.PersonalRelations);
                }
                else
                {
                    foreach (var rel in relations.Relations)
                    {
                        var cur = _context.PersonalRelations
                            .Where(r => r.Type == (RelationType)rel.RelationType
                                && (
                                    (r.PersonId == id && r.RelatedPersonId == rel.PersonId)
                                    ||
                                    (r.PersonId == rel.PersonId && r.RelatedPersonId == id)
                                )
                            )
                            .ToList();
                        if (cur.IsNullOrEmpty())
                        {
                            throw new ArgumentException($"Person with id {id} doesn't have a relation with type {rel.RelationType} to person {rel.PersonId}.");
                        }
                        else
                        {
                            _context.PersonalRelations.RemoveRange(cur);
                        }
                    }
                }
            }

            _context.SaveChanges();

            ret = GetPersonById(oldPerson.Id);

            return ret;
        }

        public void DeletePerson(int id)
        {
            var person = _context.Person.First(p => p.Id == id);

            if (person == null) throw new ArgumentException($"Person with id {id} not found.");

            var phones = _context.Phone.Where(ph => ph.PersonId == person.Id);
            var relations = _context.PersonalRelations.Where(rel => rel.PersonId == person.Id || rel.RelatedPersonId == person.Id).ToList();

            _context.Phone.RemoveRange(phones);
            _context.PersonalRelations.RemoveRange(relations);
            _context.Person.Remove(person);
            _context.SaveChanges();
        }

        public ICollection<PersonDto> GetPersonsLike(PersonSearchLike person)
        {
            ArgumentNullException.ThrowIfNull(person);

            var filteredPersons = _context.Person
                .Where(p =>
                    (!string.IsNullOrEmpty(person.FirstName) && p.FirstName.Contains(person.FirstName)) ||
                    (!string.IsNullOrEmpty(person.LastName) && p.LastName.Contains(person.LastName)) ||
                    (!string.IsNullOrEmpty(person.IdNum) && p.IdNum.Contains(person.IdNum))
                );

            var personList = filteredPersons
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

            return personList;
        }

        public ICollection<PersonDto> GetPersonsDetailed(PersonSearchDetailed person)
        {
            ArgumentNullException.ThrowIfNull(person);

            Validator.ValidateOrThrow(person.FirstName, Person.ValidateName);

            Validator.ValidateOrThrow(person.LastName, Person.ValidateName);

            Validator.ValidateOrThrow(person.IdNum, Person.ValidateIdNum);

            Validator.ValidateOrThrow((Gender)person.Gender, Person.ValidateGender);

            Validator.ValidateOrThrow(person.DateOfBirth, Person.ValidateAge);

            var filteredPersons = _context.Person
                .Where(p =>
                    p.FirstName == person.FirstName &&
                    p.LastName == person.LastName &&
                    p.IdNum == person.IdNum &&
                    p.Gender == (Gender)person.Gender &&
                    p.DateOfBirth == person.DateOfBirth
                );

            var personList = filteredPersons
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

            return personList;
        }

        public ICollection<PersonDto> GetPersonsPaged(int pageNum)
        {
            PersonCount personCount = GetPersonCount();

            if(pageNum < personCount.TotalPages || pageNum > personCount.TotalPages)
            {
                throw new ArgumentOutOfRangeException();
            }

            int skip = (pageNum - 1) * PersonDto.PAGESIZE;

            var personList = _context.Person
                                     .OrderBy(p => p.Id)
                                     .Skip(skip)
                                     .Take(PersonDto.PAGESIZE)
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

            return personList;
        }

        public PersonCount GetPersonCount()
        {
            PersonCount personCount = new PersonCount();

            personCount.TotalNumber = _context.Person.Count();
            personCount.PageSize = PersonDto.PAGESIZE;
            personCount.TotalPages = personCount.TotalNumber / personCount.PageSize;
            if(personCount.TotalNumber % personCount.PageSize > 0) personCount.TotalPages++;

            return personCount;
        }
    }
}

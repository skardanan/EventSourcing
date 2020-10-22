using EventSourcing.Abstraction;
using EventSourcing.ApplicationService.Dtos;
using System;
using System.Threading.Tasks;
using EventSourcing.Domain.Entities;
namespace EventSourcing.ApplicationService.Services
{
    public class PersonServices : IPersonService
    {
        public PersonServices(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        private readonly IPersonRepository _personRepository;
        public async Task<bool> Create(PersonDto person)
        {
            var personModel = Person.Create(person.Name,
                                            person.Family,
                                            person.NationalCode,
                                            person.MotherName,
                                            person.FatherName,
                                            person.BirthDate);
            return await _personRepository.SaveAsync(personModel);
        }

        public async Task<PersonDto> Get(string personId)
        {
            var person = await _personRepository.GetAsync(Guid.Parse(personId));
            if (person.IsDeleted) throw new ArgumentNullException("داده یافت نشد");
            return new PersonDto()
            {
                BirthDate = person.BirthDate,
                Name = person.Name,
                Family = person.Family,
                FatherName = person.FatherName,
                MotherName = person.MotherName,
                NationalCode = person.NationalCode
            };
        }
        public async Task<bool> Update(string personId, PersonDto person)
        {
            var personData = await _personRepository.GetAsync(Guid.Parse(personId));
            if (personData.IsDeleted) throw new ArgumentNullException("داده یافت نشد");
            personData.Changed(personId, person.Name,
                                        person.Family,
                                        person.NationalCode,
                                        person.MotherName,
                                        person.FatherName,
                                        person.BirthDate);
            var res = await _personRepository.SaveAsync(personData);
            return res;
        }
        public async Task<bool> Delete(string personId)
        {
            var person = await _personRepository.GetAsync(Guid.Parse(personId));
            person.DeletePerson(personId);
            var res = await _personRepository.SaveAsync(person);
            return res;
        }
    }
}

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
        public async Task Create(PersonDto person)
        {
            var personModel = Person.Create(person.Name,
                                            person.Family,
                                            person.NationalCode,
                                            person.MotherName,
                                            person.FatherName,
                                            person.BirthDate);
             await _personRepository.SaveAsync(personModel);
        }

        public async Task<PersonDto> Get(string personId)
        {
            var person = await _personRepository.GetAsync(Guid.Parse(personId));
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
    }
}

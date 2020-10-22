using EventSourcing.Database;
using EventSourcing.Domain.Entities;
using EventSourcing.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.DataAccess.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IEventHandler _eventHandler;

        public PersonRepository(IEventHandler eventHandler)
        {
            _eventHandler = eventHandler;
        }
        public async Task<Person> GetAsync(Guid personId)
        {
            var eventList = await _eventHandler.LoadEventsAsync(personId, typeof(Person).Name);
            return new Person(eventList);
        }

        public async Task<bool> SaveAsync(Person person)
        {
           return await _eventHandler.SaveEventAsync(person.PersonId, person.GetType().Name,person.Version, person.EventList);
        }
        public async Task<bool> DeleteAsync(Guid personId)
        {
            return await _eventHandler.DeleteEventsAsync(personId, typeof(Person).Name);
        }
    }
}

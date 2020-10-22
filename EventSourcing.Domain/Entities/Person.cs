using EventSourcing.Domain.Common;
using EventSourcing.Domain.DomainEvents;
using System;
using System.Collections.Generic;

namespace EventSourcing.Domain.Entities
{
    public class Person : AggregateRoot
    {
        public Person()
        {
        }
        public Person(IEnumerable<IEventModel> events) : base(events)
        {
        }
        public Person(string name, string family, string nationalCode, string motherName, string fatherName, string birthDate)
        {
            Name = name;
            Family = family;
            NationalCode = nationalCode;
            MotherName = motherName;
            FatherName = fatherName;
            BirthDate = birthDate;
        }
        public Guid PersonId { get; private set; }
        public string Name { get; private set; }
        public string Family { get; private set; }
        public string NationalCode { get; private set; }
        public string MotherName { get; private set; }
        public string FatherName { get; private set; }
        public string BirthDate { get; private set; }
        public static Person Create(string name, string family, string nationalCode, string motherName, string fatherName, string birthDate)
        {
            var person = new Person();
            var createEvent = new PersonCreated(Guid.NewGuid().ToString(), name, family, motherName, fatherName, birthDate, nationalCode);
            person.ApplyEvent(createEvent);
            return person;
        }
        public void On(PersonCreated @event)
        {
            PersonId = Guid.Parse(@event.PersonId);
            Name = @event.Name;
            Family = @event.Family;
            MotherName = @event.MotherName;
            FatherName = @event.FatherName;
            NationalCode = @event.NationalCode;
            BirthDate = @event.BirthDate;
        }
        public void DeletePerson(string personId)
        {
            ApplyEvent(new PersonDeleted(true, personId));
        }
        public void On(PersonDeleted @event)
        {
            PersonId = Guid.Parse(@event.PersonId);
            IsDeleted = @event.IsDeleted;
        }
        public void Changed(string personId, string name, string family, string nationalCode, string motherName, string fatherName, string birthDate)
        {
            var createEvent = new PersonDataChanged(personId, name, family, motherName, fatherName, birthDate, nationalCode);
            ApplyEvent(createEvent);
        }
        public void On(PersonDataChanged @event)
        {
            PersonId = Guid.Parse(@event.PersonId);
            Name = @event.Name;
            Family = @event.Family;
            MotherName = @event.MotherName;
            FatherName = @event.FatherName;
            NationalCode = @event.NationalCode;
            BirthDate = @event.BirthDate;
        }
    }
}

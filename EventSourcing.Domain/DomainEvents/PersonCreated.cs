using EventSourcing.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventSourcing.Domain.DomainEvents
{
    public class PersonCreated : IEventModel
    {
        public PersonCreated(string personId,string name, string family, string birthDate, string fatherName, string motherName, string nationalCode)
        {
            PersonId = personId;
            Name = name;
            Family = family;
            BirthDate = birthDate;
            FatherName = fatherName;
            MotherName = motherName;
            NationalCode = nationalCode;
        }
        public string PersonId { get; }
        public string Name { get; }
        public string Family { get; }
        public string BirthDate { get; }
        public string FatherName { get; }
        public string MotherName { get; }
        public string NationalCode { get; }

    }
}

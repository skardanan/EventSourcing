using EventSourcing.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventSourcing.Domain.DomainEvents
{
    public class PersonDeleted : IEventModel
    {
        public PersonDeleted(bool isDeleted, string personId)
        {
            IsDeleted = isDeleted;
            PersonId = personId;
        }
        public bool IsDeleted { get; }
        public string PersonId { get; }
    }
}

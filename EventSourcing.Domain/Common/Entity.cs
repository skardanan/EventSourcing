using System;

namespace EventSourcing.Domain.Common
{
    public class Entity
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}

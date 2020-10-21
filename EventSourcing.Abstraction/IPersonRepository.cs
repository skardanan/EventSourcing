using EventSourcing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.Abstraction
{
    public interface IPersonRepository
    {
        Task SaveAsync(Person person);
        Task<Person> GetAsync(Guid personId);
    }
}

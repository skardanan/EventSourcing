using EventSourcing.Domain.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventSourcing.Database.EventStore
{
    public class EventStoreHandler : IEventHandler
    {
        public Task<IReadOnlyCollection<IEventModel>> LoadEventsAsync(Guid aggregateRootId, string aggregateName)
        {
            throw new NotImplementedException();
        }

        public Task SaveEventAsync(Guid aggregateRootId, string aggregateName, IReadOnlyCollection<IEventModel> events)
        {
            throw new NotImplementedException();
        }
    }
}

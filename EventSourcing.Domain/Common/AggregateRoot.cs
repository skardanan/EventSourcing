using System.Collections.Generic;

namespace EventSourcing.Domain.Common
{
    public class AggregateRoot : Entity
    {
        public AggregateRoot()
        {
        }
        public AggregateRoot(IEnumerable<IEventModel> events)
        {
            if (events == null) return;
            foreach (var item in events)
            {
                Mutate(item);
                Version++;
            }
        }
        public int Version { get; private set; } = -1;

        private readonly List<IEventModel> _eventList = new List<IEventModel>();
        public IReadOnlyCollection<IEventModel> EventList => _eventList.AsReadOnly();
        public void ApplyEvent(IEventModel @event)
        {
            Mutate(@event);
            _eventList.Add(@event);
        }
        private void Mutate(IEventModel @event)
        {
            ((dynamic)this).On((dynamic)@event);
        }
    }
}

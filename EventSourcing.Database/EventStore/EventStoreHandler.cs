using EventSourcing.Domain.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Newtonsoft.Json;
namespace EventSourcing.Database.EventStore
{
    public class EventStoreHandler : IEventHandler
    {
        public EventStoreHandler(IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("EventStore").GetSection("ConnectionString").Value;
            _eventStoreConnection = EventStoreConnection.Create(connectionString, ConnectionSettings.Create().DisableTls().KeepReconnecting(), "EventSourcingApp");
            _eventStoreConnection.ConnectAsync().Wait();
        }

        private readonly IEventStoreConnection _eventStoreConnection;
        private readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All,
            NullValueHandling = NullValueHandling.Ignore
        };

        public async Task<IReadOnlyCollection<IEventModel>> LoadEventsAsync(Guid aggregateRootId, string aggregateName)
        {
            string streamName = $"{aggregateName}-{aggregateRootId}";
            var page = await _eventStoreConnection.ReadStreamEventsForwardAsync(streamName, 0, 1024, false);
            var domainEvents = page.Events.Select(c => Deserialize(c.Event.Data) as IEventModel).ToList();

            return domainEvents;
        }

        public async Task<bool> SaveEventAsync(Guid aggregateRootId, string aggregateName, int version, IReadOnlyCollection<IEventModel> events)
        {
            string streamName = $"{aggregateName}-{aggregateRootId}";

            var changes = events
                           .Select(@event =>
                               new EventData(
                                   eventId: Guid.NewGuid(),
                                   type: @event.GetType().Name,
                                   isJson: true,
                                   data: Serialize(@event),
                                   metadata: Serialize(new
                                   {
                                       ClrType = @event.GetType().AssemblyQualifiedName,
                                       Version = version
                                   })
                               ))
                           .ToArray();


            await _eventStoreConnection.AppendToStreamAsync(streamName, version, changes);
            return true;
        }
        private byte[] Serialize(object data)
          => System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data, _jsonSerializerSettings));
        private object Deserialize(byte[] data)
        {
            var jsonData = System.Text.Encoding.UTF8.GetString(data);
            return JsonConvert.DeserializeObject(jsonData, _jsonSerializerSettings);
        }

    }
}

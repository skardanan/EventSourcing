using EventSourcing.Domain.Common;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;
using Newtonsoft.Json;
namespace EventSourcing.Database.SqlServer
{
    public class SqlHandler : IEventHandler
    {
        public SqlHandler(IConfiguration configuration)
        {
            _connectionString = configuration.GetSection("SqlDB").GetSection("ConnectionString").Value;
        }
        private readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All,
            NullValueHandling = NullValueHandling.Ignore
        };
        private readonly string _connectionString;
        public async Task<bool> SaveEventAsync(Guid AggregateRootId, string AggregateName, int version, IReadOnlyCollection<IEventModel> events)
        {
            if (events.Count <= 0) return false;

            string query = @"Insert into EventStore(Id, CreatedAt, Name, AggregateId, Data, Aggregate,Version) 
                            Values(@Id, @CreatedAt, @Name, @AggregateId, @Data, @Aggregate,@Version)";

            var createdAt = DateTime.Now;

            var values = events.Select(e => new
            {
                Aggregate = AggregateName,
                Id = Guid.NewGuid(),
                CreatedAt = createdAt,
                AggregateId = AggregateRootId.ToString(),
                Data = JsonConvert.SerializeObject(e, Formatting.Indented, _jsonSerializerSettings),
                Name = e.GetType().Name,
                Version = ++version
            });

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var res = await sqlConnection.ExecuteAsync(query, values);
                return res > 0;
            }
        }
        public async Task<IReadOnlyCollection<IEventModel>> LoadEventsAsync(Guid aggregateRootId, string aggregateName)
        {
            if (aggregateRootId == null) throw new InvalidOperationException("AggregateRootId cannot be null");
            if (string.IsNullOrWhiteSpace(aggregateName)) throw new InvalidOperationException("AggregateName cannot be null");

            var query = $"select * from EventStore where AggregateId = @AggregateId and Aggregate= @Aggregate order by Version,CreatedAt ";
            var value = new
            {
                AggregateId = aggregateRootId,
                Aggregate = aggregateName
            };
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var result = await sqlConnection.QueryAsync(query, value);
                return result.Select(item => JsonConvert.DeserializeObject(item.Data, _jsonSerializerSettings) as IEventModel).ToList();
            }
        }
    }

}

﻿using EventSourcing.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.Database
{
    public interface IEventHandler
    {
        Task<bool> SaveEventAsync(Guid aggregateRootId, string aggregateName,int version, IReadOnlyCollection<IEventModel> events);
        Task<IReadOnlyCollection<IEventModel>> LoadEventsAsync(Guid aggregateRootId, string aggregateName);
    }
}

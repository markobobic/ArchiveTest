using ArchiveData.Model;
using System.Collections.Generic;
using System.Linq;

namespace ArchiveData.Extensions
{
    public static class ArchiveExtensions
    {
        public static IEnumerable<ArchivedInputNotification> ToArchived(this IEnumerable<InputNotificationEventEntity> entites) =>
           entites.Select(x => new ArchivedInputNotification()
           {
               EventDefinition = x.EventDefinition,
               EventId = x.EventId,
               ClientId = x.ClientId,
               EventDefinitionId = x.EventDefinitionId,
               AcknowledgmentTimeStampUtc = x.AcknowledgmentTimeStampUtc,
               EventTargetId = x.EventTargetId,
               SourceEventTimeStampUtc = x.SourceEventTimeStampUtc

           });
    }
}

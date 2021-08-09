using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArchiveData.Model
{
    public class ArchivedInputNotification 
    {
        [Key]
        public Guid EventId { get; set; }
        public Guid EventDefinitionId { get; set; }

        [ForeignKey("EventDefinitionId")]
        public InputNotificationEventDefinitionEntity EventDefinition { get; set; } = new InputNotificationEventDefinitionEntity();
       
        public Guid EventTargetId { get; set; }
        public DateTime SourceEventTimeStampUtc { get; set; }
        public DateTime AcknowledgmentTimeStampUtc { get; set; }
       
        public string ClientId { get; set; }
    }
}

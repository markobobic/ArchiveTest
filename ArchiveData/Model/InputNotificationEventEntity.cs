using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArchiveData.Model
{
    [Index(nameof(SourceEventTimeStampUtc), Name = "IX_SourceEventTimeStampUTC")]

    public class InputNotificationEventEntity
    {
        [Key]
        public Guid EventId { get; set; }
        public Guid EventDefinitionId { get; set; }

        [ForeignKey("EventDefinitionId")]
        public InputNotificationEventDefinitionEntity EventDefinition { get; set; } = new InputNotificationEventDefinitionEntity();
        [StringLength(maximumLength: 36)]
        public Guid EventTargetId { get; set; }
        public DateTime SourceEventTimeStampUtc { get; set; }
        public DateTime AcknowledgmentTimeStampUtc { get; set; }
        public string ClientId { get; set; }

    }
}

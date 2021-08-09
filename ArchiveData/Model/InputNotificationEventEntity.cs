using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ArchiveData.Model
{
    [Index(nameof(SourceEventTimeStampUtc), Name = "IX_SourceEventTimeStampUTC")]

    public class InputNotificationEventEntity
    {
        [Key]
        [StringLength(maximumLength: 36)]
        public string EventId { get; set; }
        public string EventDefinitionId { get; set; }

        [ForeignKey("EventDefinitionId")]
        public InputNotificationEventDefinitionEntity EventDefinition { get; set; } = new InputNotificationEventDefinitionEntity();
        [StringLength(maximumLength: 36)]
        public string EventTargetId { get; set; }
        public DateTime SourceEventTimeStampUtc { get; set; }
        public DateTime AcknowledgmentTimeStampUtc { get; set; }
        [StringLength(maximumLength: 36)]
        public string ClientId { get; set; }


    }
}

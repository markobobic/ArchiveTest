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

        [StringLength(maximumLength: 36)]
        [Column(TypeName = "CHAR")]
        public string EventTargetId { get; set; }
        [Column(TypeName = "smalldatetime")]
        
        public DateTime SourceEventTimeStampUtc { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime AcknowledgmentTimeStampUtc { get; set; }

        [StringLength(maximumLength: 36)]
        [Column(TypeName = "CHAR")]
        public string ClientId { get; set; }
    }
}

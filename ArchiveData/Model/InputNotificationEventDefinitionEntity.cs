using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArchiveData.Model
{
    public class InputNotificationEventDefinitionEntity
    {
        [Key]
        [Required]
        public Guid Id { get; set; } 

        public EventType EventType { get; set; }

        public TermType TermType { get; set; }
        public InputNotificationEventDefinitionEntity()
        {
                
        }

        public InputNotificationEventDefinitionEntity(EventType eventType,TermType termType)
        {
            EventType = eventType;
            TermType = termType;
        }

    }
}
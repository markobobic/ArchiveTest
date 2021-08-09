using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArchiveData.Model
{
    public class InputNotificationEventDefinitionEntity
    {
        [Key]
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
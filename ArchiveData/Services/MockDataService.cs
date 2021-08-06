using ArchiveData.Configuration;
using ArchiveData.Model;
using ArchiveData.Tests;
using System;
using System.Collections.Generic;

namespace ArchiveData.Services
{
    public  class MockDataService
    {
        public  List<InputNotificationEventEntity> Populate(TestConfig config, InsertionTypeEnum insertionType)
        {
            var listOfInputs = new List<InputNotificationEventEntity>();
            var inputDefinition = new InputNotificationEventDefinitionEntity(EventType.Imported, TermType.Batch);
            inputDefinition.Id = Guid.Parse("b343d53b-8d23-44a6-b733-d43a138a3f6c");
            var iterationsLength = insertionType == InsertionTypeEnum.Initial ? config.BulkInsertMax : config.AdditionalInserts;
            for (int j = 1; j <= iterationsLength; j++)
            {
                var input = new InputNotificationEventEntity()
                {
                    EventDefinition = inputDefinition,
                    EventDefinitionId = Guid.Parse("b343d53b-8d23-44a6-b733-d43a138a3f6c"),
                    AcknowledgmentTimeStampUtc = DateTime.Now.AddSeconds(j + 5),
                    SourceEventTimeStampUtc = DateTime.Now.AddSeconds(j),
                    ClientId = "b343d53b-8d23-44a6-b733-d43a138a3f6c",
                    EventTargetId = "b343d53b-8d23-44a6-b733-d43a138a3f6c"
                };
                
                listOfInputs.Add(input);
            }
            return listOfInputs;
        }
    }
}

using Microsoft.WindowsAzure.Storage.Table;

namespace CodeDebate.Samples.RoomFinderBot.Entities
{
    public class RoomLocations : TableEntity
    {
        public RoomLocations(string typeOfSettings, string locationName)
        {
            PartitionKey = typeOfSettings;
            RowKey = locationName;
        }

        public RoomLocations()
        {
        }
    }
}
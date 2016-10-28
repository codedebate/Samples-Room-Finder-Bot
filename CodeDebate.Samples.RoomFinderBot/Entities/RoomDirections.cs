using Microsoft.WindowsAzure.Storage.Table;

namespace CodeDebate.Samples.RoomFinderBot.Entities
{
    public class RoomDirections : TableEntity
    {
        public RoomDirections(string location, string meetingRoomName, string directions)
        {
            PartitionKey = location;
            RowKey = meetingRoomName;
            Directions = directions;
        }

        public RoomDirections()
        {
        }

        public string Directions { get; set; }
    }
}
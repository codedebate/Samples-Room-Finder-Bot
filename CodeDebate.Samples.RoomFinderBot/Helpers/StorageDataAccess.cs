using System.Collections.Generic;
using System.Linq;
using CodeDebate.Samples.RoomFinderBot.Entities;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace CodeDebate.Samples.RoomFinderBot.Helpers
{
    public static class StorageDataAccess
    {
        public static string GetBotMessage(string key)
        {
            var storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            var tableClient = storageAccount.CreateCloudTableClient();

            var table = tableClient.GetTableReference("BotMessages");

            var retrieveOperation = TableOperation.Retrieve<BotMessages>("Bot Messages", key);

            var retrievedResult = table.Execute(retrieveOperation);

            return retrievedResult.Result != null ? ((BotMessages) retrievedResult.Result).BotMessage : string.Empty;
        }

        public static string GetRoomDirections(string location, string name)
        {
            var storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            var tableClient = storageAccount.CreateCloudTableClient();

            var table = tableClient.GetTableReference("RoomDirections");

            var retrieveOperation = TableOperation.Retrieve<RoomDirections>(location, name);

            var retrievedResult = table.Execute(retrieveOperation);

            return retrievedResult.Result != null ? ((RoomDirections) retrievedResult.Result).Directions : string.Empty;
        }

        public static List<string> GetListOfLocations()
        {
            var storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            var tableClient = storageAccount.CreateCloudTableClient();

            var table = tableClient.GetTableReference("BotSettings");

            var queryOperation =
                new TableQuery<RoomLocations>().Where(
                    TableQuery.GenerateFilterCondition(
                        "PartitionKey", QueryComparisons.Equal, "Locations"));

            var retrievedResult = table.ExecuteQuery(queryOperation).ToList();

            var locations = retrievedResult.Select(
                retrievedItem => retrievedItem.RowKey).ToList();

            return locations;
        }

        public static List<string> GetListOfRooms(string location)
        {
            var storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            var tableClient = storageAccount.CreateCloudTableClient();

            var table = tableClient.GetTableReference("RoomDirections");

            var queryOperation =
                new TableQuery<RoomLocations>().Where(
                    TableQuery.GenerateFilterCondition(
                        "PartitionKey", QueryComparisons.Equal, location));

            var retrievedResult = table.ExecuteQuery(queryOperation).ToList();

            var rooms = retrievedResult.Select(
                retrievedItem => retrievedItem.RowKey).ToList();

            return rooms;
        }
    }
}
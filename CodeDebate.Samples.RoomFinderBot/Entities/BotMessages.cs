using Microsoft.WindowsAzure.Storage.Table;

namespace CodeDebate.Samples.RoomFinderBot.Entities
{
    public class BotMessages : TableEntity
    {
        public BotMessages(string typeOfMessage, string botMessageKey, string botMessage)
        {
            PartitionKey = typeOfMessage;
            RowKey = botMessageKey;
            BotMessage = botMessage;
        }

        public BotMessages()
        {
        }

        public string BotMessage { get; set; }
    }
}
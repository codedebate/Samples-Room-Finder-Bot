using System;
using System.Threading.Tasks;
using CodeDebate.Samples.RoomFinderBot.Helpers;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace CodeDebate.Samples.RoomFinderBot.Dialogs
{
    [Serializable]
    public class RoomFinderDialog : IDialog<object>
    {
        private string _roomLocation;
        private string _roomName;

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(Step01_GreetUserDisplayIntentions);
        }

        public async Task Step01_GreetUserDisplayIntentions(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            await context.PostAsync(
                StorageDataAccess.GetBotMessage("GreetingsMessage"));

            await Step02_AskUser4RoomLocation(context, result);
        }

        public async Task Step02_AskUser4RoomLocation(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            await context.PostAsync(
                StorageDataAccess.GetBotMessage("Ask4LocationMessage"));

            PromptDialog.Choice(
                context,
                Step03_AskUser4RoomName,
                StorageDataAccess.GetListOfLocations(),
                string.Empty,
                StorageDataAccess.GetBotMessage("InputNotOK"));
        }

        public async Task Step03_AskUser4RoomName(IDialogContext context, IAwaitable<string> result)
        {
            _roomLocation = result.GetAwaiter().GetResult();

            await context.PostAsync(
                StorageDataAccess.GetBotMessage("Ask4RoomMessage"));

            PromptDialog.Choice(
                context,
                Step04_InformUserOfRoomDirections,
                StorageDataAccess.GetListOfRooms(_roomLocation),
                string.Empty,
                StorageDataAccess.GetBotMessage("InputNotOK"));
        }

        public async Task Step04_InformUserOfRoomDirections(IDialogContext context, IAwaitable<string> result)
        {
            _roomName = result.GetAwaiter().GetResult();

            var directions = StorageDataAccess.GetRoomDirections(_roomLocation, _roomName);

            if (string.IsNullOrEmpty(directions))
                await context.PostAsync(
                    StorageDataAccess.GetBotMessage(
                            "RoomNotFoundMessage").Replace("XXXX", directions));
            else
                await context.PostAsync(
                    StorageDataAccess.GetBotMessage(
                            "FoundRoomMessage").Replace("XXXX", directions));

            await Step05_GoodbyeUser(context, null);
        }

        public async Task Step05_GoodbyeUser(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            await context.PostAsync(
                StorageDataAccess.GetBotMessage("GoodByeUserMessage"));

            context.Wait(Step01_GreetUserDisplayIntentions);
        }
    }
}
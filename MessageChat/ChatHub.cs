using Microsoft.AspNetCore.SignalR;

namespace MessageChat
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(int chatId, string user, string message)
        {
            await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", user, message);
        }

        public async Task JoinChat(int chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
        }

        public async Task LeaveChat(int chatId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId.ToString());
        }
    }
}
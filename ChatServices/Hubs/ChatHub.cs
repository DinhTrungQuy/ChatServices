using ChatServices.Models;
using ChatServices.Services;
using Microsoft.AspNetCore.SignalR;

namespace ChatServices.Hubs
{
    public class ChatHub(ChatDbServices chatDbServices) : Hub
    {
        private readonly ChatDbServices _chatDbServices = chatDbServices;

        public async Task JoinChat(UserConnection conn)
        {
            _chatDbServices.Connections.TryAdd(Context.ConnectionId, conn);
            await Clients.All.SendAsync("ReceiveMessage", "Admin", $"{conn.UserName} has joined");
        }
        public async Task JoinSpecificChatRoom(UserConnection conn)
        {
            _chatDbServices.Connections.TryAdd(Context.ConnectionId, conn);
            await Groups.AddToGroupAsync(Context.ConnectionId, conn.ChatRoom);
            await Clients.Group(conn.ChatRoom).SendAsync("ReceiveNotify", "Admin", $"{conn.UserName} has joined Room {conn.ChatRoom}");
        }
        public async Task SendMessage(string message)
        {
            if (_chatDbServices.Connections.TryGetValue(Context.ConnectionId,out var conn))
                await Clients.Groups(conn.ChatRoom).SendAsync("ReceiveMessage", conn.UserName, message);
        }
    }
}

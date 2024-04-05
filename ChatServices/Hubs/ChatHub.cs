using ChatServices.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatServices.Hubs
{
    public class ChatHub : Hub
    {
        public async Task JoinChat(UserConnection conn)
        {
            await Clients.All.SendAsync("ReceiveMessage", "Admin: ", $"{conn.UserName} has joined");
        }
        public async Task JoinSpecificChatRoom(UserConnection conn)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, conn.ChatRoom);
            await Clients.Group(conn.ChatRoom).SendAsync("ReceiveMessage", "Admin: ", $"{conn.UserName} has joined {conn.ChatRoom}");
        }
    }
}

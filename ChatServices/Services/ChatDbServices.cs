using ChatServices.Models;
using System.Collections.Concurrent;

namespace ChatServices.Services
{
    public class ChatDbServices
    {
        private readonly ConcurrentDictionary<string, UserConnection> _connections = new();
        public ConcurrentDictionary<string, UserConnection> Connections => _connections;
    }
}

using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace BlooperFE.Hubs
{
    public class MessageHub : Hub
    {
        public const string HubUrl = "/messagehub";

        public async Task SendMessage(string to_id, string from_id, string message)
        {
            Console.WriteLine($"Sending message {message} to {to_id}");
            await Clients.All.SendAsync("ReceiveMessage", to_id, from_id, message);
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"{Context.ConnectionId} connected");
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception e)
        {
            Console.WriteLine($"Disconnected {e?.Message} {Context.ConnectionId}");
            await base.OnDisconnectedAsync(e);
        }
    }
}

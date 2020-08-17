using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace api.Hubs
{
    public class ChatHub : Hub
    {
        // Se crea el metodo para invocar al hub 
        public async Task SendMessage(string username, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", username, message);
        }
    }
}
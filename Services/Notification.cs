using api.Hubs;
using api.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;

namespace dotnetcoreSignalR.Services {
    public interface INotificationService {
        void config ();
    }

    public class Notifications : INotificationService {
        private readonly IConfiguration _configuration;
        private readonly IHubContext<ChatHub> _chatHub;
        private readonly PruebaContext _context;

        public Notifications (IConfiguration configuration, IHubContext<ChatHub> chatHub, PruebaContext context) {
            configuration = _configuration;
            chatHub = _chatHub;
            context = _context;
        }
        public void config () {
            SubscribeChange ();
        }
        private void SubscribeChange () {

        }
    }
}
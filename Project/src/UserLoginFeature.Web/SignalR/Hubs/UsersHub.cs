using Microsoft.AspNetCore.SignalR;
using UserLoginFeature.Web.Constants;

namespace UserLoginFeature.Web.SignalR.Hubs
{
    public class UsersHub : Hub
    {
        private readonly IHubContext<WebsiteStatsHub> _hubContext;

        public UsersHub(IHubContext<WebsiteStatsHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public override Task OnConnectedAsync()
        {
            OnlineUsers.Count++;
            _hubContext.Clients.All.SendAsync("updateOnlineUsersCounter", OnlineUsers.Count);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            OnlineUsers.Count--;
            _hubContext.Clients.All.SendAsync("updateOnlineUsersCounter", OnlineUsers.Count);
            return base.OnDisconnectedAsync(exception);
        }
    }
}

using Microsoft.AspNetCore.SignalR;
using UserLoginFeature.Web.Constants;

namespace UserLoginFeature.Web.SignalR.Hubs
{
    public class WebsiteStatsHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Clients.All.SendAsync("updateOnlineUsersCounter", OnlineUsers.Count);
            return base.OnConnectedAsync();
        }
    }
}
